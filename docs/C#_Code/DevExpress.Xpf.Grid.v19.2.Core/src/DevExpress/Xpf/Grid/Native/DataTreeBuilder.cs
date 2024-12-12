namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class DataTreeBuilder
    {
        private readonly DevExpress.Xpf.Grid.HeadersData headersData;
        private readonly DataViewBase view;
        private readonly DevExpress.Xpf.Grid.DetailNodeContainer rootNodeContainer;
        private readonly MasterNodeContainer masterRootNodeContainer;
        private readonly DevExpress.Xpf.Grid.DetailRowsContainer rootRowsContainer;
        private readonly MasterRowsContainer masterRootRowsContainer;

        public DataTreeBuilder(DataViewBase view, MasterNodeContainer masterRootNode, MasterRowsContainer masterRootDataItem)
        {
            this.view = view;
            this.headersData = new DevExpress.Xpf.Grid.HeadersData(this);
            this.rootRowsContainer = (masterRootDataItem != null) ? new DevExpress.Xpf.Grid.DetailRowsContainer(this, 0) : new MasterRowsContainer(this, 0);
            DataTreeBuilder rootRowsContainer = this;
            if (masterRootDataItem == null)
            {
                rootRowsContainer = (DataTreeBuilder) ((MasterRowsContainer) this.rootRowsContainer);
            }
            masterRootDataItem.masterRootRowsContainer = (MasterRowsContainer) rootRowsContainer;
            this.rootNodeContainer = (masterRootNode != null) ? new DevExpress.Xpf.Grid.DetailNodeContainer(this, 0) : new MasterNodeContainer(this, 0);
            DataTreeBuilder rootNodeContainer = this;
            if (masterRootNode == null)
            {
                rootNodeContainer = (DataTreeBuilder) ((MasterNodeContainer) this.rootNodeContainer);
            }
            masterRootNode.masterRootNodeContainer = (MasterNodeContainer) rootNodeContainer;
        }

        internal virtual void AddGroupSummaryRowNode(int rowHandle, DataRowNode node)
        {
        }

        public virtual void AfterSynchronization()
        {
        }

        public virtual void ClearRowsCache()
        {
        }

        internal static T CreateRowElement<T>(bool isGroupRow, Func<T> groupRowCreator, Func<T> dataRowCreator) => 
            isGroupRow ? groupRowCreator() : dataRowCreator();

        public virtual void ForceLayout()
        {
        }

        internal abstract object GetCellValue(RowData rowData, string fieldName);
        internal abstract IList<ColumnBase> GetFixedLeftColumns();
        internal abstract IList<ColumnBase> GetFixedNoneColumns();
        internal abstract IList<ColumnBase> GetFixedRightColumns();
        internal abstract ColumnBase GetGroupColumnByNode(DataRowNode node);
        internal abstract GroupTextHighlightingProperties GetGroupHighlightingPropertiesByNode(DataRowNode node);
        internal abstract string GetGroupRowDisplayTextByNode(DataRowNode node);
        internal abstract string GetGroupRowHeaderCaptionByNode(DataRowNode node);
        internal abstract string[] GetGroupRowHeadersCaptionsByNode(DataRowNode node);
        internal abstract IList<SummaryItemBase> GetGroupSummaries();
        internal virtual DataRowNode GetGroupSummaryRowNode(int rowHandle) => 
            null;

        internal abstract object GetGroupValueByNode(DataRowNode node);
        internal abstract object[] GetGroupValuesByNode(DataRowNode node);
        protected internal virtual int GetRowHandleByVisibleIndexCore(int visibleIndex) => 
            this.view.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);

        protected internal virtual int GetRowLevelByControllerRow(int rowHandle) => 
            this.view.DataProviderBase.GetRowLevelByControllerRow(rowHandle);

        protected internal virtual int GetRowLevelByVisibleIndex(int visibleIndex) => 
            this.view.DataProviderBase.GetRowLevelByVisibleIndex(visibleIndex);

        internal virtual DataRowNode GetRowNode(CreateRowNodeDelegate createDelegate, DataControllerValuesContainer controllerValues) => 
            createDelegate(controllerValues);

        internal abstract object GetRowValue(RowData rowData);
        protected internal virtual int GetRowVisibleIndexByHandleCore(int rowHandle) => 
            this.view.DataControl.GetRowVisibleIndexByHandleCore(rowHandle);

        internal abstract object GetServiceTotalSummaryValue(ServiceSummaryItem item);
        internal object GetServiceTotalSummaryValue(string fieldName, ConditionalFormatSummaryType summaryType) => 
            ((((summaryType != ConditionalFormatSummaryType.Average) && (summaryType != ConditionalFormatSummaryType.Min)) && (summaryType != ConditionalFormatSummaryType.Max)) || !this.View.DataControl.HasConditionFormatFilters) ? this.GetServiceTotalSummaryValueNoFormatConditionFilter(fieldName, summaryType) : this.View.DataControl.GetFormatConditionSummary(fieldName, summaryType, TopBottomFilterKind.Conditional);

        internal object GetServiceTotalSummaryValueNoFormatConditionFilter(string fieldName, ConditionalFormatSummaryType summaryType)
        {
            IDictionary<ServiceSummaryItemKey, ServiceSummaryItem> serviceSummaries = this.View.ViewBehavior.GetServiceSummaries();
            if (!serviceSummaries.Any<KeyValuePair<ServiceSummaryItemKey, ServiceSummaryItem>>())
            {
                return null;
            }
            ServiceSummaryItem item = null;
            serviceSummaries.TryGetValue(ConditionalFormatSummaryInfoHelper.ToSummaryItemKey(summaryType, fieldName, this.View.DataControl.DataProviderBase), out item);
            return ((item == null) ? null : this.GetServiceTotalSummaryValue(item));
        }

        internal abstract IList<ColumnBase> GetVisibleColumns();
        internal abstract object GetWpfRow(RowData rowData, int listSourceRowIndex);
        protected internal virtual bool IsEvenRow(RowData rowData) => 
            this.view.IsEvenRow(rowData.RowHandle.Value);

        public virtual void SetRowStateDirty()
        {
        }

        public virtual void Synchronize(RowsContainer dataContainer, NodeContainer nodeContainer)
        {
        }

        public void SynchronizeMasterNode()
        {
            this.MasterRootRowsContainer.Synchronize(this.MasterRootNodeContainer);
            this.AfterSynchronization();
            this.MasterRootRowsContainer.UpdatePostponedData(true, this.MasterRootRowsContainer.RowsToUpdate.Count != 0);
        }

        internal abstract bool TryGetGroupSummaryValue(RowData rowData, SummaryItemBase item, out object value);
        internal virtual void UpdateCellData(RowData rowData, GridCellData cellData, ColumnBase column)
        {
            cellData.Data = rowData.DataContext;
        }

        internal virtual void UpdateColumnData(ColumnsRowDataBase rowData, GridColumnData cellData, ColumnBase column)
        {
        }

        internal virtual void UpdateGroupRowData(RowData rowData)
        {
        }

        internal abstract void UpdateRowData(RowData rowData);
        internal virtual void UpdateRowDataError(RowData rowData)
        {
        }

        public abstract bool SupportsHorizontalVirtualization { get; }

        public abstract bool SupportsMasterDetail { get; }

        public DevExpress.Xpf.Grid.DetailNodeContainer RootNodeContainer =>
            this.rootNodeContainer;

        public MasterNodeContainer MasterRootNodeContainer =>
            this.masterRootNodeContainer;

        public DevExpress.Xpf.Grid.DetailRowsContainer RootRowsContainer =>
            this.rootRowsContainer;

        public MasterRowsContainer MasterRootRowsContainer =>
            this.masterRootRowsContainer;

        public DataViewBase View =>
            this.view;

        public virtual int VisibleCount =>
            !this.IsPagingMode ? (this.view.DataControl.VisibleRowCount + this.View.CalcGroupSummaryVisibleRowCount()) : this.View.ItemsOnPage;

        public DevExpress.Xpf.Grid.HeadersData HeadersData =>
            this.headersData;

        protected internal virtual bool GenerateBottomFixedRowInEnd =>
            false;

        protected internal virtual bool IsPrinting =>
            false;

        protected internal virtual int PageOffset =>
            this.view.PageOffset;

        protected internal virtual bool IsPagingMode =>
            this.view.IsPagingMode;

        internal delegate DataRowNode CreateRowNodeDelegate(DataControllerValuesContainer controllerValues);
    }
}

