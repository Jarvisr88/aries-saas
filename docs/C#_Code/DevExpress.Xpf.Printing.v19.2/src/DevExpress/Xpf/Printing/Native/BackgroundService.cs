namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting.Preview;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    internal class BackgroundService : IBackgroundService
    {
        private readonly Dispatcher dispatcher;

        public BackgroundService() : this(Dispatcher.CurrentDispatcher)
        {
        }

        public BackgroundService(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public void PerformAction()
        {
            Action callback = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Action local1 = <>c.<>9__3_0;
                callback = <>c.<>9__3_0 = delegate {
                };
            }
            this.dispatcher.Invoke(callback, DispatcherPriority.Background);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BackgroundService.<>c <>9 = new BackgroundService.<>c();
            public static Action <>9__3_0;

            internal void <PerformAction>b__3_0()
            {
            }
        }
    }
}

