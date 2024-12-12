namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXColorTransformation
    {
        private readonly int width;
        private readonly int height;
        private readonly int[] shift;
        private readonly JPXTileComponentData[] componentsData;

        protected JPXColorTransformation(int width, int height, JPXTileComponentData[] componentsData)
        {
            this.componentsData = componentsData;
            this.width = width;
            this.height = height;
            this.shift = new int[componentsData.Length];
            for (int i = 0; i < this.shift.Length; i++)
            {
                this.shift[i] = 1 << ((componentsData[i].BitsPerComponent - 1) & 0x1f);
            }
        }

        public static JPXColorTransformation Create(JPXTile tile)
        {
            JPXTileComponentData[] componentData = JPXTileComponentDataConstructor.Create(tile.Components);
            switch (componentData.Length)
            {
                case 0:
                case 2:
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;

                case 1:
                    return new JPXGrayColorTransformation(tile.Width, tile.Height, componentData);
            }
            return (!tile.IsMultipleComponentTransformationSpecified ? new JPXMultipleComponentTransformation(tile.Width, tile.Height, componentData) : (!tile.UseWaveletTransformation ? ((JPXColorTransformation) new JPXIrreversibleColorTransformation(tile.Width, tile.Height, componentData)) : ((JPXColorTransformation) new JPXReversibleColorTransformation(tile.Width, tile.Height, componentData))));
        }

        protected static byte Normalize(float value)
        {
            uint num = (uint) (value + 0.5);
            return ((num > 0xff) ? ((value <= 0f) ? 0 : 0xff) : ((byte) num));
        }

        public void Transform(byte[] result, int startOffset, int rowWidth)
        {
            int length = this.componentsData.Length;
            int num2 = rowWidth * length;
            int v = 0;
            int num4 = startOffset;
            while (v < this.height)
            {
                int h = 0;
                int destOffset = num4;
                while (true)
                {
                    if (h >= this.width)
                    {
                        v++;
                        num4 += num2;
                        break;
                    }
                    this.TransformColor(v, h, result, destOffset);
                    h++;
                    destOffset += length;
                }
            }
        }

        protected abstract void TransformColor(int v, int h, byte[] dest, int destOffset);
        public void TransformLine(byte[] result, int startOffset, int v)
        {
            int length = this.componentsData.Length;
            int h = 0;
            for (int i = startOffset; h < this.width; i += length)
            {
                this.TransformColor(v, h, result, i);
                h++;
            }
        }

        protected JPXTileComponentData[] ComponentsData =>
            this.componentsData;

        protected int[] Shift =>
            this.shift;

        protected int Width =>
            this.width;
    }
}

