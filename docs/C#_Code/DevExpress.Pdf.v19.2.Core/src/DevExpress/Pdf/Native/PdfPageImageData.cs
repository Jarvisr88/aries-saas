namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Utils;
    using System;

    public class PdfPageImageData
    {
        private static readonly PdfPoint[] corners = new PdfPoint[] { new PdfPoint(0.0, 0.0), new PdfPoint(0.0, 1.0), new PdfPoint(1.0, 0.0), new PdfPoint(1.0, 1.0) };
        private readonly PdfImage image;
        private readonly PdfTransformationMatrix matrix;
        private PdfRectangle boundingRectangle;

        public PdfPageImageData(PdfImage image, PdfTransformationMatrix matrix)
        {
            this.image = image;
            this.matrix = matrix;
        }

        public bool Equals(PdfPageImageData imageData) => 
            ReferenceEquals(this.image, imageData.image) && this.matrix.Equals(imageData.matrix);

        public override bool Equals(object obj)
        {
            PdfPageImageData imageData = obj as PdfPageImageData;
            return ((imageData != null) && this.Equals(imageData));
        }

        public override int GetHashCode() => 
            HashCodeHelper.Calculate(this.image.GetHashCode(), this.matrix.GetHashCode());

        public PdfImage Image =>
            this.image;

        public PdfTransformationMatrix Matrix =>
            this.matrix;

        public PdfRectangle BoundingRectangle
        {
            get
            {
                this.boundingRectangle ??= PdfRectangle.CreateBoundingBox(this.matrix.Transform(corners));
                return this.boundingRectangle;
            }
        }
    }
}

