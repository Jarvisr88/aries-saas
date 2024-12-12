namespace DevExpress.Data.TreeList.DataHelpers
{
    using DevExpress.Data;
    using DevExpress.Data.TreeList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TreeListUnboundDataHelper : TreeListDataHelperBase
    {
        public readonly int MaxNodeId;

        public TreeListUnboundDataHelper(TreeListDataControllerBase controller);
        protected override void CalcNodeIds();
        public override void DeleteNode(TreeListNodeBase node, bool deleteChildren, bool modifySource);
        protected override PropertyDescriptor GetActualComplexPropertyDescriptor(ComplexColumnInfo info);
        public override IEnumerable<IBindingList> GetBindingLists();
        public override void LoadData();
        public override void PopulateColumns();
        public override void RecalcNodeIdsIfNeeded();
        protected internal override void UpdateNodeId(TreeListNodeBase node);

        protected int NodeCounter { get; set; }

        public override Type ItemType { get; }

        public override bool AllowEdit { get; }

        public override bool AllowRemove { get; }

        public override bool IsReady { get; }

        public override bool IsLoaded { get; }

        public override bool IsUnboundMode { get; }
    }
}

