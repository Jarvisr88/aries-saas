namespace DevExpress.Data.TreeList
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

    public class TreeListNodeCollectionBase<NodeType> : Collection<NodeType> where NodeType: TreeListNodeBase
    {
        private Dictionary<int, NodeType> idToNodeMap;
        protected internal int minID;
        protected internal int maxID;
        private bool clearing;

        public TreeListNodeCollectionBase(NodeType owner);
        protected internal void AddInternal(NodeType node);
        private void CalcMinMaxIndices();
        protected bool CanBeAddedAsChild(NodeType item);
        protected override void ClearItems();
        protected void ClearItemsCore();
        protected NodeType FindNodeById(int id);
        protected virtual void InitializeNode(NodeType currentNode, NodeType owner);
        protected void InitializeNodeAndDescendants(NodeType node, NodeType owner);
        protected override void InsertItem(int index, NodeType item);
        protected virtual void LinkNode(NodeType node);
        protected virtual void OnChanged(NodeType node, NodeChangeType changeType);
        protected virtual void OnChanging(NodeType node, NodeChangeType changeType);
        protected void OnNodeIdChanged(NodeType node, int oldId, int newId);
        protected internal void RemoveInternal(NodeType node);
        protected override void RemoveItem(int index);
        private void RemoveNodeFromIdToNodeMap(NodeType node, int id);
        private void ResetMinMaxIndices();
        protected override void SetItem(int index, NodeType item);
        protected internal virtual void SortNodes(IComparer<NodeType> comparer);
        protected virtual void UnlinkNode(NodeType node);
        protected void UpdateIndices();
        private void UpdateMinMaxIndices(int newId);
        private void UpdateMinMaxIndicesOnNodeIdChanged(int oldId, int newId);

        protected Dictionary<int, NodeType> IdToNodeMap { get; }

        protected internal bool IsDeletingChildrenInProgress { get; }

        protected internal NodeType OwnerCore { get; set; }

        protected TreeListDataControllerBase Controller { get; }

        private bool IsSelfReferenceMode { get; }
    }
}

