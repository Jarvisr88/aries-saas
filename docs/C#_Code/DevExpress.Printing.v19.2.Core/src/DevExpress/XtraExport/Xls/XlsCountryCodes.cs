namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public static class XlsCountryCodes
    {
        private const int countryCodeUS = 1;
        private static Dictionary<int, CultureInfo> codeTable = new Dictionary<int, CultureInfo>();
        private static Dictionary<CultureInfo, int> cultureTable = new Dictionary<CultureInfo, int>();

        static XlsCountryCodes()
        {
            InitializeCodeTable();
            InitializeCultureTable();
        }

        private static CultureInfo CreateSpecificCulture(string name) => 
            CultureInfo.CreateSpecificCulture(name);

        public static int GetCountryCode(CultureInfo cultureInfo) => 
            !cultureTable.ContainsKey(cultureInfo) ? 1 : cultureTable[cultureInfo];

        public static CultureInfo GetCultureInfo(int countryCode)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            CultureInfo parentCulture = currentCulture;
            if (codeTable.ContainsKey(countryCode))
            {
                parentCulture = codeTable[countryCode];
                if (parentCulture.IsNeutralCulture)
                {
                    return (!parentCulture.IsParentOf(currentCulture) ? CreateSpecificCulture(parentCulture.Name) : currentCulture);
                }
            }
            return parentCulture;
        }

        private static void InitializeCodeTable()
        {
            codeTable.Add(1, new CultureInfo("en-US"));
            codeTable.Add(2, new CultureInfo("en-CA"));
            codeTable.Add(3, new CultureInfo("es"));
            codeTable.Add(7, new CultureInfo("ru-RU"));
            codeTable.Add(20, new CultureInfo("ar-EG"));
            codeTable.Add(30, new CultureInfo("el-GR"));
            codeTable.Add(0x1f, new CultureInfo("nl-NL"));
            codeTable.Add(0x20, new CultureInfo("nl-BE"));
            codeTable.Add(0x21, new CultureInfo("fr-FR"));
            codeTable.Add(0x22, new CultureInfo("es-ES"));
            codeTable.Add(0x24, new CultureInfo("hu-HU"));
            codeTable.Add(0x27, new CultureInfo("it-IT"));
            codeTable.Add(0x29, new CultureInfo("de-CH"));
            codeTable.Add(0x2b, new CultureInfo("de-AT"));
            codeTable.Add(0x2c, new CultureInfo("en-GB"));
            codeTable.Add(0x2d, new CultureInfo("da-DK"));
            codeTable.Add(0x2e, new CultureInfo("sv-SE"));
            codeTable.Add(0x2f, new CultureInfo("no"));
            codeTable.Add(0x30, new CultureInfo("pl-PL"));
            codeTable.Add(0x31, new CultureInfo("de-DE"));
            codeTable.Add(0x34, new CultureInfo("es-MX"));
            codeTable.Add(0x37, new CultureInfo("pt-BR"));
            codeTable.Add(0x3d, new CultureInfo("en-AU"));
            codeTable.Add(0x40, new CultureInfo("en-NZ"));
            codeTable.Add(0x42, new CultureInfo("th-TH"));
            codeTable.Add(0x51, new CultureInfo("ja-JP"));
            codeTable.Add(0x52, new CultureInfo("ko-KR"));
            codeTable.Add(0x54, new CultureInfo("vi-VN"));
            codeTable.Add(0x56, new CultureInfo("zh-CN"));
            codeTable.Add(90, new CultureInfo("tr-TR"));
            codeTable.Add(0xd5, new CultureInfo("ar-DZ"));
            codeTable.Add(0xd8, new CultureInfo("ar-MA"));
            codeTable.Add(0xda, new CultureInfo("ar-LY"));
            codeTable.Add(0x15f, new CultureInfo("pt-PT"));
            codeTable.Add(0x162, new CultureInfo("is-IS"));
            codeTable.Add(0x166, new CultureInfo("fi-FI"));
            codeTable.Add(420, new CultureInfo("cs-CZ"));
            codeTable.Add(0x376, new CultureInfo("zh-TW"));
            codeTable.Add(0x3c1, new CultureInfo("ar-LB"));
            codeTable.Add(0x3c2, new CultureInfo("ar-JO"));
            codeTable.Add(0x3c3, new CultureInfo("ar-SY"));
            codeTable.Add(0x3c4, new CultureInfo("ar-IQ"));
            codeTable.Add(0x3c5, new CultureInfo("ar-KW"));
            codeTable.Add(0x3c6, new CultureInfo("ar-SA"));
            codeTable.Add(0x3cb, new CultureInfo("ar-AE"));
            codeTable.Add(0x3cc, new CultureInfo("he-IL"));
            codeTable.Add(0x3ce, new CultureInfo("ar-QA"));
            codeTable.Add(0x3d5, new CultureInfo("fa-IR"));
        }

        private static void InitializeCultureTable()
        {
            foreach (KeyValuePair<int, CultureInfo> pair in codeTable)
            {
                if (!pair.Value.IsNeutralCulture)
                {
                    cultureTable.Add(pair.Value, pair.Key);
                }
            }
            cultureTable.Add(new CultureInfo("es-AR"), 3);
            cultureTable.Add(new CultureInfo("es-BO"), 3);
            cultureTable.Add(new CultureInfo("es-CL"), 3);
            cultureTable.Add(new CultureInfo("es-CO"), 3);
            cultureTable.Add(new CultureInfo("es-CR"), 3);
            cultureTable.Add(new CultureInfo("es-DO"), 3);
            cultureTable.Add(new CultureInfo("es-EC"), 3);
            cultureTable.Add(new CultureInfo("es-SV"), 3);
            cultureTable.Add(new CultureInfo("es-GT"), 3);
            cultureTable.Add(new CultureInfo("es-HN"), 3);
            cultureTable.Add(new CultureInfo("es-NI"), 3);
            cultureTable.Add(new CultureInfo("es-PA"), 3);
            cultureTable.Add(new CultureInfo("es-PY"), 3);
            cultureTable.Add(new CultureInfo("es-PE"), 3);
            cultureTable.Add(new CultureInfo("es-PR"), 3);
            cultureTable.Add(new CultureInfo("es-UY"), 3);
            cultureTable.Add(new CultureInfo("es-VE"), 3);
            cultureTable.Add(new CultureInfo("nb-NO"), 0x2f);
            cultureTable.Add(new CultureInfo("nn-NO"), 0x2f);
        }

        public static bool IsParentOf(this CultureInfo parentCulture, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(culture.Name))
            {
                if (string.IsNullOrEmpty(parentCulture.Name))
                {
                    return true;
                }
                for (CultureInfo info = culture.Parent; !string.IsNullOrEmpty(info.Name); info = info.Parent)
                {
                    if (info.Name == parentCulture.Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

