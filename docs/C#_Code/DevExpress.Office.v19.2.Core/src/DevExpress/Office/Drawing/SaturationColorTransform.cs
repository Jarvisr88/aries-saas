namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class SaturationColorTransform : ColorTransformValueBase
    {
        public SaturationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplySaturation(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new SaturationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            SaturationColorTransform transform = obj as SaturationColorTransform;
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

