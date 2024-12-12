namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class AlphaModulationColorTransform : ColorTransformValueBase
    {
        public AlphaModulationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color)
        {
            double num = (base.ToDoubleValue(color.A) * base.Value) / 100000.0;
            return DXColor.FromArgb(base.ToIntValue(num), color.R, color.G, color.B);
        }

        public override ColorTransformBase Clone() => 
            new AlphaModulationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            AlphaModulationColorTransform transform = obj as AlphaModulationColorTransform;
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

