namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class ReraiseEventHelper
    {
        public static MouseButtonEventArgs CloneMouseButtonEventArgs(MouseButtonEventArgs eventArgs) => 
            new MouseButtonEventArgs(eventArgs.MouseDevice, eventArgs.Timestamp, eventArgs.ChangedButton, eventArgs.StylusDevice);

        public static MouseEventArgs CloneMouseEventArgs(MouseEventArgs eventArgs) => 
            new MouseEventArgs(eventArgs.MouseDevice, eventArgs.Timestamp, eventArgs.StylusDevice);

        public static void ReraiseEvent<T>(T sourceEventArgs, UIElement element, RoutedEvent tunnelingEvent, RoutedEvent bubblingEvent, Func<T, T> cloneFunc) where T: RoutedEventArgs
        {
            if (element != null)
            {
                T e = cloneFunc(sourceEventArgs);
                e.RoutedEvent = tunnelingEvent;
                element.RaiseEvent(e);
                if (!e.Handled)
                {
                    e = cloneFunc(sourceEventArgs);
                    e.RoutedEvent = bubblingEvent;
                    element.RaiseEvent(e);
                }
            }
        }
    }
}

