using System;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var url =
                "http://www.alfanous.org/json?search=%D8%A7%D9%84%D8%AD%D9%85%D8%AF&highlight=bbcode&sortedby=tanzil&page=2&traduction=shakir&recitation=Mishary+Rashid+Alafasy";
            var url2 = "http://www.alfanous.org/json?search=الجنة&sortedby=tanzil&page=2&traduction=shakir&recitation=Mishary+Rashid+Alafasy";
            var searchTerm = queryTextBox.Text;
            var queryUrl =
                string.Format("http://www.alfanous.org/json?search={0}&sortedby=tanzil&page=2&traduction=shakir&recitation=Mishary+Rashid+Alafasy", searchTerm);
            var client = new WebClient();
            client.DownloadStringCompleted += OnDownloadStringCompleted;
            client.DownloadStringAsync(new Uri(queryUrl));

        }

        private void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var token = JToken.Parse(e.Result);
            var ayas = token["ayas"];
            var results = (from result in token["ayas"].Children()
                          select result.First["aya"]["text"].ToString()).ToList();
            resultsListBox.ItemsSource = results;
        }
    }
}