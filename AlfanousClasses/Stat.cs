using System;
namespace AlfanousWP7.AlfanousClasses
{
    public class Stat
    {
        public int? Letters { get; set; }
        public int? GodNames { get; set; }
        public int? Words { get; set; }
        public override string ToString()
        {
            var kalimat = Words == 1
                       ? "كلمة واحدة"
                       : Words == 2
                             ? "كلمتين"
                             : Words >= 3 && Words <= 10
                                   ? Words + " كلمات "
                                   : Words + " كلمة ";
            var asmaaAllah = GodNames == 0
                                 ? string.Empty
                                 : string.Format("و {0} من أسماء الله الحسنى", GodNames);
            return String.Format("الآية تحتوي على {0} حرفا، {1} {2}", Letters, kalimat, asmaaAllah);
        }
    }
}