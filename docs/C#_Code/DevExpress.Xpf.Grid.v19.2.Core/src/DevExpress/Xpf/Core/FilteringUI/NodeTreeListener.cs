namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class NodeTreeListener
    {
        public NodeTreeListener(Action onTreeChanged, Action<NodeModelBase> onOperandAdding, Action<NodeModelBase> onOperandRemoving, Action<NodeModelBase> onRemovingNode, Action<NodeModelBase> onNodeAdded)
        {
            this.<OnTreeChanged>k__BackingField = onTreeChanged;
            this.<OnOperandAdding>k__BackingField = onOperandAdding;
            this.<OnOperandRemoving>k__BackingField = onOperandRemoving;
            this.<OnRemovingNode>k__BackingField = onRemovingNode;
            this.<OnNodeAdded>k__BackingField = onNodeAdded;
        }

        public Action OnTreeChanged { get; }

        public Action<NodeModelBase> OnOperandAdding { get; }

        public Action<NodeModelBase> OnOperandRemoving { get; }

        public Action<NodeModelBase> OnRemovingNode { get; }

        public Action<NodeModelBase> OnNodeAdded { get; }
    }
}

