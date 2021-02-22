using SearchEngineCrawlerServices.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchEngineCrawlerWebApi.SearchEngines
{
    class GoogleSearchEngine : ISearchEngine
    {
        public string Name => "Google";
        public string Url => @"https://www.google.com/search";

        public string SearchResultItemPattern => @"\<div class=""ZINbbc xpd O9g5cc uUPGi"">.+?\<\/div>";

        public string GetQueryString(string keyword) => $"num=100&q={keyword.Replace(' ', '+')}";

    }
}
