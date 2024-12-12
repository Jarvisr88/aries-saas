namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTiffPredictorDataSource : PdfFlateLZWDecodeFilterPredictorDataSource
    {
        public PdfTiffPredictorDataSource(PdfFlateLZWDecodeFilter filter, IPdfFlateDataSource source) : base(filter, source, 0)
        {
        }

        private void Process16bppRow()
        {
            byte[] currentRow = base.CurrentRow;
            int num = base.RowLength / 2;
            byte[] previousPixel = base.PreviousPixel;
            int bytesPerPixel = base.BytesPerPixel;
            int num3 = 0;
            int index = 0;
            int num5 = 0;
            while (num3 < num)
            {
                int num6 = (currentRow[num5] << 8) | currentRow[num5 + 1];
                int num7 = (previousPixel[index] << 8) | previousPixel[index + 1];
                int num8 = num6 + num7;
                byte num9 = (byte) (num8 >> 8);
                byte num10 = (byte) num8;
                currentRow[num5++] = num9;
                currentRow[num5++] = num10;
                previousPixel[index++] = num9;
                previousPixel[index++] = num10;
                if (index == bytesPerPixel)
                {
                    index = 0;
                }
                num3++;
            }
        }

        protected override void ProcessRow()
        {
            if (base.BitsPerComponent == 0x10)
            {
                this.Process16bppRow();
            }
            else
            {
                byte[] currentRow = base.CurrentRow;
                int rowLength = base.RowLength;
                byte[] previousPixel = base.PreviousPixel;
                int bytesPerPixel = base.BytesPerPixel;
                int index = 0;
                int num4 = 0;
                while (index < rowLength)
                {
                    byte num5 = currentRow[index];
                    byte num6 = (byte) (previousPixel[num4] + num5);
                    currentRow[index] = num6;
                    previousPixel[num4] = num6;
                    if (++num4 == bytesPerPixel)
                    {
                        num4 = 0;
                    }
                    index++;
                }
            }
        }
    }
}

