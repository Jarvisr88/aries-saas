namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class HwndHostEventProvider
    {
        [ThreadStatic]
        private static bool hasListeners;
        [ThreadStatic]
        private static WeakList<EventHandler> hasListenersChanged;
        [ThreadStatic]
        private static WeakList<IHwndHostEventListener> listeners;

        internal static event EventHandler HasListenersChanged;

        public static void OnMouseDown(DependencyObject hwndHost, MouseButtonEventArgs eventArgs);
        private static void RaiseHasListenersChanged();
        public static void Register(IHwndHostEventListener listener);
        public static void Unregister(IHwndHostEventListener listener);

        public static bool HasListeners { get; private set; }
    }
}

