namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class InverseColorTransform : ColorTransformBase
    {
        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(base.ToIntValue(base.ApplyDefaultGamma((double) (1.0 - base.ApplyInverseDefaultGamma(base.ToDoubleValue(color.R))))), base.ToIntValue(base.ApplyDefaultGamma((double) (1.0 - base.ApplyInverseDefaultGamma(base.ToDoubleValue(color.G))))), base.ToIntValue(base.ApplyDefaultGamma((double) (1.0 - base.ApplyInverseDefaultGamma(base.ToDoubleValue(color.B))))));

        public override ColorTransformBase Clone() => 
            new InverseColorTransform();

        public override bool Equals(object obj) => 
            obj is InverseColorTransform;

        public override int GetHashCode() => 
            base.GetType().GetHashCode();

        public override void Visit(IColorTransformVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

