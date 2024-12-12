namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using System;

    public class SingleColumnTreeListNodeComaprer : TreeListNodeComparerBase
    {
        private Comparison<TreeListNodeBase> comparision;
        private ColumnSortOrder sortOrder;
        private TreeListDataColumnSortInfo sortInfo;

        public SingleColumnTreeListNodeComaprer(TreeListDataControllerBase controller);
        public override int Compare(TreeListNodeBase node1, TreeListNodeBase node2);
        public override void Init(ITreeListNodeCollection nodes);
    }
}

