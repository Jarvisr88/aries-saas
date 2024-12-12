namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;

    internal class NodeParent<T>
    {
        public readonly Action<NodeBase<T>> OnChildIsVisibleChanged;
        public readonly Action<NodeBase<T>, int?> OnChildCountSummaryChanged;
        public readonly Action<NodeBase<T>> OnChildCreated;
        public readonly Action<NodeBase<T>, bool?> OnChildIsCheckedChanged;
        public readonly Action<NodeBase<T>, bool?> OnChildActualIsCheckedChanged;

        public NodeParent(Action<NodeBase<T>> onChildIsVisibleChanged, Action<NodeBase<T>, int?> onChildCountSummaryChanged, Action<NodeBase<T>> onChildCreated, Action<NodeBase<T>, bool?> onChildIsCheckedChanged, Action<NodeBase<T>, bool?> onChildActualIsCheckedChanged)
        {
            this.OnChildIsVisibleChanged = onChildIsVisibleChanged;
            this.OnChildCountSummaryChanged = onChildCountSummaryChanged;
            this.OnChildCreated = onChildCreated;
            this.OnChildIsCheckedChanged = onChildIsCheckedChanged;
            this.OnChildActualIsCheckedChanged = onChildActualIsCheckedChanged;
        }
    }
}

