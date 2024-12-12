namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;

    public class RowDataItemsEnumerator : VirtualItemsEnumeratorBase
    {
        public RowDataItemsEnumerator(RowsContainer containerItem) : base(containerItem)
        {
        }

        protected sealed override IEnumerator GetContainerEnumerator(object obj)
        {
            RowsContainer container = obj as RowsContainer;
            return container?.GetEnumerable().GetEnumerator();
        }

        protected sealed override IEnumerable GetGroupEnumerable(object obj)
        {
            RowData data = obj as RowData;
            return ((data != null) ? data.GetCurrentViewChildItems() : VirtualItemsEnumeratorBase.EmptyEnumerable);
        }

        public RowData Current =>
            (RowData) base.Enumerator.Current;
    }
}

