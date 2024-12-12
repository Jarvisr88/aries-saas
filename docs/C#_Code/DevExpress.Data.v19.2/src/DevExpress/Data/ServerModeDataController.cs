namespace DevExpress.Data
{
    using DevExpress.Data.Details;
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ServerModeDataController : ServerModeDataControllerBase
    {
        protected override void CalcTotalSummary();
        protected override void ChangeAllExpanded(bool expanded);
        protected override void ChangeExpanded(int groupRowHandle, bool expanded, bool recursive);
        protected override void ChangeExpandedLevel(int groupLevel, bool expanded, bool recursive);
        private void CheckChildrenReady(ServerModeGroupRowInfo sgroup, bool expanded, bool recursive);
        private void CreateChildren(IList destination, byte level, int startIndex, ListSourceGroupInfo linfo, ServerModeGroupRowInfo group);
        public override IClassicRowKeeper CreateControllerRowsKeeperClassic();
        protected override IClassicRowKeeper CreateControllerRowsKeeperCore();
        protected override FilterHelper CreateFilterHelper();
        private void CreateGroupInfo(List<ListSourceGroupInfo> list, IList destination, ServerModeGroupRowInfo parentGroup);
        protected override GroupRowInfoCollection CreateGroupRowInfoCollection();
        protected override MasterRowInfoCollection CreateMasterRowCollection();
        protected override VisibleIndexCollection CreateVisibleIndexCollection();
        protected override void DoGroupRowsCore();
        protected override void DoSortRows();
        public override int FindIncremental(string text, int columnHandle, int startRowHandle, bool down, bool ignoreStartRow, bool allowLoop, CompareIncrementalValue compareValue, params OperationCompleted[] completed);
        public override int FindRowByBeginWith(string columnName, string text);
        public override int FindRowByRowValue(object value, int tryListSourceIndex = -1);
        public override int FindRowByValue(string columnName, object value, params OperationCompleted[] completed);
        public override int FindRowByValues(Dictionary<DataColumnInfo, object> values);
        public override IList GetAllFilteredAndSortedRows(Function<bool> callBackMethod);
        protected override object GetCurrentControllerRowObject();
        protected override IList GetListSource();
        protected internal override void MakeGroupRowVisible(GroupRowInfo group);
        public override bool PrefetchAllData(Function<bool> callbackMethod);
        protected override bool ProcessListServerAction(string fieldName, ColumnServerActionType action, out bool res);
        protected virtual ServerModeGroupRowInfo RequestChildren(ServerModeGroupRowInfo sgroup);
        protected override GroupRowInfo RequestSummary(GroupRowInfo group);
        protected internal override void RestoreGroupExpanded(GroupRowInfo group);
        public override void UpdateGroupSummary(List<SummaryItem> changedItems);

        public override bool AllowNew { get; }

        public override bool IsReady { get; }

        public IListServer ListSourceEx { get; }

        public IListServerCaps ListSourceEx2 { get; }

        public override bool CanSort { get; }

        public override bool CanGroup { get; }

        public override bool CanFilter { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServerModeDataController.<>c <>9;
            public static Func<KeyValuePair<DataColumnInfo, object>, bool> <>9__26_0;

            static <>c();
            internal bool <FindRowByValues>b__26_0(KeyValuePair<DataColumnInfo, object> vPair);
        }
    }
}

