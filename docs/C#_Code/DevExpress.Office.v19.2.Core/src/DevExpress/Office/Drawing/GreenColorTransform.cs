namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class GreenColorTransform : ColorTransformValueBase
    {
        public GreenColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(color.R, base.GetRGBFromValue(), color.B);

        public override ColorTransformBase Clone() => 
            new GreenColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            GreenColorTransform transform = obj as GreenColorTransform;
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

