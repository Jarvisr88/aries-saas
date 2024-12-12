namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Threading;

    public static class DispatcherHelper
    {
        public static void DoEvents(int count = 1)
        {
            DoEvents(DispatcherPriority.Background, count);
        }

        public static void DoEvents(DispatcherPriority priority, int count = 1)
        {
            int num = count;
            while (--num >= 0)
            {
                DispatcherFrame arg = new DispatcherFrame();
                Dispatcher.CurrentDispatcher.BeginInvoke(priority, new DispatcherOperationCallback(DispatcherHelper.ExitFrame), arg);
                if (!arg.Dispatcher.HasShutdownFinished)
                {
                    Dispatcher.PushFrame(arg);
                }
            }
        }

        private static object ExitFrame(object f)
        {
            ((DispatcherFrame) f).Continue = false;
            return null;
        }

        private static void UpdateLayout()
        {
        }

        public static void UpdateLayoutAndDoEvents()
        {
            UpdateLayoutAndDoEvents(null);
        }

        public static void UpdateLayoutAndDoEvents(UIElement element)
        {
            UpdateLayoutAndDoEvents(element, DispatcherPriority.Background);
        }

        public static void UpdateLayoutAndDoEvents(UIElement element, DispatcherPriority priority)
        {
            if (element == null)
            {
                UpdateLayout();
            }
            else
            {
                element.UpdateLayout();
            }
            DoEvents(priority, 1);
        }
    }
}

