namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public class ClickHelper
    {
        private const double MinDoubleClickTime = 1000.0;
        private const double MinTouchWidth = 10.0;

        public event EventHandler<InputEventArgs> Click;

        public event EventHandler<InputEventArgs> DoubleClick;

        public ClickHelper(FrameworkElement inputContainer)
        {
            this.InputContainer = inputContainer;
            this.SubcribeInputEvents();
        }

        private void DetectInput(InputEventArgs e, bool isMouse)
        {
            this.InitLastClickPosition();
            if (isMouse ? this.IsDoubleClick() : this.IsDoubleTap())
            {
                this.RaiseDoubleClick(e);
            }
            else
            {
                this.RaiseClick(e);
            }
            this.LastClickPosition = this.CurrentClickPosition;
        }

        private void InitLastClickPosition()
        {
            Point point = new Point();
            if (this.LastClickPosition == point)
            {
                this.LastClickPosition = this.CurrentClickPosition;
                this.LastClickTime = DateTime.Now;
            }
        }

        private bool IsDoubleClick() => 
            this.IsMinTime() && (this.LastClickPosition == this.CurrentClickPosition);

        private bool IsDoubleTap()
        {
            double num2 = Math.Abs((double) (this.CurrentClickPosition.Y - this.LastClickPosition.Y));
            return ((Math.Abs((double) (this.CurrentClickPosition.X - this.LastClickPosition.X)) < 10.0) && (num2 < 10.0));
        }

        private bool IsMinTime() => 
            (DateTime.Now - this.LastClickTime).TotalMilliseconds < 1000.0;

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.CurrentClickPosition = e.GetPosition(this.InputContainer);
            this.DetectInput(e, true);
        }

        private void RaiseClick(InputEventArgs args)
        {
            if (this.Click != null)
            {
                this.Click(this, args);
            }
        }

        private void RaiseDoubleClick(InputEventArgs args)
        {
            if (this.DoubleClick != null)
            {
                this.DoubleClick(this, args);
            }
        }

        private void SubcribeInputEvents()
        {
            this.InputContainer.MouseUp += new MouseButtonEventHandler(this.MouseUp);
            this.InputContainer.TouchUp += new EventHandler<TouchEventArgs>(this.TouchUp);
        }

        private void TouchUp(object sender, TouchEventArgs e)
        {
            this.DetectInput(e, false);
        }

        private Point CurrentClickPosition { get; set; }

        private Point LastClickPosition { get; set; }

        private DateTime LastClickTime { get; set; }

        private FrameworkElement InputContainer { get; set; }
    }
}

