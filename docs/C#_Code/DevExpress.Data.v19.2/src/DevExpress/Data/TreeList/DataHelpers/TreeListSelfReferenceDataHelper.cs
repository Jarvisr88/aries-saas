namespace DevExpress.Data.TreeList.DataHelpers
{
    using DevExpress.Data.TreeList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TreeListSelfReferenceDataHelper : TreeListBoundDataHelper
    {
        private IBindingList bindingList;
        private int deletingChildrenInProgress;

        public TreeListSelfReferenceDataHelper(TreeListDataControllerBase controller, object dataSource);
        protected internal override void CheckNewNodePrimaryKeyValidity(TreeListNodeBase node);
        protected virtual bool CheckParentIDChanging(TreeListNodeBase node, string memberName);
        protected void CheckServiceColumns();
        public void DeleteNode(TreeListNodeBase node);
        public void DeleteNode(TreeListNodeBase node, bool deleteChildren);
        public override void DeleteNode(TreeListNodeBase node, bool deleteChildren, bool modifySource);
        public override Action DeleteNodeWithChildrenAndSource(TreeListNodeBase node, bool allowRollback);
        public override void Dispose();
        public override object GetCellValueByListIndex(int listSourceRowIndex, string fieldName);
        private IEnumerable<TreeListNodeBase> GetChildrenById(object id);
        public override object GetDataRowByListIndex(int listIndex);
        public override int GetListIndexByDataRow(object row);
        protected object GetListItem(int index);
        private Dictionary<object, TreeListNodeBase> GetNodeDictionaryFromListSource();
        protected override void InitNewNodeId(TreeListNodeBase node);
        protected override bool IsServiceColumnName(string fieldName);
        public override void LoadData();
        protected virtual void LoadDataCore();
        private void LoadLinearData(Dictionary<object, TreeListNodeBase> tempMap);
        private void MoveChildrenToParent(TreeListNodeBase parentNode, IEnumerable<TreeListNodeBase> children);
        protected virtual void OnBindingListChanged(object sender, ListChangedEventArgs e);
        private void OnBindingListChangedCore(object sender, ListChangedEventArgs e);
        protected virtual void OnItemAdded(int index);
        protected virtual void OnItemChanged(int index, string changedPropertyName);
        protected virtual void OnItemDeleted(int index);
        protected virtual void OnListChanged(ListChangedEventArgs e);
        protected virtual void OnReset();
        protected void ProcessUpdateNodeIndicesAction(ITreeListNodeCollection nodes, TreeListNodeBase nodeToRemove, Func<TreeListNodeBase, bool> action);
        private void RemoveRange(NodesIdInfo range);
        public override void SetCellValueByListIndex(int listSourceRowIndex, string fieldName, object value);
        private void SetParentChildRelation(TreeListNodeBase node);
        private void UpdateNodeIdsOnItemAdded(TreeListNodeBase node, int index);
        private void UpdateNodeIdsOnItemRemoved(int index);
        private void UpdateNodeIdsOnItemRemoved(NodesIdInfoManager idInfoManager, TreeListNodeBase nodeToRemove);

        protected override IBindingList BindingList { get; set; }

        protected string KeyFieldName { get; }

        protected string ParentFieldName { get; }

        protected object RootValue { get; }

        public override bool IsDeletingChildrenInProgress { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TreeListSelfReferenceDataHelper.<>c <>9;
            public static Comparison<TreeListSelfReferenceDataHelper.TreeListNodeRestoreInfo> <>9__28_1;

            static <>c();
            internal int <DeleteNodeWithChildrenAndSource>b__28_1(TreeListSelfReferenceDataHelper.TreeListNodeRestoreInfo x, TreeListSelfReferenceDataHelper.TreeListNodeRestoreInfo y);
        }

        private sealed class TreeListNodeRestoreInfo
        {
            public readonly TreeListNodeBase Node;
            public readonly int Id;
            public readonly List<TreeListNodeBase> Children;
            public readonly object Content;

            public TreeListNodeRestoreInfo(TreeListNodeBase node, int id, List<TreeListNodeBase> children, object content);
        }
    }
}

