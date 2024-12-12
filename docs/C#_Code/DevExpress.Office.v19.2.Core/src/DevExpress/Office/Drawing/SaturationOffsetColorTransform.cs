namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class SaturationOffsetColorTransform : ColorTransformValueBase
    {
        public SaturationOffsetColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplySaturationOffset(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new SaturationOffsetColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            SaturationOffsetColorTransform transform = obj as SaturationOffsetColorTransform;
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

