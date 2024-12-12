namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.VisualElements;
    using System;

    public class AutoHideViewActionListener : LayoutViewActionListener
    {
        public override void OnHide(bool immediately)
        {
            if (immediately)
            {
                this.Tray.DoClosePanel();
            }
            else
            {
                this.Tray.DoCollapseIfPossible(false);
            }
        }

        protected AutoHideTray Tray =>
            ((AutoHideView) base.View).Tray;
    }
}

