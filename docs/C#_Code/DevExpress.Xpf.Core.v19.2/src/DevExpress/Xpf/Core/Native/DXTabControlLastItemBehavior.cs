namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class DXTabControlLastItemBehavior : Behavior<DXTabControl>
    {
        public static readonly DependencyProperty IsEnabledProperty;
        public static readonly DependencyProperty IsLastItemProperty;

        static DXTabControlLastItemBehavior();
        public static bool GetIsEnabled(DXTabControl obj);
        public static bool GetIsLastItem(DXTabItem obj);
        protected override void OnAttached();
        protected override void OnDetaching();
        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private void OnTabControlLoaded(object sender, RoutedEventArgs e);
        private void OnTabHidden(object sender, TabControlTabHiddenEventArgs e);
        private void OnTabItemsChanged(object sender, ItemsChangedEventArgs e);
        private void OnTabItemsChangedCore();
        private void OnTabShown(object sender, TabControlTabShownEventArgs e);
        public static void SetIsEnabled(DXTabControl obj, bool value);
        public static void SetIsLastItem(DXTabItem obj, bool value);
    }
}

