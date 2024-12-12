namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;

    public class AutoHideViewRegularDragListener : RegularListener
    {
        public override void OnLeave()
        {
            this.View.AdornerHelper.BeginHideAdornerWindowAndResetTabHeadersHints();
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;
    }
}

