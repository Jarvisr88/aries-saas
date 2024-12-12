namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Windows;

    public class HideClosingItemsBehavior : Behavior<DockLayoutManager>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.DockItemClosing += new DockItemCancelEventHandler(this.OnDockItemClosing);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.DockItemClosing -= new DockItemCancelEventHandler(this.OnDockItemClosing);
        }

        private void OnDockItemClosing(object sender, ItemCancelEventArgs e)
        {
            e.Item.Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
    }
}

