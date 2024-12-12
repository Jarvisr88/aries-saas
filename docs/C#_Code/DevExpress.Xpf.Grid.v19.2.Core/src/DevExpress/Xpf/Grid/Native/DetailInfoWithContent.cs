namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class DetailInfoWithContent : RowDetailInfoBase
    {
        private Dictionary<DetailNodeKind, DetailRowNode> nodesCache = new Dictionary<DetailNodeKind, DetailRowNode>();
        private DetailNodeContainer nodeContainer;
        private RowDetailInfoBase.DetailRowsContainer rowsContainer;
        protected readonly RowDetailContainer container;
        protected readonly DetailDescriptorBase detailDescriptor;

        protected DetailInfoWithContent(DetailDescriptorBase detailDescriptor, RowDetailContainer container)
        {
            this.detailDescriptor = detailDescriptor;
            this.container = container;
        }

        protected int CalcBottomRowCount() => 
            this.detailDescriptor.HasBottomMargin ? 1 : 0;

        protected int CalcContentRowCount() => 
            (this.detailDescriptor.ContentTemplate != null) ? 1 : 0;

        protected int CalcHeaderRowCount()
        {
            int num = this.detailDescriptor.ShowHeader ? 1 : 0;
            if (this.detailDescriptor.HasTopMargin)
            {
                num++;
            }
            return num;
        }

        public sealed override int CalcRowsCount() => 
            this.IsExpanded ? (this.GetServiceRowsCount() + this.GetNextLevelRowCount()) : 0;

        public override int CalcTotalLevel() => 
            this.GetTopServiceRowsCount();

        public sealed override int CalcVisibleDataRowCount() => 
            this.IsExpanded ? this.GetVisibleDataRowCount() : 0;

        public virtual void Clear()
        {
            this.RowsContainer.Clear();
        }

        protected DetailHeaderControlBase CreateDetailHeaderControl(Func<DetailHeaderControlBase> createControl)
        {
            DetailHeaderControlBase base2 = createControl();
            base2.DetailDescriptor = this.detailDescriptor;
            return base2;
        }

        internal virtual DetailRowNode CreateDetailRowNode(DetailNodeKind detailNodeKind, Func<DetailNodeKind, RowDataBase> createRowDataDelegate) => 
            new DetailHeaderRowNode(this, detailNodeKind, createRowDataDelegate);

        protected RowNode FindNodeByDetailNodeKind(DetailNodeKind detailNodeKind) => 
            this.nodeContainer.Items.FirstOrDefault<RowNode>(node => ((DetailNodeKind) node.MatchKey) == detailNodeKind);

        public void ForceCreateContainers()
        {
            this.nodeContainer ??= new DetailNodeContainer(this);
            this.rowsContainer ??= new RowDetailInfoBase.DetailRowsContainer(this.MasterRootRowsContainer, this.detailDescriptor);
        }

        protected RowNode GetDetailContentNode(bool showLastDetailMargin) => 
            this.GetRowNode(DetailNodeKind.DetailContent, delegate (DetailNodeKind nodeKind) {
                Func<FrameworkElement> <>9__1;
                Func<FrameworkElement> createRowElementDelegate = <>9__1;
                if (<>9__1 == null)
                {
                    Func<FrameworkElement> local1 = <>9__1;
                    createRowElementDelegate = <>9__1 = delegate {
                        Func<DetailHeaderControlBase> <>9__2;
                        Func<DetailHeaderControlBase> createControl = <>9__2;
                        if (<>9__2 == null)
                        {
                            Func<DetailHeaderControlBase> local1 = <>9__2;
                            createControl = <>9__2 = () => this.MasterTableViewBehavior.CreateDetailContentElement(showLastDetailMargin);
                        }
                        return this.CreateDetailHeaderControl(createControl);
                    };
                }
                return new DetailRowData(nodeKind, createRowElementDelegate);
            });

        protected RowNode GetDetailHeaderNode(bool showLastDetailMargin) => 
            this.GetRowNode(DetailNodeKind.DetailHeader, delegate (DetailNodeKind nodeKind) {
                Func<FrameworkElement> <>9__1;
                Func<FrameworkElement> createRowElementDelegate = <>9__1;
                if (<>9__1 == null)
                {
                    Func<FrameworkElement> local1 = <>9__1;
                    createRowElementDelegate = <>9__1 = delegate {
                        Func<DetailHeaderControlBase> <>9__2;
                        Func<DetailHeaderControlBase> createControl = <>9__2;
                        if (<>9__2 == null)
                        {
                            Func<DetailHeaderControlBase> local1 = <>9__2;
                            createControl = <>9__2 = () => this.MasterTableViewBehavior.CreateDetailHeaderElement(showLastDetailMargin);
                        }
                        return this.CreateDetailHeaderControl(createControl);
                    };
                }
                return new DetailRowData(nodeKind, createRowElementDelegate);
            });

        protected RowNode GetDetailMarginNode(DetailNodeKind detailNodeKind, bool showBottomLine)
        {
            DetailHeaderRowNode rowNode = (DetailHeaderRowNode) this.GetRowNode(detailNodeKind, delegate (DetailNodeKind nodeKind) {
                Func<FrameworkElement> <>9__1;
                Func<FrameworkElement> createRowElementDelegate = <>9__1;
                if (<>9__1 == null)
                {
                    Func<FrameworkElement> local1 = <>9__1;
                    createRowElementDelegate = <>9__1 = delegate {
                        Func<DetailHeaderControlBase> <>9__2;
                        Func<DetailHeaderControlBase> createControl = <>9__2;
                        if (<>9__2 == null)
                        {
                            Func<DetailHeaderControlBase> local1 = <>9__2;
                            createControl = <>9__2 = () => this.MasterTableViewBehavior.CreateDetailMarginElement(detailNodeKind == DetailNodeKind.TopMarginContainer);
                        }
                        return this.CreateDetailHeaderControl(createControl);
                    };
                }
                return new DetailRowData(nodeKind, createRowElementDelegate);
            });
            rowNode.ShowBottomLine = showBottomLine;
            return rowNode;
        }

        public override DevExpress.Xpf.Grid.NodeContainer GetNodeContainer() => 
            this.nodeContainer;

        internal RowNode GetNodeToScroll()
        {
            foreach (DetailNodeKind kind in this.GetNodeScrollOrder())
            {
                RowNode node = this.FindNodeByDetailNodeKind(kind);
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        [IteratorStateMachine(typeof(<GetRowDetailInfoEnumerator>d__48))]
        internal override IEnumerable<RowDetailInfoBase> GetRowDetailInfoEnumerator()
        {
            <GetRowDetailInfoEnumerator>d__48 d__1 = new <GetRowDetailInfoEnumerator>d__48(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        internal DetailRowNode GetRowNode(DetailNodeKind detailNodeKind, Func<DetailNodeKind, RowDataBase> createRowDataDelegate)
        {
            DetailRowNode node = null;
            if (!this.nodesCache.TryGetValue(detailNodeKind, out node))
            {
                node = this.CreateDetailRowNode(detailNodeKind, createRowDataDelegate);
                this.nodesCache[detailNodeKind] = node;
            }
            return node;
        }

        internal IEnumerator<RowNode> GetRowNodesEnumerator(int startVisibleIndex)
        {
            int nextLevelVisibleRowsCount = this.GetNextLevelRowCount() - startVisibleIndex;
            return this.GetRowNodesEnumeratorCore(startVisibleIndex, nextLevelVisibleRowsCount, this.GetServiceRowsCount() + Math.Min(nextLevelVisibleRowsCount, 0));
        }

        public override DevExpress.Xpf.Grid.RowsContainer GetRowsContainerAndUpdateMasterRowData(RowData masterRowData)
        {
            this.UpdateMasterRowData(masterRowData);
            return this.rowsContainer;
        }

        private int GetServiceRowsCount() => 
            this.GetTopServiceRowsCount() + this.GetBottomServiceRowsCount();

        internal virtual bool IsFixedNode(DetailRowNode detailRowNode) => 
            false;

        protected internal override void OnCollapsed()
        {
        }

        protected internal override void OnExpanded()
        {
            this.ForceCreateContainers();
        }

        public override void UpdateMasterRowData(RowData masterRowData)
        {
            if (this.rowsContainer != null)
            {
                this.rowsContainer.MasterRowData = masterRowData;
            }
        }

        protected override void UpdateRow(object row)
        {
            this.container.Row = row;
        }

        protected TableViewBehavior MasterTableViewBehavior =>
            ((ITableView) this.MasterRootRowsContainer.View).TableViewBehavior;

        internal DetailNodeContainer NodeContainer =>
            this.nodeContainer;

        internal override RowDetailInfoBase.DetailRowsContainer RowsContainer =>
            this.rowsContainer;

        protected MasterRowsContainer MasterRootRowsContainer =>
            this.MasterDataControl.DataView.MasterRootRowsContainer;

        internal DataControlBase MasterDataControl =>
            this.container.MasterDataControl;

        protected object Row =>
            this.container.Row;

        protected int MasterVisibleIndex
        {
            get
            {
                int rowHandleByListIndex = this.MasterDataControl.DataProviderBase.GetRowHandleByListIndex(this.container.MasterListIndex);
                return this.MasterDataControl.DataProviderBase.GetRowVisibleIndexByHandle(rowHandleByListIndex);
            }
        }

        [CompilerGenerated]
        private sealed class <GetRowDetailInfoEnumerator>d__48 : IEnumerable<RowDetailInfoBase>, IEnumerable, IEnumerator<RowDetailInfoBase>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RowDetailInfoBase <>2__current;
            private int <>l__initialThreadId;
            public DetailInfoWithContent <>4__this;

            [DebuggerHidden]
            public <GetRowDetailInfoEnumerator>d__48(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>2__current = this.<>4__this;
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<RowDetailInfoBase> IEnumerable<RowDetailInfoBase>.GetEnumerator()
            {
                DetailInfoWithContent.<GetRowDetailInfoEnumerator>d__48 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new DetailInfoWithContent.<GetRowDetailInfoEnumerator>d__48(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Grid.Native.RowDetailInfoBase>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            RowDetailInfoBase IEnumerator<RowDetailInfoBase>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        internal class DetailColumnsData : ColumnsRowDataBase
        {
            private readonly DetailNodeKind nodeKind;

            public DetailColumnsData(DetailNodeKind nodeKind, DataTreeBuilder treeBuilder, Func<FrameworkElement> createRowElementDelegate) : base(treeBuilder, createRowElementDelegate)
            {
                this.nodeKind = nodeKind;
            }

            protected override RowDataBase.NotImplementedRowDataReusingStrategy CreateReusingStrategy(Func<FrameworkElement> createRowElementDelegate) => 
                new RowDataBase.DetailRowDataReusingStrategy(this, createRowElementDelegate);

            private bool IsNotLastRow() => 
                !base.View.ShowFixedTotalSummary ? ((this.nodeKind != DetailNodeKind.ColumnHeaders) ? ((this.nodeKind == DetailNodeKind.NewItemRow) && (base.View.DataControl.VisibleRowCount > 0)) : (base.View.IsNewItemRowVisible || (base.View.DataControl.VisibleRowCount > 0))) : true;

            internal override void UpdateLineLevel()
            {
                bool isLastRow = true;
                if (this.IsNotLastRow())
                {
                    base.LineLevel = 0;
                    base.DetailLevel = base.GetDetailLevel(this, 0, ref isLastRow);
                }
                else
                {
                    DataViewBase targetView = null;
                    int targetVisibleIndex = -1;
                    if (!base.View.DataControl.DataControlParent.FindMasterRow(out targetView, out targetVisibleIndex) || (targetView.GetRowData(targetView.DataControl.GetRowHandleByVisibleIndexCore(targetVisibleIndex)) == null))
                    {
                        base.LineLevel = 0;
                        base.DetailLevel = 0;
                    }
                    else
                    {
                        base.LineLevel = this.GetLineLevel(this, 0, 0);
                        base.DetailLevel = base.GetDetailLevel(this, 0, ref isLastRow);
                        if (isLastRow)
                        {
                            base.LineLevel = base.DetailLevel;
                        }
                    }
                }
            }

            internal override object MatchKey =>
                this.nodeKind;
        }

        internal class DetailHeaderRowNode : DetailInfoWithContent.DetailRowNode
        {
            public DetailHeaderRowNode(DetailInfoWithContent owner, DetailNodeKind detailNodeKind, Func<DetailNodeKind, RowDataBase> createRowDataDelegate) : base(owner, detailNodeKind, createRowDataDelegate)
            {
            }

            internal override void AssignToRowData(RowDataBase rowData, RowDataBase masterRowData)
            {
                if (rowData is DetailInfoWithContent.DetailRowData)
                {
                    (rowData as DetailInfoWithContent.DetailRowData).SetMasterView(masterRowData.View);
                }
                ((DetailHeaderControlBase) rowData.WholeRowElement).ShowBottomLine = this.ShowBottomLine;
            }

            internal bool ShowBottomLine { get; set; }
        }

        internal class DetailNodeContainer : NodeContainer
        {
            private readonly DetailInfoWithContent detailInfo;

            public DetailNodeContainer(DetailInfoWithContent detailInfo)
            {
                this.detailInfo = detailInfo;
            }

            internal override RowNode GetNodeToScroll() => 
                this.detailInfo.GetNodeToScroll()?.GetNodeToScroll();

            protected override IEnumerator<RowNode> GetRowDataEnumerator() => 
                this.detailInfo.GetRowNodesEnumerator(base.StartScrollIndex);
        }

        public class DetailRowData : RowDataBase
        {
            private readonly DetailNodeKind nodeKind;

            public DetailRowData(DetailNodeKind nodeKind, Func<FrameworkElement> createRowElementDelegate) : base(createRowElementDelegate)
            {
                this.nodeKind = nodeKind;
            }

            protected override RowDataBase.NotImplementedRowDataReusingStrategy CreateReusingStrategy(Func<FrameworkElement> createRowElementDelegate) => 
                new RowDataBase.DetailRowDataReusingStrategy(this, createRowElementDelegate);

            internal virtual void SetMasterView(DataViewBase view)
            {
                base.View = view;
            }

            internal override void UpdateLineLevel()
            {
                base.LineLevel = 0;
                base.DetailLevel = 0x7fffffff;
            }

            internal override object MatchKey =>
                this.nodeKind;
        }

        internal abstract class DetailRowNode : RowNode
        {
            private bool isDataExpanded;
            private readonly Func<DetailNodeKind, RowDataBase> createRowDataDelegate;
            internal readonly DetailNodeKind detailNodeKind;
            protected readonly DetailInfoWithContent owner;

            public DetailRowNode(DetailInfoWithContent owner, DetailNodeKind detailNodeKind, Func<DetailNodeKind, RowDataBase> createRowDataDelegate)
            {
                this.detailNodeKind = detailNodeKind;
                this.createRowDataDelegate = createRowDataDelegate;
                this.owner = owner;
            }

            internal abstract void AssignToRowData(RowDataBase rowData, RowDataBase masterRowData);
            internal override RowDataBase CreateRowData() => 
                this.createRowDataDelegate(this.detailNodeKind);

            internal override LinkedList<FreeRowDataInfo> GetFreeRowDataQueue(SynchronizationQueues synchronizationQueues) => 
                ((DetailSynchronizationQueues) synchronizationQueues).GetSynchronizationQueue(this.detailNodeKind);

            internal override RowDataBase GetRowData() => 
                this.CurrentRowData;

            internal override FrameworkElement GetRowElement() => 
                this.CurrentRowData?.WholeRowElement;

            public void UpdateExpandInfoEx(NodeContainer childrenNodeContainer, RowsContainer childrenRowsContainer, int startVisibleIndex, bool isRowVisible)
            {
                base.NodesContainer = childrenNodeContainer;
                this.childrenRowsContainer = childrenRowsContainer;
                this.isDataExpanded = childrenNodeContainer != null;
                base.UpdateExpandInfo(startVisibleIndex, isRowVisible);
            }

            internal RowsContainer childrenRowsContainer { get; private set; }

            internal RowDataBase CurrentRowData { get; set; }

            protected override bool IsDataExpanded =>
                this.isDataExpanded;

            public override object MatchKey =>
                this.detailNodeKind;

            internal override bool IsFixedNode =>
                this.owner.IsFixedNode(this);
        }
    }
}

