namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;

    public class ShowingDockHintsAttachedBehavior : Behavior<DockLayoutManager>
    {
        private void AssociatedObjectOnShowingDockHints(object sender, ShowingDockHintsEventArgs e)
        {
            e.Hide(DockGuide.Top);
            e.Hide(DockGuide.Bottom);
            e.Hide(DockGuide.Center);
            e.Handled = true;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.ShowingDockHints += new ShowingDockHintsEventHandler(this.AssociatedObjectOnShowingDockHints);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.ShowingDockHints -= new ShowingDockHintsEventHandler(this.AssociatedObjectOnShowingDockHints);
        }
    }
}

