namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Threading;

    public static class BackgroundHelper
    {
        public static void DoInBackground(Action backgroundAction, Action mainThreadAction);
        public static void DoInBackground(Action backgroundAction, Action mainThreadAction, int milliseconds, ThreadPriority threadPriority = 0, ApartmentState apartmentState = 1);
        public static void DoWithDispatcher(Dispatcher dispatcher, Action action);
    }
}

