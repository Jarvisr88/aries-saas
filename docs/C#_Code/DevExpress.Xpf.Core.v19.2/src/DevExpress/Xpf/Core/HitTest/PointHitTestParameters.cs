namespace DevExpress.Xpf.Core.HitTest
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PointHitTestParameters : HitTestParameters
    {
        public PointHitTestParameters(Point point)
        {
            this.HitPoint = point;
        }

        public Point HitPoint { get; protected set; }
    }
}

