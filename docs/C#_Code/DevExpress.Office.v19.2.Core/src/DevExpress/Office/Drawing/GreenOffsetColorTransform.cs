namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class GreenOffsetColorTransform : ColorTransformValueBase
    {
        public GreenOffsetColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(color.R, base.ApplyRGBOffset(color.G), color.B);

        public override ColorTransformBase Clone() => 
            new GreenOffsetColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            GreenOffsetColorTransform transform = obj as GreenOffsetColorTransform;
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

