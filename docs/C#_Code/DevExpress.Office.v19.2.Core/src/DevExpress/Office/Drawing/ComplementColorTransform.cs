namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class ComplementColorTransform : ColorTransformBase
    {
        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).GetComplementColor().ToRgb();

        public override ColorTransformBase Clone() => 
            new ComplementColorTransform();

        public override bool Equals(object obj) => 
            obj is ComplementColorTransform;

        public override int GetHashCode() => 
            base.GetType().GetHashCode();

        public override void Visit(IColorTransformVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

