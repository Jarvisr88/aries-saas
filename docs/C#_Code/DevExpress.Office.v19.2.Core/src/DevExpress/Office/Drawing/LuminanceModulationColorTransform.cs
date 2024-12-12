namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class LuminanceModulationColorTransform : ColorTransformValueBase
    {
        public LuminanceModulationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplyLuminanceMod(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new LuminanceModulationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            LuminanceModulationColorTransform transform = obj as LuminanceModulationColorTransform;
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

