namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXShapedGlyphInfo
    {
        internal DXShapedGlyphInfo(short[] clusters, float[] widths)
        {
            this.<Clusters>k__BackingField = clusters;
            this.<Widths>k__BackingField = widths;
        }

        public short[] Clusters { get; }

        public float[] Widths { get; }
    }
}

