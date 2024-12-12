namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfOrientedRectangle
    {
        private readonly double top;
        private readonly double left;
        private readonly double width;
        private readonly double height;
        private readonly double angle;
        private PdfRectangle boundingRectangle;
        private IList<PdfPoint> vertices;

        public PdfOrientedRectangle(PdfPoint topLeft, double width, double height) : this(topLeft, width, height, 0.0)
        {
        }

        public PdfOrientedRectangle(PdfPoint topLeft, double width, double height, double angle)
        {
            this.top = topLeft.Y;
            this.left = topLeft.X;
            this.width = width;
            this.height = height;
            this.angle = PdfMathUtils.NormalizeAngle(angle);
        }

        private PdfPoint CalcBottomLeft(double sin, double cos) => 
            new PdfPoint(this.left + (this.height * sin), this.top - (this.height * cos));

        private PdfPoint CalcTopRight(double sin, double cos) => 
            new PdfPoint(this.left + (this.width * cos), this.top + (this.width * sin));

        public bool Contains(PdfOrientedRectangle rectangle)
        {
            bool flag;
            using (IEnumerator<PdfPoint> enumerator = rectangle.Vertices.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfPoint current = enumerator.Current;
                        if (this.Contains(current))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        public bool Contains(PdfPoint point) => 
            this.PointIsInRect(point, 0.0, 0.0);

        internal IList<PdfPoint> GetBoundingBoxPoints(double widthFactor, double heightFactor)
        {
            double num = widthFactor * this.height;
            double num2 = heightFactor * this.height;
            return new PdfOrientedRectangle(new PdfPoint(this.OffsetLeft(-num, -num2), this.OffsetTop(-num, -num2)), this.width + (2.0 * num), this.height + (2.0 * num2), this.angle).Vertices;
        }

        private double OffsetLeft(double width, double height) => 
            (this.left + (width * Math.Cos(this.angle))) + (height * Math.Sin(this.angle));

        private double OffsetTop(double width, double height) => 
            (this.top + (width * Math.Sin(this.angle))) - (height * Math.Cos(this.angle));

        internal bool PointIsInRect(PdfPoint point, double expandX = 0.0, double expandY = 0.0)
        {
            PdfPoint point2 = PdfTextUtils.RotatePoint(point, -this.angle);
            PdfPoint point3 = PdfTextUtils.RotatePoint(this.TopLeft, -this.angle);
            return ((point2.X >= (point3.X - expandX)) && ((point2.X <= ((point3.X + this.width) + expandX)) && ((point2.Y <= (point3.Y + expandY)) && (point2.Y >= ((point3.Y - this.height) - expandY)))));
        }

        public double Left =>
            this.left;

        public double Top =>
            this.top;

        public double Width =>
            this.width;

        public double Height =>
            this.height;

        public double Angle =>
            this.angle;

        public IList<PdfPoint> Vertices
        {
            get
            {
                if (this.vertices == null)
                {
                    List<PdfPoint> list = new List<PdfPoint>();
                    double cos = Math.Cos(this.angle);
                    double sin = Math.Sin(this.angle);
                    list.Add(this.TopLeft);
                    list.Add(this.CalcTopRight(sin, cos));
                    list.Add(new PdfPoint((this.left + (this.width * cos)) + (this.height * sin), (this.top - (this.height * cos)) + (this.width * sin)));
                    list.Add(this.CalcBottomLeft(sin, cos));
                    this.vertices = list.AsReadOnly();
                }
                return this.vertices;
            }
        }

        public PdfRectangle BoundingRectangle
        {
            get
            {
                if (this.boundingRectangle == null)
                {
                    PdfPoint topLeft = this.TopLeft;
                    PdfPoint point2 = PdfTextUtils.RotatePoint(topLeft, -this.angle);
                    double x = point2.X;
                    double y = point2.Y;
                    double num3 = y - this.height;
                    PdfPoint[] points = new PdfPoint[] { topLeft, PdfTextUtils.RotatePoint(new PdfPoint(x + this.width, y), this.angle), PdfTextUtils.RotatePoint(new PdfPoint(x, num3), this.angle), PdfTextUtils.RotatePoint(new PdfPoint(x + this.width, num3), this.angle) };
                    this.boundingRectangle = PdfRectangle.CreateBoundingBox(points);
                }
                return this.boundingRectangle;
            }
        }

        internal PdfPoint TopLeft =>
            new PdfPoint(this.left, this.top);

        internal double Bottom =>
            this.OffsetTop(this.width, this.height);

        internal double Right =>
            this.OffsetLeft(this.width, this.height);

        internal PdfPoint TopRight =>
            this.CalcTopRight(Math.Sin(this.angle), Math.Cos(this.angle));

        internal PdfPoint BottomLeft =>
            this.CalcBottomLeft(Math.Sin(this.angle), Math.Cos(this.angle));
    }
}

