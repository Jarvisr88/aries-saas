namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    internal class ThemeChangedEventManager : BaseWeakEventManager<ThemeChangedEventManager>
    {
        public static void AddListener(UIElement source, IWeakEventListener listener)
        {
            GetManager().ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(UIElement source, IWeakEventListener listener)
        {
            GetManager().ProtectedRemoveListener(source, listener);
        }

        protected override void StartListening(object source)
        {
            if (source is DependencyObject)
            {
                ThemeManager.AddThemeChangedHandler((DependencyObject) source, new ThemeChangedRoutedEventHandler(this.ThemeManager_ThemeChanged));
                ThemeManager.AddThemeChangingHandler((DependencyObject) source, new ThemeChangingRoutedEventHandler(this.ThemeManager_ThemeChanging));
            }
        }

        protected override void StopListening(object source)
        {
            if (source is DependencyObject)
            {
                ThemeManager.RemoveThemeChangedHandler((DependencyObject) source, new ThemeChangedRoutedEventHandler(this.ThemeManager_ThemeChanged));
                ThemeManager.RemoveThemeChangingHandler((DependencyObject) source, new ThemeChangingRoutedEventHandler(this.ThemeManager_ThemeChanging));
            }
        }

        private void ThemeManager_ThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            DockLayoutManager manager = sender as DockLayoutManager;
            if (manager != null)
            {
                manager.OnThemeChanged(e);
            }
        }

        private void ThemeManager_ThemeChanging(DependencyObject sender, ThemeChangingRoutedEventArgs e)
        {
            DockLayoutManager manager = sender as DockLayoutManager;
            if (manager != null)
            {
                manager.OnThemeChanging(e);
            }
        }
    }
}

