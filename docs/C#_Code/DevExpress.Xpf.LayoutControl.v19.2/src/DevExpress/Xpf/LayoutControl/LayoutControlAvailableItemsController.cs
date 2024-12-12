namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;

    public class LayoutControlAvailableItemsController : ControlControllerBase
    {
        public LayoutControlAvailableItemsController(ILayoutControlAvailableItemsControl control) : base(control)
        {
        }

        protected override void OnIsMouseEnteredChanged()
        {
            base.OnIsMouseEnteredChanged();
            this.IControl.IsListOpen = base.IsMouseEntered;
        }

        public ILayoutControlAvailableItemsControl IControl =>
            (ILayoutControlAvailableItemsControl) base.IControl;
    }
}

