namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;

    public class DXMouseEventArgs : EventArgs
    {
        public DXMouseEventArgs()
        {
        }

        public DXMouseEventArgs(MouseEventArgs args)
        {
            this.OriginalSource = args.OriginalSource;
            this.StylusDevice = args.StylusDevice;
            this.RelativePositionElement = null;
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                this.RelativePosition = args.GetPosition(null);
            }
            else
            {
                PresentationSource source = PresentationSource.FromDependencyObject((DependencyObject) this.OriginalSource);
                if ((source != null) && ((source.CompositionTarget != null) && this.IsValidArgs(args)))
                {
                    this.RelativePosition = args.GetPosition((IInputElement) source.RootVisual);
                    this.RelativePosition = source.RootVisual.PointToScreen(this.RelativePosition);
                }
            }
        }

        public Point GetPosition(UIElement relativeTo) => 
            !ReferenceEquals(relativeTo, this.RelativePositionElement) ? ((this.RelativePositionElement == null) ? ((FrameworkElement) relativeTo).MapPointFromScreen(this.RelativePosition) : ((relativeTo == null) ? this.RelativePositionElement.PointToScreen(this.RelativePosition) : this.RelativePositionElement.TranslatePoint(this.RelativePosition, relativeTo))) : this.RelativePosition;

        private bool IsValidArgs(MouseEventArgs args)
        {
            bool flag = this.IsValidArgsCore(args);
            if (!flag)
            {
                args.Handled = true;
            }
            return flag;
        }

        private bool IsValidArgsCore(MouseEventArgs args)
        {
            if (!IsValidHwndSource(PresentationSource.FromDependencyObject((DependencyObject) this.OriginalSource)))
            {
                return false;
            }
            MouseDevice device = args.Device as MouseDevice;
            return ((device != null) ? IsValidHwndSource(device.ActiveSource) : true);
        }

        private static bool IsValidHwndSource(PresentationSource ps)
        {
            HwndSource source = ps as HwndSource;
            return ((source != null) ? !(source.Handle == IntPtr.Zero) : true);
        }

        public object OriginalSource { get; protected set; }

        public System.Windows.Input.StylusDevice StylusDevice { get; protected set; }

        protected Point RelativePosition { get; set; }

        protected UIElement RelativePositionElement { get; set; }
    }
}

