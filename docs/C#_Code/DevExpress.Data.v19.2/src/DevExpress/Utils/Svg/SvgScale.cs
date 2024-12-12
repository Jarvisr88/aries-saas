namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    [SvgTransformNameAlias("scale")]
    public sealed class SvgScale : SvgTransform
    {
        public SvgScale(double[] data) : base(data)
        {
        }

        public override SvgTransform DeepCopy() => 
            new SvgScale(base.Data);

        public override Matrix GetMatrix(double scale) => 
            MatrixHelper.CreateScaleMatrix(this.X, this.Y);

        protected override void Initialize(double[] data)
        {
            if ((data.Length != 0) && (data.Length <= 2))
            {
                this.X = data[0];
                this.Y = data[0];
                if (data.Length > 1)
                {
                    this.Y = data[1];
                }
            }
        }

        public override string ToString()
        {
            if (this.X == this.Y)
            {
                object[] objArray1 = new object[] { this.X };
                return string.Format(CultureInfo.InvariantCulture, "scale({0})", objArray1);
            }
            object[] args = new object[] { this.X, this.Y };
            return string.Format(CultureInfo.InvariantCulture, "scale({0}, {1})", args);
        }

        public double X { get; private set; }

        public double Y { get; private set; }
    }
}

