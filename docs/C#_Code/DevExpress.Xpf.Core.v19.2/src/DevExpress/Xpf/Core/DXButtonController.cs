namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class DXButtonController : ControlControllerBase
    {
        public event RoutedEventHandler Click;

        public DXButtonController(IControl control) : base(control)
        {
            base.CaptureMouseOnDown = true;
        }

        protected virtual void OnClick()
        {
            if (this.Click != null)
            {
                this.Click(base.Control, new RoutedEventArgs());
            }
        }

        protected override void OnMouseLeftButtonDown(DXMouseButtonEventArgs e)
        {
            if (this.ClickOnMouseDown)
            {
                this.OnClick();
                e.Handled = true;
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(DXMouseButtonEventArgs e)
        {
            bool flag = (base.IsMouseLeftButtonDown && base.IsMouseEntered) && !this.ClickOnMouseDown;
            base.OnMouseLeftButtonUp(e);
            if (flag)
            {
                this.OnClick();
                if (e != null)
                {
                    e.Handled = true;
                }
            }
        }

        protected bool ClickOnMouseDown { get; set; }
    }
}

