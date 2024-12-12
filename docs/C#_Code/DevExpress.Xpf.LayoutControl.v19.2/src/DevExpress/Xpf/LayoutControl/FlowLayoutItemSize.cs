namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FlowLayoutItemSize
    {
        public double Width;
        public double Length;
        public FlowLayoutItemSize(double width, double length)
        {
            this.Width = width;
            this.Length = length;
        }
    }
}

