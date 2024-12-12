namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class HueOffsetColorTransform : ColorTransformValueBase
    {
        public HueOffsetColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplyHueOffset(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new HueOffsetColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            HueOffsetColorTransform transform = obj as HueOffsetColorTransform;
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

