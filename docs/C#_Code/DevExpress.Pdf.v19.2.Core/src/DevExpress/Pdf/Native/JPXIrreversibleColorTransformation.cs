namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXIrreversibleColorTransformation : JPXMultipleComponentTransformation
    {
        public JPXIrreversibleColorTransformation(int width, int height, JPXTileComponentData[] components) : base(width, height, components)
        {
        }

        protected override void Transform(byte[] data, float y0, float y1, float y2, int destOffset)
        {
            float num = y0 + (1.402f * y2);
            base.Transform(data, num, (y0 - (0.34413f * y1)) - (0.71414f * y2), y0 + (1.772f * y1), destOffset);
        }
    }
}

