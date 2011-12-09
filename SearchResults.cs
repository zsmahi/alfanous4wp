using System.Collections.Generic;
namespace AlfanousWP7
{
    public class SearchResults
    {
        public IEnumerable<SearchResultItem> SearchResultItems { get; set; }
        public bool HasError { get; set; }
    }
}