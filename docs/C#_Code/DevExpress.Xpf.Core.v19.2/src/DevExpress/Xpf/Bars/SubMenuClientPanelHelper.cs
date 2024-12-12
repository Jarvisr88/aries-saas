namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public class SubMenuClientPanelHelper
    {
        public static readonly DependencyProperty SkipArrangeProperty;
        public static readonly DependencyProperty RowIndexProperty;
        public static readonly DependencyProperty ColumnIndexProperty;

        static SubMenuClientPanelHelper();
        public static int GetColumnIndex(DependencyObject obj);
        public static int GetRowIndex(DependencyObject obj);
        public static bool GetSkipArrange(DependencyObject obj);
        public static void SetColumnIndex(DependencyObject obj, int value);
        public static void SetRowIndex(DependencyObject obj, int value);
        public static void SetSkipArrange(DependencyObject obj, bool value);
    }
}

