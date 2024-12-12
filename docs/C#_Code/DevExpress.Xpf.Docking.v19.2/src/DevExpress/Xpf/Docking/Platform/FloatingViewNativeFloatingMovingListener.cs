namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class FloatingViewNativeFloatingMovingListener : FloatingViewFloatingMovingListener
    {
        public override void OnBegin(Point point, ILayoutElement element)
        {
            base.OnBegin(point, element);
            this.UpdateFloatGroupBounds();
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            if (!this.UpdateFloatGroupBounds())
            {
                base.OnDragging(point, element);
            }
        }

        private bool UpdateFloatGroupBounds() => 
            base.View.FloatGroup.RequestFloatingBoundsUpdate();
    }
}

