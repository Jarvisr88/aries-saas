namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class RedOffsetColorTransform : ColorTransformValueBase
    {
        public RedOffsetColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(base.ApplyRGBOffset(color.R), color.G, color.B);

        public override ColorTransformBase Clone() => 
            new RedOffsetColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            RedOffsetColorTransform transform = obj as RedOffsetColorTransform;
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

