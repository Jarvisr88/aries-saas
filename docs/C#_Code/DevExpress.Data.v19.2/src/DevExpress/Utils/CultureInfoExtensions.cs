namespace DevExpress.Utils
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class CultureInfoExtensions
    {
        public static CultureInfo CreateSpecificCulture(string name) => 
            CultureInfo.CreateSpecificCulture(name);

        public static string[] GetAllDateTimePatterns(this CultureInfo culture) => 
            culture.DateTimeFormat.GetAllDateTimePatterns();

        public static string[] GetAllDateTimePatterns(this CultureInfo culture, char format) => 
            culture.DateTimeFormat.GetAllDateTimePatterns(format);

        public static CultureInfo GetCultureInfo(string name) => 
            CultureInfo.GetCultureInfo(name);

        public static string GetDateSeparator(this CultureInfo culture) => 
            culture.DateTimeFormat.DateSeparator;

        public static int GetLCID(this CultureInfo culture) => 
            culture.LCID;

        public static char GetListSeparator(this CultureInfo culture) => 
            culture.TextInfo.ListSeparator[0];

        public static string GetTimeSeparator(this CultureInfo culture) => 
            culture.DateTimeFormat.TimeSeparator;

        public static void SetCurrentCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public static void SetCurrentUICulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}

