namespace DevExpress.Xpf.Bars
{
    using System;

    public class BarManagerMenuController : BarManagerLinksHolderController
    {
        public BarManagerMenuController();
        public BarManagerMenuController(PopupMenu menu);
        protected override void SetUpActionContainer(BarManagerActionContainer container);

        public PopupMenu Menu { get; set; }
    }
}

