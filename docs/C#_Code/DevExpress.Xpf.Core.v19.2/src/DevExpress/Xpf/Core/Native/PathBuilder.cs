namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PathBuilder
    {
        public PathBuilder(Point startPoint, Point endPoint, double speedRate);
        protected internal void CalcArcAngles();
        protected internal void CalcCenterPoint(Point referencePoint, bool isReferenceInside);
        public void CreateArcPath(double radius, Point referencePoint, bool isReferenceInside);
        public void CreateLinePath();
        protected internal Point GetArcPoint();
        protected internal Point GetLinePoint();
        public Point GetPoint();
        protected internal Point NextPoint(double x, double y);
        public void Reset();

        public int NumberOfPoints { get; private set; }

        public Point StartPoint { get; private set; }

        public Point EndPoint { get; private set; }

        protected internal double SpeedRate { get; set; }

        protected internal double Radius { get; set; }

        protected internal Point CenterPoint { get; set; }

        protected internal bool IsArc { get; set; }

        protected internal double DeltaX { get; set; }

        protected internal double DeltaY { get; set; }

        protected internal int CurrentPointNumber { get; set; }

        protected internal double StartAngle { get; set; }

        protected internal double EndAngle { get; set; }

        protected internal double DeltaAngle { get; set; }

        protected internal bool IsPath { get; }
    }
}

