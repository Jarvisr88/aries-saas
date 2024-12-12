namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PointD
    {
        public double X;
        public double Y;
        public PointD(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public PointF ToPointF() => 
            new PointF((float) this.X, (float) this.Y);

        public override string ToString()
        {
            object[] args = new object[] { this.X, this.Y };
            return string.Format(CultureInfo.CurrentCulture, "{{X={0}, Y={1}}}", args);
        }
    }
}

