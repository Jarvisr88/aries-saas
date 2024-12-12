namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Threading;

    internal static class InvokeHelper
    {
        public static void BeginInvoke(DependencyObject dObj, Delegate method)
        {
            if (dObj != null)
            {
                dObj.Dispatcher.BeginInvoke(method, new object[0]);
            }
        }

        public static DispatcherOperation BeginInvoke(DependencyObject dObj, Delegate method, DispatcherPriority priority, params object[] args) => 
            dObj?.Dispatcher.BeginInvoke(method, priority, args);

        public static DispatcherOperation BeginInvoke(DispatcherObject d, Func<bool> condition, Action action, DispatcherPriority priority = 9)
        {
            DispatcherOperation operation = null;
            if (condition())
            {
                operation = d.Dispatcher.BeginInvoke(action, priority, new object[0]);
            }
            else
            {
                action();
            }
            return operation;
        }

        public static DispatcherTimer GetBackgroundTimer(double milliseconds)
        {
            DispatcherTimer timer1 = new DispatcherTimer(DispatcherPriority.Background);
            timer1.Interval = TimeSpan.FromMilliseconds(milliseconds);
            return timer1;
        }
    }
}

