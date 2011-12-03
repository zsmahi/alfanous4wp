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
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

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
            translationRtb.Xaml = GetFormattedText(result.Aya.Translation);
        }
        private string GetFormattedText(string text)
        {
            var highlightStart = "<Run TextDecorations=\"Underline\" FontWeight=\"ExtraBlack\" Foreground=\"Green\" >";
            var highlightEnd = "</Run>";
            var actualFormattedText = text.Replace("<b>", highlightStart).Replace("</b>", highlightEnd);
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