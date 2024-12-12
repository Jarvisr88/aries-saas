namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    [FormatElement("rotate")]
    public class SvgRotate : SvgTransform
    {
        public SvgRotate();
        public SvgRotate(double angle);
        public SvgRotate(double angle, double x, double y);
        public override void FillTransform(string[] values);
        public override string GetTransform(IFormatProvider provider);
        public override Matrix GetTransformMatrix();

        public double Angle { get; internal set; }

        public override bool IsIdentity { get; }
    }
}

