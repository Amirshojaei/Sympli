using System.Text.RegularExpressions;

namespace SearchEngineCrawlerServices.Contract
{
    public interface ISearchEngine
    {

        string Name { get; }

        string Url { get; }

        string GetQueryString(string keyword);

        string SearchResultItemPattern { get; }
    }
}
