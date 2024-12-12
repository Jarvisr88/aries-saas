namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXReversibleColorTransformation : JPXMultipleComponentTransformation
    {
        public JPXReversibleColorTransformation(int width, int height, JPXTileComponentData[] components) : base(width, height, components)
        {
        }

        protected override void Transform(byte[] data, float y0, float y1, float y2, int destOffset)
        {
            float num = y0 - ((y1 + y2) / 4f);
            float num2 = num + y2;
            base.Transform(data, num2, num, num + y1, destOffset);
        }
    }
}

