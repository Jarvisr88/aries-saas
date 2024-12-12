namespace DevExpress.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class OfficeMouseWheelEventArgs : MouseEventArgs
    {
        public OfficeMouseWheelEventArgs(MouseButtons buttons, int clicks, int x, int y, int delta) : base(buttons, clicks, x, y, delta)
        {
        }

        public bool IsHorizontal { get; set; }
    }
}

