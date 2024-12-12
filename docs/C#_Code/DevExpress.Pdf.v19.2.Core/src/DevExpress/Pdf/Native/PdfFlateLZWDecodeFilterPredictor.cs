namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfFlateLZWDecodeFilterPredictor
    {
        private readonly int bytesPerPixel;
        private readonly int rowLength;

        protected PdfFlateLZWDecodeFilterPredictor(PdfFlateLZWDecodeFilter filter)
        {
            int bitsPerComponent = filter.BitsPerComponent;
            int colors = filter.Colors;
            this.bytesPerPixel = (bitsPerComponent * colors) / 8;
            this.bytesPerPixel ??= 1;
            if (bitsPerComponent == 0x10)
            {
                this.rowLength = (filter.Columns * colors) * 2;
            }
            else
            {
                int num3 = 8 / bitsPerComponent;
                int num4 = filter.Columns * colors;
                this.rowLength = num4 / num3;
                if ((num4 % num3) != 0)
                {
                    this.rowLength++;
                }
            }
        }

        protected int CalcRowCount(int dataLength)
        {
            int actualRowLength = this.ActualRowLength;
            int num2 = dataLength / actualRowLength;
            if ((dataLength % actualRowLength) != 0)
            {
                num2++;
            }
            return num2;
        }

        protected abstract byte[] Decode(byte[] data);
        public static byte[] Decode(byte[] data, PdfFlateLZWDecodeFilter filter)
        {
            PdfFlateLZWDecodeFilterPredictor predictor;
            PdfFlateLZWFilterPredictor predictor2 = filter.Predictor;
            if (predictor2 == PdfFlateLZWFilterPredictor.TiffPredictor)
            {
                predictor = new PdfTiffPredictor(filter);
            }
            else
            {
                switch (predictor2)
                {
                    case PdfFlateLZWFilterPredictor.PngNonePrediction:
                    case PdfFlateLZWFilterPredictor.PngSubPrediction:
                    case PdfFlateLZWFilterPredictor.PngUpPrediction:
                    case PdfFlateLZWFilterPredictor.PngAveragePrediction:
                    case PdfFlateLZWFilterPredictor.PngPaethPrediction:
                    case PdfFlateLZWFilterPredictor.PngOptimumPrediction:
                        predictor = new PdfPngPredictor(filter);
                        break;

                    default:
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                        return data;
                }
            }
            return predictor.Decode(data);
        }

        protected int BytesPerPixel =>
            this.bytesPerPixel;

        protected int RowLength =>
            this.rowLength;

        protected abstract int ActualRowLength { get; }
    }
}

