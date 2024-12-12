namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    [FormatElement("matrix")]
    public class SvgMatrix : SvgTransformBase
    {
        private const int ElementsCount = 6;
        private Matrix matrix;

        public SvgMatrix();
        public SvgMatrix(double m11, double m12, double m21, double m22, double x, double y);
        public override void FillTransform(string[] values);
        public override string GetTransform(IFormatProvider provider);
        public override Matrix GetTransformMatrix();
        protected internal void SetTransformMatrix(Matrix matrix);
    }
}

