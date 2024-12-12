namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows.Threading;

    internal class Win32AdornerWindowProvider : IDisposable
    {
        private DispatcherTimer _HideTimer;
        private AdornerWindow adornerWindowCore;
        private bool isDisposing;
        private bool fCancelHiding;

        public Win32AdornerWindowProvider()
        {
            this.HideTimer.Tick += new EventHandler(this.OnHideTimerTick);
        }

        public void CancelHideAdornerWindow()
        {
            this.fCancelHiding = true;
            this.HideTimer.Stop();
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.HideTimer.Stop();
                this.HideTimer.Tick -= new EventHandler(this.OnHideTimerTick);
                Ref.Dispose<AdornerWindow>(ref this.adornerWindowCore);
            }
            GC.SuppressFinalize(this);
        }

        public void EnqueueHideAdornerWindow()
        {
            this.fCancelHiding = false;
            this.HideTimer.Start();
        }

        public AdornerWindow GetWindow(IAdornerWindowClient client, DockLayoutManager manager)
        {
            if ((this.adornerWindowCore != null) && !this.adornerWindowCore.IsDisposing)
            {
                this.adornerWindowCore.Client = client;
            }
            else
            {
                this.adornerWindowCore = new AdornerWindow(client, manager);
            }
            return this.adornerWindowCore;
        }

        public void HideAdornerWindow()
        {
            if (this.adornerWindowCore != null)
            {
                this.adornerWindowCore.IsOpen = false;
            }
        }

        private void OnHideTimerTick(object sender, EventArgs e)
        {
            this.HideTimer.Stop();
            if (!this.fCancelHiding)
            {
                this.HideAdornerWindow();
            }
        }

        private DispatcherTimer HideTimer
        {
            get
            {
                this._HideTimer ??= InvokeHelper.GetBackgroundTimer(180.0);
                return this._HideTimer;
            }
        }
    }
}

