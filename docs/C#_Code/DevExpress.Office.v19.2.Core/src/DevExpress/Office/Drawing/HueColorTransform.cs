namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class HueColorTransform : ColorTransformValueBase
    {
        public HueColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplyHue(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new HueColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            HueColorTransform transform = obj as HueColorTransform;
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

