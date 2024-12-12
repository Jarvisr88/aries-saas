namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class PdfGDIFontSubstitutionHelper
    {
        private const int normalWeight = 400;
        private const int boldWeight = 700;
        private const string timesNewRomanFontName = "Times New Roman";
        private const string courierNewFontName = "Courier New";
        private const string segoeUiFontName = "Segoe UI";
        private const string mtSuffix = "MT";
        private const string tahomaFontName = "Tahoma";
        private const string normalizedSymbolFontName = "symbol";
        private static IDictionary<string, PdfFontFamilyInfo> fontFamilyInfoes;

        private static void AddFamilyIfNotExists(IDictionary<string, PdfFontFamilyInfo> dictionary, string key, string value)
        {
            string normalizedFontFamily = PdfFontNamesHelper.GetNormalizedFontFamily(key);
            if (!dictionary.ContainsKey(normalizedFontFamily))
            {
                dictionary.Add(normalizedFontFamily, new PdfFontFamilyInfo(value));
            }
        }

        public static PdfFontParameters GetSubstituteFontParameters(PdfFont font)
        {
            PdfFontFamilyInfo info;
            string text1;
            string fontName = font.FontName;
            List<string> list = new List<string> {
                font.FontName,
                font.BaseFont
            };
            PdfFontDescriptor fontDescriptor = font.FontDescriptor;
            if (fontDescriptor != null)
            {
                text1 = fontDescriptor.FontName;
            }
            else
            {
                PdfFontDescriptor local1 = fontDescriptor;
                text1 = null;
            }
            list.Add(text1);
            string normalizedFontFamily = PdfFontNamesHelper.GetNormalizedFontFamily(fontName);
            string normalizedFontStyle = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (!string.IsNullOrEmpty(list[i]))
                {
                    normalizedFontStyle = PdfFontNamesHelper.GetNormalizedFontStyle(list[i]);
                }
                if (!string.IsNullOrEmpty(normalizedFontStyle))
                {
                    break;
                }
            }
            PdfFontDescriptor descriptor = font.FontDescriptor;
            int weight = (descriptor != null) ? descriptor.FontWeight : 400;
            string fontFamily = fontName;
            bool flag = false;
            if (!FontFamilyInfoes.TryGetValue(normalizedFontFamily, out info))
            {
                fontFamily = PdfFontNamesHelper.GetFontFamily(fontName);
            }
            else if (((font is PdfTrueTypeFont) || (font is PdfCIDType2Font)) && (normalizedFontFamily == "symbol"))
            {
                fontFamily = PdfFontNamesHelper.GetFontFamily(fontName);
            }
            else
            {
                foreach (string str5 in PdfFontNamesHelper.ExtractAdditionalStyles(normalizedFontStyle))
                {
                    if (info.AdditionalStyles.TryGetValue(str5, out fontFamily))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    fontFamily = info.SystemFontName;
                }
            }
            if ((!flag && PdfFontNamesHelper.ContainsBoldStyle(normalizedFontStyle)) || (!string.IsNullOrEmpty(normalizedFontStyle) && ((descriptor != null) && ((descriptor.Flags & PdfFontFlags.ForceBold) == PdfFontFlags.ForceBold))))
            {
                weight = 700;
            }
            return new PdfFontParameters(fontFamily, weight, !flag && PdfFontNamesHelper.ContainsItalicStyle(normalizedFontStyle));
        }

        public static string GetSystemFontFamily(string inputFontFamily)
        {
            PdfFontFamilyInfo info;
            return (!FontFamilyInfoes.TryGetValue(PdfFontNamesHelper.GetNormalizedFontFamily(inputFontFamily), out info) ? null : info.SystemFontName);
        }

        private static IDictionary<string, PdfFontFamilyInfo> FontFamilyInfoes
        {
            get
            {
                if (fontFamilyInfoes == null)
                {
                    fontFamilyInfoes = new Dictionary<string, PdfFontFamilyInfo>();
                    bool flag = false;
                    FontFamily[] families = FontFamily.Families;
                    int index = 0;
                    while (true)
                    {
                        PdfFontFamilyInfo info;
                        if (index >= families.Length)
                        {
                            AddFamilyIfNotExists(fontFamilyInfoes, "Courier", "Courier New");
                            AddFamilyIfNotExists(fontFamilyInfoes, "CourierNew", "Courier New");
                            AddFamilyIfNotExists(fontFamilyInfoes, "CourierNewPS", "Courier New");
                            AddFamilyIfNotExists(fontFamilyInfoes, "CourierNewPSMT", "Courier New");
                            AddFamilyIfNotExists(fontFamilyInfoes, "Times-Roman", "Times New Roman");
                            AddFamilyIfNotExists(fontFamilyInfoes, "Times", "Times New Roman");
                            AddFamilyIfNotExists(fontFamilyInfoes, "TimesNewRomanPS", "Times New Roman");
                            AddFamilyIfNotExists(fontFamilyInfoes, "TimesNewRomanPSMT", "Times New Roman");
                            AddFamilyIfNotExists(fontFamilyInfoes, "ArialMT", "Arial");
                            AddFamilyIfNotExists(fontFamilyInfoes, "TallPaul", "Gabriola");
                            AddFamilyIfNotExists(fontFamilyInfoes, "CenturyGothic", "Century Gothic");
                            AddFamilyIfNotExists(fontFamilyInfoes, "GothicText", "MS Gothic");
                            AddFamilyIfNotExists(fontFamilyInfoes, "Flama", "Tahoma");
                            AddFamilyIfNotExists(fontFamilyInfoes, "FlamaBook", "Tahoma");
                            AddFamilyIfNotExists(fontFamilyInfoes, "Helvetica", "Arial");
                            string str = PdfFontNamesHelper.Normalize("Symbol");
                            fontFamilyInfoes[str] = flag ? new PdfFontFamilyInfo("Segoe UI") : new PdfFontFamilyInfo("Arial Unicode MS");
                            AddFamilyIfNotExists(fontFamilyInfoes, "ZapfDingbats", "MS Gothic");
                            break;
                        }
                        FontFamily family = families[index];
                        string name = family.Name;
                        string normalizedFontFamily = PdfFontNamesHelper.GetNormalizedFontFamily(name);
                        string normalizedFontStyle = PdfFontNamesHelper.GetNormalizedFontStyle(name);
                        if (!fontFamilyInfoes.TryGetValue(normalizedFontFamily, out info))
                        {
                            info = new PdfFontFamilyInfo(name);
                            fontFamilyInfoes.Add(normalizedFontFamily, info);
                        }
                        if (!string.IsNullOrEmpty(normalizedFontStyle))
                        {
                            info.AddAdvancedStyle(normalizedFontStyle, name);
                        }
                        if (name.Contains("Segoe UI"))
                        {
                            flag = true;
                        }
                        index++;
                    }
                }
                return fontFamilyInfoes;
            }
        }

        private class PdfFontFamilyInfo
        {
            private readonly string systemFontName;
            private readonly IDictionary<string, string> advancedStyles = new Dictionary<string, string>();

            public PdfFontFamilyInfo(string systemFontName)
            {
                this.systemFontName = systemFontName;
            }

            public void AddAdvancedStyle(string style, string systemName)
            {
                this.advancedStyles[style] = systemName;
            }

            public string SystemFontName =>
                this.systemFontName;

            public IDictionary<string, string> AdditionalStyles =>
                this.advancedStyles;
        }
    }
}

