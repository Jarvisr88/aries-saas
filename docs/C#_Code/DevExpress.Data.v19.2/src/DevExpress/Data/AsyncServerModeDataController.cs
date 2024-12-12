namespace DevExpress.Data
{
    using DevExpress.Data.Async;
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class AsyncServerModeDataController : ServerModeDataControllerBase
    {
        public static readonly object NoValue;
        private object currentControllerRowObjectEx;
        private int groupedColumnCount;
        internal bool requireExpandAll;
        private bool asyncStatusIsBusy;
        private int maxListGroupCount;
        private static readonly object Tag_PGroup_ServerModeGroupRowInfo;
        private static readonly object Tag_ExpandChildren_bool;
        private int logCounter;
        private bool rootGroupInfoRequested;

        static AsyncServerModeDataController();
        public AsyncServerModeDataController();
        protected void BaseClearVisibleInfo();
        protected override void CalcTotalSummary();
        public override void CancelFindIncremental();
        public override void CancelWeakFindIncremental();
        protected override void ChangeAllExpanded(bool expanded);
        protected override void ChangeExpanded(int groupRowHandle, bool expanded, bool recursive);
        protected override void ChangeExpandedLevel(int groupLevel, bool expanded, bool recursive);
        protected override void CheckCurrentControllerRowObjectOnRefresh();
        protected bool CheckIsAllGroupsReady();
        private List<ListSourceGroupInfo> CheckLimitServerGroupResult(List<ListSourceGroupInfo> childList);
        protected override void CheckRaiseVisibleCountChanged(int prevVCount);
        public override void ClearInvalidRowsCache();
        protected override void ClearVisibleInfoOnRefresh();
        private void CreateChildren(IList destination, byte level, int startIndex, ListSourceGroupInfo linfo, ServerModeGroupRowInfo group, bool expandChildren);
        public override IClassicRowKeeper CreateControllerRowsKeeperClassic();
        protected override IClassicRowKeeper CreateControllerRowsKeeperCore();
        protected override FilterHelper CreateFilterHelper();
        private void CreateGroupInfo(List<ListSourceGroupInfo> list, IList destination, ServerModeGroupRowInfo parentGroup, bool expandChildren);
        protected override GroupRowInfoCollection CreateGroupRowInfoCollection();
        protected override BaseDataControllerHelper CreateHelper();
        protected override VisibleIndexCollection CreateVisibleIndexCollection();
        public override void Dispose();
        protected void DisposeWrapper();
        private void DoExpandAll();
        protected override void DoGroupRowsCore();
        protected override void DoRefresh(bool useRowsKeeper);
        protected override void DoSortRows();
        public override void EnsureRowLoaded(int controllerRow, OperationCompleted completed);
        public override int FindIncremental(string text, int columnHandle, int startRowHandle, bool down, bool ignoreStartRow, bool allowLoop, CompareIncrementalValue compareValue, params OperationCompleted[] completed);
        private ListSourceGroupInfo FindMatchedGroup(ref int groupStartIndex, int index, List<ListSourceGroupInfo> list);
        public override int FindRowByBeginWith(string columnName, string text);
        public override int FindRowByRowValue(object value, int tryListSourceIndex = -1);
        public override int FindRowByValue(string columnName, object value, params OperationCompleted[] completed);
        private int GetAdditionalCurrentRow();
        public override IList GetAllFilteredAndSortedRows(Function<bool> callBackMethod);
        protected override object GetCurrentControllerRowObject();
        protected override IList GetListSource();
        internal object GetLoadedRowKey(int controllerRow);
        internal object GetLoadedRowKey(int controllerRow, bool allowGroupRow);
        private void GetServerGroupInfo(ServerModeGroupRowInfo groupInfo, bool expandChildren);
        private ListSortDescriptionCollection GetSortCollection();
        private bool IsAllowAutoExpandGroupInfo(List<ListSourceGroupInfo> childList);
        internal bool IsAllowRequestMoreAutoExpandGroups();
        public static bool IsNoValue(object value);
        public override bool IsRowLoaded(int controllerRow);
        public override void LoadRowHierarchy(int rowHandle, OperationCompleted completed);
        private bool LoadRowHierarchyCore(AsyncRowInfo info, OperationCompleted completed);
        public override void LoadRows(int startFrom, int count);
        internal void Log(string text, params object[] args);
        protected internal override void MakeGroupRowVisible(GroupRowInfo group);
        internal void OnAfterAsyncGroupInfoReceived(bool isRootGroup);
        internal void OnAsyncBusyChanged(bool busy);
        internal void OnAsyncGroupInfoReceived(List<CommandGetGroupInfo> results);
        internal void OnAsyncGroupInfoReceivedCore(CommandGetGroupInfo result);
        protected virtual void OnAsyncInvalidGroupInfoReceived();
        internal void OnAsyncRowReceived(int rowIndex);
        internal void OnAsyncTotalsReceived(CommandGetTotals result);
        protected override void OnCurrentControllerRowObjectChanging(object oldObject, object newObject, int level, int sourceIndex);
        protected override void OnPostRefresh(bool useRowsKeeper);
        protected virtual void OnRootGroupReceived();
        protected virtual void OnTotalsReceived();
        internal void OnTotalsRequested();
        public override bool PrefetchAllData(Function<bool> callBackMethod);
        private bool PreloadDataRows(Function<bool> callBackMethod);
        private void PreloadGroups(ServerModeGroupRowInfo[] sgroups);
        private void RequestChildren(ServerModeGroupRowInfo sgroup, bool expandChildren);
        protected override GroupRowInfo RequestSummary(GroupRowInfo group);
        protected override void Reset();
        protected internal override void RestoreGroupExpanded(GroupRowInfo group);
        internal void RestoreGroupHierarchy(CommandGetRowIndexByKey result, bool expandGroups, OperationCompleted completed);
        public override void ScrollingCancelAllGetRows();
        public override void ScrollingCheckRowLoaded(int rowHandle);
        protected override void SetListSourceCore(IList value);
        public override void UpdateGroupSummary(List<SummaryItem> changedItems);
        internal void UpdateTotalSummaryResult(List<object> summaryResults);

        public AsyncServerModeGroupRowInfoCollection GroupInfo { get; }

        protected IClassicRowKeeperAsync RowsKeeperAsync { get; }

        public override bool AllowNew { get; }

        public override bool IsReady { get; }

        protected internal AsyncListWrapper Wrapper { get; }

        public IAsyncListServer Server { get; }

        internal object CurrentControllerRowObjectEx { get; }

        public override bool IsBusy { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncServerModeDataController.<>c <>9;
            public static Func<GroupRowInfo, ServerModeGroupRowInfo> <>9__64_0;
            public static Func<ServerModeGroupRowInfo, bool> <>9__64_1;
            public static Func<ServerModeGroupRowInfo, ListSourceGroupInfo> <>9__64_2;
            public static OperationCompleted <>9__80_0;

            static <>c();
            internal void <LoadRowHierarchy>b__80_0(object x);
            internal ServerModeGroupRowInfo <PrefetchAllData>b__64_0(GroupRowInfo q);
            internal bool <PrefetchAllData>b__64_1(ServerModeGroupRowInfo q);
            internal ListSourceGroupInfo <PrefetchAllData>b__64_2(ServerModeGroupRowInfo q);
        }
    }
}

