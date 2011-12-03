namespace AlfanousWP7.AlfanousClasses
{
    public class SuraStat:Stat
    {
        public int? Ayas { get; set; }
        public override string ToString()
        {
            start = string.Format("ÇáÓæÑÉ ÊÍæí {0}¡ ", NumberToString(Ayas.Value, "ÂíÉ", "ÂíÊíä", "ÂíÇÊ"));
            return base.ToString();
        }
    }
}