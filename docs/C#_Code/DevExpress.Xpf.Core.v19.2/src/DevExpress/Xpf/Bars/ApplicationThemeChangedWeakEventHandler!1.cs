namespace DevExpress.Xpf.Bars
{
    using DevExpress.Data.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class ApplicationThemeChangedWeakEventHandler<T> : WeakEventHandler<T, ThemeChangedRoutedEventArgs, ThemeChangedRoutedEventHandler> where T: class
    {
        private static Action<WeakEventHandler<T, ThemeChangedRoutedEventArgs, ThemeChangedRoutedEventHandler>, object> onDetachAction;
        private static Func<WeakEventHandler<T, ThemeChangedRoutedEventArgs, ThemeChangedRoutedEventHandler>, ThemeChangedRoutedEventHandler> createHandlerFunction;

        static ApplicationThemeChangedWeakEventHandler();
        public ApplicationThemeChangedWeakEventHandler(T owner, Action<T, object, ThemeChangedRoutedEventArgs> onEventAction);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ApplicationThemeChangedWeakEventHandler<T>.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(WeakEventHandler<T, ThemeChangedRoutedEventArgs, ThemeChangedRoutedEventHandler> h, object _);
            internal ThemeChangedRoutedEventHandler <.cctor>b__3_1(WeakEventHandler<T, ThemeChangedRoutedEventArgs, ThemeChangedRoutedEventHandler> h);
        }
    }
}

