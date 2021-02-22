using Microsoft.AspNetCore.Mvc;
using SearchEngineCrawlerServices.Contract;
using SearchEngineCrawlerWebApi.Models;
using SearchEngineCrawlerWebApi.SearchEngines;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngineCrawlerWebApi.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        private readonly List<ISearchEngine> searchEngines = new List<ISearchEngine>
        {
        new GoogleSearchEngine(),
        new BingSearchEngine()
        };

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        public string[] LoadSearchEngines()
        {
            return searchEngines.Select(x => x.Name).ToArray();
        }

        [HttpGet]
        public SearchResultViewModel Search(string Keyword, string Url, string searchEngineName)
        {
            var selectedSearchEngine = searchEngines.FirstOrDefault(x => x.Name == searchEngineName);

            var serviceResult = searchService.SearchAsync(selectedSearchEngine, Keyword, Url).Result;

            return new SearchResultViewModel(serviceResult.Success, serviceResult.Result, serviceResult.ErrorMessages);
        }

    }
}

