namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public static class ContentControlHelper
    {
        private static readonly Action<ContentControl, bool> setContentInNotLogical;
        private static Action<ContentControl, object> removeLogicalChild;
        private static Action<ContentControl, object> addLogicalChild;
        public static readonly DependencyProperty ContentIsNotLogicalProperty;

        static ContentControlHelper();
        public static bool? GetContentIsNotLogical(ContentControl obj);
        private static void OnContentIsNotLogicalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetContentIsNotLogical(ContentControl obj, bool value);

        private static Action<ContentControl, object> RemoveLogicalChild { get; }

        private static Action<ContentControl, object> AddLogicalChild { get; }
    }
}

