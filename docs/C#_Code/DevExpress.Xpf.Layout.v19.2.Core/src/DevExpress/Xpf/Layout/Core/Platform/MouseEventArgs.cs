namespace DevExpress.Xpf.Layout.Core.Platform
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [DebuggerStepThrough]
    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(System.Windows.Point point, MouseButtons buttons) : this(point, buttons, MouseButtons.None)
        {
        }

        public MouseEventArgs(System.Windows.Point point, MouseButtons buttons, MouseButtons changed)
        {
            this.Point = point;
            this.Buttons = buttons;
            this.ChangedButtons = changed;
        }

        public System.Windows.Point Point { get; private set; }

        public MouseButtons Buttons { get; private set; }

        public MouseButtons ChangedButtons { get; private set; }

        public bool Handled { get; set; }

        public object OriginalEvent { get; set; }
    }
}

