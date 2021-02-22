using Xunit;
using SearchEngineCrawlerServices.Service;
using SearchEngineCrawlerServices.Dto;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace SearchEngineCrawlerServices.Tests
{
    public class CacheServiceTest
    {
        [Fact]
        public async Task SearchURLInGoogleResult_WithKeywordAndUrl_ReturnsIndexOfOcurrencesAsync()
        {
            CacheService cacheService = new CacheService(new MemoryCache(new MemoryCacheOptions()));

            var samplekey = "SampleKey";
            var sampleValue = "sampleValue";
            ;

            //Act
            var cacheResult = await cacheService.SetAsync(samplekey, sampleValue);
            var cachedValue = await cacheService.GetAsync<string>(samplekey);

            //Assert

            Assert.Same(sampleValue, cacheResult);
            Assert.Same(sampleValue, cachedValue);
        }
    }
}
