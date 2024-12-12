namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Windows;

    public static class PreviewClickHelper
    {
        public static readonly DependencyProperty NavigationPairProperty;
        public static readonly DependencyProperty TagProperty;
        public static readonly DependencyProperty UrlProperty;

        static PreviewClickHelper();
        public static string GetNavigationPair(DependencyObject obj);
        public static string GetTag(DependencyObject obj);
        public static string GetUrl(DependencyObject obj);
        private static void NavigateToUrl(string url);
        private static void OnNavigationPairChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetNavigationPair(DependencyObject obj, string value);
        public static void SetTag(DependencyObject obj, string value);
        public static void SetUrl(DependencyObject obj, string value);
    }
}

