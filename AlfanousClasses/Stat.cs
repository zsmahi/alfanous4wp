using System;

namespace AlfanousWP7.AlfanousClasses
{
    public class Stat
    {
        protected string start = "الآية تحتوي على";
        public int? Letters { get; set; }
        public int? GodNames { get; set; }
        public int? Words { get; set; }

        public override string ToString()
        {
            var single = "كلمة";
            var couple = "كلمتين";
            var plural = "كلمات";
            string kalimat = NumberToString(Words.Value, single, couple, plural);
            var asmaaAllah = GodNames == 0
                                 ? string.Empty
                                 : string.Format("و {0} من أسماء الله الحسنى", GodNames);
            return String.Format("{3} {0} حرفا، {1}  {2}", Letters, kalimat, asmaaAllah, start);
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