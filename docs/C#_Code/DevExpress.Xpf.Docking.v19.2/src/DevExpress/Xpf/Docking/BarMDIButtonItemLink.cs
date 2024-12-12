namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class BarMDIButtonItemLink : BarButtonItemLink
    {
        public BarMDIButtonItemLink()
        {
            base.MergeType = BarItemMergeType.Replace;
            base.MergeOrder = 0x7fffffff;
            base.Alignment = BarItemAlignment.Far;
        }

        internal BarMDIButtonItemLink(BarMDIButtonItem item) : this()
        {
            base.Item = item;
        }

        public override void Assign(BarItemLinkBase link)
        {
            base.Assign(link);
            BindingHelper.SetBinding(this, BarItemLinkBase.CustomResourcesProperty, link, "CustomResources");
            BindingHelper.SetBinding(this, ContentElement.IsEnabledProperty, link, "IsEnabled");
        }

        protected override bool AllowShowCustomizationMenu
        {
            get => 
                false;
            set
            {
            }
        }

        public override bool IsPrivate
        {
            get => 
                true;
            protected set
            {
            }
        }

        protected override bool IsPrivateLinkInCustomizationMode
        {
            get => 
                true;
            set
            {
            }
        }
    }
}

