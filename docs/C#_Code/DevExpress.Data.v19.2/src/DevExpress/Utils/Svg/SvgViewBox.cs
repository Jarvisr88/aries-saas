namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    public class SvgViewBox : ICloneable
    {
        [ThreadStatic]
        private static SvgViewBox emptyCore;

        public SvgViewBox()
        {
        }

        public SvgViewBox(double minX, double minY, double width, double height)
        {
            this.MinX = minX;
            this.MinY = minY;
            this.Width = width;
            this.Height = height;
        }

        public object Clone() => 
            new SvgViewBox(this.MinX, this.MinY, this.Width, this.Height);

        public static SvgViewBox Empty
        {
            get
            {
                emptyCore ??= new SvgViewBox(0.0, 0.0, 0.0, 0.0);
                return emptyCore;
            }
        }

        public double MinX { get; set; }

        public double MinY { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public bool IsEmpty =>
            (this.MinX == 0.0) && ((this.MinY == 0.0) && ((this.Width == 0.0) && (this.Height == 0.0)));
    }
}

