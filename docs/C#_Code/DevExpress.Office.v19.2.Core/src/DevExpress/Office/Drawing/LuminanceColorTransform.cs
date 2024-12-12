namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class LuminanceColorTransform : ColorTransformValueBase
    {
        public LuminanceColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplyLuminance(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new LuminanceColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            LuminanceColorTransform transform = obj as LuminanceColorTransform;
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

