using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace AlfanousWP7.AlfanousClasses
{
    public static class AlfanousLists
    {
        public static Dictionary<string, TranslationEnumeration> Translations { get; set; }
        public static Dictionary<string, string> Recitations { get; set; }
        public static event EventHandler ListDownloadComplete;

        static AlfanousLists()
        {
            Initialize();
        }

        private static void Initialize()
        {

            InitializeRecitations();
            InitializeTranslations();
        }

        private static void InitializeRecitations()
        {
            var webClient2 = new WebClient();
            var recitationListUri = new Uri("http://www.alfanous.org/json?list=recitations");
            webClient2.DownloadStringCompleted += OnRecitationListDownloadCompleted;
            webClient2.DownloadStringAsync(recitationListUri);
        }
        private static void InitializeTranslations()
        {
            //var webClient1 = new WebClient();
            //var translationListUri = new Uri("http://www.alfanous.org/json?list=translations");
            //webClient1.DownloadStringCompleted += OnTranslationListDownloadCompleted;
            //webClient1.DownloadStringAsync(translationListUri);
            Translations=new Dictionary<string, TranslationEnumeration>();
            Translations["بلا ترجمة"]= TranslationEnumeration.None;
            Translations["انجليزية - محمد حبيب شاكر"]= TranslationEnumeration.EnglishTranslation;
            Translations["تهجئة انجليزية"]= TranslationEnumeration.EnglishTransliteration;
        }

        private static void OnRecitationListDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                Recitations = GetList(e.Result);
                CheckAndFireCompletedEvent();
            }
            catch (Exception ex)
            {
                
            }
        }

        //private static void OnTranslationListDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    Translations = GetList(e.Result);
        //    CheckAndFireCompletedEvent();
        //}


        private static Dictionary<string, string> GetList(string jsonText)
        {
            try
            {
                var jsonList = JToken.Parse(jsonText);
                return jsonList.Select(item => item.ToString().Replace("\"", "").Split(':')).ToDictionary(item => item[0], item => item[1]);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private static void CheckAndFireCompletedEvent()
        {
            if (Recitations != null && Translations != null && ListDownloadComplete != null)
                ListDownloadComplete(null, null);
        }
    }
    public enum TranslationEnumeration
    {
        None,
        EnglishTranslation,
        EnglishTransliteration
    }
}