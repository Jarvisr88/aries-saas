namespace DevExpress.Data.TreeList
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TreeListNodesInfo
    {
        private List<TreeListNodeBase> rowHandleToNodeCache;
        private List<TreeListNodeBase> visibleIndexToNodeCache;
        private List<TreeListNodeBase> parentNodes;
        private bool shouldRefreshRowHandles;
        private bool shouldRefreshVisibleIndicies;
        private int oldVisibleNodesCount;
        protected bool allNodes;
        private TreeListNodeBase newNode;

        public TreeListNodesInfo(TreeListDataControllerBase controller, bool allNodes = false);
        protected void BuildCache(ITreeListNodeCollection nodes, ref int rowHandleCounter, ref int visibleIndexCounter, bool isParentExpanded);
        protected virtual void ClearRowHandles();
        protected virtual void ClearVisibleIndicies();
        protected void EnsureCacheValid();
        public TreeListNodeBase FindNodeById(int id);
        public TreeListNodeBase FindVisibleNode(int rowHandle);
        public TreeListNodeBase GetNodeByRowHandle(int rowHandle);
        public TreeListNodeBase GetNodeByVisibleIndex(int visibleIndex);
        public int GetRowHandleByNode(TreeListNodeBase node);
        public int GetVisibleIndexByNode(TreeListNodeBase node);
        protected virtual void OnCacheCreated();
        protected virtual void OnVisibleIndexAssigned(TreeListNodeBase node);
        public void SetDirty();
        public virtual void SetDirty(bool visibleIndiciesOnly);
        internal void SetNewNode(TreeListNodeBase node);

        protected TreeListDataControllerBase Controller { get; private set; }

        public int TotalNodesCount { get; }

        public int TotalVisibleNodesCount { get; }
    }
}

