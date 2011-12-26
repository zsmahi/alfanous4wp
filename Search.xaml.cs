using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AlfanousWP7.AlfanousClasses;
using AlfanousWP7.Helpers;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace AlfanousWP7
{
    public partial class Search : PhoneApplicationPage
    {
        private const string RecitationSettingsKey = "Recitation";
        public const string TranslationSettingsKey = "Translation";
        private const string LoadMoreResultsString = "تحميل نتائج أخرى ...";

        private readonly Button moreResultsButton = new Button
                                                        {
                                                            Name = "moreResultsButton",
                                                            Content = LoadMoreResultsString,
                                                            Margin = new Thickness(5),
                                                            Width = 390
                                                        };

        private readonly IAlfanousSearchService searchService = new AlfanousSearchService();
        private int currentPage;

        private ObservableCollection<object> searchResults;

        public Search()
        {
            InitializeComponent();
            //progressBar.IsIndeterminate = true;
            Initilize();
            //AlfanousLists.ListDownloadComplete += OnListDownloadComplete;
            moreResultsButton.Tap += OnMoreResultsTap;
            var app = (App) Application.Current;
            var backgroundImage = app.CurrentTheme== DeviceTheme.Dark
                ? "/AlfanousWP7;component/Images/el-fanoos_panorama_background_blackWashed.png"
                : "/AlfanousWP7;component/Images/el-fanoos_panorama_background_whiteWashed.png";
            panorama.Background = new ImageBrush
                                      {
                                          ImageSource =
                                              new BitmapImage(
                                              new Uri(backgroundImage,
                                                      UriKind.Relative)),
                                          Transform = new CompositeTransform { TranslateX = -150}
                                      };
            
        }

        private void Initilize()
        {
            //progressBar.IsIndeterminate = false;
            translationListPicker.ItemsSource = AlfanousLists.Translations;
            recitationListPicker.ItemsSource = AlfanousLists.Recitations;
            LoadSettings();
            translationListPicker.SelectionChanged += OnTranslationChanged;
            recitationListPicker.SelectionChanged += OnRecitationChanged;
        }

        private void OnListDownloadComplete(object sender, EventArgs e)
        {
            Initilize();
        }

        private void LoadSettings()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(TranslationSettingsKey))
            {
                var translationKey =
                    (TranslationEnumeration) IsolatedStorageSettings.ApplicationSettings[TranslationSettingsKey];
                translationListPicker.SelectedItem =
                    AlfanousLists.Translations.Single(item => item.Value == translationKey);
                searchService.Translation = translationKey;
            }

            if (IsolatedStorageSettings.ApplicationSettings.Contains(RecitationSettingsKey))
            {
                var recitationKey = (string) IsolatedStorageSettings.ApplicationSettings[RecitationSettingsKey];
                recitationListPicker.SelectedItem = AlfanousLists.Recitations.Single(item=>item.Key== recitationKey);
                searchService.Recitation = recitationKey;
            }
        }

        private void OnSearchIconTapped(object sender, EventArgs e)
        {
            currentPage = 1;
            var searchTerm = queryTextBox.Text;
            progressBar.IsIndeterminate = true;
            searchService.Search(searchTerm, currentPage, results =>
            {
                progressBar.IsIndeterminate = false;
                if (results.HasError)
                {
                    searchDetailsTB.Text = "حدث خطأ، الرجاء إعادة المحاولة ...";
                    return;
                }
                if (results.SearchResultItems == null || results.SearchResultItems.Count() == 0)
                {
                    searchDetailsTB.Text = "لا توجد أية نتائج للبحث ...";
                    return;
                }
                var formattedToal = HelperMethods.NumberToString(results.TotalResultCount, "نتيجة", "نتيجتان", "نتائج");
                searchDetailsTB.Text = String.Format("تم تحميل {0} من {1}", results.LastFetched, formattedToal);
                searchResults = new ObservableCollection<object>(results.SearchResultItems.Cast<object>());
                resultsListBox.ItemsSource = searchResults;
                if (results.HasMore)
                    searchResults.Add(moreResultsButton);
            });
        }

        private void OnResultsListBoxItemTap(object sender, GestureEventArgs e)
        {
            var selectedAya = resultsListBox.SelectedItem as SearchResultItem;
            if (selectedAya == null)
                return;
            Pipe.SearchResultItem = selectedAya;
            NavigationService.Navigate(new Uri("/Details.xaml", UriKind.Relative));
        }

        private void OnTranslationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (translationListPicker.ItemsSource == null)
                return;
            var translationKey =
                ((KeyValuePair<string, TranslationEnumeration>) translationListPicker.SelectedItem).Value;
            searchService.Translation = translationKey;
            IsolatedStorageSettings.ApplicationSettings[TranslationSettingsKey] = translationKey;
        }

        private void OnRecitationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (recitationListPicker.ItemsSource == null)
                return;
            var recitationKey = ((KeyValuePair<string,string>) recitationListPicker.SelectedItem).Key;
            searchService.Recitation = recitationKey;
            IsolatedStorageSettings.ApplicationSettings[RecitationSettingsKey] = recitationKey;
        }

        private void OnMoreResultsTap(object sender, GestureEventArgs e)
        {
            moreResultsButton.Content = "جاري التحميل ...";
            moreResultsButton.IsEnabled = false;
            progressBar.IsIndeterminate = true;
            currentPage++;
            searchService.Search(queryTextBox.Text,
                                 currentPage,
                                 results =>
                                     {
                                         progressBar.IsIndeterminate = false;
                                         moreResultsButton.Content = LoadMoreResultsString;
                                         moreResultsButton.IsEnabled = true;
                                         searchResults.Remove(moreResultsButton);
                                         foreach (var item in results.SearchResultItems)
                                             searchResults.Add(item);
                                         var formattedToal =
                                             HelperMethods.NumberToString( results.TotalResultCount, "نتيجة","نتيجتان", "نتائج");
                                         searchDetailsTB.Text =String.Format("تم تحميل {0} من {1}",
                                                           results.LastFetched,
                                                           formattedToal);
                                         if (results.HasMore)
                                             searchResults.Add(moreResultsButton);
                                     });
        }

        private void OnQueryTextBoxKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                OnSearchIconTapped(this, null);
        }
    }
}