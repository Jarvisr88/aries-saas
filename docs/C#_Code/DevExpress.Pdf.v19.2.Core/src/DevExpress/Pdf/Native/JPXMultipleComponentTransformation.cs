namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXMultipleComponentTransformation : JPXColorTransformation
    {
        private readonly float[] firstComponentsData;
        private readonly float[] secondComponentData;
        private readonly float[] thirdComponentData;
        private readonly int shift;

        public JPXMultipleComponentTransformation(int width, int height, JPXTileComponentData[] components) : base(width, height, components)
        {
            this.firstComponentsData = components[0].Data;
            this.secondComponentData = components[1].Data;
            this.thirdComponentData = components[2].Data;
            this.shift = base.Shift[0];
        }

        protected virtual void Transform(byte[] data, float y0, float y1, float y2, int destOffset)
        {
            data[destOffset++] = Normalize(y0 + this.shift);
            data[destOffset++] = Normalize(y1 + this.shift);
            data[destOffset] = Normalize(y2 + this.shift);
        }

        protected override void TransformColor(int v, int h, byte[] dest, int destOffset)
        {
            int index = (v * base.Width) + h;
            JPXTileComponentData[] componentsData = base.ComponentsData;
            this.Transform(dest, this.firstComponentsData[index], this.secondComponentData[index], this.thirdComponentData[index], destOffset);
            destOffset += 3;
            for (int i = 3; i < componentsData.Length; i++)
            {
                dest[destOffset++] = Normalize(base.ComponentsData[i].Data[index] + base.Shift[i]);
            }
        }
    }
}

