namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;

    public class BarMDIButtonItem : BarButtonItem
    {
        public BarMDIButtonItem()
        {
            base.IsPrivate = true;
        }

        public override BarItemLink CreateLink(bool isPrivate) => 
            new BarMDIButtonItemLink(this);

        protected override Type GetLinkType() => 
            typeof(BarMDIButtonItemLink);

        protected override bool CanKeyboardSelect =>
            false;
    }
}

