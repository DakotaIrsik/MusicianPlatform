using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
using SoundSesh.Common.Extensions;
using SoundSesh.Common.Helpers;
using SoundSesh.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.Entities.Models
{
    public class MusicianContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IElasticSearchService _elastic;
        private readonly AppSettings _settings;
        private readonly IMapper _mapper;
        public MusicianContext(DbContextOptions<MusicianContext> options, 
                            IHttpContextAccessor httpContext, 
                            IElasticSearchService elastic, 
                            IOptions<AppSettings> settings, 
                            IMapper mapper) : base(options)
        {
            _httpContext = httpContext;
            _elastic = elastic;
            _settings = settings.Value;
            _mapper = mapper;
        }

        public DbSet<Musician> Musician { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Chat> Chat { get; set; }

#pragma warning disable 0809
        [Obsolete("Consider using BaseDomain's ValidateAndSaveChanges(), it does some basic error handling for common SQL errors, and will give you messages if your save is successful.", true)]
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var x = 0;
            OnBeforeSaving();
            x = base.SaveChanges(acceptAllChangesOnSuccess);
            OnAfterSaving();
            return x;
        }

        [Obsolete("Consider using BaseDomain's ValidateAndSaveChangesAsync(), it does some basic error handling for common SQL errors, and will give you messages if your save is successful.", true)]
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var x = 0;
            OnBeforeSaving();
            x = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSavingAsync();
            return x;
        }
        #pragma warning restore 0809

        public void BulkAdd(List<Musician> studios)
        {
            this.BulkInsert(studios);
        }

        public void BulkAdd(List<User> users)
        {
            this.BulkInsert(users);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is Trackable trackable)
                {
                    var now = DateTime.UtcNow;
                    var user = GetCurrentUserName();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdateDate = now;
                            trackable.UpdatedBy = user;
                            break;

                        case EntityState.Added:
                            trackable.CreateDate = now;
                            trackable.CreatedBy = user;
                            trackable.UpdateDate = now;
                            trackable.UpdatedBy = user;
                            break;
                    }
                }
                //if there's a FK to UserId, set it here to keep the BLL clean.
                var userIdFk = entry.Entity.GetType().GetProperty("UserId");
                if (userIdFk != null && userIdFk.GetValue(entry.Entity) == null)
                {
                    userIdFk.SetValue(entry.Entity, GetCurrentUserId());
                }
            }
        }

        private async Task OnAfterSavingAsync()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name == _settings.EntityAssemblyName);
            List<object> elasticDocuments = new List<object>();
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                var elasticType = assembly.GetTypes().SingleOrDefault(t => t.Name == $"Elastic{entry.Entity.GetType().Name}");
                if (elasticType != null)
                {
                    elasticDocuments.Add(MapToElastic(entry.Entity, elasticType));
                }
            }
            if (elasticDocuments.Any())
            {
                //TODO: find out why the entities aren't being 'let go' by the Change Tracker, 
                //this is resulting in this list of elastic documents to contain objects that should belong in different indexes
                //preventing index name inference (if the first elastic document is of type studio, then it tries to send all documents
                //to the studio index.
                var seperateLists = elasticDocuments.PartitionByType();
                foreach (var list in seperateLists)
                {
                    var result = await _elastic.IndexManyAsync(list, list.Safe().FirstOrDefault().GetType().ElasticIndexResolver());
                }
            }
        }

        private void OnAfterSaving()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name == _settings.EntityAssemblyName);
            List<object> elasticDocuments = new List<object>();
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                var elasticType = assembly.GetTypes().SingleOrDefault(t => t.Name == $"Elastic{entry.Entity.GetType().Name}");
                if (elasticType != null)
                {
                    elasticDocuments.Add(MapToElastic(entry.Entity, elasticType));
                }
            }
            if (elasticDocuments.Any())
            {
                //TODO: find out why the entities aren't being 'let go' by the Change Tracker, 
                //this is resulting in this list of elastic documents to contain objects that should belong in different indexes
                //preventing index name inference (if the first elastic document is of type studio, then it tries to send all documents
                //to the studio index.
                var seperateLists = elasticDocuments.PartitionByType();
                foreach (var list in seperateLists)
                {
                    var result = _elastic.IndexMany(elasticDocuments, list.Safe().FirstOrDefault().GetType().ElasticIndexResolver());
                }
            }
        }

        private object MapToElastic(object obj, Type elasticType)
        {
            var elasticObject = Activator.CreateInstance(elasticType);
            var mappedObject = _mapper.Map(obj, elasticObject, obj.GetType(), elasticType);
            return mappedObject;
        }

        private ClaimsPrincipal GetCurrentUser()
        {
            return _httpContext.HttpContext?.User;
        }

        private string GetCurrentUserName()
        {
            return GetCurrentUser()?.Identity?.Name ?? "System";
        }

        private string GetCurrentUserId()
        {
            return GetCurrentUser()?.Claims?.SingleOrDefault(c => c.Type == "sub")?.Value;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            AddDefaultEntities(builder);

            builder.Entity<User>()
                .HasIndex(u => u.IdentityUserId)
                .IsUnique();
        }

        private void AddDefaultEntities(ModelBuilder builder)
        {
            builder.Entity<User>()
              .ToTable(TableConsts.Users)
              .HasIndex(u => u.Id)
              .IsUnique();


            builder.Entity<Log>(log =>
            {
                log.ToTable(TableConsts.Log);
                log.HasKey(x => x.Id);
                log.Property(x => x.Level).HasMaxLength(128);
            });
        }
    }
}
