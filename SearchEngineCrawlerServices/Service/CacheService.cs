using Microsoft.Extensions.Caching.Memory;
using SearchEngineCrawlerServices.Contract;
using System;
using System.Threading.Tasks;

namespace SearchEngineCrawlerServices.Service
{
    public class CacheService : ICacheService
    {
        private IMemoryCache memoryCache { get; set; }
        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public Task<TValue> SetAsync<TValue>(string key, TValue value)
        {
            return Task.Run(() => memoryCache.Set(key, value, Constants.CatchItemExpirationPeriod));
        }

        public Task<TValue> GetAsync<TValue>(string key)
        {
            return Task.Run(() => memoryCache.Get<TValue>(key));
        }

    }
}
