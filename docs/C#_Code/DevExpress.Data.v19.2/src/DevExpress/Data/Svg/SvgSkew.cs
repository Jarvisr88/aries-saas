namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public abstract class SvgSkew : SvgTransform
    {
        public SvgSkew();
        public SvgSkew(double x, double y);
        public override Matrix GetTransformMatrix();

        public override bool IsIdentity { get; }
    }
}

