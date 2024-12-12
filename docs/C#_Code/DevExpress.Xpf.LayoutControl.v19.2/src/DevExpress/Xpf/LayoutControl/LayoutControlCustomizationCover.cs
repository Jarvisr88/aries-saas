namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class LayoutControlCustomizationCover : Canvas
    {
        public LayoutControlCustomizationCover()
        {
            base.Background = new SolidColorBrush(Colors.Transparent);
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters) => 
            ((this.HitTestClip == null) || this.HitTestClip.FillContains(hitTestParameters.HitPoint)) ? base.HitTestCore(hitTestParameters) : null;

        public Geometry HitTestClip { get; set; }
    }
}

