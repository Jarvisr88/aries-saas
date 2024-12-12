namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class ShadeColorTransform : ColorTransformValueBase
    {
        public ShadeColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color)
        {
            double modulation = ((double) base.Value) / 100000.0;
            return DXColor.FromArgb(base.ApplyRGBModulationCore(color.R, modulation), base.ApplyRGBModulationCore(color.G, modulation), base.ApplyRGBModulationCore(color.B, modulation));
        }

        public override ColorTransformBase Clone() => 
            new ShadeColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            ShadeColorTransform transform = obj as ShadeColorTransform;
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

