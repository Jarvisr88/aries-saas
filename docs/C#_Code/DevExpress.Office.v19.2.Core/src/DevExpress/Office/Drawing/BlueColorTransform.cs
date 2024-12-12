namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class BlueColorTransform : ColorTransformValueBase
    {
        public BlueColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(color.R, color.G, base.GetRGBFromValue());

        public override ColorTransformBase Clone() => 
            new BlueColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            BlueColorTransform transform = obj as BlueColorTransform;
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

