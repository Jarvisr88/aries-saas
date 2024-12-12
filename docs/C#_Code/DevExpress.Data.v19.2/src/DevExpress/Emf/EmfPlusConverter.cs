namespace DevExpress.Emf
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class EmfPlusConverter
    {
        private static TResult[] ConvertArray<TInput, TResult>(TInput[] array, Func<TInput, TResult> converter)
        {
            int length = array.Length;
            TResult[] localArray = new TResult[length];
            for (int i = 0; i < length; i++)
            {
                localArray[i] = converter(array[i]);
            }
            return localArray;
        }

        public static ARGBColor ConvertColor(Color color) => 
            ARGBColor.FromArgb(color.A, color.R, color.G, color.B);

        public static DXPathPointTypes[] ConvertPathPointTypes(byte[] pathTypes)
        {
            Func<byte, DXPathPointTypes> converter = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<byte, DXPathPointTypes> local1 = <>c.<>9__5_0;
                converter = <>c.<>9__5_0 = type => (DXPathPointTypes) type;
            }
            return ConvertArray<byte, DXPathPointTypes>(pathTypes, converter);
        }

        public static DXPointF[] ConvertPoints(Point[] points)
        {
            Func<Point, DXPointF> converter = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<Point, DXPointF> local1 = <>c.<>9__4_0;
                converter = <>c.<>9__4_0 = point => new DXPointF((float) point.X, (float) point.Y);
            }
            return ConvertArray<Point, DXPointF>(points, converter);
        }

        public static DXPointF[] ConvertPoints(PointF[] points)
        {
            Func<PointF, DXPointF> converter = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<PointF, DXPointF> local1 = <>c.<>9__3_0;
                converter = <>c.<>9__3_0 = point => new DXPointF(point.X, point.Y);
            }
            return ConvertArray<PointF, DXPointF>(points, converter);
        }

        public static DXRectangleF ConvertRectangle(RectangleF rectangle) => 
            new DXRectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EmfPlusConverter.<>c <>9 = new EmfPlusConverter.<>c();
            public static Func<PointF, DXPointF> <>9__3_0;
            public static Func<Point, DXPointF> <>9__4_0;
            public static Func<byte, DXPathPointTypes> <>9__5_0;

            internal DXPathPointTypes <ConvertPathPointTypes>b__5_0(byte type) => 
                (DXPathPointTypes) type;

            internal DXPointF <ConvertPoints>b__3_0(PointF point) => 
                new DXPointF(point.X, point.Y);

            internal DXPointF <ConvertPoints>b__4_0(Point point) => 
                new DXPointF((float) point.X, (float) point.Y);
        }
    }
}

