namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class GreenModulationColorTransform : ColorTransformValueBase
    {
        public GreenModulationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(color.R, base.ApplyRGBModulation(color.G), color.B);

        public override ColorTransformBase Clone() => 
            new GreenModulationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            GreenModulationColorTransform transform = obj as GreenModulationColorTransform;
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

