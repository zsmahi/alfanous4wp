using System;
using System.Collections.Generic;
using System.Linq;

namespace AlfanousWP7.Helpers
{
    public static class HelperMethods
    {
        public static string Stringify(this IEnumerable<char> charEnumerable)
        {
            return new string(charEnumerable.ToArray());
        }
        public static string NumberToString(int number, string single, string couple, string plural)
        {
            return number == 1
                       ? String.Format("{0} واحدة", single)
                       : number == 2
                             ? couple
                             : number >= 3 && number <= 10
                                   ? String.Format("{0} {1}", number, plural)
                                   : String.Format("{0} {1}", number, single);
        }
    }
}