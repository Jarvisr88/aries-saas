namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DataRowNode : RowNode
    {
        internal readonly DataTreeBuilder treeBuilder;
        private DataControllerValuesContainer controllerValues;
        private int supressUpdateStateCount;
        private object row;
        internal static bool DisableUpdateOptimization;
        protected internal RowNode summaryNode;

        public DataRowNode(DataTreeBuilder treeBuilder, DataControllerValuesContainer controllerValues)
        {
            this.treeBuilder = treeBuilder;
            this.ControllerValues = controllerValues;
        }

        internal void Clear()
        {
            this.row = null;
        }

        internal override RowDataBase CreateRowData() => 
            this.View.ViewBehavior.CreateRowDataCore(this.treeBuilder, false);

        internal override LinkedList<FreeRowDataInfo> GetFreeRowDataQueue(SynchronizationQueues synchronizationQueues) => 
            synchronizationQueues.FreeRowDataQueue;

        private ItemType GetItemType(object matchKey)
        {
            if (matchKey is GroupSummaryRowKey)
            {
                return ItemType.GroupSummaryRow;
            }
            DevExpress.Xpf.Data.RowHandle handle = (DevExpress.Xpf.Data.RowHandle) matchKey;
            return (((handle.Value >= 0) || ((handle.Value == -2147483647) || (handle.Value == -2147483646))) ? ItemType.DataRow : ItemType.GroupRow);
        }

        internal override RowDataBase GetRowData() => 
            this.View.GetRowData(this.RowHandle.Value);

        internal override FrameworkElement GetRowElement()
        {
            RowDataBase rowData = this.GetRowData();
            return (((rowData == null) || (((ISupportVisibleIndex) rowData).VisibleIndex < 0)) ? null : this.View.GetRowVisibleElement(rowData));
        }

        internal override bool IsMatchedRowData(RowDataBase data)
        {
            RowData data2 = (RowData) data;
            if (DisableUpdateOptimization || (!data2.GetIsReady() || (data2.IsGroupRowInAsyncServerMode || (this.View.IsNewItemRowHandle(this.RowHandle.Value) || (this.RowHandle.Value == -2147483646)))))
            {
                return base.IsMatchedRowData(data);
            }
            if (this.GetItemType(this.MatchKey) != this.GetItemType(data.MatchKey))
            {
                return false;
            }
            this.row ??= this.treeBuilder.View.DataControl.GetRow(this.RowHandle.Value);
            return data2.IsSameRow(this.row);
        }

        internal override bool IsRowExpandedForNavigation() => 
            this.View.IsDataRowNodeExpanded(this);

        internal void ResumeUpdateState()
        {
            this.supressUpdateStateCount--;
        }

        internal void SupressUpdateState()
        {
            this.supressUpdateStateCount++;
        }

        internal void Update(DataControllerValuesContainer info)
        {
            this.ControllerValues = info;
        }

        internal void UpdateDetailInfo(int startVisibleIndex)
        {
            base.NodesContainer = this.View.DataControl.MasterDetailProvider.GetDetailNodeContainer(this.RowHandle.Value);
            base.UpdateExpandInfo(startVisibleIndex, true);
        }

        protected virtual void ValidateControllerValues()
        {
            if ((this.RowHandle.Value < 0) && ((this.RowHandle.Value != -2147483647) && (this.RowHandle.Value != -2147483646)))
            {
                throw new ArgumentException("Internal error: RowHandle should be positive");
            }
            this.row = null;
        }

        public override object MatchKey =>
            this.RowHandle;

        public RowNodePrintInfo PrintInfo { get; set; }

        internal DataViewBase View =>
            this.treeBuilder.View;

        protected DataControlBase DataControl =>
            this.View.DataControl;

        protected override bool IsDataExpanded =>
            this.DataControl.MasterDetailProvider.IsMasterRowExpanded(this.RowHandle.Value, null);

        internal DataControllerValuesContainer ControllerValues
        {
            get => 
                this.controllerValues;
            private set
            {
                this.controllerValues = value;
                this.ValidateControllerValues();
            }
        }

        internal override bool CanGenerateItems
        {
            get => 
                base.CanGenerateItems && this.CanGenerateItemsCore;
            set => 
                base.CanGenerateItems = value;
        }

        protected virtual bool CanGenerateItemsCore =>
            this.treeBuilder.SupportsMasterDetail;

        public DevExpress.Xpf.Data.RowHandle RowHandle =>
            this.ControllerValues.RowHandle;

        public int Level =>
            this.ControllerValues.Level;

        protected override bool CanUpdateState =>
            this.supressUpdateStateCount == 0;

        internal object Row =>
            this.row;

        public virtual int CurrentLevelItemCount =>
            1;

        private enum ItemType
        {
            DataRow,
            GroupRow,
            GroupSummaryRow
        }
    }
}

