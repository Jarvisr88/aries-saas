namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.IO;
    using System.IO.Compression;

    public class PdfFlateDecodeFilter : PdfFlateLZWDecodeFilter
    {
        internal const string Name = "FlateDecode";
        internal const string ShortName = "Fl";

        internal PdfFlateDecodeFilter(PdfReaderDictionary parameters) : base(parameters)
        {
        }

        internal PdfFlateDecodeFilter(PdfFlateLZWFilterPredictor predictor, int colors, int bitsPerComponent, int columns) : base(predictor, colors, bitsPerComponent, columns)
        {
        }

        protected internal override PdfScanlineTransformationResult CreateScanlineSource(PdfImage image, int componentsCount, byte[] data)
        {
            IPdfFlateDataSource source = new PdfFlateDataSource(data);
            PdfFlateLZWFilterPredictor predictor = base.Predictor;
            if (predictor == PdfFlateLZWFilterPredictor.TiffPredictor)
            {
                source = new PdfTiffPredictorDataSource(this, source);
            }
            else
            {
                switch (predictor)
                {
                    case PdfFlateLZWFilterPredictor.PngNonePrediction:
                    case PdfFlateLZWFilterPredictor.PngSubPrediction:
                    case PdfFlateLZWFilterPredictor.PngUpPrediction:
                    case PdfFlateLZWFilterPredictor.PngAveragePrediction:
                    case PdfFlateLZWFilterPredictor.PngPaethPrediction:
                    case PdfFlateLZWFilterPredictor.PngOptimumPrediction:
                        source = new PdfPngPredictorDataSource(this, source);
                        break;

                    default:
                        break;
                }
            }
            return new PdfScanlineTransformationResult(new PdfFlateImageScanlineSource(source, PdfImageScanlineDecoder.CreateImageScanlineDecoder(image, componentsCount)));
        }

        protected override byte[] PerformDecode(byte[] data)
        {
            int length = data.Length;
            if (length == 0)
            {
                return data;
            }
            if (length < 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (length == 2)
            {
                return new byte[0];
            }
            MemoryStream stream = new MemoryStream(data) {
                Position = 2L
            };
            using (MemoryStream stream2 = new MemoryStream())
            {
                using (DeflateStream stream3 = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    stream3.CopyTo(stream2);
                }
                return stream2.ToArray();
            }
        }

        protected internal override string FilterName =>
            "FlateDecode";
    }
}

