namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfPoint
    {
        internal static readonly PdfPoint Empty;
        private readonly double x;
        private readonly double y;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator +(PdfPoint left, PdfPoint right) => 
            new PdfPoint(left.X + right.X, left.Y + right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator -(PdfPoint value) => 
            new PdfPoint(-value.X, -value.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator -(PdfPoint left, PdfPoint right) => 
            new PdfPoint(left.X - right.X, left.Y - right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator *(PdfPoint left, double right) => 
            new PdfPoint(left.X * right, left.Y * right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator *(PdfPoint left, PdfPoint right) => 
            new PdfPoint(left.X * right.X, right.Y * left.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator *(double left, PdfPoint right) => 
            new PdfPoint(left * right.X, left * right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator /(PdfPoint left, PdfPoint right) => 
            new PdfPoint(left.X / right.X, left.Y / right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PdfPoint operator /(PdfPoint value1, double value2) => 
            new PdfPoint(value1.X / value2, value1.Y / value2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PdfPoint Add(PdfPoint vec1, PdfPoint vec2) => 
            new PdfPoint(vec1.X + vec2.X, vec1.Y + vec2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PdfPoint Sub(PdfPoint vec1, PdfPoint vec2) => 
            new PdfPoint(vec1.X - vec2.X, vec1.Y - vec2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PdfPoint Lerp(PdfPoint vec1, PdfPoint vec2, double t) => 
            new PdfPoint(vec1.X + ((vec2.X - vec1.X) * t), vec1.Y + ((vec2.Y - vec1.Y) * t));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double Dot(PdfPoint vec1, PdfPoint vec2) => 
            (vec1.X * vec2.X) + (vec1.Y * vec2.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double Distance(PdfPoint vec1, PdfPoint vec2)
        {
            double num = vec2.X - vec1.X;
            double num2 = vec2.Y - vec1.Y;
            return Math.Sqrt((num * num) + (num2 * num2));
        }

        internal static double Distance(PdfRectangle rect, PdfPoint point)
        {
            double left = rect.Left;
            double bottom = rect.Bottom;
            double right = rect.Right;
            double top = rect.Top;
            return Math.Min(Math.Min(Distance(point, left, top, right, top), Distance(point, right, top, right, bottom)), Math.Min(Distance(point, right, bottom, left, bottom), Distance(point, left, bottom, left, top)));
        }

        private static double Distance(PdfPoint point, double startX, double startY, double endX, double endY)
        {
            double num = endX - startX;
            double num2 = endY - startY;
            double num3 = (num * (point.X - startX)) + (num2 * (point.Y - startY));
            if (num3 <= 0.0)
            {
                return Distance(point, new PdfPoint(startX, startY));
            }
            double num4 = (num * num) + (num2 * num2);
            if (num4 <= num3)
            {
                return Distance(point, new PdfPoint(endX, endY));
            }
            double num5 = num3 / num4;
            return Distance(point, new PdfPoint(startX + (num * num5), startY + (num2 * num5)));
        }

        public double X =>
            this.x;
        public double Y =>
            this.y;
        public PdfPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        internal PdfPoint(PdfStack operands)
        {
            this.y = operands.PopDouble();
            this.x = operands.PopDouble();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal double Length() => 
            Math.Sqrt((this.X * this.X) + (this.Y * this.Y));

        static PdfPoint()
        {
        }
    }
}

