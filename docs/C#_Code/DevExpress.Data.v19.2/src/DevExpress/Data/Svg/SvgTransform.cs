namespace DevExpress.Data.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class SvgTransform : SvgTransformBase
    {
        public SvgTransform();
        public SvgTransform(double x, double y);
        public override void FillTransform(string[] values);
        public virtual double GetDefaultY(double x);

        public double X { get; internal set; }

        public double Y { get; internal set; }

        public override bool IsIdentity { get; }
    }
}

