namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class DataNodeContainer : NodeContainer
    {
        protected internal readonly DataTreeBuilder treeBuilder;
        private readonly DataRowNode parentNode;
        internal int parentVisibleIndex;
        private int lastVisibleCount;
        private List<int> selectedRows;

        public DataNodeContainer(DataTreeBuilder treeBuilder, int level, DataRowNode parentNode)
        {
            this.GroupLevel = level;
            this.treeBuilder = treeBuilder;
            this.parentNode = parentNode;
        }

        protected virtual IEnumerator<RowNode> GetBottomRowsEnumerator() => 
            NodeContainer.EmptyEnumerator;

        private bool GetHasBottom() => 
            this.DataIterator.GetHasBottom(this);

        private bool GetHasTop() => 
            this.DataIterator.GetHasTop(this);

        protected override IEnumerator<RowNode> GetRowDataEnumerator() => 
            ((this.parentNode == null) || (this.treeBuilder.View.DataProviderBase.GetChildRowCount(this.parentNode.ControllerValues.RowHandle.Value) != 0)) ? this.GetRowDataEnumeratorCore() : NodeContainer.EmptyEnumerator;

        [IteratorStateMachine(typeof(<GetRowDataEnumeratorCore>d__35))]
        private IEnumerator<RowNode> GetRowDataEnumeratorCore()
        {
            IEnumerator<RowNode> <bottomRowsEnumerator>5__4;
            this.lastVisibleCount = this.TotalVisibleCount;
            bool shouldBreak = false;
            IEnumerator<RowNode> topRowsEnumerator = this.GetTopRowsEnumerator();
        Label_PostSwitchInIterator:;
            if (topRowsEnumerator.MoveNext())
            {
                yield return topRowsEnumerator.Current;
                goto Label_PostSwitchInIterator;
            }
            int index = this.StartScrollIndex + this.FixedTopRowsCount;
            goto TR_000B;
        TR_0004:
            <bottomRowsEnumerator>5__4 = this.GetBottomRowsEnumerator();
            while (true)
            {
                if (!<bottomRowsEnumerator>5__4.MoveNext())
                {
                }
                yield return <bottomRowsEnumerator>5__4.Current;
            }
        TR_0007:
            if (shouldBreak)
            {
                goto TR_0004;
            }
            else
            {
                int num2 = index;
                index = num2 + 1;
            }
        TR_000B:
            while (true)
            {
                if (index < (this.lastVisibleCount - this.FixedBottomRowsCount))
                {
                    RowNode node = this.DataIterator.GetRowNodeForCurrentLevel(this, index, (index == this.StartScrollIndex) ? this.DetailStartScrollIndex : 0, false, ref shouldBreak);
                    if ((node != null) && this.IsNodeSelected(node))
                    {
                        index = node.SkipChildNodes(index);
                        yield return node;
                        break;
                    }
                }
                else
                {
                    goto TR_0004;
                }
                break;
            }
            goto TR_0007;
        }

        internal RowPosition GetRowPosition(RowNode node)
        {
            GridContainerRowsLocation rowsLocation = this.GetRowsLocation();
            if (base.Items.Count == 0)
            {
                return RowPosition.Top;
            }
            bool flag = base.Items[0] == node;
            bool flag2 = base.Items[base.Items.Count - 1] == node;
            return ((rowsLocation != GridContainerRowsLocation.TopOnly) ? ((rowsLocation != GridContainerRowsLocation.BottomOnly) ? ((rowsLocation != GridContainerRowsLocation.TopAndBottom) ? RowPosition.Middle : (!(flag & flag2) ? (!flag ? (!flag2 ? RowPosition.Middle : RowPosition.Bottom) : RowPosition.Top) : RowPosition.Single)) : (flag2 ? RowPosition.Bottom : RowPosition.Middle)) : (flag ? RowPosition.Top : RowPosition.Middle));
        }

        private GridContainerRowsLocation GetRowsLocation() => 
            this.GetRowsLocation(this.GetHasTop(), this.GetHasBottom());

        private GridContainerRowsLocation GetRowsLocation(bool hasTop, bool hasBottom) => 
            !(hasTop & hasBottom) ? (!hasTop ? (!hasBottom ? GridContainerRowsLocation.Middle : GridContainerRowsLocation.BottomOnly) : GridContainerRowsLocation.TopOnly) : GridContainerRowsLocation.TopAndBottom;

        protected virtual IEnumerator<RowNode> GetTopRowsEnumerator() => 
            NodeContainer.EmptyEnumerator;

        private bool IsNodeSelected(RowNode node)
        {
            if (!this.EnumerateOnlySelectedRows)
            {
                return true;
            }
            DataRowNode node2 = node as DataRowNode;
            if (node2 == null)
            {
                return false;
            }
            int item = node2.RowHandle.Value;
            return this.SelectedRows.Contains(item);
        }

        protected internal override void OnDataChangedCore()
        {
            this.parentVisibleIndex = this.DataIterator.GetRowParentIndex(this, base.StartScrollIndex + this.FixedTopRowsCount, this.GroupLevel);
            base.OnDataChangedCore();
        }

        protected override void OnItemsGenerated()
        {
            this.treeBuilder.SetRowStateDirty();
        }

        internal int DetailStartScrollIndex { get; set; }

        protected DataIteratorBase DataIterator =>
            this.treeBuilder.View.DataIterator;

        internal bool IsGroupRowsContainer =>
            this.DataIterator.IsGroupRowsContainer(this);

        internal int TotalVisibleCount =>
            this.treeBuilder.VisibleCount;

        internal bool EnumerateOnlySelectedRows { get; set; }

        public int GroupLevel { get; private set; }

        public NodeContainerPrintInfo PrintInfo { get; set; }

        protected internal override bool IsEnumeratorValid =>
            this.lastVisibleCount == this.TotalVisibleCount;

        protected internal virtual int FixedTopRowsCount =>
            0;

        protected internal int FixedBottomRowsCount =>
            this.treeBuilder.View.ActualFixedBottomRowsCount;

        internal List<int> SelectedRows
        {
            get
            {
                this.selectedRows ??= new List<int>(this.treeBuilder.View.DataControl.DataProviderBase.Selection.GetSelectedRows());
                return this.selectedRows;
            }
        }

        public int CurrentLevelItemCount
        {
            get
            {
                if (!this.IsGroupRowsContainer)
                {
                    return base.Items.Count;
                }
                int num = 0;
                if (base.Items != null)
                {
                    foreach (RowNode node in base.Items)
                    {
                        DataRowNode node2 = node as DataRowNode;
                        if (node2 != null)
                        {
                            num += node2.CurrentLevelItemCount;
                        }
                    }
                }
                return num;
            }
        }

    }
}

