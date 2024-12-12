namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    [SvgTransformNameAlias("matrix")]
    public sealed class SvgMatrix : SvgTransform
    {
        public SvgMatrix(double[] data) : base(data)
        {
        }

        public SvgMatrix(Matrix m) : this(Array.ConvertAll<float, double>(m.Elements, converter1))
        {
            Converter<float, double> converter1 = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Converter<float, double> local1 = <>c.<>9__1_0;
                converter1 = <>c.<>9__1_0 = x => (double) x;
            }
        }

        public override SvgTransform DeepCopy() => 
            new SvgMatrix(base.Data);

        public override Matrix GetMatrix(double scale) => 
            MatrixHelper.FromPoints(this.Points, scale);

        protected override void Initialize(double[] data)
        {
            if (data.Length != 6)
            {
                this.Points = new List<double>(6);
            }
            else
            {
                this.Points = new List<double>(data);
            }
        }

        public override string ToString()
        {
            object[] args = new object[] { this.Points[0], this.Points[1], this.Points[2], this.Points[3], this.Points[4], this.Points[5] };
            return string.Format(CultureInfo.InvariantCulture, "matrix({0}, {1}, {2}, {3}, {4}, {5})", args);
        }

        public List<double> Points { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgMatrix.<>c <>9 = new SvgMatrix.<>c();
            public static Converter<float, double> <>9__1_0;

            internal double <.ctor>b__1_0(float x) => 
                (double) x;
        }
    }
}

