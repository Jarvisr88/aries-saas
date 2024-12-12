namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PdfGeometry : PdfElement
    {
        private Geometry geometry;

        public PdfGeometry(System.Windows.Media.Brush brush, PdfPageViewModel page, double zoomFactor, double angle, IEnumerable<PdfPoint> vertices)
        {
            this.Brush = brush;
            this.Vertices = vertices;
            this.Page = page;
            this.Angle = angle;
            this.ZoomFactor = zoomFactor;
            this.geometry = this.GenerateGeometry();
            this.geometry.Freeze();
        }

        private Geometry GenerateGeometry()
        {
            List<PdfPoint> list = this.Vertices.ToList<PdfPoint>();
            if (list.Count < 2)
            {
                return null;
            }
            Point start = this.Page.GetPoint(list[0], this.ZoomFactor, this.Angle);
            List<LineSegment> list2 = new List<LineSegment>();
            for (int i = 1; i < list.Count; i++)
            {
                Point point = this.Page.GetPoint(list[i], this.ZoomFactor, this.Angle);
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

        public IEnumerable<PdfPoint> Vertices { get; private set; }

        public PdfPageViewModel Page { get; private set; }

        public double Angle { get; private set; }

        public double ZoomFactor { get; private set; }
    }
}

