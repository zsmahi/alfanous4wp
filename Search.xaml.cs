using System;
using System.Linq;
using System.Net;
using System.Windows;
using AlfanousWP7.AlfanousClasses;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace AlfanousWP7
{
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            InitializeComponent();
        }

        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            var searchTerm = queryTextBox.Text;
            if(string.IsNullOrWhiteSpace(searchTerm))
                return;
            var url =
                "http://www.alfanous.org/json?search=%D8%A7%D9%84%D8%AD%D9%85%D8%AF&highlight=bbcode&sortedby=tanzil&page=2&traduction=shakir&recitation=Mishary+Rashid+Alafasy";
            var url2 =
                "http://www.alfanous.org/json?search=الجنة&sortedby=tanzil&page=2&traduction=shakir&recitation=Mishary+Rashid+Alafasy&highlight=bold";
            var queryUrl =
                string.Format(
                    "http://www.alfanous.org/json?search={0}&sortedby=mushaf&translation=shakir&recitation=Mishary+Rashid+Alafasy&highlight=bold",
                    searchTerm);
            var sajdaUrl = "http://www.alfanous.org/json?search=سجدة:نعم&traduction=shakir";
            var client = new WebClient();
            client.DownloadStringCompleted += OnDownloadStringCompleted;
            client.DownloadStringAsync(new Uri(queryUrl));
        }

        private void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
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
                                                         Name = sura[Constants.Name].ToString().Replace("<b>","").Replace("</b>",""),
                                                         Type = sura[Constants.Type].ToString(),
                                                         Order = sura[Constants.Order].Value<int?>(),
                                                         Stat = new SuraStat
                                                                    {
                                                                        Letters = sura[Constants.Stat][Constants.Letters].Value<int?>(),
                                                                        Words = sura[Constants.Stat][Constants.Words].Value<int?>(),
                                                                        GodNames = sura[Constants.Stat][Constants.GodNames].Value<int?>(),
                                                                        Ayas = sura[Constants.Stat][Constants.Ayas].Value<int?>()
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
            resultsListBox.ItemsSource = results;
        }

        private void OnResultsListBoxItemTap(object sender, GestureEventArgs e)
        {
            var selectedAya = (SearchResultItem) resultsListBox.SelectedItem;
            Pipe.SearchResultItem = selectedAya;
            NavigationService.Navigate(new Uri("/Details.xaml",UriKind.Relative));
        }
    }
}