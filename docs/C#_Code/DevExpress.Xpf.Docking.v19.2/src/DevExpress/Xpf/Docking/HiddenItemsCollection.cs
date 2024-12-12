namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class HiddenItemsCollection : BaseLayoutItemCollection
    {
        private LayoutGroup currentParent;
        private DockLayoutManager Manager;

        public HiddenItemsCollection(DockLayoutManager owner, LayoutGroup customizationRoot) : base(customizationRoot)
        {
            this.Manager = owner;
        }

        public void Add(BaseLayoutItem item)
        {
            this.Add(item, null);
        }

        public void Add(BaseLayoutItem item, LayoutGroup currentParent)
        {
            if (!(item is FixedItem))
            {
                this.currentParent = currentParent;
                base.Add(item);
            }
        }

        protected override void BeforeItemAdded(BaseLayoutItem item)
        {
            this.CheckItemRules(item);
        }

        protected override void CheckItemRules(BaseLayoutItem item)
        {
            if ((item.ItemType != LayoutItemType.ControlItem) && ((item.ItemType != LayoutItemType.Group) && !(item is FixedItem)))
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.ItemCanNotBeHidden));
            }
        }

        protected override void OnItemAdded(BaseLayoutItem item)
        {
            if (base.Owner != null)
            {
                item.Manager = this.Manager;
            }
            item.SetHidden(true, this.currentParent);
        }

        protected override void OnItemRemoved(BaseLayoutItem item)
        {
            item.SetHidden(false, null);
        }
    }
}

