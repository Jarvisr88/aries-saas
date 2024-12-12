namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;

    public class GammaColorTransform : ColorTransformBase
    {
        public override Color ApplyTransform(Color color) => 
            base.ApplyDefaultGamma(color);

        public override ColorTransformBase Clone() => 
            new GammaColorTransform();

        public override bool Equals(object obj) => 
            obj is GammaColorTransform;

        public override int GetHashCode() => 
            base.GetType().GetHashCode();

        public override void Visit(IColorTransformVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

