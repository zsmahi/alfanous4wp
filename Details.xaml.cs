using System;
using System.IO.IsolatedStorage;
using System.Windows;
using AlfanousWP7.AlfanousClasses;
using Microsoft.Phone.Controls;

namespace AlfanousWP7
{
    public partial class Details : PhoneApplicationPage
    {
        private SearchResultItem result;

        public Details()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            result = Pipe.SearchResultItem;
            DataContext = result;
            textRtb.Xaml= GetFormattedText(result.Aya.Text);
            translationRtb.Xaml = GetFormattedTranslation(result.Aya.Translation);
            SetTranslationVisibility();
        }
        private void SetTranslationVisibility()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(Search.TranslationSettingsKey))
                return;
            var translation =
                (TranslationEnumeration)IsolatedStorageSettings.ApplicationSettings[Search.TranslationSettingsKey];
            if (translation == TranslationEnumeration.None)
            {
                translationTab.Visibility = Visibility.Collapsed;
                translationTab.IsEnabled = false;
            }
            else
            {
                translationTab.Visibility = Visibility.Visible;
                translationTab.IsEnabled = true;
            }
        }
        private string GetFormattedText(string text)
        {
            var highlightStart = "<Run TextDecorations=\"Underline\" FontWeight=\"ExtraBlack\" Foreground=\"Green\" FlowDirection=\"LeftToRight\" >";
            var highlightEnd = "</Run>";
            var actualFormattedText = text.Replace("<b>", highlightStart).Replace("</b>", highlightEnd);
            var formattedText = "<Section xml:space=\"preserve\" HasTrailingParagraphBreakOnPaste=\"False\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>" +
                                 actualFormattedText +
                                 "</Paragraph></Section>";
            return formattedText;
        }
        private string GetFormattedTranslation(string text)
        {
            var underlinedStart = "<Run TextDecorations=\"Underline\">";
            var boldStart = "<Run FontWeight=\"Bold\">";
            var italicStart = "<Run FontStyle=\"Italic\">";
            var highlightEnd = "</Run>";
            var actualFormattedText = text
                .Replace("<b>", boldStart)
                .Replace("<u>",underlinedStart)
                .Replace("<i>",italicStart)
                .Replace("</b>", highlightEnd)
                .Replace("</u>", highlightEnd)
                .Replace("</i>", highlightEnd);
            var formattedText = "<Section xml:space=\"preserve\" HasTrailingParagraphBreakOnPaste=\"False\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>" +
                                 actualFormattedText +
                                 "</Paragraph></Section>";
            return formattedText;
        }

        private void OnPlayButtonTap(object sender, EventArgs eventArgs)
        {
            mediaElement.Play();
        }
        private void OnPauseButtonTap(object sender, EventArgs eventArgs)
        {
            mediaElement.Pause();
        }
        private void OnStopButtonTap(object sender, EventArgs eventArgs)
        {
            mediaElement.Stop();
        }
    }
}