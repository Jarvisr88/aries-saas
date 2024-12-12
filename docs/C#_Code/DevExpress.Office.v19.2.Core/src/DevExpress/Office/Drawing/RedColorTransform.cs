namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class RedColorTransform : ColorTransformValueBase
    {
        public RedColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(base.GetRGBFromValue(), color.G, color.B);

        public override ColorTransformBase Clone() => 
            new RedColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            RedColorTransform transform = obj as RedColorTransform;
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

