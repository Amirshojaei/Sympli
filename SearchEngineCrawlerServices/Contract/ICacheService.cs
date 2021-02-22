using System.Threading.Tasks;

namespace SearchEngineCrawlerServices.Contract
{
    public interface ICacheService
    {
        Task<TValue> SetAsync<TValue>(string key, TValue value);

        Task<TValue> GetAsync<TValue>(string key);
    }
}
