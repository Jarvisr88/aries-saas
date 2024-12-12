namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public abstract class SvgSkew : SvgTransform
    {
        public SvgSkew(double[] data) : base(data)
        {
        }

        public override Matrix GetMatrix(double scale) => 
            MatrixHelper.CreateSkewMatrix(this.AngleX, this.AngleY, scale);

        public override string ToString()
        {
            if (this.AngleY == 0.0)
            {
                object[] objArray1 = new object[] { this.AngleX };
                return string.Format(CultureInfo.InvariantCulture, "skewX({0})", objArray1);
            }
            object[] args = new object[] { this.AngleY };
            return string.Format(CultureInfo.InvariantCulture, "skewY({0})", args);
        }

        public double AngleX { get; set; }

        public double AngleY { get; set; }
    }
}

