namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class BlueModulationColorTransform : ColorTransformValueBase
    {
        public BlueModulationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(color.R, color.G, base.ApplyRGBModulation(color.B));

        public override ColorTransformBase Clone() => 
            new BlueModulationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            BlueModulationColorTransform transform = obj as BlueModulationColorTransform;
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

