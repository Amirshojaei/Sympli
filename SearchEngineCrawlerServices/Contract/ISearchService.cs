using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngineCrawlerServices.Contract
{
    public interface ISearchService
    {
        Task<IServiceResult<IEnumerable<int>>> SearchAsync(ISearchEngine searchEngine, string keyword, string URL);
    }
}
