namespace DevExpress.Utils.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FontManager
    {
        public const string DefaultFontFamilyName = "Arial";
        private static string[] PredefinedFontFamilyNamesWin;
        private static string[] PredefinedFontFilesWin;
        private static Dictionary<string, FontDescriptorManager> customFontDescriptorCache;
        private static Dictionary<string, FontDescriptorManager> predefinedFontDescriptorCache;
        private static List<WeakReference> ElementsToNotify;

        static FontManager()
        {
            string[] textArray1 = new string[0x2d];
            textArray1[0] = "Arial";
            textArray1[1] = "Arial Black";
            textArray1[2] = "Comic Sans MS";
            textArray1[3] = "Courier New";
            textArray1[4] = "Georgia";
            textArray1[5] = "Lucida Sans Unicode";
            textArray1[6] = "Times New Roman";
            textArray1[7] = "Trebuchet MS";
            textArray1[8] = "Verdana";
            textArray1[9] = "MS Mincho";
            textArray1[10] = "MS PMincho";
            textArray1[11] = "SimSun";
            textArray1[12] = "SimSun-ExtB";
            textArray1[13] = "Calibri";
            textArray1[14] = "Book Antiqua";
            textArray1[15] = "Bookman Old Style";
            textArray1[0x10] = "Cambria";
            textArray1[0x11] = "Candara";
            textArray1[0x12] = "Century";
            textArray1[0x13] = "Century Gothic";
            textArray1[20] = "Century Schoolbook";
            textArray1[0x15] = "Consolas";
            textArray1[0x16] = "Cordia New";
            textArray1[0x17] = "Franklin Gothic Book";
            textArray1[0x18] = "Garamond";
            textArray1[0x19] = "Gill Sans MT";
            textArray1[0x1a] = "Impact";
            textArray1[0x1b] = "Lucida Console";
            textArray1[0x1c] = "Lucida Sans";
            textArray1[0x1d] = "MingLiU";
            textArray1[30] = "MingLiU_HKSCS";
            textArray1[0x1f] = "MS Gothic";
            textArray1[0x20] = "Palatino Linotype";
            textArray1[0x21] = "PMingLiU-ExtB";
            textArray1[0x22] = "PMingLiU";
            textArray1[0x23] = "Rockwell";
            textArray1[0x24] = "Segoe UI";
            textArray1[0x25] = "Shruti";
            textArray1[0x26] = "Simhei";
            textArray1[0x27] = "Sylfaen";
            textArray1[40] = "Tahoma";
            textArray1[0x29] = "Tunga";
            textArray1[0x2a] = "TW Cen MT";
            textArray1[0x2b] = "Vrinda";
            textArray1[0x2c] = "Angsana New";
            PredefinedFontFamilyNamesWin = textArray1;
            string[] textArray2 = new string[0x2d];
            textArray2[0] = "Arial";
            textArray2[1] = "ArialBlack";
            textArray2[2] = "ComicSansMS";
            textArray2[3] = "CourierNew";
            textArray2[4] = "Georgia";
            textArray2[5] = "LucidaSansUnicode";
            textArray2[6] = "Times";
            textArray2[7] = "TrebuchetMS";
            textArray2[8] = "Verdana";
            textArray2[9] = "MSMincho";
            textArray2[10] = "MSPMincho";
            textArray2[11] = "SimSun";
            textArray2[12] = "SimSunExtB";
            textArray2[13] = "Calibri";
            textArray2[14] = "BookAntiqua";
            textArray2[15] = "BookmanOldStyle";
            textArray2[0x10] = "Cambria";
            textArray2[0x11] = "Candara";
            textArray2[0x12] = "Century";
            textArray2[0x13] = "CenturyGothic";
            textArray2[20] = "CenturySchoolbook";
            textArray2[0x15] = "Consolas";
            textArray2[0x16] = "CordiaNew";
            textArray2[0x17] = "FranklinGothicBook";
            textArray2[0x18] = "Garamond";
            textArray2[0x19] = "GillSansMT";
            textArray2[0x1a] = "Impact";
            textArray2[0x1b] = "LucidaConsole";
            textArray2[0x1c] = "LucidaSans";
            textArray2[0x1d] = "MingLiU";
            textArray2[30] = "MingLiU_HKSCS";
            textArray2[0x1f] = "MSGothic";
            textArray2[0x20] = "PalatinoLinotype";
            textArray2[0x21] = "PMingLiU-ExtB";
            textArray2[0x22] = "PMingLiU";
            textArray2[0x23] = "Rockwell";
            textArray2[0x24] = "SegoeUI";
            textArray2[0x25] = "Shruti";
            textArray2[0x26] = "Simhei";
            textArray2[0x27] = "Sylfaen";
            textArray2[40] = "Tahoma";
            textArray2[0x29] = "Tunga";
            textArray2[0x2a] = "TWCenMT";
            textArray2[0x2b] = "Vrinda";
            textArray2[0x2c] = "AngsaNew";
            PredefinedFontFilesWin = textArray2;
            customFontDescriptorCache = new Dictionary<string, FontDescriptorManager>();
            predefinedFontDescriptorCache = new Dictionary<string, FontDescriptorManager>();
            ElementsToNotify = new List<WeakReference>();
            RegisterPredefinedFonts();
        }

        private static FontDescriptorManager CreateFontDescriptorManager(string familyName, string fileName)
        {
            try
            {
                return new FontDescriptorManager(familyName, GetPredefinedFontStream(fileName), GetPredefinedFontStream(fileName + "B"), GetPredefinedFontStream(fileName + "I"), GetPredefinedFontStream(fileName + "BI"), true);
            }
            catch
            {
                return null;
            }
        }

        public static FontDescriptor GetFontDescriptor(string familyName, bool bold, bool italic)
        {
            FontDescriptorManager manager;
            return (!customFontDescriptorCache.TryGetValue(familyName, out manager) ? (!predefinedFontDescriptorCache.TryGetValue(familyName, out manager) ? GetFontDescriptor("Arial", bold, italic) : manager.GetFontDescriptor(bold, italic)) : manager.GetFontDescriptor(bold, italic));
        }

        public static IEnumerable<string> GetFontFamilyNames()
        {
            List<string> list = new List<string>();
            list.AddRange(customFontDescriptorCache.Keys);
            foreach (string str in predefinedFontDescriptorCache.Keys)
            {
                if (!list.Contains(str))
                {
                    list.Add(str);
                }
            }
            list.Sort();
            return list;
        }

        internal static Stream GetPredefinedFontStream(string name) => 
            typeof(FontManager).Assembly.GetManifestResourceStream("DevExpress.Data.Utils.Drawing.FontManager.Fonts." + name + ".fmx");

        private static void RaiseFontFamilyNamesChanged()
        {
            foreach (WeakReference reference in ElementsToNotify)
            {
                (reference.Target as IFontBasedElement).OnFontsChanged();
            }
        }

        public static void RegisterFontBasedElement(IFontBasedElement element)
        {
            ElementsToNotify.Add(new WeakReference(element));
        }

        public static void RegisterFontFamily(string familyName, Stream normalFontStream)
        {
            RegisterFontFamily(familyName, normalFontStream, null, null, null);
        }

        public static void RegisterFontFamily(string familyName, Stream normalFontStream, Stream boldFontStream, Stream italicFontStream, Stream boldItalicFontStream)
        {
            if (!customFontDescriptorCache.ContainsKey(familyName))
            {
                customFontDescriptorCache.Add(familyName, new FontDescriptorManager(familyName, normalFontStream, boldFontStream, italicFontStream, boldItalicFontStream, false));
                RaiseFontFamilyNamesChanged();
            }
        }

        private static void RegisterPredefinedFonts()
        {
            string[] predefinedFontFamilyNamesWin = PredefinedFontFamilyNamesWin;
            string[] predefinedFontFilesWin = PredefinedFontFilesWin;
            for (int i = 0; i < predefinedFontFamilyNamesWin.Length; i++)
            {
                FontDescriptorManager manager = CreateFontDescriptorManager(predefinedFontFamilyNamesWin[i], predefinedFontFilesWin[i]);
                if (manager != null)
                {
                    predefinedFontDescriptorCache.Add(predefinedFontFamilyNamesWin[i], manager);
                }
            }
        }

        public static void UnregisterFontFamily(string familyName)
        {
            if (customFontDescriptorCache.ContainsKey(familyName))
            {
                customFontDescriptorCache.Remove(familyName);
                RaiseFontFamilyNamesChanged();
            }
        }
    }
}

