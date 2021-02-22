using SearchEngineCrawlerServices.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineCrawlerWebApi.SearchEngines
{
    public class BingSearchEngine : ISearchEngine
    {
        public string Name => "Bing";

        public string Url => @"https://www.bing.com/search";

        public string SearchResultItemPattern => @"<cite>.+?\<\/cite>";

        public string GetQueryString(string keyword) => $"q={keyword.Replace(' ', '+')}&count=50";
    }
}
