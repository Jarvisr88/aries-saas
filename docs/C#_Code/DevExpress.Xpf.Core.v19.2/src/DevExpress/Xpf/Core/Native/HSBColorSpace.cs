namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class HSBColorSpace
    {
        public static HSBColorSpace FromColor(Color RGBColor);
        public Color ToColor();

        public double H { get; set; }

        public double S { get; set; }

        public double B { get; set; }

        public byte A { get; set; }
    }
}

