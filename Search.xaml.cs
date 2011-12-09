using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private readonly AlfanousSearchService searchService=new AlfanousSearchService();
        public Search()
        {
            InitializeComponent();
            progressBar.IsIndeterminate = true;
            AlfanousLists.ListDownloadComplete += OnListDownloadComplete;
        }

        private void OnListDownloadComplete(object sender, EventArgs e)
        {
            progressBar.IsIndeterminate = false;
            translationListPicker.ItemsSource = AlfanousLists.Translations;
            recitationListPicker.ItemsSource = AlfanousLists.Recitations.Keys.ToList();
            LoadSettings();
            translationListPicker.SelectionChanged += OnTranslationChanged;
            recitationListPicker.SelectionChanged += OnRecitationChanged;
        }

        private void LoadSettings()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(TranslationSettingsKey))
            {
                var translationKey =
                    (TranslationEnumeration) IsolatedStorageSettings.ApplicationSettings[TranslationSettingsKey];
                translationListPicker.SelectedItem =
                    AlfanousLists.Translations.Single(item => item.Value == translationKey);
            }

            if (IsolatedStorageSettings.ApplicationSettings.Contains(RecitationSettingsKey))
            {
                var recitationKey = IsolatedStorageSettings.ApplicationSettings[RecitationSettingsKey];
                recitationListPicker.SelectedItem = recitationKey;
            }
        }
        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            var searchTerm = queryTextBox.Text;
            progressBar.IsIndeterminate = true;
            searchService.Search(searchTerm, results =>
                                                 {
                                                     progressBar.IsIndeterminate = false;
                                                     if (results.HasError)
                                                     {
                                                         searchDetailsTB.Text = "حدث خطأ، الرجاء إعادة المحاولة ...";
                                                         return;
                                                     }
                                                     if (results.SearchResultItems == null || results.SearchResultItems.Count() == 0)
                                                         searchDetailsTB.Text = "لا توجد أية نتائج للبحث ...";
                                                     else
                                                     {
                                                         searchDetailsTB.Text =
                                                             HelperMethods.NumberToString(
                                                                 results.SearchResultItems.Count(), "نتيجة", "نتيجتان",
                                                                 "نتائج");
                                                     }
                                                     resultsListBox.ItemsSource = results.SearchResultItems;
                                                 });

        }

        private void OnResultsListBoxItemTap(object sender, GestureEventArgs e)
        {
            var selectedAya = (SearchResultItem) resultsListBox.SelectedItem;
            if (selectedAya==null)
                return;
            Pipe.SearchResultItem = selectedAya;
            NavigationService.Navigate(new Uri("/Details.xaml",UriKind.Relative));
        }

        private void OnTranslationChanged(object sender, SelectionChangedEventArgs e)
        {
            if(translationListPicker.ItemsSource==null)
                return;
            var translationKey = ((KeyValuePair<string, TranslationEnumeration>)translationListPicker.SelectedItem).Value;
            searchService.Translation = translationKey;
            IsolatedStorageSettings.ApplicationSettings[TranslationSettingsKey] = translationKey;
        }

        private void OnRecitationChanged(object sender, SelectionChangedEventArgs e)
        {
            if(recitationListPicker.ItemsSource==null)
                return;
            //var recitationKey = ((KeyValuePair<string, string>)recitationListPicker.SelectedItem).Key;
            var recitationKey = (string)recitationListPicker.SelectedItem;
            searchService.Recitation = recitationKey;
            IsolatedStorageSettings.ApplicationSettings[RecitationSettingsKey] = recitationKey;
        }
    }
}