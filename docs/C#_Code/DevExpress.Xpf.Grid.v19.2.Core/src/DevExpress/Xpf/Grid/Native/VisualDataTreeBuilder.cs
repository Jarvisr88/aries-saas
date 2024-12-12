namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class VisualDataTreeBuilder : DataTreeBuilder
    {
        private readonly DevExpress.Xpf.Grid.Native.SynchronizationQueues synchronizationQueues;
        private readonly Dictionary<int, RowData> rows;
        private readonly Dictionary<int, DataRowNode> nodes;
        private readonly Dictionary<int, RowData> groupSummaryRows;
        private readonly Dictionary<int, DataRowNode> groupSummaryNodes;

        public VisualDataTreeBuilder(DataViewBase view, MasterNodeContainer masterRootNode, MasterRowsContainer masterRootDataItem, DevExpress.Xpf.Grid.Native.SynchronizationQueues synchronizationQueues) : base(view, masterRootNode, masterRootDataItem)
        {
            this.rows = new Dictionary<int, RowData>();
            this.nodes = new Dictionary<int, DataRowNode>();
            this.groupSummaryRows = new Dictionary<int, RowData>();
            this.groupSummaryNodes = new Dictionary<int, DataRowNode>();
            this.synchronizationQueues = synchronizationQueues;
        }

        internal override void AddGroupSummaryRowNode(int rowHandle, DataRowNode node)
        {
            this.GroupSummaryNodes[rowHandle] = node;
        }

        public override void AfterSynchronization()
        {
            this.SynchronizationQueues.SynchronizeUnsynchronizedNodes();
            base.View.DataControl.MasterDetailProvider.SynchronizeDetailTree();
        }

        public void CacheGroupSummaryRowData(RowData rowData)
        {
            this.GroupSummaryRows[rowData.RowHandle.Value] = rowData;
        }

        public void CacheRowData(RowData rowData)
        {
            this.Rows[rowData.RowHandle.Value] = rowData;
        }

        public override void ClearRowsCache()
        {
            this.Rows.Clear();
            this.GroupSummaryRows.Clear();
        }

        public override void ForceLayout()
        {
            base.View.ForceLayout();
        }

        internal override object GetCellValue(RowData rowData, string fieldName) => 
            base.View.DataControl.GetCellValue(rowData, fieldName);

        internal override IList<ColumnBase> GetFixedLeftColumns() => 
            ((ITableView) base.View).TableViewBehavior.FixedLeftVisibleColumns;

        internal override IList<ColumnBase> GetFixedNoneColumns() => 
            ((ITableView) base.View).TableViewBehavior.FixedNoneVisibleColumns;

        internal override IList<ColumnBase> GetFixedRightColumns() => 
            ((ITableView) base.View).TableViewBehavior.FixedRightVisibleColumns;

        internal override ColumnBase GetGroupColumnByNode(DataRowNode node) => 
            base.View.GetColumnBySortLevel(node.Level);

        internal override GroupTextHighlightingProperties GetGroupHighlightingPropertiesByNode(DataRowNode node) => 
            base.View.GetGroupHighlightingProperties(node.RowHandle.Value);

        internal override string GetGroupRowDisplayTextByNode(DataRowNode node) => 
            base.View.GetGroupRowDisplayText(node.RowHandle.Value);

        internal override string GetGroupRowHeaderCaptionByNode(DataRowNode node) => 
            base.View.GetGroupRowHeaderCaption(node.RowHandle.Value);

        internal override string[] GetGroupRowHeadersCaptionsByNode(DataRowNode node) => 
            base.View.GetGroupRowHeadersCaptions(node.RowHandle.Value);

        internal override IList<SummaryItemBase> GetGroupSummaries() => 
            base.View.DataControl.GetGroupSummaries();

        internal override DataRowNode GetGroupSummaryRowNode(int rowHandle)
        {
            DataRowNode node;
            return (!this.GroupSummaryNodes.TryGetValue(rowHandle, out node) ? null : node);
        }

        internal override object GetGroupValueByNode(DataRowNode node) => 
            base.View.GetGroupDisplayValue(node.RowHandle.Value);

        internal override object[] GetGroupValuesByNode(DataRowNode node) => 
            base.View.GetGroupDisplayValues(node.RowHandle.Value);

        internal override DataRowNode GetRowNode(DataTreeBuilder.CreateRowNodeDelegate createDelegate, DataControllerValuesContainer controllerValues)
        {
            DataRowNode node;
            if (this.Nodes.TryGetValue(controllerValues.RowHandle.Value, out node))
            {
                node.Update(controllerValues);
                return node;
            }
            node = createDelegate(controllerValues);
            this.Nodes.Add(controllerValues.RowHandle.Value, node);
            return node;
        }

        internal override object GetRowValue(RowData rowData) => 
            base.View.GetRowValue(rowData.RowHandle);

        internal override object GetServiceTotalSummaryValue(ServiceSummaryItem item) => 
            base.View.DataControl.DataProviderBase.GetTotalSummaryValue(item);

        internal override IList<ColumnBase> GetVisibleColumns() => 
            base.View.VisibleColumnsCore;

        internal override object GetWpfRow(RowData rowData, int listSourceRowIndex) => 
            base.View.GetWpfRow(rowData.RowHandle, listSourceRowIndex);

        public override void SetRowStateDirty()
        {
            base.View.RowsStateDirty = true;
        }

        public override void Synchronize(RowsContainer dataContainer, NodeContainer nodeContainer)
        {
            dataContainer.CreateRowsContainerSyncronizer().Synchronize(nodeContainer);
        }

        internal override bool TryGetGroupSummaryValue(RowData rowData, SummaryItemBase item, out object value) => 
            rowData.View.DataControl.DataProviderBase.TryGetGroupSummaryValue(rowData.RowHandle.Value, item, out value);

        internal override void UpdateCellData(RowData rowData, GridCellData cellData, ColumnBase column)
        {
            base.UpdateCellData(rowData, cellData, column);
            cellData.UpdateFullState(rowData.RowHandle.Value);
            if (base.View.NeedCellsWidthUpdateOnScrolling)
            {
                cellData.SyncCellContentPresenterProperties();
            }
            rowData.UpdateCellDataError(column, cellData, rowData.CustomCellValidate);
        }

        internal override void UpdateRowData(RowData rowData)
        {
            rowData.UpdateFullState();
        }

        internal override void UpdateRowDataError(RowData rowData)
        {
            RowValidationError rowStateError = base.View.DataControl.RowStateError;
            if ((rowStateError != null) && (rowData.RowHandleCore.Value == base.View.FocusedRowHandle))
            {
                BaseEditHelper.SetValidationError(rowData, rowStateError);
                base.View.ValidationError = rowStateError;
            }
            else
            {
                if (!base.View.HasCellEditorError || !ReferenceEquals(base.View.ValidationError, BaseEditHelper.GetValidationError(rowData)))
                {
                    if (!base.View.ItemsSourceErrorInfoShowMode.HasFlag(ItemsSourceErrorInfoShowMode.Row))
                    {
                        BaseEditHelper.SetValidationError(rowData, null);
                    }
                    else
                    {
                        MultiErrorInfo multiErrorInfo = base.View.DataProviderBase.GetMultiErrorInfo(rowData.RowHandle);
                        BaseValidationError error = multiErrorInfo.HasErrors() ? base.View.CreateRowValidationError(multiErrorInfo.ErrorText, multiErrorInfo.Errors, multiErrorInfo.ErrorType, rowData.RowHandle.Value) : null;
                        BaseEditHelper.SetValidationError(rowData, error);
                        if (!base.View.HasCellEditorError)
                        {
                            base.View.ValidationError = null;
                        }
                    }
                }
                rowData.UpdateIndicatorState();
            }
        }

        internal DevExpress.Xpf.Grid.Native.SynchronizationQueues SynchronizationQueues =>
            this.synchronizationQueues;

        public Dictionary<int, RowData> Rows =>
            this.rows;

        public Dictionary<int, RowData> GroupSummaryRows =>
            this.groupSummaryRows;

        public Dictionary<int, DataRowNode> Nodes =>
            this.nodes;

        public Dictionary<int, DataRowNode> GroupSummaryNodes =>
            this.groupSummaryNodes;

        public override bool SupportsMasterDetail =>
            true;

        public override bool SupportsHorizontalVirtualization =>
            true;
    }
}

