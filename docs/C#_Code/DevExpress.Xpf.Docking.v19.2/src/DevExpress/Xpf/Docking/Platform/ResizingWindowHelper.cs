namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ResizingWindowHelper : IDisposable
    {
        private UIElement Container;
        private ResizingOverlayWindow windowCore;
        private bool isDisposing;
        private FloatingMode Mode;
        private ShadowResizeAdorner Adorner;
        private int windowShowCount;

        public ResizingWindowHelper(LayoutView view)
        {
            this.View = view;
            this.Container = this.View.Container;
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.Container = null;
                Ref.Dispose<ResizingOverlayWindow>(ref this.windowCore);
            }
            GC.SuppressFinalize(this);
        }

        public void HideResizeWindow()
        {
            if (this.Mode != FloatingMode.Adorner)
            {
                if (this.windowCore != null)
                {
                    this.Window.Hide();
                    this.windowShowCount--;
                }
            }
            else
            {
                if (this.Adorner != null)
                {
                    this.Adorner.EndResizing();
                    this.Adorner.Update(false);
                }
                if (this.View != null)
                {
                    this.View.AdornerHelper.TryHideAdornerWindow();
                }
                this.Adorner = null;
            }
        }

        internal void Reset()
        {
            if (this.windowShown)
            {
                this.HideResizeWindow();
            }
        }

        private void ShowResizeOverlayWindow(UIElement content, Rect initialBounds, UIElement owner)
        {
            this.Window.ShowContentInBounds(content, initialBounds);
            this.windowShowCount++;
        }

        public void ShowResizeWindow(UIElement Resizer, Rect InitialBounds, FloatingMode mode)
        {
            this.Mode = mode;
            if (this.Mode != FloatingMode.Adorner)
            {
                this.ShowResizeOverlayWindow(Resizer, InitialBounds, this.View.Container);
            }
            else if (this.View != null)
            {
                this.View.AdornerHelper.TryShowAdornerWindow(true);
                this.Adorner = this.View.AdornerHelper.GetShadowResizeAdorner();
                if (this.Adorner != null)
                {
                    this.Adorner.StartResizing(Resizer, InitialBounds);
                    this.Adorner.Update(true);
                }
            }
        }

        public void UpdateBackground(Rect bounds)
        {
            if (this.Adorner != null)
            {
                this.Adorner.ShowBackground(bounds);
            }
        }

        public void UpdateResizeWindow(Rect bounds1)
        {
            if (this.Mode != FloatingMode.Adorner)
            {
                this.UpdateWindowBounds(bounds1);
            }
            else if (this.Adorner != null)
            {
                this.Adorner.Resize(bounds1);
            }
        }

        private void UpdateWindowBounds(Rect bounds)
        {
            this.Window.UpdateBounds(bounds);
        }

        public LayoutView View { get; private set; }

        private ResizingOverlayWindow Window
        {
            get
            {
                this.windowCore ??= new ResizingOverlayWindow(this.Container);
                return this.windowCore;
            }
        }

        private bool windowShown =>
            this.windowShowCount > 0;
    }
}

