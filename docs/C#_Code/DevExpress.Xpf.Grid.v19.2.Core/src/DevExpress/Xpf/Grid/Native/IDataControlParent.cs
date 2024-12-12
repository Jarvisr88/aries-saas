namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IDataControlParent
    {
        int CalcTotalLevel();
        void CollectParentFixedRowsScrollIndexes(List<int> scrollIndexes);
        void CollectViewVisibleIndexChain(List<KeyValuePair<DataViewBase, int>> chain);
        void EnumerateParentDataControls(Action<DataControlBase, int> action);
        bool FindMasterRow(out DataViewBase targetView, out int targetVisibleIndex);
        DataViewBase FindMasterView();
        bool FindNextOuterMasterRow(out DataViewBase targetView, out int targetVisibleIndex);
        IEnumerable<ColumnsRowDataBase> GetColumnsRowDataEnumerator();
        ColumnsRowDataBase GetHeadersRowData();
        ColumnsRowDataBase GetNewItemRowData();
        void InvalidateTree();
        void ValidateMasterDetailConsistency(DataControlBase dataControl);
    }
}

