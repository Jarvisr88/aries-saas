namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class RowNode
    {
        protected static IEnumerable<RowNode> EmptyEnumerable = new RowNode[0];
        private bool canGenerateItems;
        private bool isRowVisible = true;

        protected RowNode()
        {
            this.FixedRowPosition = DevExpress.Xpf.Grid.FixedRowPosition.None;
        }

        internal abstract RowDataBase CreateRowData();
        protected internal int GenerateItems(int count) => 
            (!this.CanGenerateItems || (this.NodesContainer == null)) ? 0 : this.NodesContainer.GenerateItems(count);

        internal IEnumerable<RowNode> GetChildItems() => 
            (this.NodesContainer != null) ? this.NodesContainer.Items : EmptyEnumerable;

        internal abstract LinkedList<FreeRowDataInfo> GetFreeRowDataQueue(SynchronizationQueues synchronizationQueues);
        internal virtual RowNode GetNodeToScroll() => 
            (this.NodesContainer != null) ? (!this.IsExpanded ? this : (this.NodesContainer.GetNodeToScroll() ?? this)) : this;

        internal abstract RowDataBase GetRowData();
        internal abstract FrameworkElement GetRowElement();
        internal IEnumerable<RowNode> GetSkipCollapsedChildItems() => 
            this.IsExpanded ? this.GetChildItems() : EmptyEnumerable;

        internal virtual bool IsMatchedRowData(RowDataBase data) => 
            Equals(data.MatchKey, this.MatchKey);

        internal virtual bool IsRowExpandedForNavigation() => 
            false;

        public virtual int SkipChildNodes(int index) => 
            index;

        internal void UpdateExpandInfo(int startVisibleIndex, bool isRowVisible)
        {
            if (this.NodesContainer != null)
            {
                this.IsRowVisible = isRowVisible;
                this.CanGenerateItems = (!this.IsCollapsing && !this.IsExpanding) && this.IsDataExpanded;
                if (this.CanUpdateState)
                {
                    this.IsExpanded = this.IsDataExpanded;
                }
                this.NodesContainer.ReGenerateExpandItems(startVisibleIndex, 0);
            }
        }

        public bool IsRowVisible
        {
            get => 
                this.isRowVisible;
            protected set => 
                this.isRowVisible = value;
        }

        public abstract object MatchKey { get; }

        public NodeContainer NodesContainer { get; protected set; }

        public int ItemsCount
        {
            get
            {
                int num = 0;
                if (this.IsDataExpanded && (this.NodesContainer != null))
                {
                    num += this.NodesContainer.ItemCount;
                }
                if (this.IsRowVisible)
                {
                    num++;
                }
                return num;
            }
        }

        protected abstract bool IsDataExpanded { get; }

        public bool IsExpanding { get; internal set; }

        public bool IsExpanded { get; internal set; }

        internal bool IsCollapsing { get; set; }

        internal bool IsFinished =>
            this.IsDataExpanded ? (!this.CanGenerateItems || this.NodesContainer.IsFinished) : true;

        protected virtual bool CanUpdateState =>
            true;

        internal virtual bool CanGenerateItems
        {
            get => 
                this.canGenerateItems;
            set => 
                this.canGenerateItems = value;
        }

        internal virtual bool IsFixedNode =>
            false;

        public DevExpress.Xpf.Grid.FixedRowPosition FixedRowPosition { get; internal set; }

        public bool IsLastFixedRow { get; internal set; }
    }
}

