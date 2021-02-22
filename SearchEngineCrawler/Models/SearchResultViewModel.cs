using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineCrawlerWebApi.Models
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel(bool success, IEnumerable<int> searchResultMatchIndex, IEnumerable<string> errorMessage)
        {
            Success = success;
            SearchResultMatchIndex = searchResultMatchIndex;
            ErrorMessage = errorMessage;
        }

        public bool Success { get; }

        public IEnumerable<int> SearchResultMatchIndex { get; }

        public IEnumerable<string> ErrorMessage { get; }
    }
}
