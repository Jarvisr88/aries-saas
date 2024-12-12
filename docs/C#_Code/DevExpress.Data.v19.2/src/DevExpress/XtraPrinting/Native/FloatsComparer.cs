namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class FloatsComparer
    {
        public const double DefaultEpsilon = 0.001;
        public static FloatsComparer Default = new FloatsComparer(0.001);
        private double epsilon;

        protected FloatsComparer(double epsilon)
        {
            this.epsilon = epsilon;
        }

        public int CompareDoubles(double first, double second) => 
            ComparingUtils.CompareDoubles(first, second, this.epsilon);

        public bool ContainsByX(RectangleF rect1, RectangleF rect2) => 
            this.FirstLessOrEqualSecond((double) rect1.X, (double) rect2.X) && this.FirstLessOrEqualSecond((double) rect2.Right, (double) rect1.Right);

        public bool ContainsByY(RectangleF rect1, RectangleF rect2) => 
            this.FirstLessOrEqualSecond((double) rect1.Y, (double) rect2.Y) && this.FirstLessOrEqualSecond((double) rect2.Bottom, (double) rect1.Bottom);

        public bool FirstEqualsSecond(double first, double second) => 
            ComparingUtils.CompareDoubles(first, second, this.epsilon) == 0;

        public bool FirstGreaterOrEqualSecond(double first, double second) => 
            ComparingUtils.CompareDoubles(first, second, this.epsilon) >= 0;

        public bool FirstGreaterSecond(double first, double second) => 
            ComparingUtils.CompareDoubles(first, second, this.epsilon) > 0;

        public bool FirstGreaterSecondLessThird(double first, double second, double third) => 
            this.FirstGreaterSecond(first, second) && this.FirstLessSecond(first, third);

        public bool FirstLessOrEqualSecond(double first, double second) => 
            ComparingUtils.CompareDoubles(first, second, this.epsilon) <= 0;

        public bool FirstLessSecond(double first, double second) => 
            ComparingUtils.CompareDoubles(first, second, this.epsilon) < 0;

        public bool IntersectByX(RectangleF rect1, RectangleF rect2) => 
            this.FirstLessSecond((double) rect1.X, (double) rect2.Right) && this.FirstLessSecond((double) rect2.X, (double) rect1.Right);

        public bool IntersectByY(RectangleF rect1, RectangleF rect2) => 
            this.FirstLessSecond((double) rect1.Y, (double) rect2.Bottom) && this.FirstLessSecond((double) rect2.Y, (double) rect1.Bottom);

        public bool PointFEquals(PointF point1, PointF point2) => 
            (ComparingUtils.CompareDoubles((double) point1.X, (double) point2.X, this.epsilon) == 0) && (ComparingUtils.CompareDoubles((double) point1.Y, (double) point2.Y, this.epsilon) == 0);

        public bool RectangleFEquals(RectangleF rectangle1, RectangleF rectangle2) => 
            this.PointFEquals(rectangle1.Location, rectangle2.Location) && this.SizeFEquals(rectangle1.Size, rectangle2.Size);

        public bool RectangleIsEmpty(RectangleF rect) => 
            this.FirstGreaterSecond((double) rect.Width, 0.0) ? (this.FirstLessSecond((double) rect.Height, 0.0) || this.FirstEqualsSecond((double) rect.Height, 0.0)) : true;

        public bool SizeFEquals(SizeF size1, SizeF size2) => 
            (ComparingUtils.CompareDoubles((double) size1.Width, (double) size2.Width, this.epsilon) == 0) && (ComparingUtils.CompareDoubles((double) size1.Height, (double) size2.Height, this.epsilon) == 0);
    }
}

