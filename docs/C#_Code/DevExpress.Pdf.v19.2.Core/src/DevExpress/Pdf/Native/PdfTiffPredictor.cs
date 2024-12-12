namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTiffPredictor : PdfFlateLZWDecodeFilterPredictor
    {
        private readonly int bitsPerComponent;
        private readonly int componentsCount;

        public PdfTiffPredictor(PdfFlateLZWDecodeFilter filter) : base(filter)
        {
            this.bitsPerComponent = filter.BitsPerComponent;
            this.componentsCount = filter.Colors;
        }

        protected override byte[] Decode(byte[] data)
        {
            if (this.bitsPerComponent == 0x10)
            {
                return this.Decode16bpp(data);
            }
            int bytesPerPixel = base.BytesPerPixel;
            int rowLength = base.RowLength;
            int length = data.Length;
            int num4 = base.CalcRowCount(length);
            byte[] buffer = new byte[length];
            byte[] array = new byte[bytesPerPixel];
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            while (num5 < num4)
            {
                Array.Clear(array, 0, bytesPerPixel);
                int num8 = 0;
                int index = 0;
                while (true)
                {
                    if ((num8 >= rowLength) || (num6 >= length))
                    {
                        num5++;
                        break;
                    }
                    byte num11 = (byte) (array[index] + data[num6++]);
                    buffer[num7++] = num11;
                    array[index] = num11;
                    if (++index == bytesPerPixel)
                    {
                        index = 0;
                    }
                    num8++;
                }
            }
            return buffer;
        }

        private byte[] Decode16bpp(byte[] data)
        {
            int num = base.RowLength / 2;
            int length = data.Length;
            int num3 = base.CalcRowCount(length);
            byte[] buffer = new byte[length];
            int[] array = new int[this.componentsCount];
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            while (num4 < num3)
            {
                Array.Clear(array, 0, this.componentsCount);
                int num7 = 0;
                int index = 0;
                while (true)
                {
                    if ((num7 >= num) || (num5 >= (length - 1)))
                    {
                        num4++;
                        break;
                    }
                    int num10 = ((data[num5++] << 8) | data[num5++]) + array[index];
                    buffer[num6++] = (byte) (num10 >> 8);
                    buffer[num6++] = (byte) num10;
                    array[index] = num10;
                    if (++index == this.componentsCount)
                    {
                        index = 0;
                    }
                    num7++;
                }
            }
            return buffer;
        }

        protected override int ActualRowLength =>
            base.RowLength;
    }
}

