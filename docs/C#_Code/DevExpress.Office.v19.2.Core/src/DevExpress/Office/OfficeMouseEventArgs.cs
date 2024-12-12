namespace DevExpress.Office
{
    using System;
    using System.Windows.Forms;

    public class OfficeMouseEventArgs : HandledMouseEventArgs
    {
        private bool horizontal;

        public OfficeMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta) : this(button, clicks, x, y, delta, false)
        {
        }

        public OfficeMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, bool horizontal) : base(button, clicks, x, y, delta)
        {
            this.horizontal = horizontal;
        }

        public static OfficeMouseEventArgs Convert(MouseEventArgs e)
        {
            OfficeMouseEventArgs args = e as OfficeMouseEventArgs;
            return new OfficeMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta);
        }

        public bool Horizontal =>
            this.horizontal;
    }
}

