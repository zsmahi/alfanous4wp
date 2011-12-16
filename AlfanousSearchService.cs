using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using AlfanousWP7.AlfanousClasses;
using Newtonsoft.Json.Linq;

namespace AlfanousWP7
{
    public class AlfanousSearchService : IAlfanousSearchService 
    {
        public string Recitation { get; set; }
        public TranslationEnumeration Translation { get; set; }

        public void Search(string searchTerm, int page, Action<SearchResults> callback)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return;
            var queryUrl = GetQueryUrl(searchTerm, page);
            var client = new WebClient();
            client.DownloadStringCompleted += OnDownloadStringCompleted;
            client.DownloadStringAsync(new Uri(queryUrl), callback);
        }
        private string GetQueryUrl(string searchTerm, int page)
        {
            return string.Format("http://www.alfanous.org/json?search={0}&sortedby=mushaf{1}&recitation={2}&highlight=bold&page={3}", 
                       searchTerm, 
                       GetTranslation(), 
                       Recitation,
                       page);
        }
        private string GetTranslation()
        {
            var translationQueryParameter = "&translation=";
            switch (Translation)
            {

                case TranslationEnumeration.None:
                    translationQueryParameter = string.Empty;
                    break;
                case TranslationEnumeration.EnglishTranslation:
                     translationQueryParameter+="shakir";
                    break;
                case TranslationEnumeration.EnglishTransliteration:
                    translationQueryParameter+="transliteration-en";
                    break;
                default:
                    return null;
            }
            return translationQueryParameter;
        }
        private static void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var callback = (Action<SearchResults>)e.UserState;
            try
            {
                var token = JToken.Parse(e.Result);
                var ayas = token[Constants.Ayas];
                var results = (from result in ayas.Children()
                           let aya = result.First[Constants.Aya]
                           let sura = result.First[Constants.Sura]
                           let stat = result.First[Constants.Stat]
                           let theme = result.First[Constants.Theme]
                           let position = result.First[Constants.Position]
                           let sajda = result.First[Constants.Sajda]
                           select new SearchResultItem
                                      {
                                          Aya = new Aya
                                                    {
                                                        Id = aya[Constants.Id].Value<int?>(),
                                                        UthmaniText = aya[Constants.TextUthmani].ToString(),
                                                        Text = aya[Constants.Text].ToString(),
                                                        Translation = aya[Constants.Traduction].ToString(),
                                                        RecitationLink = aya[Constants.RecitationLink].ToString()
                                                    },
                                          Sura = new Sura
                                                     {
                                                         Id = sura[Constants.Id].Value<int?>(),
                                                         Name =
                                                             sura[Constants.Name].ToString().Replace("<b>", "").Replace(
                                                                 "</b>", ""),
                                                         Type = sura[Constants.Type].ToString(),
                                                         Order = sura[Constants.Order].Value<int?>(),
                                                         Stat = new SuraStat
                                                                    {
                                                                        Letters =
                                                                            sura[Constants.Stat][Constants.Letters].
                                                                            Value<int?>(),
                                                                        Words =
                                                                            sura[Constants.Stat][Constants.Words].Value
                                                                            <int?>(),
                                                                        GodNames =
                                                                            sura[Constants.Stat][Constants.GodNames].
                                                                            Value<int?>(),
                                                                        Ayas =
                                                                            sura[Constants.Stat][Constants.Ayas].Value
                                                                            <int?>()
                                                                    }
                                                     },
                                          Stat = new Stat
                                                     {
                                                         Words = stat[Constants.Words].Value<int?>(),
                                                         Letters = stat[Constants.Letters].Value<int?>(),
                                                         GodNames = stat[Constants.GodNames].Value<int?>()
                                                     },
                                          Theme = new Theme
                                                      {
                                                          Chapter = theme[Constants.Chapter].ToString(),
                                                          Topic = theme[Constants.Topic].ToString(),
                                                          SubTopic = theme[Constants.SubTopic].ToString()
                                                      },
                                          Position = new Position
                                                         {
                                                             Hizb = position[Constants.Hizb].ToString(),
                                                             Manzil = position[Constants.Manzil].ToString(),
                                                             Page = position[Constants.Page].ToString(),
                                                             Rubu = position[Constants.Rubu].ToString()
                                                         },
                                          Sajda = new Sajda
                                                      {
                                                          Id = sajda[Constants.Id].Value<int?>(),
                                                          Type = sajda[Constants.Type].ToString(),
                                                          Exists = sajda[Constants.Exists].Value<bool>()
                                                      }
                                      }
                          );
                callback(new SearchResults{SearchResultItems = results, HasError = false});
            }
            catch (Exception)
            {
                callback(new SearchResults{HasError = true, SearchResultItems = null});
            }
        }
    }
}
