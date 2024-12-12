namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PdfCombinedGeometry : PdfElement
    {
        private Geometry geometry;

        public PdfCombinedGeometry(System.Windows.Media.Brush brush, PdfPageViewModel page, double zoomFactor, double angle, IEnumerable<PdfOrientedRectangle> rectangles)
        {
            this.Brush = brush;
            this.Rectangles = rectangles;
            this.Page = page;
            this.Angle = angle;
            this.ZoomFactor = zoomFactor;
            this.geometry = this.GenerateGeometry();
        }

        private Geometry GenerateGeometry()
        {
            CombinedGeometry geometry1 = new CombinedGeometry();
            geometry1.GeometryCombineMode = GeometryCombineMode.Union;
            CombinedGeometry geometry = geometry1;
            foreach (PdfOrientedRectangle rectangle in this.Rectangles)
            {
                Geometry rectangleGeometry = this.GetRectangleGeometry(rectangle);
                geometry = new CombinedGeometry(GeometryCombineMode.Union, geometry, rectangleGeometry);
            }
            return geometry;
        }

        private Geometry GetRectangleGeometry(PdfOrientedRectangle rectangle)
        {
            IList<PdfPoint> vertices = rectangle.Vertices;
            Point start = this.Page.GetPoint(vertices[0], this.ZoomFactor, this.Angle);
            List<LineSegment> list2 = new List<LineSegment>();
            for (int i = 1; i < vertices.Count; i++)
            {
                Point point = this.Page.GetPoint(vertices[i], this.ZoomFactor, this.Angle);
                list2.Add(new LineSegment(point, true));
            }
            PathFigure figure = new PathFigure(start, (IEnumerable<PathSegment>) list2, true);
            return new PathGeometry { Figures = { figure } };
        }

        public override void Render(DrawingContext dc, Size renderSize)
        {
            if (this.geometry != null)
            {
                dc.DrawGeometry(this.Brush, new Pen(this.Brush, 1.0), this.geometry);
            }
        }

        public System.Windows.Media.Brush Brush { get; private set; }

        public IEnumerable<PdfOrientedRectangle> Rectangles { get; private set; }

        public PdfPageViewModel Page { get; private set; }

        public double Angle { get; private set; }

        public double ZoomFactor { get; private set; }
    }
}

