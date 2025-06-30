using Microsoft.EntityFrameworkCore;
using SoundSesh.Venues.Entities.Models;

namespace SoundSesh.Venues.Entities.Models
{

    public class StudioContext : DbContext
    {
        public StudioContext(DbContextOptions<StudioContext> options)
            : base(options) { }

        public DbSet<Studio> Studio { get; set; }
        public DbSet<StudioStaging> StudioStaging { get; set; }
        public DbSet<StudioSchedule> StudioSchedule { get; set; }
        public DbSet<LookUp> LookUp { get; set; }

        public DbSet<UserAccount> UserAccount { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Studio>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<StudioStaging>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<UserAccount>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Default Genres
            builder.Entity<LookUp>().HasData(
                new { Id = 1, Category = "Genres", Description = "Rock", DisplayDesc = "Rock" },
                new { Id = 2, Category = "Genres", Description = "Country", DisplayDesc = "Country" },
                new { Id = 3, Category = "Genres", Description = "R&B", DisplayDesc = "R&B" },
                new { Id = 4, Category = "Genres", Description = "Electronic", DisplayDesc = "Electronic" },
                new { Id = 5, Category = "Genres", Description = "Jazz", DisplayDesc = "Jazz" },
                new { Id = 6, Category = "Genres", Description = "Reggae", DisplayDesc = "Reggae" },
                new { Id = 7, Category = "Genres", Description = "Rap", DisplayDesc = "Rap" },
                new { Id = 8, Category = "Genres", Description = "Dubstep", DisplayDesc = "Dubstep" }

            );
        }
    }
}
