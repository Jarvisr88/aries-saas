namespace DevExpress.Data.TreeList
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class TreeListNodeBase : INotifyPropertyChanged
    {
        private object content;
        private bool expanded;
        private bool isVisible;
        private int idCore;
        private ITreeListNodeCollection nodesCore;
        protected internal TreeListNodeBase parentNodeInternal;
        protected internal int rowHandleCore;
        protected internal int visibleIndexCore;
        private TreeListDataControllerBase controller;

        public event PropertyChangedEventHandler PropertyChanged;

        public TreeListNodeBase();
        public TreeListNodeBase(object content);
        protected void ChangeExpanded(bool value);
        public void CollapseAll();
        protected abstract ITreeListNodeCollection CreateNodeCollection();
        public void ExpandAll();
        protected virtual void InitializeController();
        protected internal virtual void InitializeNodeInternal();
        [Browsable(false)]
        public bool IsDescendantOf(TreeListNodeBase node);
        protected void NotifyDataController(NodeChangeType nodeChangeType);
        protected void RaisePropertyChanged(string propertyName);
        protected virtual void SetExpanded(bool expanded);
        protected void ToggleExpandAll(bool expand);
        protected internal void ToggleExpandedAllCore(bool expand);
        protected virtual void UninitializeController(TreeListDataControllerBase oldController);
        internal void UpdateId();

        protected internal ITreeListNodeCollection NodesCore { get; }

        public object Tag { get; set; }

        protected internal virtual TreeListNodeBase ParentNodeCore { get; set; }

        protected internal virtual TreeListNodeBase VisibleParentCore { get; }

        public object Content { get; set; }

        public bool HasChildren { get; }

        public virtual bool IsExpanded { get; set; }

        public int RowHandle { get; }

        public int VisibleIndex { get; }

        public bool IsFiltered { get; }

        public bool IsVisible { get; internal set; }

        public int Id { get; protected internal set; }

        protected internal bool IsExpandedSetInternally { get; internal set; }

        protected internal TreeListDataControllerBase Controller { get; set; }

        protected internal virtual bool IsTogglableCore { get; }
    }
}

