using System;
using AlfanousWP7.AlfanousClasses;

namespace AlfanousWP7
{
    public class FakeAlfanousSearchService : IAlfanousSearchService
    {
        #region IAlfanousSearchService Members

        public string Recitation { get; set; }

        public TranslationEnumeration Translation { get; set; }

        public void Search(string searchTerm, int page, Action<SearchResults> callback)
        {
            var searchResults = new SearchResults
                                    {
                                        TotalResultCount = 35,
                                        CurrentPage = page,
                                        SearchResultItems = new[]
                                                                {
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                    new SearchResultItem {Aya = new Aya {Text = "Aya", UthmaniText = "Aya Uthmani"}},
                                                                }
                                    };
            callback(searchResults);
        }

        #endregion
    }

    public interface IAlfanousSearchService
    {
        string Recitation { get; set; }
        TranslationEnumeration Translation { get; set; }
        void Search(string searchTerm, int page, Action<SearchResults> callback);
    }
}