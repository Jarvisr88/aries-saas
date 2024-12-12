namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;

    internal class ResizingOverlayWindow : Window, IDisposable
    {
        private DockLayoutManager manager;
        private MatrixTransform transform;
        private bool isDisposingCore;

        public ResizingOverlayWindow()
        {
            base.ResizeMode = ResizeMode.NoResize;
            base.WindowStyle = WindowStyle.None;
            base.ShowInTaskbar = false;
            base.AllowsTransparency = true;
            base.Background = null;
            base.ShowActivated = false;
            base.IsHitTestVisible = false;
        }

        public ResizingOverlayWindow(UIElement container)
        {
            base.ResizeMode = ResizeMode.NoResize;
            base.WindowStyle = WindowStyle.None;
            base.ShowInTaskbar = false;
            base.AllowsTransparency = true;
            base.Background = null;
            base.ShowActivated = false;
            base.IsHitTestVisible = false;
            Window window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(container);
            FrameworkElement ancestor = window;
            this.manager = DockLayoutManager.GetDockLayoutManager(container);
            if (this.manager.OwnsFloatWindows)
            {
                base.Owner = window;
            }
            if ((ancestor == null) || !this.manager.IsDescendantOf(ancestor))
            {
                ancestor = LayoutHelper.GetTopLevelVisual(this.manager);
            }
            if (ancestor != null)
            {
                this.transform = this.manager.TransformToVisual(ancestor) as MatrixTransform;
                if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
                {
                    Matrix matrix = this.transform.Matrix;
                    this.transform = new MatrixTransform(Math.Abs(matrix.M11), matrix.M12, matrix.M21, Math.Abs(matrix.M22), 0.0, 0.0);
                }
            }
            ContentControlHelper.SetContentIsNotLogical(this, true);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            (oldContent as UIElement).Do<UIElement>(x => this.manager.DockHintsContainer.Remove(x));
            (newContent as UIElement).Do<UIElement>(x => this.manager.DockHintsContainer.Add(x));
        }

        internal void ShowContentInBounds(UIElement element, Rect bounds)
        {
            base.Content = element;
            this.UpdateBounds(bounds);
            base.Show();
        }

        void IDisposable.Dispose()
        {
            if (!this.isDisposingCore)
            {
                this.isDisposingCore = true;
                base.Content = null;
                base.Close();
            }
            GC.SuppressFinalize(this);
        }

        public void UpdateBounds(Rect bounds)
        {
            if (PresentationSource.FromVisual(this) != null)
            {
                bounds.Transform(PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice);
            }
            if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
            {
                Point point = this.transform.Transform(new Point(bounds.Width, bounds.Height));
                bounds.Width = point.X;
                bounds.Height = point.Y;
            }
            HwndSource source2 = PresentationSource.FromVisual(this) as HwndSource;
            if (source2 != null)
            {
                NativeHelper.SetWindowPosSafe(source2.Handle, IntPtr.Zero, (int) bounds.X, (int) bounds.Y, (int) bounds.Width, (int) bounds.Height, 0x54);
            }
            else
            {
                base.Left = bounds.X;
                base.Top = bounds.Y;
                base.Width = bounds.Width;
                base.Height = bounds.Height;
            }
        }
    }
}

