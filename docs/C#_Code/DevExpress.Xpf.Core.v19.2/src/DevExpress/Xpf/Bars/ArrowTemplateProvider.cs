namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public class ArrowTemplateProvider : DependencyObject
    {
        public static readonly DependencyProperty ArrowStyleInBarProperty;
        public static readonly DependencyProperty ArrowStyleInMainMenuProperty;
        public static readonly DependencyProperty ArrowStyleInMenuProperty;
        public static readonly DependencyProperty ArrowStyleInStatusBarProperty;
        public static readonly DependencyProperty ArrowStyleInRibbonPageGroupProperty;
        public static readonly DependencyProperty ArrowStyleInButtonGroupProperty;
        public static readonly DependencyProperty ArrowStyleInQuickAccessToolbarProperty;
        public static readonly DependencyProperty ArrowStyleInQuickAccessToolbarFooterProperty;
        public static readonly DependencyProperty ArrowStyleInRibbonPageHeaderProperty;
        public static readonly DependencyProperty ArrowStyleInRibbonStatusBarLeftProperty;
        public static readonly DependencyProperty ArrowStyleInRibbonStatusBarRightProperty;

        static ArrowTemplateProvider();
        public static Style GetArrowStyleInBar(DependencyObject target);
        public static Style GetArrowStyleInButtonGroup(DependencyObject target);
        public static Style GetArrowStyleInMainMenu(DependencyObject target);
        public static Style GetArrowStyleInMenu(DependencyObject target);
        public static Style GetArrowStyleInQuickAccessToolbar(DependencyObject target);
        public static Style GetArrowStyleInQuickAccessToolbarFooter(DependencyObject target);
        public static Style GetArrowStyleInRibbonPageGroup(DependencyObject target);
        public static Style GetArrowStyleInRibbonPageHeader(DependencyObject target);
        public static Style GetArrowStyleInRibbonStatusBarLeft(DependencyObject target);
        public static Style GetArrowStyleInRibbonStatusBarRight(DependencyObject target);
        public static Style GetArrowStyleInStatusBar(DependencyObject target);
        public static void SetArrowStyleInBar(DependencyObject target, Style value);
        public static void SetArrowStyleInButtonGroup(DependencyObject target, Style value);
        public static void SetArrowStyleInMainMenu(DependencyObject target, Style value);
        public static void SetArrowStyleInMenu(DependencyObject target, Style value);
        public static void SetArrowStyleInQuickAccessToolbar(DependencyObject target, Style value);
        public static void SetArrowStyleInQuickAccessToolbarFooter(DependencyObject target, Style value);
        public static void SetArrowStyleInRibbonPageGroup(DependencyObject target, Style value);
        public static void SetArrowStyleInRibbonPageHeader(DependencyObject target, Style value);
        public static void SetArrowStyleInRibbonStatusBarLeft(DependencyObject target, Style value);
        public static void SetArrowStyleInRibbonStatusBarRight(DependencyObject target, Style value);
        public static void SetArrowStyleInStatusBar(DependencyObject target, Style value);
    }
}

