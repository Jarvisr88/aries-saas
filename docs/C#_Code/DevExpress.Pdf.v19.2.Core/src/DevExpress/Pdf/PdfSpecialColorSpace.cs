namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfSpecialColorSpace : PdfCustomColorSpace
    {
        private readonly PdfColorSpace alternateSpace;
        private readonly PdfCustomFunction tintTransform;

        protected PdfSpecialColorSpace(PdfObjectCollection objects, IList<object> array)
        {
            if (!this.CheckArraySize(array.Count))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.alternateSpace = objects.GetColorSpace(array[2]);
            this.tintTransform = PdfCustomFunction.Parse(objects, array[3]);
            if ((this.tintTransform == null) || (this.tintTransform.RangeSize != this.alternateSpace.ComponentsCount))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected abstract bool CheckArraySize(int actualSize);
        protected internal override PdfColor Transform(PdfColor color) => 
            this.alternateSpace.AlternateTransform(new PdfColor(this.tintTransform.Transform(color.Components)));

        protected internal override PdfScanlineTransformationResult Transform(IPdfImageScanlineSource data, int width) => 
            new PdfScanlineTransformationResult(new PdfCIEBasedImageScanlineSource(data, this, width, this.ComponentsCount), PdfPixelFormat.Argb24bpp);

        public PdfColorSpace AlternateSpace =>
            this.alternateSpace;

        public PdfCustomFunction TintTransform =>
            this.tintTransform;
    }
}

