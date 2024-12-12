namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ThemeHelper
    {
        [ThreadStatic]
        private static InplaceResourceProvider instance;

        public static string GetEditorThemeName(DependencyObject obj) => 
            GetEditorThemeNameCore(obj, false);

        private static string GetEditorThemeNameCore(DependencyObject obj, bool getBase)
        {
            FrameworkElement element = obj as FrameworkElement;
            if ((element != null) && element.OverridesDefaultStyle)
            {
                return null;
            }
            FrameworkContentElement element2 = obj as FrameworkContentElement;
            return (((element2 == null) || !element2.OverridesDefaultStyle) ? GetTreewalkerThemeName(ThemeManager.GetTreeWalker(obj), getBase) : null);
        }

        public static string GetRealThemeName(DependencyObject obj)
        {
            DependencyObject obj2 = obj;
            DependencyObject parent = null;
            while (obj2 != null)
            {
                string themeName = ThemeManager.GetThemeName(obj2);
                if (!string.IsNullOrEmpty(themeName))
                {
                    return themeName;
                }
                parent = LayoutHelper.GetParent(obj2, false);
                if ((parent == null) && (obj2 is FrameworkElement))
                {
                    parent = ((FrameworkElement) obj2).Parent;
                }
                obj2 = parent;
            }
            return null;
        }

        public static string GetRealThemeNameEx(DependencyObject obj)
        {
            FrameworkElement element = obj as FrameworkElement;
            if ((element != null) && element.OverridesDefaultStyle)
            {
                return null;
            }
            FrameworkContentElement element2 = obj as FrameworkContentElement;
            if ((element2 != null) && element2.OverridesDefaultStyle)
            {
                return null;
            }
            string realThemeName = GetRealThemeName(obj);
            return ((realThemeName == "DeepBlue") ? null : realThemeName);
        }

        public static InplaceResourceProvider GetResourceProvider(DependencyObject d)
        {
            ThemeTreeWalker treeWalker = ThemeManager.GetTreeWalker(d);
            return ((treeWalker != null) ? treeWalker.InplaceResourceProvider : Instance);
        }

        public static string GetThemeName(DependencyObject obj) => 
            GetRealThemeNameEx(obj);

        public static string GetTreewalkerThemeName(ThemeTreeWalker theme, bool getBase)
        {
            string themeName = ((theme == null) || ((theme.ThemeName == Theme.DeepBlue.Name) || !theme.IsRegistered)) ? null : theme.ThemeName;
            return (getBase ? (Theme.GetBaseThemeName(themeName) ?? themeName) : themeName);
        }

        public static string GetWindowThemeName(DependencyObject obj)
        {
            ThemeTreeWalker treeWalker = ThemeManager.GetTreeWalker(obj);
            string treewalkerThemeName = GetTreewalkerThemeName(treeWalker, false);
            if (treewalkerThemeName == null)
            {
                return null;
            }
            Func<ThemeTreeWalker, bool> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<ThemeTreeWalker, bool> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.IsTouch;
            }
            Func<ThemeTreeWalker, string> func2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<ThemeTreeWalker, string> local2 = <>c.<>9__0_1;
                func2 = <>c.<>9__0_1 = x => ";touch";
            }
            return (treewalkerThemeName + treeWalker.If<ThemeTreeWalker>(evaluator).Return<ThemeTreeWalker, string>(func2, (<>c.<>9__0_2 ??= () => string.Empty)));
        }

        public static bool IsBlackTheme(DependencyObject obj)
        {
            string editorThemeNameCore = GetEditorThemeNameCore(obj, true);
            return ((editorThemeNameCore == "Office2010Black") || ((editorThemeNameCore == "MetropolisDark") || (editorThemeNameCore == "TouchlineDark")));
        }

        public static bool IsTouchTheme(DependencyObject obj) => 
            GetThemeName(obj) == "TouchlineDark";

        public static bool ShouldUpdateForeground(DependencyObject d) => 
            GetEditorThemeName(d) == "Office2010Black";

        internal static InplaceResourceProvider Instance =>
            instance ??= new InplaceResourceProvider("DeepBlue");

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemeHelper.<>c <>9 = new ThemeHelper.<>c();
            public static Func<ThemeTreeWalker, bool> <>9__0_0;
            public static Func<ThemeTreeWalker, string> <>9__0_1;
            public static Func<string> <>9__0_2;

            internal bool <GetWindowThemeName>b__0_0(ThemeTreeWalker x) => 
                x.IsTouch;

            internal string <GetWindowThemeName>b__0_1(ThemeTreeWalker x) => 
                ";touch";

            internal string <GetWindowThemeName>b__0_2() => 
                string.Empty;
        }
    }
}

