using System;
using AlfanousWP7.Helpers;

namespace AlfanousWP7.AlfanousClasses
{
    public class Stat
    {
        protected string start = "الآية تحتوي على:";
        public int? Letters { get; set; }
        public int? GodNames { get; set; }
        public int? Words { get; set; }

        public override string ToString()
        {
            var hourouf = HelperMethods.NumberToString(Letters.Value, "حرف", "حرفين", "أحرف");
            var kalimat = HelperMethods.NumberToString(Words.Value, "كلمة", "كلمتين", "كلمات");
            var asmaaAllah = GodNames == 0
                                 ? string.Empty
                                 : string.Format("\n\tو {0} من أسماء الله الحسنى", GodNames);
            return String.Format("{0} \n\t{1}\n\t{2}{3}", start, hourouf, kalimat, asmaaAllah);
        }
    }
}