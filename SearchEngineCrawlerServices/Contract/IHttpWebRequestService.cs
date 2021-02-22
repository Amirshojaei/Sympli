using System.Threading.Tasks;

namespace SearchEngineCrawlerServices.Contract
{
    public interface IHttpWebRequestService
    {
        Task<IServiceResult<string>> GetPageAsStringAsync(string pageUrl, string queryString = "");
    }
}
