namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class MouseWheelEventArgsEx : MouseWheelEventArgs
    {
        public MouseWheelEventArgsEx(MouseDevice mouse, int timestamp, int delta) : base(mouse, timestamp, delta)
        {
        }

        public MouseWheelEventArgsEx(MouseDevice mouse, int timestamp, int deltaX, int deltaY) : this(mouse, timestamp, deltaY)
        {
            this.DeltaX = deltaX;
        }

        public int DeltaX { get; private set; }

        public int DeltaY =>
            base.Delta;
    }
}

