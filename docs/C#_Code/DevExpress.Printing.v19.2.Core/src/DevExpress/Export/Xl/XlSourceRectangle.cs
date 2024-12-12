namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct XlSourceRectangle
    {
        private readonly double left;
        private readonly double top;
        private readonly double right;
        private readonly double bottom;
        public XlSourceRectangle(double left, double top, double right, double bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public double Left =>
            this.left;
        public double Top =>
            this.top;
        public double Right =>
            this.right;
        public double Bottom =>
            this.bottom;
        public bool IsDefault =>
            (this.left == 0.0) && ((this.top == 0.0) && ((this.right == 0.0) && (this.bottom == 0.0)));
    }
}

