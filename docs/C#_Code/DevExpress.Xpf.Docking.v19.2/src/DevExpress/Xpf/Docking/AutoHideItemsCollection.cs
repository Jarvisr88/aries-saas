namespace DevExpress.Xpf.Docking
{
    using System;

    public class AutoHideItemsCollection : BaseLayoutItemCollection
    {
        public AutoHideItemsCollection(AutoHideGroup group) : base(group)
        {
        }

        protected override void BeforeItemAdded(BaseLayoutItem item)
        {
            base.BeforeItemAdded(item);
            item.SetAutoHidden(true);
        }

        protected override void ClearItems()
        {
            BaseLayoutItem[] array = new BaseLayoutItem[base.Items.Count];
            base.Items.CopyTo(array, 0);
            base.ClearItems();
            foreach (BaseLayoutItem item in array)
            {
                item.SetAutoHidden(false);
            }
        }

        protected override void InsertItem(int index, BaseLayoutItem item)
        {
            if (!(item is LayoutGroup) || base.Owner.IsInContainerGeneration)
            {
                base.InsertItem(index, item);
            }
            else
            {
                BaseLayoutItem[] itemArray = DockControllerHelper.Decompose(item);
                for (int i = 0; i < itemArray.Length; i++)
                {
                    base.InsertItem(index + i, itemArray[i]);
                }
            }
        }

        protected override void OnItemRemoved(BaseLayoutItem item)
        {
            base.OnItemRemoved(item);
            item.SetAutoHidden(false);
        }

        protected override void SetItem(int index, BaseLayoutItem item)
        {
            item.SetAutoHidden(true);
            base.SetItem(index, item);
        }
    }
}

