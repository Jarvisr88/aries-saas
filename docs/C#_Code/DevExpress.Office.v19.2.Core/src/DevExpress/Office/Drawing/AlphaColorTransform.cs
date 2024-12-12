namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class AlphaColorTransform : ColorTransformValueBase
    {
        public AlphaColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            DXColor.FromArgb(base.ToIntValue(((double) base.Value) / 100000.0), color.R, color.G, color.B);

        public override ColorTransformBase Clone() => 
            new AlphaColorTransform(base.Value);

        public static AlphaColorTransform CreateFromA(int a) => 
            new AlphaColorTransform((int) ((a * 100000.0) / 255.0));

        public override bool Equals(object obj)
        {
            AlphaColorTransform transform = obj as AlphaColorTransform;
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

