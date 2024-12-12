namespace DevExpress.Xpf.Core.HitTest
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PointHitTestResult : HitTestResult
    {
        public PointHitTestResult(DependencyObject visualHit, Point pointHit) : base(visualHit)
        {
            this.PointHit = pointHit;
        }

        public Point PointHit { get; private set; }
    }
}

