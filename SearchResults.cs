using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AlfanousWP7
{
    public class SearchResults
    {
        public IEnumerable<SearchResultItem> SearchResultItems { get; set; }
        public int TotalResultCount { get; set; }
        public int CurrentPage { get; set; }
        public bool HasError { get; set; }
        
        public bool HasMore
        {
            get { return (CurrentPage-1)*10 + SearchResultItems.Count() < TotalResultCount; }
        }
        
    }
}