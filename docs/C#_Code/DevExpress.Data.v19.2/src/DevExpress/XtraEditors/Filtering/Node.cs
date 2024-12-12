namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.XtraEditors;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class Node : INode
    {
        private static IList<INode> nullableChildren;
        private FilterTreeNodeModel model;
        private Node parentNode;
        private List<NodeEditableElement> elements;
        private IBoundPropertyCollection filterProperties;

        static Node();
        protected Node(FilterTreeNodeModel model);
        protected abstract object Accept(INodeVisitor visitor);
        protected NodeEditableElement AddEditableElement(ElementType elementType, string text);
        public abstract void AddElement();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ChangeElement(ElementType elementType);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ChangeElement(ElementType elementType, object value);
        protected virtual void ChangeElement(NodeEditableElement element, object value);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ChangeElement(int elementIndex, object value);
        private void ClearFilterProperties();
        private void CollectFlatNodeList(List<Node> list);
        internal Node.ObservableList<INode> CreateObserveblaListForChildNodes();
        public virtual void DeleteElement();
        object INode.Accept(INodeVisitor visitor);
        void INode.SetParentNode(IGroupNode node);
        public List<Node> GetAbsoluteList();
        public virtual void GetAbsoluteList(List<Node> list);
        public virtual CriteriaOperator GetAdditionalOperand(int elementIndex);
        public virtual IList<INode> GetChildren();
        protected virtual IBoundPropertyCollection GetChildrenFilterProperties();
        public virtual object GetCurrentValue(int elementIndex);
        protected NodeEditableElement GetElement(ElementType elementType);
        protected NodeEditableElement GetElement(int elementIndex);
        private List<Node> GetFlatNodeList();
        public virtual IBoundProperty GetFocusedFilterProperty(int elementIndex);
        public Node GetNextNode();
        public Node GetPrevNode();
        protected void NodeChanged(FilterChangedAction action);
        protected virtual void NodeChanged(FilterChangedAction action, Node node);
        public virtual void RebuildElements();
        [Obsolete("Obsolete method. No need to update label infos manually."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void RecalcLabelInfo();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetFilterProperties();
        protected internal void SetParentNode(INode node);
        [Obsolete("Use FilterControl.FilterCriteria or FilterControl.ToCriteria instead"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public CriteriaOperator ToCriteria();

        public int Index { get; }

        public Node RootNode { get; }

        public int Level { get; }

        protected FilterControlFocusInfo FocusInfo { get; set; }

        public List<NodeEditableElement> Elements { get; }

        public virtual int LastTabElementIndex { get; }

        public abstract string Text { get; }

        public FilterTreeNodeModel Model { get; }

        public IBoundPropertyCollection FilterProperties { get; }

        public Node ParentNode { get; set; }

        IGroupNode INode.ParentNode { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Node.<>c <>9;
            public static Node.ObservableList<INode>.ObservableListDelegate <>9__5_2;

            static <>c();
            internal void <CreateObserveblaListForChildNodes>b__5_2(int index, INode item);
        }

        private protected class ObservableList<T> : Collection<T>
        {
            private Node.ObservableList<T>.ObservableListDelegate onInsert;
            private Node.ObservableList<T>.ObservableListDelegate onRemove;
            private Node.ObservableList<T>.ObservableListDelegate onSet;

            public ObservableList(Node.ObservableList<T>.ObservableListDelegate onInsert, Node.ObservableList<T>.ObservableListDelegate onRemove, Node.ObservableList<T>.ObservableListDelegate onSet);
            protected override void ClearItems();
            protected override void InsertItem(int index, T item);
            protected override void RemoveItem(int index);
            protected override void SetItem(int index, T item);

            public delegate void ObservableListDelegate(int index, T item);
        }
    }
}

