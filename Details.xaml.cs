using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;

namespace AlfanousWP7
{
    public partial class Details : PhoneApplicationPage
    {
        public Details()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            var sura = NavigationContext.QueryString["sura"];
            var aya = int.Parse(NavigationContext.QueryString["aya"]);
            var client = new WebClient();
            string detailsUrl = string.Format("http://www.alfanous.org/json?search=سورة:{0}+رقم_الآية:{1}", sura, aya);
            client.DownloadStringCompleted += OnDownloadStringCompleted;
            client.DownloadStringAsync(new Uri(detailsUrl));
        }

        private void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var token = JToken.Parse(e.Result);
            var ayas = token[Constants.Ayas];
        }
    }
}