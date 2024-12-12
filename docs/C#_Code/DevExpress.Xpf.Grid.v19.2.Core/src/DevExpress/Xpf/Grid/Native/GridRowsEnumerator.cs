namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Windows;

    public class GridRowsEnumerator : IEnumerator
    {
        protected VirtualItemsEnumerator en;
        protected DataViewBase view;

        public GridRowsEnumerator(DataViewBase view, NodeContainer containerItem)
        {
            this.view = view;
            this.en = this.CreateInnerEnumerator(containerItem);
        }

        protected virtual VirtualItemsEnumerator CreateInnerEnumerator(NodeContainer containerItem) => 
            new SkipCollapsedGroupVirtualItemsEnumerator(containerItem);

        protected bool IsCurrentRowInTree() => 
            LayoutHelper.FindParentObject<DataPresenterBase>(this.CurrentRow) != null;

        public virtual bool MoveNext()
        {
            while (this.en.MoveNext())
            {
                if ((this.CurrentRow != null) && this.IsCurrentRowInTree())
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            this.en.Reset();
        }

        public RowDataBase CurrentRowData =>
            this.CurrentNode.GetRowData();

        public FrameworkElement CurrentRow =>
            this.CurrentNode.GetRowElement();

        public RowNode CurrentNode =>
            this.en.Current;

        object IEnumerator.Current =>
            this.en.Current;
    }
}

