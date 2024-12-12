namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public static class LanguageIdToCultureConverter
    {
        private static readonly Dictionary<int, CultureInfo> cultureTable = CreateCultureTable();

        private static void AddCulture(Dictionary<int, CultureInfo> where, CultureInfo culture)
        {
            where[culture.LCID] = culture;
        }

        public static int Convert(CultureInfo cultureInfo) => 
            cultureInfo.LCID;

        public static CultureInfo Convert(int lcid)
        {
            CultureInfo info;
            return (cultureTable.TryGetValue(lcid, out info) ? info : null);
        }

        private static Dictionary<int, CultureInfo> CreateCultureTable()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            Dictionary<int, CultureInfo> where = new Dictionary<int, CultureInfo>();
            AddCulture(where, CultureInfo.InvariantCulture);
            foreach (CultureInfo info in cultures)
            {
                AddCulture(where, info);
            }
            return where;
        }

        public static IEnumerable<CultureInfo> GetAllLanguages() => 
            cultureTable.Values;
    }
}

