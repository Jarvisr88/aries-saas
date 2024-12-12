namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.XtraGrid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ServerModeDataControllerBase : BaseGridControllerEx
    {
        private static readonly CriteriaOperator CurrentYearBeginFunction;

        static ServerModeDataControllerBase();
        protected ServerModeDataControllerBase();
        protected override bool AllowServerAction(string fieldName, ColumnServerActionType action);
        private static CriteriaOperator BinaryEqual(CriteriaOperator left, CriteriaOperator right);
        private static CriteriaOperator BinaryGreater(CriteriaOperator left, CriteriaOperator right);
        private static CriteriaOperator BinaryGreaterOrEq(CriteriaOperator left, CriteriaOperator right);
        private static CriteriaOperator BinaryLess(CriteriaOperator left, CriteriaOperator right);
        private static CriteriaOperator BinaryLessOrEq(CriteriaOperator left, CriteriaOperator right);
        protected override void CalcGroupSummaryItem(SummaryItem summary);
        protected override bool CanFindUnboundColumn(DataColumnInfo column);
        public static CriteriaOperator DescriptorToCriteria(DataColumnInfo column);
        private static CriteriaOperator DescriptorToCriteria(PropertyDescriptor pd);
        private static CriteriaOperator DiffDay(CriteriaOperator start, CriteriaOperator end);
        private static CriteriaOperator DiffHour(CriteriaOperator start, CriteriaOperator end);
        private static CriteriaOperator DiffMonth(CriteriaOperator start, CriteriaOperator end);
        protected override void DoFilterRows();
        protected override void DoSortSummary();
        public static CriteriaOperator GetColumnGroupIntervalCriteria(CriteriaOperator plainCriteria, ColumnGroupInterval groupType, out bool isGroupInterval);
        private static CriteriaOperator GetColumnGroupIntervalCriteriaDateRange(CriteriaOperator plainCriteria);
        private static CriteriaOperator GetDate(CriteriaOperator criteria);
        private static CriteriaOperator GetDateByInterval(OutlookInterval interval);
        public override object GetGroupRowValue(GroupRowInfo group);
        protected override object GetGroupRowValue(GroupRowInfo group, int column);
        public override object[] GetGroupRowValues(GroupRowInfo group);
        protected override Hashtable GetGroupSummaryCore(GroupRowInfo group);
        protected abstract IList GetListSource();
        internal ListSourceGroupInfo GetListSourceGroupInfo(GroupRowInfo group);
        private static CriteriaOperator GetMonth(CriteriaOperator criteria);
        internal static List<ServerModeOrderDescriptor[]> GetSortCollection(DataController controller);
        private static List<ServerModeOrderDescriptor[]> GetSortCollection(DataColumnSortInfo[] sortInfo, int groupCount);
        private static CriteriaOperator GetWeekStart();
        private static CriteriaOperator Iif(CriteriaOperator condition, CriteriaOperator trueResult, CriteriaOperator falseResult);
        internal static ICollection<ServerModeSummaryDescriptor> ListSourceSummaryItemsToServerModeSummaryDescriptors(ICollection<ListSourceSummaryItem> src);
        internal static ICollection<ServerModeSummaryDescriptor> ListSourceSummaryItemsToServerModeSummaryDescriptors(ICollection<ListSourceSummaryItem> src, Func<ListSourceSummaryItem, CriteriaOperator, Aggregate, ServerModeSummaryDescriptor> createItem);
        protected override void OnBindingListChangedCore(ListChangedEventArgs e);
        protected internal override void OnColumnPopulated(DataColumnInfo info);
        protected override void OnDataSourceChanged();
        protected virtual bool ProcessListServerAction(string fieldName, ColumnServerActionType action, out bool res);
        internal static Aggregate SummaryTypeToAggregate(SummaryItemType summaryType, out bool ignoreExpression);
        internal void UpdateGroupSummary(GroupRowInfo group);
        public override void UpdateTotalSummary(List<SummaryItem> changedItems);
        public override void ValidateExpression(CriteriaOperator op);

        public override bool IsServerMode { get; }

        public override bool AutoUpdateTotalSummary { get; set; }

        public override bool ImmediateUpdateRowPosition { get; set; }

        protected internal virtual bool AllowSortUnbound { get; }

        protected override bool RequireEndEditOnGroupRows { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeDataControllerBase.<>c <>9;
            public static Func<ListSourceSummaryItem, CriteriaOperator, Aggregate, ServerModeSummaryDescriptor> <>9__50_0;

            static <>c();
            internal ServerModeSummaryDescriptor <ListSourceSummaryItemsToServerModeSummaryDescriptors>b__50_0(ListSourceSummaryItem s, CriteriaOperator c, Aggregate a);
        }
    }
}

