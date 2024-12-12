namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Selection;
    using System;

    public class LayoutViewSelectionListener : SelectionListener
    {
        public override SelectionMode CheckMode(ILayoutElement item) => 
            !KeyHelper.IsCtrlPressed ? (!KeyHelper.IsShiftPressed ? SelectionMode.SingleItem : SelectionMode.ItemRange) : SelectionMode.MultipleItems;

        public override void OnProcessSelection(ILayoutElement layoutElement)
        {
            DockingHintAdornerBase selectionAdorner = this.View.AdornerHelper.SelectionAdorner;
            if (selectionAdorner != null)
            {
                BaseLayoutItem layoutItem = ((IDockLayoutElement) layoutElement).Item;
                selectionAdorner.SelectionController.Focus(layoutItem);
            }
        }

        public override void OnSelectionChanged(ILayoutElement element, bool selected)
        {
            ((IDockLayoutElement) element).Item.SetSelected(this.View.Container, selected);
        }

        public override bool OnSelectionChanging(ILayoutElement element, bool selected)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            return !this.View.Container.RaiseItemSelectionChangingEvent(item, selected);
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;
    }
}

