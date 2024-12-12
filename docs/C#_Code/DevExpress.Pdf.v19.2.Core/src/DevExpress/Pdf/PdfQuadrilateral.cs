namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfQuadrilateral
    {
        private const string dictionaryKey = "QuadPoints";
        private readonly PdfPoint p1;
        private readonly PdfPoint p2;
        private readonly PdfPoint p3;
        private readonly PdfPoint p4;

        internal PdfQuadrilateral(PdfOrientedRectangle rectangle)
        {
            IList<PdfPoint> vertices = rectangle.Vertices;
            this.p1 = vertices[0];
            this.p2 = vertices[1];
            this.p3 = vertices[3];
            this.p4 = vertices[2];
        }

        public PdfQuadrilateral(PdfPoint p1, PdfPoint p2, PdfPoint p3, PdfPoint p4)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
        }

        internal bool Contains(PdfPoint pt) => 
            TriangleContainsPoint(this.p1, this.p2, this.p3, pt) || TriangleContainsPoint(this.p2, this.p3, this.p4, pt);

        internal static IList<PdfQuadrilateral> ParseArray(PdfReaderDictionary dictionary)
        {
            IList<object> array = dictionary.GetArray("QuadPoints");
            if (array == null)
            {
                return null;
            }
            int count = array.Count;
            if ((count % 8) != 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            count /= 8;
            List<PdfQuadrilateral> list2 = new List<PdfQuadrilateral>(count);
            int num2 = 0;
            for (int i = 0; num2 < count; i += 2)
            {
                PdfPoint point = PdfDocumentReader.CreatePoint(array, i);
                i += 2;
                PdfPoint point2 = PdfDocumentReader.CreatePoint(array, i);
                i += 2;
                PdfPoint point3 = PdfDocumentReader.CreatePoint(array, i);
                i += 2;
                PdfPoint point4 = PdfDocumentReader.CreatePoint(array, i);
                list2.Add(new PdfQuadrilateral(point, point2, point3, point4));
                num2++;
            }
            return list2;
        }

        private static bool TriangleContainsPoint(PdfPoint pt1, PdfPoint pt2, PdfPoint pt3, PdfPoint targetPoint)
        {
            PdfPoint point = PdfPoint.Sub(pt3, pt1);
            PdfPoint point2 = PdfPoint.Sub(pt2, pt1);
            PdfPoint point3 = PdfPoint.Sub(targetPoint, pt1);
            double num = PdfPoint.Dot(point, point);
            double num2 = PdfPoint.Dot(point, point2);
            double num3 = PdfPoint.Dot(point, point3);
            double num4 = PdfPoint.Dot(point2, point2);
            double num5 = PdfPoint.Dot(point2, point3);
            double num6 = 1.0 / ((num * num4) - (num2 * num2));
            double num7 = ((num4 * num3) - (num2 * num5)) * num6;
            double num8 = ((num * num5) - (num2 * num3)) * num6;
            return ((num7 > 0.0) && ((num8 > 0.0) && ((num7 + num8) <= 1.0)));
        }

        internal static void Write(PdfWriterDictionary dictionary, IList<PdfQuadrilateral> region)
        {
            if (region != null)
            {
                List<double> list = new List<double>();
                foreach (PdfQuadrilateral quadrilateral in region)
                {
                    list.AddRange(quadrilateral.Data);
                }
                dictionary.Add("QuadPoints", list);
            }
        }

        public PdfPoint P1 =>
            this.p1;

        public PdfPoint P2 =>
            this.p2;

        public PdfPoint P3 =>
            this.p3;

        public PdfPoint P4 =>
            this.p4;

        internal double[] Data =>
            new double[] { this.p1.X, this.p1.Y, this.p2.X, this.p2.Y, this.p3.X, this.p3.Y, this.p4.X, this.p4.Y };
    }
}

