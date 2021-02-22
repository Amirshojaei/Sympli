using System.Collections.Generic;
using System.Text.RegularExpressions;
using SearchEngineCrawlerServices.Contract;
using SearchEngineCrawlerServices.Dto;
using System.Threading.Tasks;
using System.Linq;

namespace SearchEngineCrawlerServices.Service
{
    public class SearchService : ISearchService
    {
        private readonly IHttpWebRequestService httpWebRequestService;
        private readonly ICacheService cacheService;
        public SearchService(IHttpWebRequestService httpWebRequestService, ICacheService cacheService)
        {
            this.httpWebRequestService = httpWebRequestService;
            this.cacheService = cacheService;
        }
        public async Task<IServiceResult<IEnumerable<int>>> SearchAsync(ISearchEngine searchEngine, string keyword, string url)
        {
            List<string> errorMessages = new List<string>();
            if (!validateSearchCriteria(keyword, url, out errorMessages))
            {
                return new ServiceResult<IEnumerable<int>>(errorMessages);
            }

            var cacheKey = getCacheKey(searchEngine.Name, keyword, url);
            var searchResultMatchIndex = cacheService.GetAsync<List<int>>(cacheKey).Result;
            if (searchResultMatchIndex == null)
            {
                searchResultMatchIndex = new List<int>();
                var serviceResult = await httpWebRequestService.GetPageAsStringAsync(searchEngine.Url, searchEngine.GetQueryString(keyword));

                if (!serviceResult.Success)
                {
                    return new ServiceResult<IEnumerable<int>>(serviceResult.ErrorMessages);
                }

                var searchItems = extractAllSearchResultItems(searchEngine.SearchResultItemPattern, serviceResult.Result);

                for (int i = 0; i < searchItems.Count; i++)
                {
                    if (searchItems[i].Contains(url))
                    {
                        searchResultMatchIndex.Add(i + 1);
                    }
                }
                _ = cacheService.SetAsync(cacheKey, searchResultMatchIndex);
            }

            return new ServiceResult<IEnumerable<int>>(searchResultMatchIndex);
        }

        private IList<string> extractAllSearchResultItems(string pattern, string pageString)
        {
            var matchCollection = Regex.Matches(pageString, pattern, RegexOptions.IgnoreCase);
            return matchCollection.Select(x => x.Groups[0].Value).ToList();
        }

        private bool validateSearchCriteria(string keyword, string url, out List<string> errorMessages)
        {
            bool isValid = true;
            errorMessages = new List<string>();
            if (string.IsNullOrEmpty(keyword))
            {
                errorMessages.Add("keyword cannot be empty.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(url))
            {
                errorMessages.Add("URL cannot be empty.");
                isValid = false;
            }

            if (!Regex.IsMatch(url, @"^[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$"))
            {
                errorMessages.Add("URL is not valid.");
                isValid = false;
            }

            return isValid;
        }

        private string getCacheKey(string searchEngineName, string keyword, string url)
        {
            return $"{searchEngineName}-{keyword}-{url}";
        }
    }
}
