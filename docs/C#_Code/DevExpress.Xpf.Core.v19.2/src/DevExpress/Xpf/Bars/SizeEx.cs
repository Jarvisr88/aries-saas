namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SizeEx
    {
        public SizeEx();
        public SizeEx(double w, double h);
        public static implicit operator Size(SizeEx second);

        public double Width { get; set; }

        public double Height { get; set; }
    }
}

