using System;

namespace AlfanousWP7.Helpers
{
    public class HelperMethods
    {
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