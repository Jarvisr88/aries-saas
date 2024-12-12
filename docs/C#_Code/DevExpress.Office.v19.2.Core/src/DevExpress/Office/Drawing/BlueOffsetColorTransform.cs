namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class BlueOffsetColorTransform : ColorTransformValueBase
    {
        public BlueOffsetColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(color.R, color.G, base.ApplyRGBOffset(color.B));

        public override ColorTransformBase Clone() => 
            new BlueOffsetColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            BlueOffsetColorTransform transform = obj as BlueOffsetColorTransform;
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

