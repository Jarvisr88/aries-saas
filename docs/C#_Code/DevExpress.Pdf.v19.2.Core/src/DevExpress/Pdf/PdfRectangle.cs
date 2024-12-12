namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfRectangle
    {
        private readonly double left;
        private readonly double bottom;
        private readonly double right;
        private readonly double top;

        internal PdfRectangle(PdfPoint point1, PdfPoint point2)
        {
            double x = point1.X;
            double num2 = point2.X;
            if (x < num2)
            {
                this.left = x;
                this.right = num2;
            }
            else
            {
                this.left = num2;
                this.right = x;
            }
            double y = point1.Y;
            double num4 = point2.Y;
            if (y < num4)
            {
                this.bottom = y;
                this.top = num4;
            }
            else
            {
                this.bottom = num4;
                this.top = y;
            }
        }

        public PdfRectangle(double left, double bottom, double right, double top) : this(left, bottom, right, top, true)
        {
        }

        private PdfRectangle(double left, double bottom, double right, double top, bool shoudlValidate)
        {
            if (shoudlValidate)
            {
                if (left > right)
                {
                    throw new ArgumentOutOfRangeException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectRectangleWidth));
                }
                if (bottom > top)
                {
                    throw new ArgumentOutOfRangeException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectRectangleHeight));
                }
            }
            this.left = left;
            this.bottom = bottom;
            this.right = right;
            this.top = top;
        }

        internal static bool AreEqual(PdfRectangle r1, PdfRectangle r2, double eps) => 
            (r1 != null) ? ((r2 != null) ? (CheckNumbers(r1.left, r2.left, eps) && (CheckNumbers(r1.bottom, r2.bottom, eps) && (CheckNumbers(r1.right, r2.right, eps) && CheckNumbers(r1.top, r2.top, eps)))) : false) : ReferenceEquals(r2, null);

        private static bool CheckNumbers(double a, double b, double eps) => 
            Math.Abs((double) (a - b)) <= eps;

        public bool Contains(PdfPoint point) => 
            (this.left <= point.X) && ((this.right >= point.X) && ((this.top >= point.Y) && (this.bottom <= point.Y)));

        internal bool Contains(PdfRectangle rectangle) => 
            (this.left <= rectangle.left) && ((this.right >= rectangle.right) && ((this.bottom <= rectangle.bottom) && (this.top >= rectangle.top)));

        private static double ConvertToDouble(object value, PdfObjectCollection collection)
        {
            value = (collection == null) ? value : collection.TryResolve(value, null);
            if (value is double)
            {
                return (double) value;
            }
            if (value is int)
            {
                return (double) ((int) value);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return -1.0;
        }

        internal static PdfRectangle CreateBoundingBox(IList<PdfPoint> points)
        {
            int count = points.Count;
            if (count == 0)
            {
                return null;
            }
            PdfPoint point = points[0];
            double x = point.X;
            double right = x;
            double y = point.Y;
            double top = y;
            for (int i = 1; i < count; i++)
            {
                point = points[i];
                double num7 = point.X;
                if (num7 < x)
                {
                    x = num7;
                }
                else if (num7 > right)
                {
                    right = num7;
                }
                double num8 = point.Y;
                if (num8 < y)
                {
                    y = num8;
                }
                else if (num8 > top)
                {
                    top = num8;
                }
            }
            return new PdfRectangle(x, y, right, top);
        }

        internal static PdfRectangle CreateBoundingBox(params PdfPoint[] points) => 
            CreateBoundingBox((IList<PdfPoint>) points);

        public override bool Equals(object obj)
        {
            PdfRectangle rectangle = obj as PdfRectangle;
            return ((rectangle != null) && ((this.left == rectangle.left) && ((this.right == rectangle.right) && ((this.top == rectangle.top) && (this.bottom == rectangle.bottom)))));
        }

        public override int GetHashCode() => 
            (((((((-1064806749 * -1521134295) + this.left.GetHashCode()) * -1521134295) + this.bottom.GetHashCode()) * -1521134295) + this.right.GetHashCode()) * -1521134295) + this.top.GetHashCode();

        internal static PdfRectangle Inflate(PdfRectangle rect, double amount) => 
            (((amount * 2.0) > rect.Width) || ((amount * 2.0) > rect.Height)) ? rect : new PdfRectangle(rect.Left + amount, rect.Bottom + amount, rect.Right - amount, rect.Top - amount);

        internal static PdfRectangle Intersect(PdfRectangle r1, PdfRectangle r2) => 
            !r1.Intersects(r2) ? null : new PdfRectangle(Math.Max(r1.Left, r2.Left), Math.Max(r1.Bottom, r2.Bottom), Math.Min(r1.Right, r2.Right), Math.Min(r1.Top, r2.Top));

        internal bool Intersects(PdfRectangle rectangle) => 
            (this.left <= rectangle.Right) && ((this.right >= rectangle.Left) && ((this.top >= rectangle.Bottom) && (this.bottom <= rectangle.Top)));

        internal static PdfRectangle Parse(IList<object> array, PdfObjectCollection collection)
        {
            if (array.Count != 4)
            {
                return null;
            }
            double left = ConvertToDouble(array[0], collection);
            double bottom = ConvertToDouble(array[1], collection);
            double right = ConvertToDouble(array[2], collection);
            double top = ConvertToDouble(array[3], collection);
            if (right < left)
            {
                right = left;
                left = right;
            }
            if (top < bottom)
            {
                bottom = top;
                top = bottom;
            }
            return new PdfRectangle(left, bottom, right, top, false);
        }

        protected internal PdfWritableDoubleArray ToWritableObject()
        {
            double[] numArray1 = new double[] { this.left, this.bottom, this.right, this.top };
            return new PdfWritableDoubleArray(numArray1);
        }

        internal PdfRectangle Trim(PdfRectangle rectangle)
        {
            double left = Math.Max(this.left, rectangle.left);
            double bottom = Math.Max(this.bottom, rectangle.bottom);
            double right = Math.Min(this.right, rectangle.right);
            double top = Math.Min(this.top, rectangle.top);
            return (((left > right) || (bottom > top)) ? null : new PdfRectangle(left, bottom, right, top));
        }

        internal static PdfRectangle Union(PdfRectangle rect1, PdfRectangle rect2) => 
            (rect1 != null) ? ((rect2 != null) ? new PdfRectangle(PdfMathUtils.Min(rect1.left, rect2.left), PdfMathUtils.Min(rect1.bottom, rect2.bottom), PdfMathUtils.Max(rect1.right, rect2.right), PdfMathUtils.Max(rect1.top, rect2.top)) : rect1) : rect2;

        internal static PdfRectangle Infinite { get; }

        public double Left =>
            this.left;

        public double Bottom =>
            this.bottom;

        public double Right =>
            this.right;

        public double Top =>
            this.top;

        public double Width =>
            this.right - this.left;

        public double Height =>
            this.top - this.bottom;

        public PdfPoint BottomLeft =>
            new PdfPoint(this.left, this.bottom);

        public PdfPoint TopLeft =>
            new PdfPoint(this.left, this.top);

        public PdfPoint BottomRight =>
            new PdfPoint(this.right, this.bottom);

        public PdfPoint TopRight =>
            new PdfPoint(this.right, this.top);
    }
}

