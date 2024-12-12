namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class RectangleUtils
    {
        public static Rectangle BoundingRectangle(params Point[] points)
        {
            int length = points.Length;
            if (length == 0)
            {
                return Rectangle.Empty;
            }
            int x = points[0].X;
            int num3 = x;
            int y = points[0].Y;
            int num5 = y;
            for (int i = 1; i < length; i++)
            {
                Point point = points[i];
                x = Math.Min(point.X, x);
                y = Math.Min(point.Y, y);
                num3 = Math.Max(point.X, num3);
                num5 = Math.Max(point.Y, num5);
            }
            return new Rectangle(x, y, num3 - x, num5 - y);
        }

        public static RectangleF BoundingRectangle(params PointF[] points)
        {
            int length = points.Length;
            if (length == 0)
            {
                return RectangleF.Empty;
            }
            float x = points[0].X;
            float num3 = x;
            float y = points[0].Y;
            float num5 = y;
            for (int i = 1; i < length; i++)
            {
                PointF tf = points[i];
                x = Math.Min(tf.X, x);
                y = Math.Min(tf.Y, y);
                num3 = Math.Max(tf.X, num3);
                num5 = Math.Max(tf.Y, num5);
            }
            return new RectangleF(x, y, num3 - x, num5 - y);
        }

        public static Rectangle BoundingRectangle(Rectangle bounds, Matrix transform)
        {
            if (transform == null)
            {
                return bounds;
            }
            Point point = transform.TransformPoint(new Point(bounds.Right, bounds.Bottom));
            Point point2 = transform.TransformPoint(new Point(bounds.Right, bounds.Top));
            Point point3 = transform.TransformPoint(new Point(bounds.Left, bounds.Bottom));
            Point point4 = transform.TransformPoint(bounds.Location);
            Point[] points = new Point[] { point, point2, point3, point4 };
            return BoundingRectangle(points);
        }

        public static Rectangle BoundingRectangle(Rectangle bounds, float angleInDegrees)
        {
            if ((angleInDegrees % 360f) == 0f)
            {
                return bounds;
            }
            using (Matrix matrix = new Matrix())
            {
                matrix.RotateAt(angleInDegrees, (PointF) CenterPoint(bounds));
                return BoundingRectangle(bounds, matrix);
            }
        }

        public static RectangleF BoundingRectangle(RectangleF bounds, Matrix transform)
        {
            PointF[] pts = new PointF[] { new PointF(bounds.Right, bounds.Bottom), new PointF(bounds.Right, bounds.Top), new PointF(bounds.Left, bounds.Bottom), bounds.Location };
            transform.TransformPoints(pts);
            PointF[] points = new PointF[] { pts[0], pts[1], pts[2], pts[3] };
            return BoundingRectangle(points);
        }

        public static RectangleF BoundingRectangle(RectangleF bounds, float angleInDegrees)
        {
            if ((angleInDegrees % 360f) == 0f)
            {
                return bounds;
            }
            using (Matrix matrix = new Matrix())
            {
                matrix.RotateAt(angleInDegrees, CenterPoint(bounds));
                return BoundingRectangle(bounds, matrix);
            }
        }

        public static Point CenterPoint(Rectangle rectangle) => 
            new Point((rectangle.Right + rectangle.Left) / 2, (rectangle.Bottom + rectangle.Top) / 2);

        public static PointF CenterPoint(RectangleF rectangle) => 
            new PointF((rectangle.Right + rectangle.Left) / 2f, (rectangle.Bottom + rectangle.Top) / 2f);

        public static unsafe Rectangle Decrease(Rectangle rect, int value)
        {
            Rectangle rectangle = rect;
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.Width -= value;
            Rectangle* rectanglePtr2 = &rectangle;
            rectanglePtr2.Height -= value;
            return rectangle;
        }

        public static unsafe Rectangle Deflate(Rectangle rect, int value)
        {
            if (value == 0)
            {
                return rect;
            }
            Rectangle rectangle = rect;
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.X += value;
            Rectangle* rectanglePtr2 = &rectangle;
            rectanglePtr2.Width -= 2 * value;
            Rectangle* rectanglePtr3 = &rectangle;
            rectanglePtr3.Y += value;
            Rectangle* rectanglePtr4 = &rectangle;
            rectanglePtr4.Height -= 2 * value;
            return rectangle;
        }

        public static unsafe Rectangle Increase(Rectangle rect, int value)
        {
            Rectangle rectangle = rect;
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.Width += value;
            Rectangle* rectanglePtr2 = &rectangle;
            rectanglePtr2.Height += value;
            return rectangle;
        }

        public static unsafe Rectangle Inflate(Rectangle rect, int value)
        {
            if (value == 0)
            {
                return rect;
            }
            Rectangle rectangle = rect;
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.X -= value;
            Rectangle* rectanglePtr2 = &rectangle;
            rectanglePtr2.Width += 2 * value;
            Rectangle* rectanglePtr3 = &rectangle;
            rectanglePtr3.Y -= value;
            Rectangle* rectanglePtr4 = &rectangle;
            rectanglePtr4.Height += 2 * value;
            return rectangle;
        }

        public static unsafe Rectangle Offset(Rectangle rectangle, Point offset)
        {
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.X += offset.X;
            Rectangle* rectanglePtr2 = &rectangle;
            rectanglePtr2.Y += offset.Y;
            return rectangle;
        }

        public static unsafe Rectangle Offset(Rectangle rectangle, int offsetX, int offsetY)
        {
            Rectangle* rectanglePtr1 = &rectangle;
            rectanglePtr1.X += offsetX;
            Rectangle* rectanglePtr2 = &rectangle;
            rectanglePtr2.Y += offsetY;
            return rectangle;
        }

        public static unsafe RectangleF Offset(RectangleF rectangle, float offsetX, float offsetY)
        {
            RectangleF* efPtr1 = &rectangle;
            efPtr1.X += offsetX;
            RectangleF* efPtr2 = &rectangle;
            efPtr2.Y += offsetY;
            return rectangle;
        }

        public static unsafe RectangleF Scale(RectangleF rectangle, float scaleFactor)
        {
            RectangleF* efPtr1 = &rectangle;
            efPtr1.X *= scaleFactor;
            RectangleF* efPtr2 = &rectangle;
            efPtr2.Y *= scaleFactor;
            RectangleF* efPtr3 = &rectangle;
            efPtr3.Width *= scaleFactor;
            RectangleF* efPtr4 = &rectangle;
            efPtr4.Height *= scaleFactor;
            return rectangle;
        }

        public static Rectangle[] SplitHorizontally(Rectangle bounds, int cellCount)
        {
            if (cellCount <= 0)
            {
                return new Rectangle[] { bounds };
            }
            Rectangle[] rectangleArray = new Rectangle[cellCount];
            int x = bounds.X;
            int height = bounds.Height;
            int width = bounds.Width;
            int num4 = width / cellCount;
            int num5 = width - (num4 * cellCount);
            int index = 0;
            while (index < cellCount)
            {
                int num7 = num4 + ((num5 > 0) ? 1 : 0);
                rectangleArray[index] = new Rectangle(x, bounds.Y, num7, height);
                x += num7;
                index++;
                num5--;
            }
            return rectangleArray;
        }

        public static Rectangle Swap(Rectangle rectangle)
        {
            int num = (rectangle.Width - rectangle.Height) / 2;
            return new Rectangle(rectangle.X + num, rectangle.Y - num, rectangle.Height, rectangle.Width);
        }

        public static RectangleF SwapWidthAndHeight(RectangleF rectangle)
        {
            float num = (rectangle.Height - rectangle.Width) / 2f;
            return new RectangleF(rectangle.X - num, rectangle.Y + num, rectangle.Height, rectangle.Width);
        }

        public static unsafe Rectangle ToRelativeBounds(Rectangle from, Rectangle to)
        {
            Rectangle* rectanglePtr1 = &to;
            rectanglePtr1.X -= from.X;
            Rectangle* rectanglePtr2 = &to;
            rectanglePtr2.Y -= from.Y;
            return to;
        }
    }
}

