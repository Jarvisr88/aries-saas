namespace DevExpress.Xpf.Core.HitTest
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class HitTestResult
    {
        public HitTestResult(DependencyObject visualHit)
        {
            this.VisualHit = visualHit;
        }

        public DependencyObject VisualHit { get; private set; }
    }
}

