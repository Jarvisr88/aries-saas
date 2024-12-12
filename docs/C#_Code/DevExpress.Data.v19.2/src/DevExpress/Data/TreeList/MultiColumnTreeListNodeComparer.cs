namespace DevExpress.Data.TreeList
{
    using System;
    using System.Runtime.CompilerServices;

    public class MultiColumnTreeListNodeComparer : TreeListNodeComparerBase
    {
        private Tuple<TreeListDataColumnSortInfo, Comparison<TreeListNodeBase>>[] comparision;

        public MultiColumnTreeListNodeComparer(TreeListDataControllerBase controller);
        public override int Compare(TreeListNodeBase node1, TreeListNodeBase node2);
        public override void Init(ITreeListNodeCollection nodes);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiColumnTreeListNodeComparer.<>c <>9;
            public static Action<Tuple<TreeListDataColumnSortInfo, Comparison<TreeListNodeBase>>> <>9__2_1;

            static <>c();
            internal void <Init>b__2_1(Tuple<TreeListDataColumnSortInfo, Comparison<TreeListNodeBase>> comp);
        }
    }
}

