namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class VirtualItemsEnumerator : VirtualItemsEnumeratorBase
    {
        private readonly NodeContainer containerItem;

        public VirtualItemsEnumerator(NodeContainer containerItem) : base(containerItem)
        {
            this.containerItem = containerItem;
        }

        protected sealed override IEnumerator GetContainerEnumerator(object obj)
        {
            NodeContainer container = obj as NodeContainer;
            return (((container == null) || !container.Initialized) ? null : container.GetSortedItemsEnumerator());
        }

        protected virtual IEnumerable<RowNode> GetGroupDataEnumerable(RowNode groupData) => 
            groupData.GetChildItems();

        protected sealed override IEnumerable GetGroupEnumerable(object obj)
        {
            RowNode groupData = obj as RowNode;
            return ((groupData != null) ? this.GetGroupDataEnumerable(groupData) : VirtualItemsEnumeratorBase.EmptyEnumerable);
        }

        public NodeContainer CurrentContainer =>
            !(base.CurrentParent is NodeContainer) ? ((RowNode) base.CurrentParent).NodesContainer : ((NodeContainer) base.CurrentParent);

        public RowNode Current =>
            (RowNode) base.Enumerator.Current;

        public RowDataBase CurrentData =>
            this.Current.GetRowData();
    }
}

