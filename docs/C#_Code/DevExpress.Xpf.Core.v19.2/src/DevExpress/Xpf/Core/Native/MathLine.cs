namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class MathLine
    {
        public const double Precision = 1E-05;

        public MathLine();
        public MathLine(Point p1, Point p2);
        public MathLine(double a, double b, double c);
        public double CalcLineFunction(Point point);
        public double CalcX(double y);
        public double CalcY(double x);
        public double Distance(Point point);
        public Point? Intersect(MathLine line);
        public bool IsDifferentSide(Point point, Point otherPoint);
        public bool IsSameSide(Point point, Point otherPoint);
        public MathLine Perpendicular(Point p);

        public double A { get; protected internal set; }

        public double B { get; protected internal set; }

        public double C { get; protected internal set; }

        public bool IsValid { get; }

        public bool IsVertical { get; }

        public bool IsHorizontal { get; }

        public double AngleCoefficient { get; }

        public bool IsMoreHorizontal { get; }

        public bool IsMoreVertical { get; }

        public double AbscissAngleRad { get; }

        public double AbscissAngleDeg { get; }
    }
}

