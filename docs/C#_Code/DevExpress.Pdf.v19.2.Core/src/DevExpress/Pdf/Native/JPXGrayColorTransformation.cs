namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXGrayColorTransformation : JPXColorTransformation
    {
        public JPXGrayColorTransformation(int width, int height, JPXTileComponentData[] componentData) : base(width, height, componentData)
        {
        }

        protected override void TransformColor(int v, int h, byte[] dest, int destOffset)
        {
            dest[destOffset] = Normalize(base.ComponentsData[0].Data[(v * base.Width) + h] + base.Shift[0]);
        }
    }
}

