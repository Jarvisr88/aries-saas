namespace ActiproSoftware.Drawing
{
    using ActiproSoftware.ComponentModel;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public class WindowsColorScheme : DisposableObject
    {
        private WindowsColorSchemeType #kre;
        private string #1Nc;
        private WindowsColorSchemeType #lre;
        private Hashtable #Vre;
        private Color #ODb;
        private static WindowsColorScheme #Iqi;
        private static WindowsColorScheme #Wre;
        private static WindowsColorScheme #Xre;
        private static WindowsColorScheme #Yre;
        private static WindowsColorScheme #Zre;
        private static WindowsColorScheme #Pre;
        private static WindowsColorScheme #Qre;
        private static WindowsColorScheme #Rre;
        private static WindowsColorScheme #Sre;
        private static WindowsColorScheme #0re;
        private static WindowsColorScheme #Tre;
        private static bool? #f6h;

        private void #4ae(object #xhb, UserPreferenceChangedEventArgs #yhb);
        private void #Awe();
        private void #Bwe();
        private void #Cwe();
        private void #Dwe();
        private void #Ewe();
        private void #Jqi();
        private void #twe();
        private void #uwe();
        private void #vwe();
        private void #wwe();
        private void #xwe();
        private void #ywe();
        private void #zwe();
        private WindowsColorScheme(WindowsColorSchemeType baseColorSchemeType);
        public WindowsColorScheme(string key, WindowsColorSchemeType baseColorSchemeType, Color tintColor);
        protected override void Dispose(bool disposing);
        public static WindowsColorScheme GetColorScheme(WindowsColorSchemeType colorSchemeType);
        public void Initialize();
        protected virtual void UpdateColors();

        private static string CurrentVisualStyleColorScheme { get; }

        private static string CurrentVisualStyleThemeFileName { get; }

        internal static bool IsThemeActive { get; }

        private static bool IsWindows8OrLater { get; }

        [Category("Appearance"), Description("The background color of a checked bar button.")]
        public virtual Color BarButtonCheckedBack { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a checked bar button background.")]
        public virtual Color BarButtonCheckedBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a checked bar button background.")]
        public virtual Color BarButtonCheckedBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a checked bar button background.")]
        public virtual Color BarButtonCheckedBackGradientMiddle { get; set; }

        [Category("Appearance"), Description("The border color of a checked bar button.")]
        public virtual Color BarButtonCheckedBorder { get; set; }

        [Category("Appearance"), Description("The background color of a hot bar button.")]
        public virtual Color BarButtonHotBack { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a hot bar button background.")]
        public virtual Color BarButtonHotBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a hot bar button background.")]
        public virtual Color BarButtonHotBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a hot bar button background.")]
        public virtual Color BarButtonHotBackGradientMiddle { get; set; }

        [Category("Appearance"), Description("The border color of a hot bar button.")]
        public virtual Color BarButtonHotBorder { get; set; }

        [Category("Appearance"), Description("The background color of a pressed bar button.")]
        public virtual Color BarButtonPressedBack { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a pressed bar button background.")]
        public virtual Color BarButtonPressedBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a pressed bar button background.")]
        public virtual Color BarButtonPressedBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a pressed bar button background.")]
        public virtual Color BarButtonPressedBackGradientMiddle { get; set; }

        [Category("Appearance"), Description("The border color of a pressed bar button.")]
        public virtual Color BarButtonPressedBorder { get; set; }

        [Category("Appearance"), Description("The color of bar button text.")]
        public virtual Color BarButtonText { get; set; }

        [Category("Appearance"), Description("The color of alternate bar button text.")]
        public virtual Color BarButtonTextAlternate { get; set; }

        [Category("Appearance"), Description("The color of disabled bar button text.")]
        public virtual Color BarButtonTextDisabled { get; set; }

        [Category("Appearance"), Description("The background color of a bar label.")]
        public virtual Color BarLabelBack { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WindowsColorSchemeType BaseColorSchemeType { get; }

        [Category("Data"), Description("The type of color scheme.")]
        public WindowsColorSchemeType ColorSchemeType { get; }

        public static WindowsColorSchemeType DefaultColorSchemeType { get; }

        [Category("Appearance"), Description("The begin gradient color of a form background.")]
        public virtual Color FormBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a form background.")]
        public virtual Color FormBackGradientEnd { get; set; }

        public string Key { get; }

        [Category("Appearance"), Description("The background color of a menu.")]
        public virtual Color MenuBack { get; set; }

        [Category("Appearance"), Description("The border color of a menu.")]
        public virtual Color MenuBorder { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a menu icon column background.")]
        public virtual Color MenuIconColumnBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a menu icon column background.")]
        public virtual Color MenuIconColumnBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a menu icon column background.")]
        public virtual Color MenuIconColumnBackGradientMiddle { get; set; }

        public static WindowsColorScheme MetroLight { get; }

        [Category("Appearance"), Description("The begin gradient color of a navigationbar header background.")]
        public virtual Color NavigationBarHeaderBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a navigationbar header background.")]
        public virtual Color NavigationBarHeaderBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The color of navigationbar header text.")]
        public virtual Color NavigationBarHeaderText { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonBackGradientMid1 { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonBackGradientMid2 { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a hot navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a hot navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a hot navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotBackGradientMid1 { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotBackGradientMid2 { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a hot/pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotPressedBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a hot/pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotPressedBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a hot/pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotPressedBackGradientMid1 { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a hot/pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonHotPressedBackGradientMid2 { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonPressedBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonPressedBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonPressedBackGradientMid1 { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a pressed navigationbar pane button background.")]
        public virtual Color NavigationPaneButtonPressedBackGradientMid2 { get; set; }

        public static WindowsColorScheme Office2007Black { get; }

        public static WindowsColorScheme Office2007Blue { get; }

        public static WindowsColorScheme Office2007Silver { get; }

        [Category("Appearance"), Description("The begin gradient color of a toolbar background.")]
        public virtual Color ToolBarBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a toolbar background.")]
        public virtual Color ToolBarBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a toolbar background.")]
        public virtual Color ToolBarBackGradientMiddle { get; set; }

        [Category("Appearance"), Description("The background color of a floating toolbar title bar.")]
        public virtual Color ToolBarFloatingTitleBack { get; set; }

        [Category("Appearance"), Description("The text color of a floating toolbar title bar.")]
        public virtual Color ToolBarFloatingTitleText { get; set; }

        [Category("Appearance"), Description("The dark color of a toolbar gripper.")]
        public virtual Color ToolBarGripperDark { get; set; }

        [Category("Appearance"), Description("The light color of a toolbar gripper.")]
        public virtual Color ToolBarGripperLight { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a toolbar options button background.")]
        public virtual Color ToolBarOptionsBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a toolbar options button background.")]
        public virtual Color ToolBarOptionsBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a toolbar options button background.")]
        public virtual Color ToolBarOptionsBackGradientMiddle { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a hot toolbar options button background.")]
        public virtual Color ToolBarOptionsHotBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a hot toolbar options button background.")]
        public virtual Color ToolBarOptionsHotBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a hot toolbar options button background.")]
        public virtual Color ToolBarOptionsHotBackGradientMiddle { get; set; }

        [Category("Appearance"), Description("The begin gradient color of a pressed toolbar options button background.")]
        public virtual Color ToolBarOptionsPressedBackGradientBegin { get; set; }

        [Category("Appearance"), Description("The end gradient color of a pressed toolbar options button background.")]
        public virtual Color ToolBarOptionsPressedBackGradientEnd { get; set; }

        [Category("Appearance"), Description("The middle gradient color of a pressed toolbar options button background.")]
        public virtual Color ToolBarOptionsPressedBackGradientMiddle { get; set; }

        [Category("Appearance"), Description("The dark color of a toolbar separator.")]
        public virtual Color ToolBarSeparatorDark { get; set; }

        [Category("Appearance"), Description("The light color of a toolbar separator.")]
        public virtual Color ToolBarSeparatorLight { get; set; }

        [Category("Appearance"), Description("The color of a toolbar shadow.")]
        public virtual Color ToolBarShadow { get; set; }

        public static WindowsColorScheme VisualStudio2005 { get; }

        public static WindowsColorScheme WindowsClassic { get; }

        public static WindowsColorScheme WindowsDefault { get; }

        public static WindowsColorScheme WindowsXPBlue { get; }

        public static WindowsColorScheme WindowsXPOliveGreen { get; }

        public static WindowsColorScheme WindowsXPRoyale { get; }

        public static WindowsColorScheme WindowsXPSilver { get; }

        private enum #Oqe
        {
            public const WindowsColorScheme.#Oqe #1re = WindowsColorScheme.#Oqe.#1re;,
            public const WindowsColorScheme.#Oqe #2re = WindowsColorScheme.#Oqe.#2re;,
            public const WindowsColorScheme.#Oqe #3re = WindowsColorScheme.#Oqe.#3re;,
            public const WindowsColorScheme.#Oqe #4re = WindowsColorScheme.#Oqe.#4re;,
            public const WindowsColorScheme.#Oqe #5re = WindowsColorScheme.#Oqe.#5re;,
            public const WindowsColorScheme.#Oqe #6re = WindowsColorScheme.#Oqe.#6re;,
            public const WindowsColorScheme.#Oqe #7re = WindowsColorScheme.#Oqe.#7re;,
            public const WindowsColorScheme.#Oqe #8re = WindowsColorScheme.#Oqe.#8re;,
            public const WindowsColorScheme.#Oqe #9re = WindowsColorScheme.#Oqe.#9re;,
            public const WindowsColorScheme.#Oqe #ase = WindowsColorScheme.#Oqe.#ase;,
            public const WindowsColorScheme.#Oqe #bse = WindowsColorScheme.#Oqe.#bse;,
            public const WindowsColorScheme.#Oqe #cse = WindowsColorScheme.#Oqe.#cse;,
            public const WindowsColorScheme.#Oqe #dse = WindowsColorScheme.#Oqe.#dse;,
            public const WindowsColorScheme.#Oqe #ese = WindowsColorScheme.#Oqe.#ese;,
            public const WindowsColorScheme.#Oqe #fse = WindowsColorScheme.#Oqe.#fse;,
            public const WindowsColorScheme.#Oqe #gse = WindowsColorScheme.#Oqe.#gse;,
            public const WindowsColorScheme.#Oqe #hse = WindowsColorScheme.#Oqe.#hse;,
            public const WindowsColorScheme.#Oqe #ise = WindowsColorScheme.#Oqe.#ise;,
            public const WindowsColorScheme.#Oqe #jse = WindowsColorScheme.#Oqe.#jse;,
            public const WindowsColorScheme.#Oqe #kse = WindowsColorScheme.#Oqe.#kse;,
            public const WindowsColorScheme.#Oqe #lse = WindowsColorScheme.#Oqe.#lse;,
            public const WindowsColorScheme.#Oqe #mse = WindowsColorScheme.#Oqe.#mse;,
            public const WindowsColorScheme.#Oqe #nse = WindowsColorScheme.#Oqe.#nse;,
            public const WindowsColorScheme.#Oqe #ose = WindowsColorScheme.#Oqe.#ose;,
            public const WindowsColorScheme.#Oqe #pse = WindowsColorScheme.#Oqe.#pse;,
            public const WindowsColorScheme.#Oqe #qse = WindowsColorScheme.#Oqe.#qse;,
            public const WindowsColorScheme.#Oqe #rse = WindowsColorScheme.#Oqe.#rse;,
            public const WindowsColorScheme.#Oqe #sse = WindowsColorScheme.#Oqe.#sse;,
            public const WindowsColorScheme.#Oqe #tse = WindowsColorScheme.#Oqe.#tse;,
            public const WindowsColorScheme.#Oqe #use = WindowsColorScheme.#Oqe.#use;,
            public const WindowsColorScheme.#Oqe #vse = WindowsColorScheme.#Oqe.#vse;,
            public const WindowsColorScheme.#Oqe #wse = WindowsColorScheme.#Oqe.#wse;,
            public const WindowsColorScheme.#Oqe #xse = WindowsColorScheme.#Oqe.#xse;,
            public const WindowsColorScheme.#Oqe #yse = WindowsColorScheme.#Oqe.#yse;,
            public const WindowsColorScheme.#Oqe #zse = WindowsColorScheme.#Oqe.#zse;,
            public const WindowsColorScheme.#Oqe #Ase = WindowsColorScheme.#Oqe.#Ase;,
            public const WindowsColorScheme.#Oqe #Bse = WindowsColorScheme.#Oqe.#Bse;,
            public const WindowsColorScheme.#Oqe #Cse = WindowsColorScheme.#Oqe.#Cse;,
            public const WindowsColorScheme.#Oqe #Dse = WindowsColorScheme.#Oqe.#Dse;,
            public const WindowsColorScheme.#Oqe #Ese = WindowsColorScheme.#Oqe.#Ese;,
            public const WindowsColorScheme.#Oqe #Fse = WindowsColorScheme.#Oqe.#Fse;,
            public const WindowsColorScheme.#Oqe #Gse = WindowsColorScheme.#Oqe.#Gse;,
            public const WindowsColorScheme.#Oqe #Hse = WindowsColorScheme.#Oqe.#Hse;,
            public const WindowsColorScheme.#Oqe #Ise = WindowsColorScheme.#Oqe.#Ise;,
            public const WindowsColorScheme.#Oqe #Jse = WindowsColorScheme.#Oqe.#Jse;,
            public const WindowsColorScheme.#Oqe #Kse = WindowsColorScheme.#Oqe.#Kse;,
            public const WindowsColorScheme.#Oqe #Lse = WindowsColorScheme.#Oqe.#Lse;,
            public const WindowsColorScheme.#Oqe #Mse = WindowsColorScheme.#Oqe.#Mse;,
            public const WindowsColorScheme.#Oqe #Nse = WindowsColorScheme.#Oqe.#Nse;,
            public const WindowsColorScheme.#Oqe #Ose = WindowsColorScheme.#Oqe.#Ose;,
            public const WindowsColorScheme.#Oqe #Pse = WindowsColorScheme.#Oqe.#Pse;,
            public const WindowsColorScheme.#Oqe #Qse = WindowsColorScheme.#Oqe.#Qse;,
            public const WindowsColorScheme.#Oqe #Rse = WindowsColorScheme.#Oqe.#Rse;,
            public const WindowsColorScheme.#Oqe #Sse = WindowsColorScheme.#Oqe.#Sse;,
            public const WindowsColorScheme.#Oqe #Tse = WindowsColorScheme.#Oqe.#Tse;,
            public const WindowsColorScheme.#Oqe #Use = WindowsColorScheme.#Oqe.#Use;,
            public const WindowsColorScheme.#Oqe #Vse = WindowsColorScheme.#Oqe.#Vse;,
            public const WindowsColorScheme.#Oqe #Wse = WindowsColorScheme.#Oqe.#Wse;,
            public const WindowsColorScheme.#Oqe #Xse = WindowsColorScheme.#Oqe.#Xse;,
            public const WindowsColorScheme.#Oqe #Yse = WindowsColorScheme.#Oqe.#Yse;,
            public const WindowsColorScheme.#Oqe #Zse = WindowsColorScheme.#Oqe.#Zse;,
            public const WindowsColorScheme.#Oqe #0se = WindowsColorScheme.#Oqe.#0se;,
            public const WindowsColorScheme.#Oqe #1se = WindowsColorScheme.#Oqe.#1se;,
            public const WindowsColorScheme.#Oqe #2se = WindowsColorScheme.#Oqe.#2se;
        }
    }
}

