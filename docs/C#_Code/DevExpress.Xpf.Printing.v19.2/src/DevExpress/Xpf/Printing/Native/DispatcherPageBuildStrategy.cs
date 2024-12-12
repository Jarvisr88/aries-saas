namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Threading;

    public class DispatcherPageBuildStrategy : BackgroundPageBuildEngineStrategy
    {
        private int subscribersCount;

        private event EventHandler tick;

        public override event EventHandler Tick
        {
            add
            {
                this.tick += value;
                this.subscribersCount++;
                this.InvokeOnIdle();
            }
            remove
            {
                this.tick -= value;
                if (this.subscribersCount > 0)
                {
                    this.subscribersCount--;
                }
            }
        }

        public override void BeginInvoke(Action0 method)
        {
            method();
        }

        private void InvokeOnIdle()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(this.OnIdle), DispatcherPriority.ApplicationIdle, new object[0]);
        }

        private void OnIdle()
        {
            if (this.subscribersCount > 0)
            {
                if (this.tick != null)
                {
                    this.tick(this, null);
                }
                this.InvokeOnIdle();
            }
        }
    }
}

