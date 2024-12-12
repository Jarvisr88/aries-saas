namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfPngPredictorDataSource : PdfFlateLZWDecodeFilterPredictorDataSource
    {
        private readonly byte[] topLeftPixel;
        private byte[] previousRow;
        private PngPrediction pngPrediction;

        public PdfPngPredictorDataSource(PdfFlateLZWDecodeFilter filter, IPdfFlateDataSource source) : base(filter, source, 1)
        {
            this.previousRow = new byte[base.RowLength];
            this.topLeftPixel = new byte[base.BytesPerPixel];
        }

        protected override void ProcessRow()
        {
            byte[] currentRow = base.CurrentRow;
            byte[] previousPixel = base.PreviousPixel;
            int rowLength = base.RowLength;
            int bytesPerPixel = base.BytesPerPixel;
            int index = 1;
            int num4 = 0;
            while (index < rowLength)
            {
                byte num6;
                byte num5 = currentRow[index];
                PngPrediction pngPrediction = this.pngPrediction;
                switch (pngPrediction)
                {
                    case PngPrediction.None:
                        num6 = num5;
                        break;

                    case PngPrediction.Sub:
                        num6 = (byte) (previousPixel[num4] + num5);
                        break;

                    case PngPrediction.Up:
                        num6 = (byte) (this.previousRow[index] + num5);
                        break;

                    case PngPrediction.Average:
                        num6 = (byte) (((previousPixel[num4] + this.previousRow[index]) / 2) + num5);
                        break;

                    case PngPrediction.Paeth:
                    {
                        byte num7 = previousPixel[num4];
                        byte num8 = this.previousRow[index];
                        byte num9 = this.topLeftPixel[num4];
                        int num10 = (num7 + num8) - num9;
                        int num11 = Math.Abs((int) (num10 - num7));
                        int num12 = Math.Abs((int) (num10 - num8));
                        int num13 = Math.Abs((int) (num10 - num9));
                        byte num14 = (num11 > num12) ? ((num12 <= num13) ? num8 : num9) : ((num11 <= num13) ? num7 : num9);
                        num6 = (byte) (num14 + num5);
                        break;
                    }
                    default:
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                        num6 = num5;
                        break;
                }
                currentRow[index] = num6;
                this.topLeftPixel[num4] = this.previousRow[index];
                this.previousRow[index] = num6;
                previousPixel[num4] = num6;
                if (++num4 == bytesPerPixel)
                {
                    num4 = 0;
                }
                index++;
            }
        }

        protected override void StartNextRow()
        {
            this.previousRow = base.CurrentRow;
            base.CurrentRow = new byte[base.RowLength];
            base.StartNextRow();
            Array.Clear(this.topLeftPixel, 0, base.BytesPerPixel);
            this.pngPrediction = (PngPrediction) base.CurrentRow[0];
        }
    }
}

