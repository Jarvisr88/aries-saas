namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Markup;

    [Obsolete("Use Theme instead")]
    public abstract class DefaultThemeInfo : MarkupExtension
    {
        public const string DeepBlue = "DeepBlue";
        public const string LightGray = "LightGray";
        public const string Office2007Blue = "Office2007Blue";
        public const string Office2007Black = "Office2007Black";
        public const string Office2007Silver = "Office2007Silver";
        public const string DeepBlueFullName = "Deep Blue";
        public const string LightGrayFullName = "Light Gray";
        public const string Office2007BlueFullName = "Office 2007 Blue";
        public const string Office2007BlackFullName = "Office 2007 Black";
        public const string Office2007SilverFullName = "Office 2007 Silver";
        private static string currentThemeName = string.Empty;
        public static List<string> Themes;

        static DefaultThemeInfo()
        {
            string[] collection = new string[] { DefaultThemeName, "Office2007Blue", "Office2007Black", "Office2007Silver", "LightGray" };
            Themes = new List<string>(collection);
        }

        protected DefaultThemeInfo()
        {
        }

        public static bool IsDefaultTheme(string themeName) => 
            string.IsNullOrEmpty(themeName) || (themeName == DefaultThemeName);

        public static string DefaultThemeName =>
            "DeepBlue";

        public static string DefaultThemeFullName =>
            "Deep Blue";
    }
}

