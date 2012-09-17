using AlfanousWP7.Helpers;

namespace AlfanousWP7.AlfanousClasses
{
    public class SuraStat:Stat
    {
        public int? Ayas { get; set; }
        public override string ToString()
        {
            start = string.Format("ÇáÓæÑÉ ÊÍÊæí Úáì: \n\t{0} ", HelperMethods.NumberToString(Ayas.Value, "ÂíÉ", "ÂíÊíä", "ÂíÇÊ"));
            return base.ToString();
        }
    }
}