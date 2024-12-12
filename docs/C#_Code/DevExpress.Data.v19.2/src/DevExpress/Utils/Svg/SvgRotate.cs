namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    [SvgTransformNameAlias("rotate")]
    public sealed class SvgRotate : SvgTransform
    {
        public SvgRotate(double[] data) : base(data)
        {
        }

        public override SvgTransform DeepCopy() => 
            new SvgRotate(base.Data);

        public override Matrix GetMatrix(double scale) => 
            MatrixHelper.CreateRotationMatrix(this.Angle, this.CenterX, this.CenterY, scale);

        protected override void Initialize(double[] data)
        {
            if ((data.Length != 1) && (data.Length != 3))
            {
                throw new FormatException("");
            }
            this.Angle = data[0];
            if (data.Length == 3)
            {
                this.CenterX = data[1];
                this.CenterY = data[2];
            }
        }

        public override string ToString()
        {
            object[] args = new object[] { this.Angle, this.CenterX, this.CenterY };
            return string.Format(CultureInfo.InvariantCulture, "rotate({0}, {1}, {2})", args);
        }

        public double Angle { get; private set; }

        public double CenterX { get; private set; }

        public double CenterY { get; private set; }
    }
}

