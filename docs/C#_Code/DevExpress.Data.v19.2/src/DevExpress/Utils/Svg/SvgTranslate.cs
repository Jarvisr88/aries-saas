namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    [SvgTransformNameAlias("translate")]
    public sealed class SvgTranslate : SvgTransform
    {
        public SvgTranslate(double[] data) : base(data)
        {
        }

        public override SvgTransform DeepCopy() => 
            new SvgTranslate(base.Data);

        public override Matrix GetMatrix(double scale) => 
            MatrixHelper.CreateTranslateMatrix(this.X, this.Y, scale);

        protected override void Initialize(double[] data)
        {
            if ((data.Length != 0) && (data.Length <= 2))
            {
                this.X = data[0];
                this.Y = 0.0;
                if (data.Length > 1)
                {
                    this.Y = data[1];
                }
            }
        }

        public override string ToString()
        {
            object[] args = new object[] { this.X, this.Y };
            return string.Format(CultureInfo.InvariantCulture, "translate({0}, {1})", args);
        }

        public double X { get; private set; }

        public double Y { get; private set; }
    }
}

