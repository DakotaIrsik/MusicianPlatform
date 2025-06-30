using AutoMapper;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using SoundSesh.Common.Helpers;
using SoundSesh.Common.Interfaces;
using SoundSesh.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SoundSesh.Common.Services
{

    public interface IElasticSearchService : IDisposable
    {
        int Index<T>(T model, string index) where T : class;
        Task<int> IndexAsync<T>(T model, string index) where T : class;
        Task<AdjustableDTO<T>> Search<T>(object searchRequest, string index) where T : class;
        Task<int> IndexManyAsync<T>(IEnumerable<T> model, string index) where T : class;
        int IndexMany<T>(IEnumerable<T> model, string index) where T : class;
        void DeleteIndex(string index);
        void CreateIndex(CreateIndexDescriptor descriptor);
    }

    public class ElasticSearchService : IElasticSearchService, IDisposable
    {

        private readonly ElasticClient _client;
        private readonly IMapper _mapper;
        private readonly AppSettings _options;

        public ElasticSearchService(IOptions<AppSettings> options, IMapper mapper)
        {
            _mapper = mapper;
            _options = options.Value;
            _client = new ElasticClient(GetConfigSettings());
        }

        public void DeleteIndex(string index)
        {
            if (_client.IndexExists(index).Exists)
            {
                _client.DeleteIndex(index);
            }
        }

        public void CreateIndex(CreateIndexDescriptor descriptor)
        {
            _client.CreateIndex(IndexName.From<CreateIndexDescriptor>(), d => descriptor);
        }

        public int Index<T>(T model, string index) where T : class
        {
            var value = model.GetType().GetProperties().SingleOrDefault(p => p.Name == "Id").GetValue(model);
            var result = _client.Index(model, m => m.Id(new Id(value)).Index(index));
            var refreshIndex = _client.Refresh(index);
            return (!string.IsNullOrEmpty(result.Id)) ? 1 : 0;
        }

        public async Task<int> IndexAsync<T>(T model, string index) where T : class
        {
            var value = model.GetType().GetProperties().SingleOrDefault(p => p.Name == "Id").GetValue(model);
            var result = await _client.IndexAsync(model, m => m.Id(new Id(value)).Index(index));
            var refreshIndex = await _client.RefreshAsync(index);
            return (!string.IsNullOrEmpty(result.Id)) ? 1 : 0;
        }

        public int IndexMany<T>(IEnumerable<T> model, string index) where T : class
        {
            var result = _client.IndexMany(model, index);
            var refreshIndex = _client.Refresh(index);
            return result.Items.Count();
        }

        public async Task<int> IndexManyAsync<T>(IEnumerable<T> model, string index) where T : class
        {
            var result = await _client.IndexManyAsync(model, index);
            var refreshIndex = await _client.RefreshAsync(index);
            return result.Items.Count();
        }

        public async Task<AdjustableDTO<T>> Search<T>(object searchRequest, string index) where T : class
        {
            var args = ((IAdjustable)searchRequest);

            var result = await _client.SearchAsync<T>(q => q
                                                    .Sort(s2 => Sort<T>(args.Sort?.Split(',')))
                                                    .Query(y => Query(searchRequest))
                                                    .Source(s => new SourceFilter { Includes = args.Fields?.Replace(" ", "").ToLower() })
                                                    .From(args.From)
                                                    .Index(index)
                                                    .Take(args.Size));

            var pagedResult = new AdjustableDTO<T>(args)
            {
                Total = result.Total,
                Data = _mapper.Map<List<T>>(result.Documents)
            };

            return pagedResult;
        }

        private QueryContainer Query<T>(T request)
        {
            QueryContainer container = new QueryContainer();
            
            foreach (var property in request.GetNonInheritedProperties())
            {
                var value = property.GetValue(request);
                if (IsLookup(property, value))
                {
                    container &= (new MatchQuery
                    {
                        Field = property.Name,
                        Query = value.ToString()
                    });
                }
                else if (IsSearchable(property, value))
                {
                    container &= (new FuzzyQuery
                    {
                        Field = property.Name,
                        Value = value.ToString(),
                        Fuzziness = Fuzziness.EditDistance(3)
                    });
                }
            }

            return container;
        }

        private SortDescriptor<IAdjustable> Sort<T>(IEnumerable<string> sortStrings)
        {
            var descriptor = new SortDescriptor<IAdjustable>();
            if (sortStrings?.Any() ?? false)
            {
                foreach (var sortString in sortStrings)
                {
                    if (typeof(T).GetProperty(SortablePropertyName(sortString)) != null)
                    {
                        descriptor.Field(SortablePropertyName(sortString), (sortString[0] == '-') ? SortOrder.Descending : SortOrder.Ascending);
                    }
                }
            }
            return descriptor;
        }

        private string SortablePropertyName(string sortString)
        {
            return sortString.Replace("-", "").Replace("+", "");
        }

        private bool IsSearchable(PropertyInfo property, object value)
        {
            return value != null && !string.IsNullOrWhiteSpace(value.ToString()) && value.ToString() != "0";
        }

        private bool IsLookup(PropertyInfo property, object value)
        {
            var isLookup = false;
            //tired of fighting this property interogation battle, resorting to infinite if....
            switch (property.Name)
            {
                case "Id":
                    long.TryParse(value.ToString(), out var i);
                    isLookup = i > 0;
                    break;
                case "UserId":
                    isLookup = value != null && !string.IsNullOrWhiteSpace(value.ToString());
                    break;
                case "IsActive":
                    isLookup = true;
                    break;
                default:
                    isLookup = false;
                    break;
            }
            return isLookup;
        }

        private bool IsNumberGreaterThanZero(object value)
        {
            bool isNumber = long.TryParse(value.ToString(), out var i);
            return isNumber && i > 0;
        }

        private ConnectionSettings GetConfigSettings()
        {
            var urls = _options.ConnectionStrings.ElasticSearch.Split(',').Select(url => new Uri(url));
            var pool = new StaticConnectionPool(urls);
            var configSettings = new ConnectionSettings(pool)
                .DefaultFieldNameInferrer(fieldName => fieldName)
                .RequestTimeout(new TimeSpan(0, 0, 60));

            return configSettings;
        }

        public void Dispose()
        {
        }
    }
}
