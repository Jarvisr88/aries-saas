namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfJPXDecodeFilter : PdfFilter
    {
        internal const string Name = "JPXDecode";

        internal PdfJPXDecodeFilter()
        {
        }

        protected internal override PdfScanlineTransformationResult CreateScanlineSource(PdfImage image, int componentsCount, byte[] data)
        {
            JPXImage image2 = JPXImage.DecodeImage(data);
            int length = image2.Size.Components.Length;
            return (((image.SMaskInData == PdfImageSMaskInDataType.None) || (length != 4)) ? new PdfScanlineTransformationResult(new PdfJPXImageScanlineSource(image2, length, false), (length == 1) ? PdfPixelFormat.Gray8bit : PdfPixelFormat.Argb24bpp) : new PdfScanlineTransformationResult(new PdfJPXImageScanlineSource(image2, length, true), PdfPixelFormat.Argb32bpp));
        }

        protected internal override byte[] Decode(byte[] data)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected internal override string FilterName =>
            "JPXDecode";
    }
}

