namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public static class SearchPanelBehavior
    {
        public static readonly DependencyProperty SearchBoxFocusedProperty = DependencyProperty.RegisterAttached("SearchBoxFocused", typeof(bool), typeof(SearchPanelBehavior), new PropertyMetadata(new PropertyChangedCallback(SearchPanelBehavior.SearchBoxFocusedChanged)));

        public static bool GetSearchBoxFocused(DependencyObject obj) => 
            (bool) obj.GetValue(SearchBoxFocusedProperty);

        private static void SearchBoxFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PrintingSearchPanel panel = d as PrintingSearchPanel;
            if ((panel != null) && Equals(e.NewValue, true))
            {
                Action method = new Action(panel.FocusSearchBox);
                Dispatcher.CurrentDispatcher.BeginInvoke(method, DispatcherPriority.Background, new object[0]);
            }
        }

        public static void SetSearchBoxFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(SearchBoxFocusedProperty, value);
        }
    }
}

