namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    [FormatElement("shear")]
    public class SvgShear : SvgTransform
    {
        public SvgShear();
        public SvgShear(double x, double y);
        public override string GetTransform(IFormatProvider provider);
        public override Matrix GetTransformMatrix();
    }
}

