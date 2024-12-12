namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public class BarItemSearchSettings
    {
        public static readonly DependencyProperty SearchTagsProperty;
        public static readonly DependencyProperty SearchCategoryProperty;
        public static readonly DependencyProperty HideFromSearchProperty;
        public static readonly DependencyProperty SearchPathSegmentProperty;

        static BarItemSearchSettings();
        internal static string GetActualSearchCategory(BarItemLink link);
        public static bool GetHideFromSearch(DependencyObject element);
        public static string GetSearchCategory(DependencyObject element);
        public static string GetSearchPathSegment(DependencyObject element);
        public static string GetSearchTags(DependencyObject element);
        public static void SetHideFromSearch(DependencyObject element, bool value);
        public static void SetSearchCategory(DependencyObject element, string value);
        public static void SetSearchPathSegment(DependencyObject element, string value);
        public static void SetSearchTags(DependencyObject element, string value);
    }
}

