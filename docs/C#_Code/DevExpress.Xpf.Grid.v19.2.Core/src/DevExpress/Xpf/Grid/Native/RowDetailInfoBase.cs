namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Hierarchy;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class RowDetailInfoBase
    {
        private bool isExpanded;

        protected RowDetailInfoBase()
        {
        }

        public abstract int CalcRowsCount();
        public abstract int CalcTotalLevel();
        public abstract int CalcVisibleDataRowCount();
        public virtual void Detach()
        {
        }

        public DataControlBase FindDetailDataControl(DataControlDetailDescriptor descriptor)
        {
            DataControlBase dataControl;
            using (IEnumerator<RowDetailInfoBase> enumerator = this.GetRowDetailInfoEnumerator().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        RowDetailInfoBase current = enumerator.Current;
                        DataControlDetailInfo info = current as DataControlDetailInfo;
                        if ((info == null) || ((descriptor != null) && !ReferenceEquals(info.DataControlDetailDescriptor, descriptor)))
                        {
                            continue;
                        }
                        dataControl = info.DataControl;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return dataControl;
        }

        public abstract DataViewBase FindFirstDetailView();
        public abstract DataViewBase FindLastInnerDetailView();
        public abstract bool FindViewAndVisibleIndexByScrollIndex(int scrollIndex, bool forwardIfServiceRow, out DataViewBase targetView, out int targetVisibleIndex);
        public abstract DataControlBase FindVisibleDetailDataControl();
        public abstract DetailDescriptorBase FindVisibleDetailDescriptor();
        protected internal abstract DataControlDetailInfo GetActualDetailInfo(DataControlDetailDescriptor detailDescriptor);
        protected internal abstract int GetBottomServiceRowsCount();
        protected internal abstract int GetNextLevelRowCount();
        public abstract NodeContainer GetNodeContainer();
        protected internal abstract DetailNodeKind[] GetNodeScrollOrder();
        internal abstract IEnumerable<RowDetailInfoBase> GetRowDetailInfoEnumerator();
        internal abstract IEnumerator<RowNode> GetRowNodesEnumeratorCore(int nextLevelStartVisibleIndex, int nextLevelVisibleRowsCount, int serviceVisibleRowsCount);
        public abstract DevExpress.Xpf.Grid.RowsContainer GetRowsContainerAndUpdateMasterRowData(RowData masterRowData);
        protected internal abstract int GetTopServiceRowsCount();
        protected internal abstract int GetVisibleDataRowCount();
        public virtual bool IsDetailRowExpanded(DetailDescriptorBase descriptor) => 
            this.IsExpanded;

        protected internal abstract void OnCollapsed();
        protected internal abstract void OnExpanded();
        public void OnUpdateRow(object row)
        {
            foreach (RowDetailInfoBase base2 in this.GetRowDetailInfoEnumerator())
            {
                base2.UpdateRow(row);
            }
        }

        internal virtual void RemoveFromDetailClones()
        {
        }

        public virtual void SetDetailRowExpanded(bool expand, DetailDescriptorBase descriptor)
        {
            this.IsExpanded = expand;
        }

        public abstract void UpdateMasterRowData(RowData masterRowData);
        protected abstract void UpdateRow(object row);

        public virtual bool IsExpanded
        {
            get => 
                this.isExpanded;
            set
            {
                if (this.isExpanded != value)
                {
                    this.isExpanded = value;
                    if (this.isExpanded)
                    {
                        this.OnExpanded();
                    }
                    else
                    {
                        this.OnCollapsed();
                    }
                }
            }
        }

        internal abstract DetailRowsContainer RowsContainer { get; }

        internal class DetailRowsContainer : RowsContainer, IDetailRootItemsContainer, IItemsContainer
        {
            private RowData masterRowData;
            private readonly MasterRowsContainer masterRowsContainer;
            private readonly DetailDescriptorBase detailDescriptor;

            public DetailRowsContainer(MasterRowsContainer masterRowsContainer, DetailDescriptorBase detailDescriptor)
            {
                this.masterRowsContainer = masterRowsContainer;
                this.detailDescriptor = detailDescriptor;
                base.InitItemsCollection();
            }

            internal override RowsContainerSyncronizerBase CreateRowsContainerSyncronizer() => 
                new RowDetailInfoBase.DetailRowsContainerSyncronizer(this);

            internal RowData MasterRowData
            {
                get => 
                    this.masterRowData;
                set
                {
                    if (!ReferenceEquals(this.masterRowData, value))
                    {
                        this.masterRowData = value;
                    }
                }
            }

            internal override MasterRowsContainer MasterRootRowsContainer =>
                this.masterRowsContainer;

            internal override DevExpress.Xpf.Grid.Native.SynchronizationQueues SynchronizationQueues =>
                this.detailDescriptor.SynchronizationQueues;

            double IItemsContainer.AnimationProgress =>
                1.0;
        }

        private class DetailRowsContainerSyncronizer : RowsContainerSyncronizerBase
        {
            private readonly Dictionary<DetailNodeKind, RowDataBase> unmatchedItems;

            public DetailRowsContainerSyncronizer(RowDetailInfoBase.DetailRowsContainer dataContainer) : base(dataContainer)
            {
                this.unmatchedItems = new Dictionary<DetailNodeKind, RowDataBase>();
            }

            protected override RowDataBase DequeueUnmatchedItem(object matchKey)
            {
                RowDataBase base2;
                DetailNodeKind key = (DetailNodeKind) matchKey;
                if (!this.unmatchedItems.TryGetValue(key, out base2))
                {
                    return null;
                }
                this.unmatchedItems.Remove(key);
                return base2;
            }

            protected override void EnqueueUnmatchedItem(RowDataBase rowData)
            {
                this.unmatchedItems.Add((DetailNodeKind) rowData.MatchKey, rowData);
            }

            protected override IEnumerable<RowDataBase> GetUnmatchedItems() => 
                this.unmatchedItems.Values;
        }
    }
}

