using AlfanousWP7.AlfanousClasses;
using System;

namespace AlfanousWP7
{
    public class SearchResultItem
    {
        public string Id { get; set; }
        public Aya Aya { get; set; }
        public Sura Sura { get; set; }
        public Stat Stat { get; set; }
        public Theme Theme { get; set; }
        public Position Position { get; set; }
        public Sajda Sajda { get; set; }
    }
}