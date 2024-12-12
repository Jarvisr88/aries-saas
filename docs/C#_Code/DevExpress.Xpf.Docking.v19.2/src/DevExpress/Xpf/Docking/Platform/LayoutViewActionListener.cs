namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core.Actions;
    using System;

    public class LayoutViewActionListener : ActionServiceListener
    {
        public override void OnHideSelection()
        {
            if (this.View.IsAdornerHelperInitialized)
            {
                this.View.AdornerHelper.HideSelection();
            }
        }

        public override void OnShowSelection()
        {
            if (this.View.IsAdornerHelperInitialized)
            {
                this.View.AdornerHelper.ShowSelection();
            }
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;
    }
}

