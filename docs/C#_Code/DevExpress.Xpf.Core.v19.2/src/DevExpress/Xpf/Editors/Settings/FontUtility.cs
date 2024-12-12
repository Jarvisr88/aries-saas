namespace DevExpress.Xpf.Editors.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Media;

    public static class FontUtility
    {
        public static ReadOnlyCollection<FontFamily> GetSystemFontFamilies()
        {
            List<FontFamily> list = new List<FontFamily>();
            try
            {
                foreach (FontFamily family in Fonts.SystemFontFamilies)
                {
                    try
                    {
                        LanguageSpecificStringDictionary familyNames = family.FamilyNames;
                        list.Add(family);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
            catch
            {
            }
            return list.AsReadOnly();
        }
    }
}

