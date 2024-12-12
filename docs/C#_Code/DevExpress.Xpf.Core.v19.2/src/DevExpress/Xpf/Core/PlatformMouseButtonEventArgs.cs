namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PlatformMouseButtonEventArgs : IndependentMouseButtonEventArgs
    {
        protected readonly MouseButtonEventArgs e;

        public PlatformMouseButtonEventArgs(MouseButtonEventArgs e) : base(e.OriginalSource)
        {
            this.e = e;
        }

        public override void CaptureMouse(UIElement element)
        {
            PlatformMouseEventArgs.CaptureMouseCore(element);
        }

        public override Point GetPosition(UIElement relativeTo) => 
            this.e.GetPosition(relativeTo);

        public override void ReleaseMouseCapture(UIElement element)
        {
            PlatformMouseEventArgs.ReleaseMouseCaptureCore(element);
        }

        internal MouseButtonEventArgs OrigninalEventArgs =>
            this.e;

        public override MouseButtonState LeftButton =>
            Mouse.LeftButton;
    }
}

