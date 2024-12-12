namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting.Native;
    using System;

    public class Range : Pair<float, float>
    {
        public Range(float left, float right);
        public bool Intersects(Range range);
        public Range Union(Range range);

        public float Left { get; }

        public float Right { get; }
    }
}

