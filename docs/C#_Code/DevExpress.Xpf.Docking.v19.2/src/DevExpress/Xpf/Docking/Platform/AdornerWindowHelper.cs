namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class AdornerWindowHelper : IDisposable
    {
        private bool isDisposing;
        private DevExpress.Xpf.Docking.Platform.AdornerWindow adornerWindowCore;

        public AdornerWindowHelper(LayoutView view, DockLayoutManager container)
        {
            this.View = view;
            this.Container = container;
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                Ref.Dispose<DevExpress.Xpf.Docking.Platform.AdornerWindow>(ref this.adornerWindowCore);
                this.View = null;
                this.Container = null;
            }
            GC.SuppressFinalize(this);
        }

        private bool EnsureAdornerWindow(bool onShow = false)
        {
            bool flag = (this.Container.IsTransparencyDisabled && (this.View is FloatingView)) & onShow;
            if ((((this.adornerWindowCore == null) || this.adornerWindowCore.IsDisposing) | flag) && (this.Container.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Window))
            {
                this.adornerWindowCore = this.View.GetAdornerWindow();
                DockLayoutManager.SetDockLayoutManager(this.AdornerWindow, this.Container);
            }
            return (this.adornerWindowCore != null);
        }

        public void HideAdornerWindow()
        {
            if (this.AdornerWindow != null)
            {
                this.AdornerWindow.IsOpen = false;
            }
        }

        public void Reset()
        {
            Ref.Dispose<DevExpress.Xpf.Docking.Platform.AdornerWindow>(ref this.adornerWindowCore);
        }

        public void ShowAdornerWindow(bool forceUpdateAdornerBounds = false)
        {
            if (this.EnsureAdornerWindow(true))
            {
                if (!this.AdornerWindow.IsOpen)
                {
                    this.AdornerWindow.IsOpen = true;
                }
                else if (forceUpdateAdornerBounds)
                {
                    this.AdornerWindow.UpdateFloatingBounds(false);
                }
            }
        }

        private UIElement TryGetAdornerWindowRoot() => 
            this.AdornerWindow?.RootElement;

        public DockLayoutManager Container { get; private set; }

        public LayoutView View { get; private set; }

        public DevExpress.Xpf.Docking.Platform.AdornerWindow AdornerWindow =>
            this.adornerWindowCore;
    }
}

