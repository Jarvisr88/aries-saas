namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class GrayscaleColorTransform : ColorTransformBase
    {
        public override Color ApplyTransform(Color color)
        {
            int red = (int) Math.Round((double) ((((0.3 * color.R) + (0.59 * color.G)) + (0.11 * color.B)) + 0.5), 0);
            return DXColor.FromArgb(red, red, red);
        }

        public override ColorTransformBase Clone() => 
            new GrayscaleColorTransform();

        public override bool Equals(object obj) => 
            obj is GrayscaleColorTransform;

        public override int GetHashCode() => 
            base.GetType().GetHashCode();

        public override void Visit(IColorTransformVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

