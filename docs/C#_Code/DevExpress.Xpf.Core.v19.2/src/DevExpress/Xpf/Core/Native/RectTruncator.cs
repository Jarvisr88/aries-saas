namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class RectTruncator
    {
        private const double Precision = 0.5;
        public RectTruncatorResultType ResultType;

        public RectTruncator(Rect rect, MathLine line, RectCorner corner);
        protected internal int CalcNumberOfIntersectPoints();
        protected internal void CorrectPointTypesArrayByDistances();
        protected internal void CorrectPointTypesArrayByNearestOutsidePointsCounterArray();
        protected internal void CreateNearestOutsidePointsCounterArray();
        protected internal void CreatePaths();
        protected internal void CreatePointsArray();
        protected internal void CreatePointTypesArray();
        protected internal int FindIntersectPointIndex(int startIndex);
        protected internal int Index(int i);
        protected internal bool IsIntersectPoint(Point point, int index);
        protected internal bool IsIntersectPointsMindless();
        protected internal bool IsInvalidNumberOfIntersectPoints();
        protected internal void Proceed();
        protected internal void ProceedPath();
        protected internal void ProceedValidLine();
        protected internal static int SafeArrayIndex(int i);

        public PointCollection ResultPoints { get; set; }

        public PointCollection InvertedResultPoints { get; set; }

        protected internal Rect Rectangle { get; set; }

        protected internal MathLine Line { get; set; }

        protected internal RectCorner Corner { get; set; }

        protected internal Point?[] Points { get; set; }

        protected internal RectTruncator.PointType[] PointTypes { get; set; }

        protected internal int[] NearestOutsidePointsCounters { get; set; }

        protected internal Point? TopIntersectPoint { get; }

        protected internal Point? BottomIntersectPoint { get; }

        protected internal Point? RightIntersectPoint { get; }

        protected internal Point? LeftIntersectPoint { get; }

        protected internal bool IsCornerDeterminant { get; }

        protected internal int FirstPointIndex { get; }

        private protected enum PointType
        {
            public const RectTruncator.PointType Null = RectTruncator.PointType.Null;,
            public const RectTruncator.PointType Simple = RectTruncator.PointType.Simple;,
            public const RectTruncator.PointType Intersect = RectTruncator.PointType.Intersect;,
            public const RectTruncator.PointType Outside = RectTruncator.PointType.Outside;
        }
    }
}

