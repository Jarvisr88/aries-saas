namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IDataProvider
    {
        void AfterPopulateColumns();
        void BeforePopulateColumns();
        void BeginInvoke(Action action);
        TreeListNodeBase CreateNode(object content);
        TreeListNodeBase CreateRootNode(TreeListDataControllerBase controller);
        int? CustomNodeSort(TreeListNodeBase node1, TreeListNodeBase node2, object value1, object value2, DataColumnInfo column, ColumnSortOrder sortOrder);
        object CustomSummary(TreeListNodeBase node, object totalValue, SummaryItem summaryItem, CustomSummaryProcess process, object fieldValue, out bool isTotalValueReady);
        ComplexColumnInfoCollection GetComplexColumns();
        object GetCustomUnboundData(TreeListNodeBase node, string propertyName, object value);
        string GetDisplayText(TreeListNodeBase node, string fieldName, object value);
        UnboundColumnInfoCollection GetUnboundColumns();
        IEnumerable<TreeListUnboundModeDataColumnInfo> GetUnboundModeDataColumns();
        void InitUnboundMode();
        bool IsServiceColumnName(string fieldName);
        void OnCurrentNodeChanged();
        void OnDataSourceChanged();
        void OnEndSorting();
        void OnStartSorting();
        bool RaiseCustomFilterPopupList(TreeListNodeBase node, DataColumnInfo columnInfo);
        bool? RaiseCustomNodeFilter(TreeListNodeBase node);
        void RaiseInvalidNodeException(TreeListNodeBase currentNode, ControllerRowExceptionEventArgs args);
        bool RaiseValidateNode(TreeListNodeBase currentNode);
        bool RequiresFilteringByDisplayText(DataColumnInfo column);
        void SetCustomUnboundData(TreeListNodeBase node, string propertyName, object value);
        void SubstituteFilter(SubstituteFilterEventArgs e);
        void SynchronizeSortInfo(TreeListDataColumnSortInfoCollection sortInfo);
        void UpdateRows();

        bool CanUseFastPropertyDescriptors { get; }

        DevExpress.Data.TreeList.FilterMode FilterMode { get; }

        bool IsCustomNodeFilterAssigned { get; }

        bool ExpandNodesOnFiltering { get; }

        bool HasCustomSummary { get; }

        bool SummariesIgnoreNullValues { get; }

        bool AutoPopulateServiceColumns { get; }

        string KeyFieldName { get; }

        string ParentFieldName { get; }

        object RootValue { get; }

        bool AllowReloadDataOnEndUpdate { get; }

        bool AutoDetectColumnTypeInHierarchicalMode { get; }

        bool InvokeRequired { get; }

        bool IsDesignMode { get; }
    }
}

