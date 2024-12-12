namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public class SkipCollapsedGroupVirtualItemsEnumerator : VirtualItemsEnumerator
    {
        public SkipCollapsedGroupVirtualItemsEnumerator(NodeContainer containerItem) : base(containerItem)
        {
        }

        protected override IEnumerable<RowNode> GetGroupDataEnumerable(RowNode groupData) => 
            groupData.GetSkipCollapsedChildItems();
    }
}

