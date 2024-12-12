namespace DevExpress.Office.Internal
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;
    using System.Windows.Media;

    public static class FontManager
    {
        public static System.Windows.Media.FontFamily GetFontFamily(string fontFamilyName)
        {
            System.Windows.Media.FontFamily family2;
            using (IEnumerator<System.Windows.Media.FontFamily> enumerator = FontUtility.GetSystemFontFamilies().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        System.Windows.Media.FontFamily current = enumerator.Current;
                        if (!current.FamilyNames.Values.Contains(fontFamilyName))
                        {
                            continue;
                        }
                        family2 = current;
                    }
                    else
                    {
                        return new System.Windows.Media.FontFamily(fontFamilyName);
                    }
                    break;
                }
            }
            return family2;
        }

        public static string GetFontFamilyName(System.Windows.Media.FontFamily fontFamily)
        {
            XmlLanguage lang = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            return GetFontFamilyName(fontFamily, lang);
        }

        public static string GetFontFamilyName(System.Windows.Media.FontFamily fontFamily, XmlLanguage lang)
        {
            try
            {
                return (fontFamily.FamilyNames.ContainsKey(lang) ? fontFamily.FamilyNames[lang] : GetFontNameFromSource(fontFamily));
            }
            catch
            {
                return GetFontNameFromSource(fontFamily);
            }
        }

        public static IEnumerable<string> GetFontFamilyNames()
        {
            List<string> list = new List<string>();
            foreach (System.Windows.Media.FontFamily family in FontUtility.GetSystemFontFamilies())
            {
                list.Add(family.ToString());
            }
            return list;
        }

        public static string GetFontNameFromSource(System.Windows.Media.FontFamily fontFamily)
        {
            if (fontFamily != null)
            {
                if (fontFamily.Source != null)
                {
                    int index = fontFamily.Source.IndexOf('#');
                    return ((index != -1) ? fontFamily.Source.Substring(index + 1) : fontFamily.Source);
                }
                XmlLanguage key = XmlLanguage.GetLanguage("en-US");
                try
                {
                    string str;
                    if (fontFamily.FamilyNames.TryGetValue(key, out str))
                    {
                        return str;
                    }
                }
                catch (ArgumentException)
                {
                }
            }
            return string.Empty;
        }

        public static IEnumerable<string> GetFontNames()
        {
            List<string> list = new List<string>();
            foreach (System.Drawing.FontFamily family in new InstalledFontCollection().Families)
            {
                string name = family.Name;
                if (!string.IsNullOrEmpty(name))
                {
                    list.Add(name);
                }
            }
            return list;
        }

        public static IEnumerable<string> GetLocalizedFontNames()
        {
            Func<string, System.Windows.Media.FontFamily> selector = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<string, System.Windows.Media.FontFamily> local1 = <>c.<>9__6_0;
                selector = <>c.<>9__6_0 = x => new System.Windows.Media.FontFamily(x);
            }
            return GetFontNames().Select<string, System.Windows.Media.FontFamily>(selector).Select<System.Windows.Media.FontFamily, string>(new Func<System.Windows.Media.FontFamily, string>(FontManager.GetFontFamilyName));
        }

        public static bool IsSymbol(System.Windows.Media.FontFamily fontFamily)
        {
            bool symbol;
            using (IEnumerator<Typeface> enumerator = fontFamily.GetTypefaces().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Typeface current = enumerator.Current;
                        try
                        {
                            GlyphTypeface typeface2;
                            if (current.TryGetGlyphTypeface(out typeface2))
                            {
                                symbol = typeface2.Symbol;
                                break;
                            }
                        }
                        catch
                        {
                            symbol = false;
                            break;
                        }
                        continue;
                    }
                    return false;
                }
            }
            return symbol;
        }

        public static bool IsValid(System.Windows.Media.FontFamily fontFamily)
        {
            bool flag;
            using (IEnumerator<Typeface> enumerator = fontFamily.GetTypefaces().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Typeface current = enumerator.Current;
                        try
                        {
                            GlyphTypeface typeface2;
                            current.TryGetGlyphTypeface(out typeface2);
                        }
                        catch
                        {
                            flag = false;
                            break;
                        }
                        continue;
                    }
                    return true;
                }
            }
            return flag;
        }

        public static void RegisterFontBasedElement(IFontBasedElement element)
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontManager.<>c <>9 = new FontManager.<>c();
            public static Func<string, System.Windows.Media.FontFamily> <>9__6_0;

            internal System.Windows.Media.FontFamily <GetLocalizedFontNames>b__6_0(string x) => 
                new System.Windows.Media.FontFamily(x);
        }
    }
}

