namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PlatformMouseEventArgs : IndependentMouseEventArgs
    {
        protected readonly MouseEventArgs e;

        public PlatformMouseEventArgs(MouseEventArgs e) : base(e.OriginalSource)
        {
            this.e = e;
        }

        public override void CaptureMouse(UIElement element)
        {
            CaptureMouseCore(element);
        }

        internal static void CaptureMouseCore(UIElement element)
        {
            Mouse.Capture(element);
        }

        public override Point GetPosition(UIElement relativeTo) => 
            this.e.GetPosition(relativeTo);

        public override void ReleaseMouseCapture(UIElement element)
        {
            ReleaseMouseCaptureCore(element);
        }

        internal static void ReleaseMouseCaptureCore(UIElement element)
        {
            Mouse.Capture(null);
        }

        public override MouseButtonState LeftButton =>
            Mouse.LeftButton;
    }
}

