
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SoundSesh.common.Services;
using StackExchange.Redis;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SoundSesh.common.Services
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set(string key, object data, int? cacheTime = null);
        bool IsSet(string key);
        void Remove(string key);
    }

    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var obj = Activator.CreateInstance(typeof(T));
            _cache.TryGetValue(key, out obj);
            return (T)obj;
        }

        public bool IsSet(string key)
        {
            var t = new object();
            return _cache.TryGetValue(key, out t);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set(string key, object data, int? cacheTime)
        {
            using (var entry = _cache.CreateEntry(key))
            {
                entry.Value = data;
                _cache.Set(key, data, new TimeSpan(0, 0, cacheTime ?? 0));
            }
        }
    }

    public class NoCacheService : ICacheService
    {
        public T Get<T>(string key)
        {
            return default(T);
        }

        public bool IsSet(string key)
        {
            return false;
        }

        public void Remove(string key)
        {
        }

        public void Set(string key, object data, int? cacheTime)
        {
        }
    }

    public class RedisCacheService : ICacheService
    {
        private static ConnectionMultiplexer _redis;
        private static IDatabase _cache;

        public RedisCacheService(string csvRedisEndpoints)
        {
            var options = ConfigurationOptions.Parse(csvRedisEndpoints);
            var connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
            _redis = connection.Value;
            _cache = _redis.GetDatabase();
        }

        public T Get<T>(string key)
        {
            var data = _cache.StringGet(key);
            return Deserialize<T>(data);
        }

        public void Set(string key, object data, int? cacheTime)
        {
            if (_redis != null)
            {
                _cache.StringSet(key, Serialize(data), new TimeSpan(0, 0, cacheTime ?? 0));
            }
        }

        public bool IsSet(string key)
        {
            return _cache.KeyExists(key);
        }

        public void Remove(string key)
        {
            if (_redis != null)
            {
                _cache.KeyDelete(key);
            }
        }

        private static string Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(o);
        }

        private static T Deserialize<T>(string data)
        {
            if (data == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}

namespace CacheExtensions
{
    public static class CacheExtensions
    {
        public static T GetOrSet<T>(this ICacheService cacheService, string key, Func<T> acquire, int? cacheTime = null)
        {
            if (cacheService.IsSet(key))
            {
                return cacheService.Get<T>(key);
            }

            var result = acquire();
            cacheService.Set(key, result, cacheTime);
            return result;
        }

        public static async Task<T> GetOrSetAsync<T>(this ICacheService cacheService, string key, Func<Task<T>> acquire, int? cacheTime = null)
        {
            if (cacheService.IsSet(key))
            {
                return cacheService.Get<T>(key);
            }

            var result = await acquire().ConfigureAwait(false);
            cacheService.Set(key, result, cacheTime);
            return result;
        }

        public static string GenerateKey(this object keyGenerationObject, string methodName)
        {
            var key = methodName;
            var thisClassProperties = keyGenerationObject.GetType().GetProperties
                (BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var prop in thisClassProperties)
            {
                key += prop.Name + "=" + JsonConvert.SerializeObject(prop.GetValue(keyGenerationObject, null));
            }

            return key;
        }
    }
}

