namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public sealed class NullDataControlParent : IDataControlParent
    {
        private ColumnsRowDataBase[] Empty = new ColumnsRowDataBase[0];
        public static readonly IDataControlParent Instance = new NullDataControlParent();

        private NullDataControlParent()
        {
        }

        int IDataControlParent.CalcTotalLevel() => 
            0;

        void IDataControlParent.CollectParentFixedRowsScrollIndexes(List<int> scrollIndexes)
        {
        }

        void IDataControlParent.CollectViewVisibleIndexChain(List<KeyValuePair<DataViewBase, int>> chain)
        {
        }

        void IDataControlParent.EnumerateParentDataControls(Action<DataControlBase, int> action)
        {
        }

        bool IDataControlParent.FindMasterRow(out DataViewBase targetView, out int targetVisibleIndex)
        {
            targetView = null;
            targetVisibleIndex = -1;
            return false;
        }

        DataViewBase IDataControlParent.FindMasterView() => 
            null;

        bool IDataControlParent.FindNextOuterMasterRow(out DataViewBase targetView, out int targetVisibleIndex)
        {
            targetView = null;
            targetVisibleIndex = -1;
            return false;
        }

        IEnumerable<ColumnsRowDataBase> IDataControlParent.GetColumnsRowDataEnumerator() => 
            this.Empty;

        ColumnsRowDataBase IDataControlParent.GetHeadersRowData() => 
            null;

        ColumnsRowDataBase IDataControlParent.GetNewItemRowData() => 
            null;

        void IDataControlParent.InvalidateTree()
        {
        }

        void IDataControlParent.ValidateMasterDetailConsistency(DataControlBase dataControl)
        {
        }
    }
}

