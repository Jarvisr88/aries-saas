namespace DevExpress.Xpf.Core.HandleDecorator
{
    using DevExpress.Xpf.Core.HandleDecorator.Helpers;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Forms;

    public abstract class HandleDecoratorWindow : LayeredWindowBase
    {
        private const int resizeDelayDurationMs = 150;
        private bool isVisible;
        private bool shouldUpdate;
        private bool isActiveCore;
        private bool firstAppearance = true;
        private bool fDisposed;
        private Timer resizeTimer;
        private Decorator ownerCore;
        private ThemeElementPainter painter;

        public HandleDecoratorWindow(Decorator owner, bool startupActiveState)
        {
            this.ownerCore = owner;
            base.hWndParent = NativeHelper.GetHandle(this.ownerCore.Control);
            this.isActiveCore = startupActiveState;
            this.BmpManager = this.CreateBitmapManager();
            this.painter = this.CreatePainter(this.ownerCore);
            Timer timer1 = new Timer();
            timer1.Interval = 150;
            this.resizeTimer = timer1;
            this.resizeTimer.Tick += new EventHandler(this.resizeTimer_Tick);
        }

        public void CommitChanges(bool updateVisibility, bool resizeDelay)
        {
            if (updateVisibility)
            {
                this.UpdateWindowPosCore(true);
            }
            if (this.IsVisible && ((this.BmpManager.CachedBmpSize != this.DecoratorWindowRect.Size) || this.shouldUpdate))
            {
                this.RenderDecoratorWindow(resizeDelay);
            }
        }

        protected virtual BitmapManager CreateBitmapManager() => 
            new BitmapManager();

        private ThemeElementPainter CreatePainter(Decorator owner) => 
            new ThemeElementPainter(owner);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.BmpManager != null)
                {
                    this.BmpManager.Dispose();
                }
                if (this.resizeTimer != null)
                {
                    this.resizeTimer.Tick -= new EventHandler(this.resizeTimer_Tick);
                    this.resizeTimer.Dispose();
                }
                this.fDisposed = true;
            }
            base.Dispose(disposing);
        }

        private void Draw(IntPtr windowDC)
        {
            Bitmap bitmap = null;
            Rectangle leftBorder = HandleDecoratorWindowLayoutCalculator.Calculate(HandleDecoratorWindowTypes.Left, this.TargetCtrlRect, this.painter);
            bitmap = this.GetBitmap(new CompositeBitmapAttributes(this.DecoratorWindowRect, leftBorder, HandleDecoratorWindowLayoutCalculator.Calculate(HandleDecoratorWindowTypes.Top, this.TargetCtrlRect, this.painter), HandleDecoratorWindowLayoutCalculator.Calculate(HandleDecoratorWindowTypes.Right, this.TargetCtrlRect, this.painter), HandleDecoratorWindowLayoutCalculator.Calculate(HandleDecoratorWindowTypes.Bottom, this.TargetCtrlRect, this.painter), this.painter, this.IsActive));
            if (bitmap != null)
            {
                IntPtr zero = IntPtr.Zero;
                try
                {
                    zero = bitmap.GetHbitmap(Color.Empty);
                    DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SelectObject(windowDC, zero);
                }
                catch (Exception)
                {
                }
                finally
                {
                    DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.DeleteObject(zero);
                    bitmap = null;
                }
            }
        }

        private void ExcludeRegion()
        {
            if (this.ShouldExlcudeRegion)
            {
                Rectangle decoratorWindowRect = this.DecoratorWindowRect;
                using (Region region = new Region(new Rectangle(new Point(0, 0), decoratorWindowRect.Size)))
                {
                    int width = HandleDecoratorWindowLayoutCalculator.Calculate(HandleDecoratorWindowTypes.Left, this.TargetCtrlRect, this.painter).Width;
                    Rectangle rect = new Rectangle(width, width, this.DecoratorWindowRect.Width - (2 * width), this.DecoratorWindowRect.Height - (2 * width));
                    region.Exclude(rect);
                    using (Graphics graphics = Graphics.FromHwndInternal(base.Handle))
                    {
                        int num4 = DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SetWindowRgn(base.Handle, region.GetHrgn(graphics), true);
                    }
                }
            }
        }

        protected virtual Bitmap GetBitmap(CompositeBitmapAttributes attr) => 
            this.BmpManager?.GetCompositeBitmap(attr);

        internal ThemeElementPainter GetPainter()
        {
            this.painter ??= this.CreatePainter(this.ownerCore);
            return this.painter;
        }

        public void HideWnd()
        {
            int uFlags = 0x293;
            DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SetWindowPos(base.Handle, base.hWndParent, this.Left, this.Top, 0, 0, uFlags);
        }

        internal void RenderDecoratorWindow(bool resizeDelay)
        {
            if (resizeDelay || this.resizeTimer.Enabled)
            {
                this.HideWnd();
                this.RestartResizeTimer();
            }
            else
            {
                this.shouldUpdate = false;
                IntPtr dC = (IntPtr) DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetDC(IntPtr.Zero);
                if (dC != IntPtr.Zero)
                {
                    IntPtr windowDC = DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.CreateCompatibleDC(dC);
                    if (windowDC != IntPtr.Zero)
                    {
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.BLENDFUNCTION pBlend = new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.BLENDFUNCTION {
                            BlendOp = 0,
                            BlendFlags = 0,
                            SourceConstantAlpha = 0xff,
                            AlphaFormat = 1
                        };
                        this.Draw(windowDC);
                        this.UpdateWindowPos(this.TargetCtrlRect);
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT point3 = new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT {
                            X = this.Left,
                            Y = this.Top
                        };
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT pptDst = point3;
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SIZE pSizeDst = new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SIZE {
                            Width = this.Width,
                            Height = this.Height
                        };
                        point3 = new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT {
                            X = 0,
                            Y = 0
                        };
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.POINT pptSrc = point3;
                        this.ExcludeRegion();
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.UpdateLayeredWindow(base.Handle, dC, ref pptDst, ref pSizeDst, windowDC, ref pptSrc, 0, ref pBlend, 2);
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.DeleteObject(windowDC);
                        DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.DeleteObject(dC);
                    }
                }
            }
        }

        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            if (!this.fDisposed)
            {
                this.resizeTimer.Stop();
                this.RenderDecoratorWindow(false);
            }
        }

        private void RestartResizeTimer()
        {
            this.resizeTimer.Stop();
            this.resizeTimer.Start();
        }

        public void UpdateWindowPos(Rectangle targetCtrlRect)
        {
            this.TargetCtrlRect = targetCtrlRect;
            this.painter.CalculateAndSetScaleFactor(this.TargetCtrlRect.Size);
            this.DecoratorWindowRect = HandleDecoratorWindowLayoutCalculator.Calculate(HandleDecoratorWindowTypes.Composite, this.TargetCtrlRect, this.painter);
            if (!this.resizeTimer.Enabled)
            {
                this.UpdateWindowPosCore();
            }
        }

        private void UpdateWindowPosCore()
        {
            this.UpdateWindowPosCore(false);
        }

        private void UpdateWindowPosCore(bool showWindow)
        {
            int uFlags = 0x611;
            if (this.BmpManager.CachedBmpSize != this.DecoratorWindowRect.Size)
            {
                uFlags |= 2;
            }
            if (!(((!this.IsVisible || (this.BmpManager.CachedBmpSize == Size.Empty)) ? this.firstAppearance : true) | showWindow))
            {
                uFlags |= 0x80;
            }
            else
            {
                uFlags |= 0x40;
                this.firstAppearance = false;
            }
            DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SetWindowPos(base.Handle, base.hWndParent, this.Left, this.Top, 0, 0, uFlags);
        }

        public void UpdateZOrder()
        {
            if (DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetWindow(base.Handle, 3) != base.hWndParent)
            {
                DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.SetWindowPos(base.Handle, base.hWndParent, 0, 0, 0, 0, 0x13);
            }
        }

        [SecuritySafeCritical]
        protected override unsafe void WndProc(ref Message m)
        {
            int msg = m.Msg;
            if (msg <= 70)
            {
                if (msg == 6)
                {
                    m.Result = IntPtr.Zero;
                }
                else if (msg == 70)
                {
                    DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WINDOWPOS structure = (DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WINDOWPOS) Marshal.PtrToStructure(m.LParam, typeof(DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WINDOWPOS));
                    int* numPtr1 = &structure.flags;
                    numPtr1[0] |= 0x10;
                    Marshal.StructureToPtr<DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WINDOWPOS>(structure, m.LParam, true);
                }
            }
            else if (msg == 0x47)
            {
                this.UpdateZOrder();
            }
            else if (msg != 0x7e)
            {
                if (msg == 0x84)
                {
                    m.Result = new IntPtr(-1);
                }
            }
            else if (this.IsVisible)
            {
                this.RenderDecoratorWindow(false);
            }
            base.WndProc(ref m);
        }

        public BitmapManager BmpManager { get; private set; }

        public bool IsVisible
        {
            get => 
                this.isVisible;
            set
            {
                if (this.IsVisible != value)
                {
                    this.isVisible = value;
                    this.shouldUpdate = true;
                }
            }
        }

        public bool IsActive
        {
            get => 
                this.isActiveCore;
            set
            {
                if (this.isActiveCore != value)
                {
                    this.isActiveCore = value;
                    if (this.IsVisible)
                    {
                        this.RenderDecoratorWindow(false);
                    }
                }
            }
        }

        protected Rectangle TargetCtrlRect { get; set; }

        protected Rectangle DecoratorWindowRect { get; set; }

        public int Left =>
            this.DecoratorWindowRect.Left;

        public int Top =>
            this.DecoratorWindowRect.Top;

        public int Width =>
            this.DecoratorWindowRect.Width;

        public int Height =>
            this.DecoratorWindowRect.Height;

        private bool ShouldExlcudeRegion =>
            (this.DecoratorWindowRect.Size.Width != 0) && (this.DecoratorWindowRect.Size.Height != 0);
    }
}

