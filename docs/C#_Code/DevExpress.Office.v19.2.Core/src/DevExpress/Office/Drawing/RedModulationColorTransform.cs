namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class RedModulationColorTransform : ColorTransformValueBase
    {
        public RedModulationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(base.ApplyRGBModulation(color.R), color.G, color.B);

        public override ColorTransformBase Clone() => 
            new RedModulationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            RedModulationColorTransform transform = obj as RedModulationColorTransform;
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

