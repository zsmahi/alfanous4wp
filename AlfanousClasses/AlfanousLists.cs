using System;
using System.Collections.Generic;

namespace AlfanousWP7.AlfanousClasses
{
    public static class AlfanousLists
    {
        static AlfanousLists()
        {
            Initialize();
        }

        public static Dictionary<string, TranslationEnumeration> Translations { get; private set; }
        //public static Dictionary<string, string> Recitations { get; set; }
        // Fields...

        public static Dictionary<string, string> Recitations { get; private set; }
        public static event EventHandler ListDownloadComplete;

        private static void Initialize()
        {
            InitializeRecitations();
            InitializeTranslations();
        }

        private static void InitializeRecitations()
        {
            //var webClient2 = new WebClient();
            //var recitationListUri = new Uri("http://www.alfanous.org/json?list=recitations");
            //webClient2.DownloadStringCompleted += OnRecitationListDownloadCompleted;
            //webClient2.DownloadStringAsync(recitationListUri);
            Recitations = new Dictionary<string, string>
                              {
                                  {"Mishary Rashid Alafasy", "مشاري بن راشد العفاسي"},
                                  {"Ahmed_ibn_Ali_al-Ajamy (From QuranExplorer.com)", "أحمد بن علي العجمي"},
                                  {"Menshawi (external source)","محمد صديق المنشاوي"},
                                  {"Saad Al Ghamadi","سعد الغامدي"},
                                  {"AbdulBasit AbdusSamad (From QuranExplorer.com)","عبد الباسط عبد الصمد"},
                                  {"AbdulBasit AbdusSamad (Murattal style)","عبد الباسط عبد الصمد (مرتل)"},
                                  {"Hani Rifai","هاني الرفاعي"},
                                  {"Muhammad Ayyoub","محمد أيوب"},
                                  {"Muhammad Ayyoub (external source)","محمد أيوب (2)"},
                                  {"Husary","محمود خليل الحصري"},
                                  {"Husary Mujawwad","محمود خليل الحصري (مجود)"},
                                  {"Saood bin Ibraaheem Ash-Shuraym", "سعود ابن ابراهيم الشريم"},
                                  {"Hudhaify","علي بن عبد الرحمن الحذيفي"},
                                  {"Abu Bakr Ash-Shaatree","أبو بكر الشاطري"},
                                  {"Ibrahim_Walk","Ibrahim Walk (قراءة للترجمة الإنجليزية)"},
                                  {"Abdullah Basfar","عبد الله بصفر"},
                              };
        }

        private static void InitializeTranslations()
        {
            //var webClient1 = new WebClient();
            //var translationListUri = new Uri("http://www.alfanous.org/json?list=translations");
            //webClient1.DownloadStringCompleted += OnTranslationListDownloadCompleted;
            //webClient1.DownloadStringAsync(translationListUri);
            Translations = new Dictionary<string, TranslationEnumeration>();
            Translations["بلا ترجمة"] = TranslationEnumeration.None;
            Translations["انجليزية - محمد حبيب شاكر"] = TranslationEnumeration.EnglishTranslation;
            Translations["تهجئة انجليزية"] = TranslationEnumeration.EnglishTransliteration;
        }

        //private static void OnRecitationListDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        Recitations = GetList(e.Result);
        //        CheckAndFireCompletedEvent();
        //    }
        //    catch
        //    {

        //    }
        //}

        //private static void OnTranslationListDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    Translations = GetList(e.Result);
        //    CheckAndFireCompletedEvent();
        //}


        //private static Dictionary<string, string> GetList(string jsonText)
        //{
        //    try
        //    {
        //        var jsonList = JToken.Parse(jsonText);
        //        return jsonList.Select(item => item.ToString().Replace("\"", "").Split(':')).ToDictionary(item => item[0], item => item[1]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}


        //private static void CheckAndFireCompletedEvent()
        //{
        //    if (Recitations != null && Translations != null && ListDownloadComplete != null)
        //        ListDownloadComplete(null, null);
        //}
    }

    public interface IAlfanousLists
    {
    }

    public enum TranslationEnumeration
    {
        None,
        EnglishTranslation,
        EnglishTransliteration
    }
}