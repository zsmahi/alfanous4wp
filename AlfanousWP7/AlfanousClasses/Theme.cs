﻿using AlfanousWP7.Helpers;

namespace AlfanousWP7.AlfanousClasses
{
    public class Theme
    {
        public string Chapter { get; set; }
        public string Topic { get; set; }
        public string SubTopic { get; set; }

        public override string ToString()
        {
            var result = string.Empty;
            
            if(string.IsNullOrWhiteSpace(Chapter))
                return result;

            result += "الفصل: " + Chapter.RemoveFormatting()+"\n";

            if (string.IsNullOrWhiteSpace(Topic))
                return result;
            
            result += "الفرع: " + Topic.RemoveFormatting()+"\n";
            
            if(string.IsNullOrWhiteSpace(SubTopic))
                return result;
            
            result += "الباب: " + SubTopic.RemoveFormatting();
            
            return result;
        }
    }
}