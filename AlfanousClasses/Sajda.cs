using System;
namespace AlfanousWP7.AlfanousClasses
{
    public class Sajda
    {
        public string Type { get; set; }
        public bool Exists { get; set; }
        public int? Id { get; set; }

        public override string ToString()
        {
            return Exists
                       ? String.Format("تحتوي الآية على السجدة الـ {0} و هي {1}", Id, Type)
                       : string.Empty;
        }
    }
}