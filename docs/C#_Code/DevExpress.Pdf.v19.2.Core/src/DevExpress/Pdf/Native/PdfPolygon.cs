namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfPolygon
    {
        private readonly List<PdfPoint> points = new List<PdfPoint>();
        private PdfPoint lastPoint = new PdfPoint(double.MinValue, double.MinValue);

        public void AddPoint(double x, double y)
        {
            PdfPoint item = new PdfPoint(x, y);
            if (!item.Equals(this.lastPoint))
            {
                this.lastPoint = item;
                this.points.Add(item);
            }
        }

        public IList<PdfPoint> Points =>
            this.points;
    }
}

