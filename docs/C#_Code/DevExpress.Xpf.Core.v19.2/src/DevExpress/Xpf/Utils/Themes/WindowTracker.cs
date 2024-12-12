namespace DevExpress.Xpf.Utils.Themes
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    internal static class WindowTracker
    {
        static WindowTracker()
        {
            ContentControl.ContentProperty.AddPropertyChangedCallback(typeof(Window), (d, e) => DoWork(d));
            EventManager.RegisterClassHandler(typeof(Window), FrameworkElement.LoadedEvent, new RoutedEventHandler(WindowTracker.OnWindowLoaded));
            EventManager.RegisterClassHandler(typeof(AdornerDecorator), FrameworkElement.LoadedEvent, new RoutedEventHandler(WindowTracker.OnAdornerLoaded));
            foreach (PresentationSource source in (from x in PresentationSource.CurrentSources.OfType<PresentationSource>()
                where x.CheckAccess()
                select x).ToList<PresentationSource>())
            {
                Window rootVisual = source.RootVisual as Window;
                if ((rootVisual != null) && rootVisual.Dispatcher.CheckAccess())
                {
                    DoWork(rootVisual);
                }
            }
        }

        private static void DoWork(object sender)
        {
            Window window = (Window) sender;
            GlobalThemeHelper.Instance.AssignApplicationThemeName(window);
            BarNameScope.EnsureRegistrator(window);
        }

        internal static void Initialize()
        {
        }

        private static void OnAdornerLoaded(object sender, RoutedEventArgs e)
        {
            BarNameScope.EnsureRegistrator(sender as DependencyObject);
        }

        private static void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            DoWork(sender);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowTracker.<>c <>9 = new WindowTracker.<>c();

            internal void <.cctor>b__0_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                WindowTracker.DoWork(d);
            }

            internal bool <.cctor>b__0_1(PresentationSource x) => 
                x.CheckAccess();
        }
    }
}

