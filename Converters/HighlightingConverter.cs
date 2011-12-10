using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using AlfanousWP7.AlfanousClasses;
using AlfanousWP7.Helpers;

namespace AlfanousWP7.Converters
{
    public class HighlightingConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        ///     Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <returns>
        ///     The value to be passed to the target dependency property.
        /// </returns>
        /// <param name = "value">The source data being passed to the target.</param>
        /// <param name = "targetType">The <see cref = "T:System.Type" /> of data expected by the target dependency property.</param>
        /// <param name = "parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name = "culture">The culture of the conversion.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var aya = (Aya) value;
            var text = aya.Text;
            var uthmaniText = aya.UthmaniText;
            //const string highlightStart =
            //    "<Run TextDecorations=\"Underline\" FontWeight=\"ExtraBlack\" Foreground=\"Green\" FlowDirection=\"LeftToRight\" >";
            //const string highlightEnd = "</Run>";
            //var actualFormattedText = text.Replace("<b>", highlightStart).Replace("</b>", highlightEnd);
            //var formattedText =
            //    "<Section xml:space=\"preserve\" HasTrailingParagraphBreakOnPaste=\"False\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>" +
            //    actualFormattedText +
            //    "</Paragraph></Section>";
            if (uthmaniText.Length <= 200)
                return uthmaniText;
            var highlightIndex = text.IndexOf('<');
            var removeLeft = highlightIndex - 100;
            var leftRemoved = uthmaniText
                .SkipWhile((c, i) => c != ' ' || i < removeLeft)
                .Stringify();
            var trimmedResult = leftRemoved
                .TakeWhile((c, i) => c != ' ' || i < 200).Stringify();
            return "... "+trimmedResult+" ...";
        }

        /// <summary>
        ///     Modifies the target data before passing it to the source object.  This method is called only in <see
        ///      cref = "F:System.Windows.Data.BindingMode.TwoWay" /> bindings.
        /// </summary>
        /// <returns>
        ///     The value to be passed to the source object.
        /// </returns>
        /// <param name = "value">The target data being passed to the source.</param>
        /// <param name = "targetType">The <see cref = "T:System.Type" /> of data expected by the source object.</param>
        /// <param name = "parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name = "culture">The culture of the conversion.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}