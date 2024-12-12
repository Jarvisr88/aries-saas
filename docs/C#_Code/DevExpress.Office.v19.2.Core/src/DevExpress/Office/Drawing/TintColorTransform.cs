namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class TintColorTransform : ColorTransformValueBase
    {
        public TintColorTransform(int value) : base(value)
        {
        }

        private double ApplyTintCore(double normalRgb, double normalTint) => 
            (normalTint > 0.0) ? ((normalRgb * (1.0 - normalTint)) + normalTint) : (normalRgb * (1.0 + normalTint));

        public override Color ApplyTransform(Color color)
        {
            double normalTint = 1.0 - (((double) base.Value) / 100000.0);
            return DXColor.FromArgb(base.ToIntValue(base.ApplyDefaultGamma(this.ApplyTintCore(base.ApplyInverseDefaultGamma(base.ToDoubleValue(color.R)), normalTint))), base.ToIntValue(base.ApplyDefaultGamma(this.ApplyTintCore(base.ApplyInverseDefaultGamma(base.ToDoubleValue(color.G)), normalTint))), base.ToIntValue(base.ApplyDefaultGamma(this.ApplyTintCore(base.ApplyInverseDefaultGamma(base.ToDoubleValue(color.B)), normalTint))));
        }

        public override ColorTransformBase Clone() => 
            new TintColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            TintColorTransform transform = obj as TintColorTransform;
            return ((transform != null) && (transform.Value == base.Value));
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ base.Value;

        public override void Visit(IColorTransformVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

