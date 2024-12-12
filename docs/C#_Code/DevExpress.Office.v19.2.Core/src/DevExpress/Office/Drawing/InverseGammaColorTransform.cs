namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;

    public class InverseGammaColorTransform : ColorTransformBase
    {
        public override Color ApplyTransform(Color color) => 
            base.ApplyInverseDefaultGamma(color);

        public override ColorTransformBase Clone() => 
            new InverseGammaColorTransform();

        public override bool Equals(object obj) => 
            obj is InverseGammaColorTransform;

        public override int GetHashCode() => 
            base.GetType().GetHashCode();

        public override void Visit(IColorTransformVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

