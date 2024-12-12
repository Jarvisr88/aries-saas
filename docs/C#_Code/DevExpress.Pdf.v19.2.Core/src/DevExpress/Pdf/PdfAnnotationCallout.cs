namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfAnnotationCallout
    {
        private readonly PdfPoint startPoint;
        private readonly PdfPoint? kneePoint;
        private readonly PdfPoint endPoint;

        internal PdfAnnotationCallout(IList<object> array)
        {
            int count = array.Count;
            if (count == 4)
            {
                this.startPoint = PdfDocumentReader.CreatePoint(array, 0);
                this.endPoint = PdfDocumentReader.CreatePoint(array, 2);
            }
            else if (count != 6)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            else
            {
                this.startPoint = PdfDocumentReader.CreatePoint(array, 0);
                this.kneePoint = new PdfPoint?(PdfDocumentReader.CreatePoint(array, 2));
                this.endPoint = PdfDocumentReader.CreatePoint(array, 4);
            }
        }

        internal IList<double> ToWritableObject()
        {
            List<double> list = new List<double> {
                this.startPoint.X,
                this.startPoint.Y
            };
            if (this.kneePoint != null)
            {
                list.Add(this.kneePoint.Value.X);
                list.Add(this.kneePoint.Value.Y);
            }
            list.Add(this.endPoint.X);
            list.Add(this.endPoint.Y);
            return list;
        }

        public PdfPoint StartPoint =>
            this.startPoint;

        public PdfPoint? KneePoint =>
            this.kneePoint;

        public PdfPoint EndPoint =>
            this.endPoint;
    }
}

