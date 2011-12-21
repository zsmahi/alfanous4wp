using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using AlfanousWP7.AlfanousClasses;
using Microsoft.Phone.Controls;

namespace AlfanousWP7
{
    public partial class Details : PhoneApplicationPage
    {
        private SearchResultItem result;
        private readonly App app;

        public Details()
        {
            InitializeComponent();
            pivot.Items.Remove(pivot.Items.Cast<PivotItem>().Single(p => p.Name == translationTab.Name));
            app = (App)Application.Current;
            var backgroundImage = app.CurrentTheme == DeviceTheme.Dark
                ? "/AlfanousWP7;component/Images/el-fanoos_pivot_background_blackWashed.png"
                : "/AlfanousWP7;component/Images/el-fanoos_pivot_background_whiteWashed.png";
            var background = new ImageBrush
            {
                ImageSource =
                    new BitmapImage(
                    new Uri(backgroundImage,
                            UriKind.Relative)),
            };
            LayoutRoot.Background = background;
            //foreach (PivotItem pivotItem in pivot.Items)
            //{
            //    pivotItem.Background = background;
            //}
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            result = Pipe.SearchResultItem;
            DataContext = result;
            textRtb.Xaml = GetFormattedText(result.Aya.Text);
            if (!string.IsNullOrWhiteSpace(result.Aya.Translation))
                translationRtb.Xaml = GetFormattedTranslation(result.Aya.Translation);
            SetTranslationVisibility();
        }

        private void SetTranslationVisibility()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(Search.TranslationSettingsKey))
                return;
            var translation =
                (TranslationEnumeration) IsolatedStorageSettings.ApplicationSettings[Search.TranslationSettingsKey];
            if (translation == TranslationEnumeration.None)
            {
                var translationPivot = pivot.Items.Cast<PivotItem>().SingleOrDefault(p => p.Name == translationTab.Name);
                if (translationPivot != null)
                    pivot.Items.Remove(translationPivot);
            }
            else
            {
                pivot.Items.Add(translationTab);
            }
        }

        private string GetFormattedText(string text)
        {
            var highlightColor = app.Resources["PhoneAccentColor"].ToString();
            var highlightStart = String.Format("<Run TextDecorations=\"Underline\" FontWeight=\"ExtraBlack\" Foreground=\"{0}\" FlowDirection=\"LeftToRight\" >", highlightColor);
            const string highlightEnd = "</Run>";
            var actualFormattedText = text.Replace("<b>", highlightStart).Replace("</b>", highlightEnd);
            var formattedText =
                "<Section xml:space=\"preserve\" HasTrailingParagraphBreakOnPaste=\"False\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>" +
                actualFormattedText +
                "</Paragraph></Section>";
            return formattedText;
        }

        private static string GetFormattedTranslation(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
                return string.Empty;
            const string underlinedStart = "<Run TextDecorations=\"Underline\">";
            const string boldStart = "<Run FontWeight=\"Bold\">";
            const string italicStart = "<Run FontStyle=\"Italic\">";
            const string highlightEnd = "</Run>";
            var actualFormattedText = text
                .Replace("<b>", boldStart)
                .Replace("<u>", underlinedStart)
                .Replace("<i>", italicStart)
                .Replace("</b>", highlightEnd)
                .Replace("</u>", highlightEnd)
                .Replace("</i>", highlightEnd);
            var formattedText =
                "<Section TextAlignment=\"Left\" xml:space=\"preserve\" HasTrailingParagraphBreakOnPaste=\"False\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>" +
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