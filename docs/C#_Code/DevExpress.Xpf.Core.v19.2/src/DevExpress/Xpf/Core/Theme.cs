namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    [TypeConverter(typeof(ThemeTypeConverter))]
    public class Theme
    {
        private string name;
        private string fullName;
        private string _paletteName;
        internal const string AzureName = "Azure";
        private static readonly Dictionary<string, Theme> ThemeNameHash = new Dictionary<string, Theme>();
        internal static List<Theme> ThemesInternal = new List<Theme>();
        public bool ShowInThemeSelector;
        public const string StandardCategory = "Standard Themes";
        public const string Office2007Category = "Office 2007 Themes";
        public const string Office2010Category = "Office 2010 Themes";
        public const string Office2013Category = "Office 2013 Themes";
        public const string Office2013TouchCategory = "Office 2013 Touch Themes";
        public const string Office2016Category = "Office 2016 Themes";
        public const string Office2016TouchCategory = "Office 2016 Touch Themes";
        public const string Office2016SECategory = "Office 2016 Special Edition Themes";
        public const string Office2016SETouchCategory = "Office 2016 Special Edition Touch Themes";
        public const string Office2019Category = "Office 2019 Themes";
        public const string Office2019TouchCategory = "Office 2019 Touch Themes";
        public const string MetropolisCategory = "Metropolis Themes";
        public const string VisualStudioCategory = "Visual Studio Themes";
        public const string DeepBlueName = "DeepBlue";
        public const string LightGrayName = "LightGray";
        public const string Office2007BlueName = "Office2007Blue";
        public const string Office2007BlackName = "Office2007Black";
        public const string Office2007SilverName = "Office2007Silver";
        public const string SevenName = "Seven";
        public const string VS2010Name = "VS2010";
        public const string DXStyleName = "DXStyle";
        public const string Office2010BlackName = "Office2010Black";
        public const string Office2010BlueName = "Office2010Blue";
        public const string Office2010SilverName = "Office2010Silver";
        public const string MetropolisDarkName = "MetropolisDark";
        public const string TouchlineDarkName = "TouchlineDark";
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public const string HybridAppName = "HybridApp";
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public const string BaseName = "Base";
        public const string VS2017LightName = "VS2017Light";
        public const string VS2017DarkName = "VS2017Dark";
        public const string VS2017BlueName = "VS2017Blue";
        public const string MetropolisLightName = "MetropolisLight";
        public const string Office2013Name = "Office2013";
        public const string Office2013LightGrayName = "Office2013LightGray";
        public const string Office2013DarkGrayName = "Office2013DarkGray";
        public const string Office2013TouchName = "Office2013;Touch";
        public const string Office2013LightGrayTouchName = "Office2013LightGray;Touch";
        public const string Office2013DarkGrayTouchName = "Office2013DarkGray;Touch";
        public const string Office2016WhiteName = "Office2016White";
        public const string Office2016BlackName = "Office2016Black";
        public const string Office2016ColorfulName = "Office2016Colorful";
        public const string Office2016WhiteTouchName = "Office2016White;Touch";
        public const string Office2016BlackTouchName = "Office2016Black;Touch";
        public const string Office2016ColorfulTouchName = "Office2016Colorful;Touch";
        public const string Office2016WhiteSEName = "Office2016WhiteSE";
        public const string Office2016DarkGraySEName = "Office2016DarkGraySE";
        public const string Office2016ColorfulSEName = "Office2016ColorfulSE";
        public const string Office2016BlackSEName = "Office2016BlackSE";
        public const string Office2016WhiteSETouchName = "Office2016WhiteSE;Touch";
        public const string Office2016DarkGraySETouchName = "Office2016DarkGraySE;Touch";
        public const string Office2016ColorfulSETouchName = "Office2016ColorfulSE;Touch";
        public const string Office2016BlackSETouchName = "Office2016BlackSE;Touch";
        public const string Office2019WhiteName = "Office2019White";
        public const string Office2019DarkGrayName = "Office2019DarkGray";
        public const string Office2019ColorfulName = "Office2019Colorful";
        public const string Office2019BlackName = "Office2019Black";
        public const string Office2019HighContrastName = "Office2019HighContrast";
        public const string Office2019WhiteTouchName = "Office2019White;Touch";
        public const string Office2019DarkGrayTouchName = "Office2019DarkGray;Touch";
        public const string Office2019ColorfulTouchName = "Office2019Colorful;Touch";
        public const string Office2019BlackTouchName = "Office2019Black;Touch";
        public const string Office2019HighContrastTouchName = "Office2019HighContrast;Touch";
        public const string DeepBlueFullName = "Deep Blue";
        public const string LightGrayFullName = "Light Gray";
        public const string Office2007BlueFullName = "Office 2007 Blue";
        public const string Office2007BlackFullName = "Office 2007 Black";
        public const string Office2007SilverFullName = "Office 2007 Silver";
        public const string SevenFullName = "Seven";
        public const string VS2010FullName = "Visual Studio 2010";
        public const string DXStyleFullName = "DevExpress Style";
        public const string Office2010BlackFullName = "Office 2010 Black";
        public const string Office2010BlueFullName = "Office 2010 Blue";
        public const string Office2010SilverFullName = "Office 2010 Silver";
        public const string MetropolisDarkFullName = "Metropolis Dark";
        public const string TouchlineDarkFullName = "Touchline Dark";
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public const string HybridAppFullName = "HybridApp";
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public const string BaseFullName = "Base";
        public const string VS2017LightFullName = "Visual Studio 2017 Light";
        public const string VS2017DarkFullName = "Visual Studio 2017 Dark";
        public const string VS2017BlueFullName = "Visual Studio 2017 Blue";
        public const string MetropolisLightFullName = "Metropolis Light";
        public const string Office2013FullName = "Office 2013";
        public const string Office2013LightGrayFullName = "Office 2013 Light Gray";
        public const string Office2013DarkGrayFullName = "Office 2013 Dark Gray";
        public const string Office2013TouchFullName = "Office 2013 Touch";
        public const string Office2013LightGrayTouchFullName = "Office 2013 Light Gray Touch";
        public const string Office2013DarkGrayTouchFullName = "Office 2013 Dark Gray Touch";
        public const string Office2016WhiteFullName = "Office 2016 White";
        public const string Office2016BlackFullName = "Office 2016 Black";
        public const string Office2016ColorfulFullName = "Office 2016 Colorful";
        public const string Office2016WhiteTouchFullName = "Office 2016 White Touch";
        public const string Office2016BlackTouchFullName = "Office 2016 Black Touch";
        public const string Office2016ColorfulTouchFullName = "Office 2016 Colorful Touch";
        public const string Office2016WhiteSEFullName = "Office 2016 White SE";
        public const string Office2016DarkGraySEFullName = "Office 2016 Dark Gray SE";
        public const string Office2016BlackSEFullName = "Office 2016 Black SE";
        public const string Office2016ColorfulSEFullName = "Office 2016 Colorful SE";
        public const string Office2016WhiteSETouchFullName = "Office 2016 White SE Touch";
        public const string Office2016DarkGraySETouchFullName = "Office 2016 Dark Gray SE Touch";
        public const string Office2016ColorfulSETouchFullName = "Office 2016 Colorful SE Touch";
        public const string Office2016BlackSETouchFullName = "Office 2016 Black SE Touch";
        public const string Office2019WhiteFullName = "Office 2019 White";
        public const string Office2019DarkGrayFullName = "Office 2019 Dark Gray";
        public const string Office2019BlackFullName = "Office 2019 Black";
        public const string Office2019ColorfulFullName = "Office 2019 Colorful";
        public const string Office2019HighContrastFullName = "Office 2019 High Contrast";
        public const string Office2019WhiteTouchFullName = "Office 2019 White Touch";
        public const string Office2019DarkGrayTouchFullName = "Office 2019 Dark Gray Touch";
        public const string Office2019ColorfulTouchFullName = "Office 2019 Colorful Touch";
        public const string Office2019BlackTouchFullName = "Office 2019 Black Touch";
        public const string Office2019HighContrastTouchFullName = "Office 2019 High Contrast Touch";
        public static Theme LightGray;
        public static Theme DeepBlue;
        public static Theme Office2007Blue;
        public static Theme Office2007Black;
        public static Theme Office2007Silver;
        public static Theme Seven;
        public static Theme VS2010;
        public static Theme DXStyle;
        public static Theme Office2010Black;
        public static Theme Office2010Blue;
        public static Theme Office2010Silver;
        public static Theme MetropolisDark;
        public static Theme TouchlineDark;
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static Theme HybridApp;
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static Theme Base;
        public static Theme VS2017Light;
        public static Theme VS2017Dark;
        public static Theme VS2017Blue;
        public static Theme MetropolisLight;
        private static bool showTouchThemes;
        public static Theme Office2013;
        public static Theme Office2013LightGray;
        public static Theme Office2013DarkGray;
        public static Theme Office2013Touch;
        public static Theme Office2013LightGrayTouch;
        public static Theme Office2013DarkGrayTouch;
        public static Theme Office2016White;
        public static Theme Office2016Black;
        public static Theme Office2016Colorful;
        public static Theme Office2016WhiteTouch;
        public static Theme Office2016BlackTouch;
        public static Theme Office2016ColorfulTouch;
        public static Theme Office2016WhiteSE;
        public static Theme Office2016DarkGraySE;
        public static Theme Office2016BlackSE;
        public static Theme Office2016ColorfulSE;
        public static Theme Office2016WhiteSETouch;
        public static Theme Office2016DarkGraySETouch;
        public static Theme Office2016ColorfulSETouch;
        public static Theme Office2016BlackSETouch;
        public static Theme Office2019White;
        public static Theme Office2019DarkGray;
        public static Theme Office2019Black;
        public static Theme Office2019Colorful;
        public static Theme Office2019HighContrast;
        public static Theme Office2019WhiteTouch;
        public static Theme Office2019DarkGrayTouch;
        public static Theme Office2019ColorfulTouch;
        public static Theme Office2019HighContrastTouch;
        public static Theme Office2019BlackTouch;
        public const string NoneName = "None";
        internal const string DefaultName = "Default";
        public static Theme Default;
        protected static Tuple<string, string>[] ThemeGuid;
        private System.Reflection.Assembly assembly;
        private string publicKeyToken;
        private string version;
        private string assemblyName;
        private Action initializeAssemblyFromStream;

        static Theme()
        {
            Theme theme1 = new Theme("LightGray");
            theme1.IsStandard = true;
            theme1.FullName = "Light Gray";
            theme1.Category = "Standard Themes";
            theme1.SmallGlyph = GetThemeGlyphUri("LightGray", false);
            theme1.LargeGlyph = GetThemeGlyphUri("LightGray", true);
            theme1.SvgGlyph = GetThemeSvgGlyphUri("LightGray");
            LightGray = theme1;
            Theme theme2 = new Theme("DeepBlue");
            theme2.IsStandard = true;
            theme2.FullName = "Deep Blue";
            theme2.Category = "Standard Themes";
            theme2.SmallGlyph = GetThemeGlyphUri("DeepBlue", false);
            theme2.LargeGlyph = GetThemeGlyphUri("DeepBlue", true);
            theme2.SvgGlyph = GetThemeSvgGlyphUri("DeepBlue");
            DeepBlue = theme2;
            Theme theme3 = new Theme("Office2007Blue");
            theme3.IsStandard = true;
            theme3.FullName = "Office 2007 Blue";
            theme3.Category = "Office 2007 Themes";
            theme3.SmallGlyph = GetThemeGlyphUri("Office2007Blue", false);
            theme3.LargeGlyph = GetThemeGlyphUri("Office2007Blue", true);
            theme3.SvgGlyph = GetThemeSvgGlyphUri("Office2007Blue");
            Office2007Blue = theme3;
            Theme theme4 = new Theme("Office2007Black");
            theme4.IsStandard = true;
            theme4.FullName = "Office 2007 Black";
            theme4.Category = "Office 2007 Themes";
            theme4.SmallGlyph = GetThemeGlyphUri("Office2007Black", false);
            theme4.LargeGlyph = GetThemeGlyphUri("Office2007Black", true);
            theme4.SvgGlyph = GetThemeSvgGlyphUri("Office2007Black");
            Office2007Black = theme4;
            Theme theme5 = new Theme("Office2007Silver");
            theme5.IsStandard = true;
            theme5.FullName = "Office 2007 Silver";
            theme5.Category = "Office 2007 Themes";
            theme5.SmallGlyph = GetThemeGlyphUri("Office2007Silver", false);
            theme5.LargeGlyph = GetThemeGlyphUri("Office2007Silver", true);
            theme5.SvgGlyph = GetThemeSvgGlyphUri("Office2007Silver");
            Office2007Silver = theme5;
            Theme theme6 = new Theme("Seven");
            theme6.IsStandard = true;
            theme6.FullName = "Seven";
            theme6.Category = "Standard Themes";
            theme6.SmallGlyph = GetThemeGlyphUri("Seven", false);
            theme6.LargeGlyph = GetThemeGlyphUri("Seven", true);
            theme6.SvgGlyph = GetThemeSvgGlyphUri("Seven");
            Seven = theme6;
            Theme theme7 = new Theme("VS2010");
            theme7.IsStandard = true;
            theme7.FullName = "Visual Studio 2010";
            theme7.Category = "Standard Themes";
            theme7.SmallGlyph = GetThemeGlyphUri("VS2010", false);
            theme7.LargeGlyph = GetThemeGlyphUri("VS2010", true);
            theme7.SvgGlyph = GetThemeSvgGlyphUri("VS2010");
            VS2010 = theme7;
            Theme theme8 = new Theme("DXStyle");
            theme8.IsStandard = true;
            theme8.FullName = "DevExpress Style";
            theme8.Category = "Standard Themes";
            theme8.SmallGlyph = GetThemeGlyphUri("DXStyle", false);
            theme8.LargeGlyph = GetThemeGlyphUri("DXStyle", true);
            theme8.SvgGlyph = GetThemeSvgGlyphUri("DXStyle");
            DXStyle = theme8;
            Theme theme9 = new Theme("Office2010Black");
            theme9.IsStandard = true;
            theme9.FullName = "Office 2010 Black";
            theme9.Category = "Office 2010 Themes";
            theme9.SmallGlyph = GetThemeGlyphUri("Office2010Black", false);
            theme9.LargeGlyph = GetThemeGlyphUri("Office2010Black", true);
            theme9.SvgGlyph = GetThemeSvgGlyphUri("Office2010Black");
            Office2010Black = theme9;
            Theme theme10 = new Theme("Office2010Blue");
            theme10.IsStandard = true;
            theme10.FullName = "Office 2010 Blue";
            theme10.Category = "Office 2010 Themes";
            theme10.SmallGlyph = GetThemeGlyphUri("Office2010Blue", false);
            theme10.LargeGlyph = GetThemeGlyphUri("Office2010Blue", true);
            theme10.SvgGlyph = GetThemeSvgGlyphUri("Office2010Blue");
            Office2010Blue = theme10;
            Theme theme11 = new Theme("Office2010Silver");
            theme11.IsStandard = true;
            theme11.FullName = "Office 2010 Silver";
            theme11.Category = "Office 2010 Themes";
            theme11.SmallGlyph = GetThemeGlyphUri("Office2010Silver", false);
            theme11.LargeGlyph = GetThemeGlyphUri("Office2010Silver", true);
            theme11.SvgGlyph = GetThemeSvgGlyphUri("Office2010Silver");
            Office2010Silver = theme11;
            Theme theme12 = new Theme("MetropolisDark");
            theme12.IsStandard = true;
            theme12.FullName = "Metropolis Dark";
            theme12.Category = "Metropolis Themes";
            theme12.SmallGlyph = GetThemeGlyphUri("MetropolisDark", false);
            theme12.LargeGlyph = GetThemeGlyphUri("MetropolisDark", true);
            theme12.SvgGlyph = GetThemeSvgGlyphUri("MetropolisDark");
            MetropolisDark = theme12;
            Theme theme13 = new Theme("TouchlineDark");
            theme13.IsStandard = true;
            theme13.FullName = "Touchline Dark";
            theme13.Category = "Metropolis Themes";
            theme13.SmallGlyph = GetThemeGlyphUri("TouchlineDark", false);
            theme13.LargeGlyph = GetThemeGlyphUri("TouchlineDark", true);
            theme13.SvgGlyph = GetThemeSvgGlyphUri("TouchlineDark");
            TouchlineDark = theme13;
            Theme theme14 = new Theme("HybridApp");
            theme14.IsStandard = true;
            theme14.FullName = "HybridApp";
            theme14.Category = "Standard Themes";
            theme14.SmallGlyph = GetThemeGlyphUri("Office2013", false);
            theme14.LargeGlyph = GetThemeGlyphUri("Office2013", true);
            theme14.SvgGlyph = GetThemeSvgGlyphUri("Office2013");
            theme14.ShowInThemeSelector = false;
            HybridApp = theme14;
            Theme theme15 = new Theme("Base");
            theme15.IsStandard = true;
            theme15.FullName = "Base";
            theme15.Category = "Standard Themes";
            theme15.SmallGlyph = GetThemeGlyphUri("Base", false);
            theme15.LargeGlyph = GetThemeGlyphUri("Base", true);
            theme15.SvgGlyph = GetThemeSvgGlyphUri("Base");
            Base = theme15;
            Theme theme16 = new Theme("VS2017Light");
            theme16.IsStandard = true;
            theme16.FullName = "Visual Studio 2017 Light";
            theme16.Category = "Visual Studio Themes";
            theme16.SmallGlyph = GetThemeGlyphUri("VS2017Light", false);
            theme16.LargeGlyph = GetThemeGlyphUri("VS2017Light", true);
            theme16.SvgGlyph = GetThemeSvgGlyphUri("VS2017Light");
            VS2017Light = theme16;
            Theme theme17 = new Theme("VS2017Dark");
            theme17.IsStandard = true;
            theme17.FullName = "Visual Studio 2017 Dark";
            theme17.Category = "Visual Studio Themes";
            theme17.SmallGlyph = GetThemeGlyphUri("VS2017Dark", false);
            theme17.LargeGlyph = GetThemeGlyphUri("VS2017Dark", true);
            theme17.SvgGlyph = GetThemeSvgGlyphUri("VS2017Dark");
            VS2017Dark = theme17;
            Theme theme18 = new Theme("VS2017Blue");
            theme18.IsStandard = true;
            theme18.FullName = "Visual Studio 2017 Blue";
            theme18.Category = "Visual Studio Themes";
            theme18.SmallGlyph = GetThemeGlyphUri("VS2017Blue", false);
            theme18.LargeGlyph = GetThemeGlyphUri("VS2017Blue", true);
            theme18.SvgGlyph = GetThemeSvgGlyphUri("VS2017Blue");
            VS2017Blue = theme18;
            Theme theme19 = new Theme("MetropolisLight");
            theme19.IsStandard = true;
            theme19.FullName = "Metropolis Light";
            theme19.Category = "Metropolis Themes";
            theme19.SmallGlyph = GetThemeGlyphUri("MetropolisLight", false);
            theme19.LargeGlyph = GetThemeGlyphUri("MetropolisLight", true);
            theme19.SvgGlyph = GetThemeSvgGlyphUri("MetropolisLight");
            MetropolisLight = theme19;
            showTouchThemes = true;
            Theme theme20 = new Theme("Office2013");
            theme20.IsStandard = true;
            theme20.FullName = "Office 2013";
            theme20.Category = "Office 2013 Themes";
            theme20.SmallGlyph = GetThemeGlyphUri("Office2013", false);
            theme20.LargeGlyph = GetThemeGlyphUri("Office2013", true);
            theme20.SvgGlyph = GetThemeSvgGlyphUri("Office2013");
            Office2013 = theme20;
            Theme theme21 = new Theme("Office2013LightGray");
            theme21.IsStandard = true;
            theme21.FullName = "Office 2013 Light Gray";
            theme21.Category = "Office 2013 Themes";
            theme21.SmallGlyph = GetThemeGlyphUri("Office2013LightGray", false);
            theme21.LargeGlyph = GetThemeGlyphUri("Office2013LightGray", true);
            theme21.SvgGlyph = GetThemeSvgGlyphUri("Office2013LightGray");
            Office2013LightGray = theme21;
            Theme theme22 = new Theme("Office2013DarkGray");
            theme22.IsStandard = true;
            theme22.FullName = "Office 2013 Dark Gray";
            theme22.Category = "Office 2013 Themes";
            theme22.SmallGlyph = GetThemeGlyphUri("Office2013DarkGray", false);
            theme22.LargeGlyph = GetThemeGlyphUri("Office2013DarkGray", true);
            theme22.SvgGlyph = GetThemeSvgGlyphUri("Office2013DarkGray");
            Office2013DarkGray = theme22;
            Theme theme23 = new Theme("Office2013;Touch");
            theme23.IsStandard = true;
            theme23.FullName = "Office 2013 Touch";
            theme23.Category = "Office 2013 Touch Themes";
            theme23.SmallGlyph = GetThemeGlyphUri("Office2013", false);
            theme23.LargeGlyph = GetThemeGlyphUri("Office2013", true);
            theme23.SvgGlyph = GetThemeSvgGlyphUri("Office2013");
            theme23.ShowInThemeSelector = showTouchThemes;
            Office2013Touch = theme23;
            Theme theme24 = new Theme("Office2013LightGray;Touch");
            theme24.IsStandard = true;
            theme24.FullName = "Office 2013 Light Gray Touch";
            theme24.Category = "Office 2013 Touch Themes";
            theme24.SmallGlyph = GetThemeGlyphUri("Office2013LightGray", false);
            theme24.LargeGlyph = GetThemeGlyphUri("Office2013LightGray", true);
            theme24.SvgGlyph = GetThemeSvgGlyphUri("Office2013LightGray");
            theme24.ShowInThemeSelector = showTouchThemes;
            Office2013LightGrayTouch = theme24;
            Theme theme25 = new Theme("Office2013DarkGray;Touch");
            theme25.IsStandard = true;
            theme25.FullName = "Office 2013 Dark Gray Touch";
            theme25.Category = "Office 2013 Touch Themes";
            theme25.SmallGlyph = GetThemeGlyphUri("Office2013DarkGray", false);
            theme25.LargeGlyph = GetThemeGlyphUri("Office2013DarkGray", true);
            theme25.SvgGlyph = GetThemeSvgGlyphUri("Office2013DarkGray");
            theme25.ShowInThemeSelector = showTouchThemes;
            Office2013DarkGrayTouch = theme25;
            Theme theme26 = new Theme("Office2016White");
            theme26.IsStandard = true;
            theme26.FullName = "Office 2016 White";
            theme26.Category = "Office 2016 Themes";
            theme26.SmallGlyph = GetThemeGlyphUri("Office2016White", false);
            theme26.LargeGlyph = GetThemeGlyphUri("Office2016White", true);
            theme26.SvgGlyph = GetThemeSvgGlyphUri("Office2016White");
            Office2016White = theme26;
            Theme theme27 = new Theme("Office2016Black");
            theme27.IsStandard = true;
            theme27.FullName = "Office 2016 Black";
            theme27.Category = "Office 2016 Themes";
            theme27.SmallGlyph = GetThemeGlyphUri("Office2016Black", false);
            theme27.LargeGlyph = GetThemeGlyphUri("Office2016Black", true);
            theme27.SvgGlyph = GetThemeSvgGlyphUri("Office2016Black");
            Office2016Black = theme27;
            Theme theme28 = new Theme("Office2016Colorful");
            theme28.IsStandard = true;
            theme28.FullName = "Office 2016 Colorful";
            theme28.Category = "Office 2016 Themes";
            theme28.SmallGlyph = GetThemeGlyphUri("Office2016Colorful", false);
            theme28.LargeGlyph = GetThemeGlyphUri("Office2016Colorful", true);
            theme28.SvgGlyph = GetThemeSvgGlyphUri("Office2016Colorful");
            Office2016Colorful = theme28;
            Theme theme29 = new Theme("Office2016White;Touch");
            theme29.IsStandard = true;
            theme29.FullName = "Office 2016 White Touch";
            theme29.Category = "Office 2016 Touch Themes";
            theme29.SmallGlyph = GetThemeGlyphUri("Office2016White", false);
            theme29.LargeGlyph = GetThemeGlyphUri("Office2016White", true);
            theme29.SvgGlyph = GetThemeSvgGlyphUri("Office2016White");
            theme29.ShowInThemeSelector = showTouchThemes;
            Office2016WhiteTouch = theme29;
            Theme theme30 = new Theme("Office2016Black;Touch");
            theme30.IsStandard = true;
            theme30.FullName = "Office 2016 Black Touch";
            theme30.Category = "Office 2016 Touch Themes";
            theme30.SmallGlyph = GetThemeGlyphUri("Office2016Black", false);
            theme30.LargeGlyph = GetThemeGlyphUri("Office2016Black", true);
            theme30.SvgGlyph = GetThemeSvgGlyphUri("Office2016Black");
            theme30.ShowInThemeSelector = showTouchThemes;
            Office2016BlackTouch = theme30;
            Theme theme31 = new Theme("Office2016Colorful;Touch");
            theme31.IsStandard = true;
            theme31.FullName = "Office 2016 Colorful Touch";
            theme31.Category = "Office 2016 Touch Themes";
            theme31.SmallGlyph = GetThemeGlyphUri("Office2016Colorful", false);
            theme31.LargeGlyph = GetThemeGlyphUri("Office2016Colorful", true);
            theme31.SvgGlyph = GetThemeSvgGlyphUri("Office2016Colorful");
            theme31.ShowInThemeSelector = showTouchThemes;
            Office2016ColorfulTouch = theme31;
            Theme theme32 = new Theme("Office2016WhiteSE");
            theme32.IsStandard = true;
            theme32.FullName = "Office 2016 White SE";
            theme32.Category = "Office 2016 Special Edition Themes";
            theme32.SmallGlyph = GetThemeGlyphUri("Office2016WhiteSE", false);
            theme32.LargeGlyph = GetThemeGlyphUri("Office2016WhiteSE", true);
            theme32.SvgGlyph = GetThemeSvgGlyphUri("Office2016WhiteSE");
            Office2016WhiteSE = theme32;
            Theme theme33 = new Theme("Office2016DarkGraySE");
            theme33.IsStandard = true;
            theme33.FullName = "Office 2016 Dark Gray SE";
            theme33.Category = "Office 2016 Special Edition Themes";
            theme33.SmallGlyph = GetThemeGlyphUri("Office2016DarkGraySE", false);
            theme33.LargeGlyph = GetThemeGlyphUri("Office2016DarkGraySE", true);
            theme33.SvgGlyph = GetThemeSvgGlyphUri("Office2016DarkGraySE");
            Office2016DarkGraySE = theme33;
            Theme theme34 = new Theme("Office2016BlackSE");
            theme34.IsStandard = true;
            theme34.FullName = "Office 2016 Black SE";
            theme34.Category = "Office 2016 Special Edition Themes";
            theme34.SmallGlyph = GetThemeGlyphUri("Office2016BlackSE", false);
            theme34.LargeGlyph = GetThemeGlyphUri("Office2016BlackSE", true);
            theme34.SvgGlyph = GetThemeSvgGlyphUri("Office2016BlackSE");
            Office2016BlackSE = theme34;
            Theme theme35 = new Theme("Office2016ColorfulSE");
            theme35.IsStandard = true;
            theme35.FullName = "Office 2016 Colorful SE";
            theme35.Category = "Office 2016 Special Edition Themes";
            theme35.SmallGlyph = GetThemeGlyphUri("Office2016ColorfulSE", false);
            theme35.LargeGlyph = GetThemeGlyphUri("Office2016ColorfulSE", true);
            theme35.SvgGlyph = GetThemeSvgGlyphUri("Office2016ColorfulSE");
            Office2016ColorfulSE = theme35;
            Theme theme36 = new Theme("Office2016WhiteSE;Touch");
            theme36.IsStandard = true;
            theme36.FullName = "Office 2016 White SE Touch";
            theme36.Category = "Office 2016 Special Edition Touch Themes";
            theme36.SmallGlyph = GetThemeGlyphUri("Office2016WhiteSE", false);
            theme36.LargeGlyph = GetThemeGlyphUri("Office2016WhiteSE", true);
            theme36.SvgGlyph = GetThemeSvgGlyphUri("Office2016WhiteSE");
            theme36.ShowInThemeSelector = showTouchThemes;
            Office2016WhiteSETouch = theme36;
            Theme theme37 = new Theme("Office2016DarkGraySE;Touch");
            theme37.IsStandard = true;
            theme37.FullName = "Office 2016 Dark Gray SE Touch";
            theme37.Category = "Office 2016 Special Edition Touch Themes";
            theme37.SmallGlyph = GetThemeGlyphUri("Office2016DarkGraySE", false);
            theme37.LargeGlyph = GetThemeGlyphUri("Office2016DarkGraySE", true);
            theme37.SvgGlyph = GetThemeSvgGlyphUri("Office2016DarkGraySE");
            theme37.ShowInThemeSelector = showTouchThemes;
            Office2016DarkGraySETouch = theme37;
            Theme theme38 = new Theme("Office2016ColorfulSE;Touch");
            theme38.IsStandard = true;
            theme38.FullName = "Office 2016 Colorful SE Touch";
            theme38.Category = "Office 2016 Special Edition Touch Themes";
            theme38.SmallGlyph = GetThemeGlyphUri("Office2016ColorfulSE", false);
            theme38.LargeGlyph = GetThemeGlyphUri("Office2016ColorfulSE", true);
            theme38.SvgGlyph = GetThemeSvgGlyphUri("Office2016ColorfulSE");
            theme38.ShowInThemeSelector = showTouchThemes;
            Office2016ColorfulSETouch = theme38;
            Theme theme39 = new Theme("Office2016BlackSE;Touch");
            theme39.IsStandard = true;
            theme39.FullName = "Office 2016 Black SE Touch";
            theme39.Category = "Office 2016 Special Edition Touch Themes";
            theme39.SmallGlyph = GetThemeGlyphUri("Office2016BlackSE", false);
            theme39.LargeGlyph = GetThemeGlyphUri("Office2016BlackSE", true);
            theme39.SvgGlyph = GetThemeSvgGlyphUri("Office2016BlackSE");
            Office2016BlackSETouch = theme39;
            Theme theme40 = new Theme("Office2019White");
            theme40.IsStandard = true;
            theme40.FullName = "Office 2019 White";
            theme40.Category = "Office 2019 Themes";
            theme40.SmallGlyph = GetThemeGlyphUri("Office2019White", false);
            theme40.LargeGlyph = GetThemeGlyphUri("Office2019White", true);
            theme40.SvgGlyph = GetThemeSvgGlyphUri("Office2019White");
            Office2019White = theme40;
            Theme theme41 = new Theme("Office2019DarkGray");
            theme41.IsStandard = true;
            theme41.FullName = "Office 2019 Dark Gray";
            theme41.Category = "Office 2019 Themes";
            theme41.SmallGlyph = GetThemeGlyphUri("Office2019DarkGray", false);
            theme41.LargeGlyph = GetThemeGlyphUri("Office2019DarkGray", true);
            theme41.SvgGlyph = GetThemeSvgGlyphUri("Office2019DarkGray");
            Office2019DarkGray = theme41;
            Theme theme42 = new Theme("Office2019Black");
            theme42.IsStandard = true;
            theme42.FullName = "Office 2019 Black";
            theme42.Category = "Office 2019 Themes";
            theme42.SmallGlyph = GetThemeGlyphUri("Office2019Black", false);
            theme42.LargeGlyph = GetThemeGlyphUri("Office2019Black", true);
            theme42.SvgGlyph = GetThemeSvgGlyphUri("Office2019Black");
            Office2019Black = theme42;
            Theme theme43 = new Theme("Office2019Colorful");
            theme43.IsStandard = true;
            theme43.FullName = "Office 2019 Colorful";
            theme43.Category = "Office 2019 Themes";
            theme43.SmallGlyph = GetThemeGlyphUri("Office2019Colorful", false);
            theme43.LargeGlyph = GetThemeGlyphUri("Office2019Colorful", true);
            theme43.SvgGlyph = GetThemeSvgGlyphUri("Office2019Colorful");
            Office2019Colorful = theme43;
            Theme theme44 = new Theme("Office2019HighContrast");
            theme44.IsStandard = true;
            theme44.FullName = "Office 2019 High Contrast";
            theme44.Category = "Office 2019 Themes";
            theme44.SmallGlyph = GetThemeGlyphUri("Office2019HighContrast", false);
            theme44.LargeGlyph = GetThemeGlyphUri("Office2019HighContrast", true);
            theme44.SvgGlyph = GetThemeSvgGlyphUri("Office2019HighContrast");
            Office2019HighContrast = theme44;
            Theme theme45 = new Theme("Office2019White;Touch");
            theme45.IsStandard = true;
            theme45.FullName = "Office 2019 White Touch";
            theme45.Category = "Office 2019 Touch Themes";
            theme45.SmallGlyph = GetThemeGlyphUri("Office2019White", false);
            theme45.LargeGlyph = GetThemeGlyphUri("Office2019White", true);
            theme45.SvgGlyph = GetThemeSvgGlyphUri("Office2019White");
            theme45.ShowInThemeSelector = showTouchThemes;
            Office2019WhiteTouch = theme45;
            Theme theme46 = new Theme("Office2019DarkGray;Touch");
            theme46.IsStandard = true;
            theme46.FullName = "Office 2019 Dark Gray Touch";
            theme46.Category = "Office 2019 Touch Themes";
            theme46.SmallGlyph = GetThemeGlyphUri("Office2019DarkGray", false);
            theme46.LargeGlyph = GetThemeGlyphUri("Office2019DarkGray", true);
            theme46.SvgGlyph = GetThemeSvgGlyphUri("Office2019DarkGray");
            theme46.ShowInThemeSelector = showTouchThemes;
            Office2019DarkGrayTouch = theme46;
            Theme theme47 = new Theme("Office2019Colorful;Touch");
            theme47.IsStandard = true;
            theme47.FullName = "Office 2019 Colorful Touch";
            theme47.Category = "Office 2019 Touch Themes";
            theme47.SmallGlyph = GetThemeGlyphUri("Office2019Colorful", false);
            theme47.LargeGlyph = GetThemeGlyphUri("Office2019Colorful", true);
            theme47.SvgGlyph = GetThemeSvgGlyphUri("Office2019Colorful");
            theme47.ShowInThemeSelector = showTouchThemes;
            Office2019ColorfulTouch = theme47;
            Theme theme48 = new Theme("Office2019HighContrast;Touch");
            theme48.IsStandard = true;
            theme48.FullName = "Office 2019 High Contrast Touch";
            theme48.Category = "Office 2019 Touch Themes";
            theme48.SmallGlyph = GetThemeGlyphUri("Office2019HighContrast", false);
            theme48.LargeGlyph = GetThemeGlyphUri("Office2019HighContrast", true);
            theme48.SvgGlyph = GetThemeSvgGlyphUri("Office2019HighContrast");
            theme48.ShowInThemeSelector = showTouchThemes;
            Office2019HighContrastTouch = theme48;
            Theme theme49 = new Theme("Office2019Black;Touch");
            theme49.IsStandard = true;
            theme49.FullName = "Office 2019 Black Touch";
            theme49.Category = "Office 2019 Touch Themes";
            theme49.SmallGlyph = GetThemeGlyphUri("Office2019Black", false);
            theme49.LargeGlyph = GetThemeGlyphUri("Office2019Black", true);
            theme49.SvgGlyph = GetThemeSvgGlyphUri("Office2019Black");
            Office2019BlackTouch = theme49;
            Default = Office2016White;
            Tuple<string, string>[] tupleArray1 = new Tuple<string, string>[0x15];
            tupleArray1[0] = new Tuple<string, string>("LightGray", "D9DA32D6-3BED-4D2F-8CC8-D4E44F9A61BC");
            tupleArray1[1] = new Tuple<string, string>("DeepBlue", "D247B88A-7194-4A9E-8838-7A36C55A5F26");
            tupleArray1[2] = new Tuple<string, string>("Office2007Black", "8CD094E2-96D3-4770-8BBD-95AA0E9E956A");
            tupleArray1[3] = new Tuple<string, string>("Office2007Blue", "B366DFA1-88E2-46C5-AAFE-F1EBF4AE86BB");
            tupleArray1[4] = new Tuple<string, string>("Office2007Silver", "3262723B-7344-4734-B8D4-F927A39A3148");
            tupleArray1[5] = new Tuple<string, string>("Seven", "DB2EE31F-95D3-4EFD-B436-3F515CFEBB0F");
            tupleArray1[6] = new Tuple<string, string>("Office2010Black", "D1421BB8-0131-4346-B5FD-307D70E81E80");
            tupleArray1[7] = new Tuple<string, string>("Office2010Blue", "7F6339CA-9E31-4E8D-8293-C26917B0E127");
            tupleArray1[8] = new Tuple<string, string>("Office2010Silver", "343F52C3-8844-4FBD-8F05-C6DF882FADBD");
            tupleArray1[9] = new Tuple<string, string>("MetropolisDark", "88DCD2D6-C501-4355-836D-AC3A438243C8");
            tupleArray1[10] = new Tuple<string, string>("TouchlineDark", "B4E676E4-4F37-431C-92EF-4D6F808417F1");
            tupleArray1[11] = new Tuple<string, string>("Office2013", "3B40F326-8562-4B44-B46F-B2935D284057");
            tupleArray1[12] = new Tuple<string, string>("HybridApp", "B722B70F-BD3D-46B9-98A4-4EFCB5741B17");
            tupleArray1[13] = new Tuple<string, string>("MetropolisLight", "82F48483-C795-416A-9307-9C6750F5DEBA");
            tupleArray1[14] = new Tuple<string, string>("Office2013LightGray", "672667F5-5095-4E47-A21E-220FA569B6FD");
            tupleArray1[15] = new Tuple<string, string>("Office2013DarkGray", "C3C62B38-CB06-4E43-B419-332B6AC59DE9");
            tupleArray1[0x10] = new Tuple<string, string>("DXStyle", "ED998E8A-6699-4665-ACB1-7CF9D6E99865");
            tupleArray1[0x11] = new Tuple<string, string>("VS2010", "0A27036E-072F-4949-8AAF-BFB30628DEFB");
            tupleArray1[0x12] = Tuple.Create<string, string>("Office2016White", "BAEDFC31-BDBF-4DDF-98E1-A24F324EE9FC");
            tupleArray1[0x13] = Tuple.Create<string, string>("Office2016Black", "295E625A-2DD6-4475-B3F9-17327FD7EE53");
            tupleArray1[20] = Tuple.Create<string, string>("Office2016Colorful", "7FB73C8C-18D0-4BBC-A718-BE3A4200DD84");
            ThemeGuid = tupleArray1;
            RegisterTheme(Office2019White);
            RegisterTheme(Office2019DarkGray);
            RegisterTheme(Office2019Colorful);
            RegisterTheme(Office2019Black);
            RegisterTheme(Office2019HighContrast);
            RegisterTheme(Office2019WhiteTouch);
            RegisterTheme(Office2019DarkGrayTouch);
            RegisterTheme(Office2019ColorfulTouch);
            RegisterTheme(Office2019BlackTouch);
            RegisterTheme(Office2019HighContrastTouch);
            RegisterTheme(VS2017Light);
            RegisterTheme(VS2017Dark);
            RegisterTheme(VS2017Blue);
            RegisterTheme(Office2016WhiteSE);
            RegisterTheme(Office2016DarkGraySE);
            RegisterTheme(Office2016ColorfulSE);
            RegisterTheme(Office2016BlackSE);
            RegisterTheme(Office2016WhiteSETouch);
            RegisterTheme(Office2016DarkGraySETouch);
            RegisterTheme(Office2016ColorfulSETouch);
            RegisterTheme(Office2016BlackSETouch);
            RegisterTheme(DXStyle);
            RegisterTheme(Office2016White);
            RegisterTheme(Office2016Black);
            RegisterTheme(Office2016Colorful);
            RegisterTheme(Office2016WhiteTouch);
            RegisterTheme(Office2016BlackTouch);
            RegisterTheme(Office2016ColorfulTouch);
            RegisterTheme(Office2013);
            RegisterTheme(Office2013DarkGray);
            RegisterTheme(Office2013LightGray);
            RegisterTheme(Office2013Touch);
            RegisterTheme(Office2013DarkGrayTouch);
            RegisterTheme(Office2013LightGrayTouch);
            RegisterTheme(Office2010Black);
            RegisterTheme(Office2010Blue);
            RegisterTheme(Office2010Silver);
            RegisterTheme(MetropolisLight);
            RegisterTheme(MetropolisDark);
            RegisterTheme(VS2010);
            RegisterTheme(Office2007Black);
            RegisterTheme(Office2007Blue);
            RegisterTheme(Office2007Silver);
            RegisterTheme(Seven);
            RegisterTheme(TouchlineDark);
            RegisterTheme(HybridApp);
            RegisterTheme(DeepBlue);
            RegisterTheme(LightGray);
            PaletteThemeCacheDirectory = ThemePaletteGenerator.DefaultCacheDirectory;
        }

        public Theme(string name) : this(name, name, "Standard Themes", null, null, null)
        {
        }

        public Theme(string name, string fullName, string category = "Standard Themes", Uri smallGlyphUri = null, Uri largeGlyphUri = null, Uri svgGlyphUri = null) : this(name, fullName, category, smallGlyphUri, largeGlyphUri, svgGlyphUri, null, null, null)
        {
        }

        internal Theme(string name, string fullName, string category, Uri smallGlyphUri, Uri largeGlyphUri, Uri svgGlyphUri, Func<MemoryStream> getAssemblyStream, Theme baseTheme, string paletteName)
        {
            this.ShowInThemeSelector = true;
            this.Initialize(name, fullName, category, smallGlyphUri, largeGlyphUri, svgGlyphUri, getAssemblyStream, baseTheme, paletteName);
        }

        public static void ClearPaletteThemeCache()
        {
            ThemePaletteGenerator.ClearPaletteThemeCache();
        }

        public static Theme CreateTheme(ThemePaletteBase palette, Theme baseTheme, string themeName = null, string fullThemeName = null, string category = null, Uri smallGlyph = null, Uri largeGlyph = null, Uri svgGlyph = null) => 
            ThemePaletteGenerator.GenerateTheme(palette, baseTheme, themeName, fullThemeName, category, smallGlyph, largeGlyph, svgGlyph);

        public static Theme FindTheme(string name)
        {
            Theme theme;
            name ??= "";
            if (!ThemeNameHash.TryGetValue(name, out theme))
            {
                ThemeNameHash.Add(name, FindThemeCore(name));
            }
            return ThemeNameHash[name];
        }

        private static Theme FindThemeCore(string name)
        {
            Theme theme2;
            if (name == "Default")
            {
                return Default;
            }
            using (List<Theme>.Enumerator enumerator = ThemesInternal.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Theme current = enumerator.Current;
                        if (current.Name != name)
                        {
                            continue;
                        }
                        theme2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return theme2;
        }

        private static Theme GenerateAndRegisterTheme(ThemePaletteBase palette, Theme baseTheme)
        {
            Theme theme = FindTheme(ThemePaletteGenerator.GenerateThemeName(baseTheme, palette.Name));
            if (theme != null)
            {
                return theme;
            }
            Theme theme2 = CreateTheme(palette, baseTheme, null, null, null, null, null, null);
            RegisterTheme(theme2);
            return theme2;
        }

        protected virtual System.Reflection.Assembly GetAssembly()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return null;
            }
            System.Reflection.Assembly assembly = this.LoadedFromStream ? this.LoadFromStream() : null;
            return AssemblyHelper.GetAssembly(this.AssemblyName, x => assembly);
        }

        protected static string GetAssemblyGuid(System.Reflection.Assembly asm)
        {
            if (asm == null)
            {
                return null;
            }
            object[] customAttributes = asm.GetCustomAttributes(typeof(GuidAttribute), false);
            if (customAttributes.Length == 0)
            {
                return null;
            }
            GuidAttribute attribute = customAttributes[0] as GuidAttribute;
            return attribute?.Value;
        }

        protected virtual string GetAssemblyName() => 
            (this.Name != DeepBlue.Name) ? (!this.IsStandard ? (!this.LoadedFromStream ? (!this.GlobalAssemblyCache ? this.Name : AssemblyHelper.GetAssemblyFullName(this.Name, this.Version, CultureInfo.InvariantCulture, this.PublicKeyToken)) : this.LoadedFromStreamAssemblyName) : AssemblyHelper.GetThemeAssemblyFullName(this.Name)) : typeof(Theme).Assembly.FullName;

        public static string GetBaseThemeName(string themeName)
        {
            Theme theme = FindTheme(themeName);
            if (theme != null)
            {
                string assemblyGuid = GetAssemblyGuid(theme.Assembly);
                if (assemblyGuid != null)
                {
                    return GetThemeNameByGuid(assemblyGuid);
                }
            }
            return null;
        }

        internal static Uri GetPaletteThemeSvgGlyphUri() => 
            GetThemeSvgGlyphUri("PaletteGallery");

        private static Uri GetThemeGlyphUri(string themeName, bool isLarge)
        {
            string str = "_16x16.png";
            string str2 = "_48x48.png";
            string str3 = $"/{"DevExpress.Xpf.Core.v19.2"};component/Themes/Images/";
            string str4 = isLarge ? str2 : str;
            return new Uri(str3 + themeName + str4, UriKind.Relative);
        }

        public static string GetThemeNameByGuid(string guid)
        {
            foreach (Tuple<string, string> tuple in ThemeGuid)
            {
                if (guid == tuple.Item2)
                {
                    return tuple.Item1;
                }
            }
            return null;
        }

        internal static Uri GetThemeSvgGlyphUri(string themeName)
        {
            ImageSourceHelper.RegisterPackScheme();
            return new Uri($"pack://application:,,,/{"DevExpress.Xpf.Core.v19.2"};component/Themes/Images/{themeName}.svg", UriKind.RelativeOrAbsolute);
        }

        private static string GetVersionFromAssemblyName(string assemblyName) => 
            new Regex(@".*?(\d\d\.\d)").Match(assemblyName).Groups[1].ToString();

        protected void Initialize(string name, string fullName, string category, Uri smallGlyphUri, Uri largeGlyphUri, Uri svgGlyphUri, Func<MemoryStream> getAssemblyStream, Theme baseTheme, string paletteName)
        {
            this.Name = name;
            this.FullName = fullName;
            this.Category = category;
            this.SmallGlyph = smallGlyphUri;
            this.LargeGlyph = largeGlyphUri;
            this.SvgGlyph = svgGlyphUri;
            this.BaseTheme = baseTheme;
            this._paletteName = paletteName;
            if (getAssemblyStream != null)
            {
                this.LoadedFromStream = true;
                this.initializeAssemblyFromStream = delegate {
                    using (MemoryStream stream = getAssemblyStream())
                    {
                        this.AssemblyFromStream = System.Reflection.Assembly.Load(stream.ToArray());
                    }
                    this.LoadedFromStreamAssemblyName = this.AssemblyFromStream.FullName;
                };
            }
        }

        public static bool IsDefaultTheme(string themeName) => 
            themeName == DefaultThemeName;

        private static bool IsThemeAndAssemblyNameSame(Theme theme)
        {
            if (theme.Name == "DeepBlue")
            {
                return true;
            }
            string assemblyName = theme.assemblyName;
            if (string.IsNullOrEmpty(assemblyName))
            {
                return true;
            }
            string a = new Regex(@"devexpress\.xpf\.themes\.(?<name>\w+).v\d\d.\d", 1).Match(assemblyName).Groups["name"].Value;
            return string.Equals(a, theme.Name.EndsWithInvariantCultureIgnoreCase(";touch") ? theme.Name.Substring(0, theme.Name.IndexOf(";")) : theme.Name);
        }

        private static bool IsThemeAndControlVersionSame(Theme theme)
        {
            if (string.IsNullOrEmpty(theme.AssemblyName))
            {
                return true;
            }
            string versionFromAssemblyName = GetVersionFromAssemblyName(theme.AssemblyName);
            return (string.IsNullOrEmpty(versionFromAssemblyName) || ("19.2" == versionFromAssemblyName));
        }

        private System.Reflection.Assembly LoadFromStream()
        {
            if (this.initializeAssemblyFromStream != null)
            {
                this.initializeAssemblyFromStream();
                this.initializeAssemblyFromStream = null;
            }
            return this.AssemblyFromStream;
        }

        private static IEnumerable<Theme> RegisterOffice2016()
        {
            List<Theme> list = new List<Theme>();
            list.AddRange(RegisterPalette(Office2016ColorfulSE));
            list.AddRange(RegisterPalette(Office2016WhiteSE));
            list.AddRange(RegisterPalette(Office2016BlackSE));
            list.AddRange(RegisterPalette(Office2016DarkGraySE));
            return list;
        }

        private static IEnumerable<Theme> RegisterOffice2019()
        {
            List<Theme> list = new List<Theme>();
            list.AddRange(RegisterPalette(Office2019Colorful));
            list.AddRange(RegisterPalette(Office2019White));
            list.AddRange(RegisterPalette(Office2019Black));
            list.AddRange(RegisterPalette(Office2019DarkGray));
            return list;
        }

        private static IEnumerable<Theme> RegisterPalette(Theme theme)
        {
            List<Theme> list1 = new List<Theme>();
            list1.Add(GenerateAndRegisterTheme(PredefinedThemePalettes.CobaltBlue, theme));
            list1.Add(GenerateAndRegisterTheme(PredefinedThemePalettes.SpruceLeaves, theme));
            list1.Add(GenerateAndRegisterTheme(PredefinedThemePalettes.Brickwork, theme));
            list1.Add(GenerateAndRegisterTheme(PredefinedThemePalettes.RedWine, theme));
            list1.Add(GenerateAndRegisterTheme(PredefinedThemePalettes.Turquoise, theme));
            list1.Add(GenerateAndRegisterTheme(PredefinedThemePalettes.DarkLilac, theme));
            return list1;
        }

        public static IEnumerable<Theme> RegisterPredefinedPaletteThemes()
        {
            List<Theme> list = new List<Theme>();
            list.AddRange(RegisterOffice2016());
            list.AddRange(RegisterOffice2019());
            list.AddRange(RegisterVS2017());
            return list;
        }

        public static void RegisterTheme(Theme theme)
        {
            if (FindTheme(theme.name) != null)
            {
                throw new ArgumentException("A theme with the same name already exists");
            }
            if (!IsThemeAndAssemblyNameSame(theme))
            {
                throw new ArgumentException("The theme and assembly name does not match.");
            }
            if (!IsThemeAndControlVersionSame(theme))
            {
                throw new ArgumentException("Wrong theme assembly version. The version of the theme and control assembly must be the same.");
            }
            ThemeNameHash.Clear();
            ThemesInternal.Add(theme);
        }

        private static IEnumerable<Theme> RegisterVS2017()
        {
            List<Theme> list = new List<Theme>();
            list.AddRange(RegisterPalette(VS2017Light));
            list.AddRange(RegisterPalette(VS2017Dark));
            list.AddRange(RegisterPalette(VS2017Blue));
            return list;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static void RemoveAllCustomThemes()
        {
            ThemeNameHash.Clear();
            for (int i = ThemesInternal.Count - 1; i >= 0; i--)
            {
                if (!ThemesInternal[i].IsStandard)
                {
                    ThemesInternal.RemoveAt(i);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static void RemoveTheme(string name)
        {
            Theme item = FindTheme(name);
            if (item != null)
            {
                ThemeNameHash.Clear();
                ThemesInternal.Remove(item);
            }
        }

        protected void ResetAssembly()
        {
            this.assembly = null;
        }

        public override string ToString() => 
            this.FullName;

        public static string DefaultThemeName =>
            Default.Name;

        public static string DefaultThemeFullName =>
            Default.FullName;

        public static bool CachePaletteThemes { get; set; }

        public static string PaletteThemeCacheDirectory { get; set; }

        public static ReadOnlyCollection<Theme> Themes =>
            ThemesInternal.AsReadOnly();

        public Uri SmallGlyph { get; private set; }

        public Uri LargeGlyph { get; private set; }

        public Uri SvgGlyph { get; private set; }

        internal Theme BaseTheme { get; private set; }

        public string Category { get; private set; }

        private bool LoadedFromStream { get; set; }

        private System.Reflection.Assembly AssemblyFromStream { get; set; }

        private string LoadedFromStreamAssemblyName { get; set; }

        public string AssemblyName
        {
            get
            {
                this.assemblyName ??= this.GetAssemblyName();
                return this.assemblyName;
            }
            set
            {
                if (this.assemblyName != value)
                {
                    this.assemblyName = value;
                    this.ResetAssembly();
                }
            }
        }

        public System.Reflection.Assembly Assembly
        {
            get
            {
                if (this.assembly == null)
                {
                    this.assembly = this.GetAssembly();
                }
                return this.assembly;
            }
        }

        public bool GlobalAssemblyCache =>
            !string.IsNullOrEmpty(this.Name) && (!string.IsNullOrEmpty(this.Version) && !string.IsNullOrEmpty(this.PublicKeyToken));

        public string PublicKeyToken
        {
            get => 
                this.publicKeyToken;
            set
            {
                if (this.publicKeyToken != value)
                {
                    this.publicKeyToken = value;
                    this.ResetAssembly();
                }
            }
        }

        public string Version
        {
            get => 
                this.version;
            set
            {
                if (this.version != value)
                {
                    this.version = value;
                    this.ResetAssembly();
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStandard { get; set; }

        public string FullName
        {
            get => 
                (this.fullName != null) ? this.fullName : this.Name;
            set
            {
                if (this.fullName != value)
                {
                    this.fullName = value;
                }
            }
        }

        public string Name
        {
            get => 
                this.name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The Name property can not be empty");
                }
                this.name = value;
            }
        }

        internal string PaletteName =>
            this._paletteName ?? this.FullName;
    }
}

