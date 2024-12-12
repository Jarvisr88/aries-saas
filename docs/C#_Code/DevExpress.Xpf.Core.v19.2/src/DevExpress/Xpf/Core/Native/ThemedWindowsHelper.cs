namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Windows;

    public class ThemedWindowsHelper
    {
        public static readonly DependencyProperty WindowProperty;
        public static readonly DependencyProperty IsMessageBoxNCActiveProperty;
        public static readonly DependencyProperty RibbonHeaderVisibilityProperty;
        public static readonly DependencyProperty RibbonControlVisibilityProperty;
        public static readonly DependencyProperty IsRibbonControlAsContentProperty;
        public static readonly DependencyProperty AllowThemedWindowIntegrationProperty;
        public static readonly DependencyProperty IsWindowClosingWithCustomCommandProperty;
        public static readonly DependencyProperty IsBlurBehindEnabledProperty;
        public static readonly DependencyProperty UseMvvmMessageResultAsDialogResultProperty;

        static ThemedWindowsHelper();
        public static bool GetAllowThemedWindowIntegration(DependencyObject element);
        public static bool GetIsBlurBehindEnabled(DependencyObject element);
        public static bool GetIsMessageBoxNCActive(DependencyObject element);
        public static bool GetIsRibbonControlAsContent(DependencyObject element);
        public static bool GetIsWindowClosingWithCustomCommand(DependencyObject element);
        public static Visibility GetRibbonControlVisibility(DependencyObject element);
        public static RibbonHeaderVisibility GetRibbonHeaderVisibility(DependencyObject element);
        public static bool GetUseMvvmMessageResultAsDialogResult(DependencyObject element);
        public static Window GetWindow(DependencyObject element);
        public static void SetAllowThemedWindowIntegration(DependencyObject element, bool value);
        public static void SetIsBlurBehindEnabled(DependencyObject element, bool value);
        public static void SetIsMessageBoxNCActive(DependencyObject element, bool value);
        public static void SetIsRibbonControlAsContent(DependencyObject element, bool value);
        public static void SetIsWindowClosingWithCustomCommand(DependencyObject element, bool value);
        public static void SetRibbonControlVisibility(DependencyObject element, Visibility value);
        public static void SetRibbonHeaderVisibility(DependencyObject element, RibbonHeaderVisibility value);
        public static void SetUseMvvmMessageResultAsDialogResult(DependencyObject element, bool value);
        public static void SetWindow(DependencyObject element, Window value);
    }
}

