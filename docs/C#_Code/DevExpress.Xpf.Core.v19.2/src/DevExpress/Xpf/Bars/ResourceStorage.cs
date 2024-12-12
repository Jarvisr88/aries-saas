namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ResourceStorage
    {
        private static readonly WeakList<EventHandler> handlersThemeChanged;
        [ThreadStatic]
        private static Dictionary<ThemeKeyExtensionGeneric, object> resources;

        public static event EventHandler ThemeChanged;

        static ResourceStorage();
        public static void AddResource(ThemeKeyExtensionGeneric key, object resource);
        private static void OnThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e);
        private static void RaiseThemeChanged(EventArgs args);
        public static object TryFindResourceInStorage(this FrameworkElement element, ThemeKeyExtensionGeneric key);
        public static object TryGetResource(ThemeKeyExtensionGeneric key);

        public static bool UseResourceStorage { get; set; }

        private static Dictionary<ThemeKeyExtensionGeneric, object> Resources { get; }
    }
}

