namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class GroupNodeModelClient
    {
        private readonly Lazy<DevExpress.Xpf.Core.FilteringUI.NodeModelFactory> lazyFactory;

        public GroupNodeModelClient(Lazy<DevExpress.Xpf.Core.FilteringUI.NodeModelFactory> nodeModelFactory, Action nodesChangedCallback, Action<NodeModelBase> nodeAddedCallback, Func<Lazy<CriteriaOperator>, DevExpress.Xpf.Core.FilteringUI.AllowedGroupFilters> allowedGroupFilters, Action<NodeModelBase> removeNode, Func<Lazy<CriteriaOperator>, bool> canExecuteRemoveAction, Func<Lazy<CriteriaOperator>, GroupNodeModelChildMenuOptions> getChildMenuOptions)
        {
            this.lazyFactory = nodeModelFactory;
            this.<NodesChangedCallback>k__BackingField = nodesChangedCallback;
            this.<NodeAddedCallback>k__BackingField = nodeAddedCallback;
            this.<AllowedGroupFilters>k__BackingField = allowedGroupFilters;
            this.<RemoveNode>k__BackingField = removeNode;
            this.<CanExecuteRemoveAction>k__BackingField = canExecuteRemoveAction;
            this.<GetChildMenuOptions>k__BackingField = getChildMenuOptions;
        }

        public DevExpress.Xpf.Core.FilteringUI.NodeModelFactory NodeModelFactory =>
            this.lazyFactory.Value;

        public Action NodesChangedCallback { get; }

        public Action<NodeModelBase> NodeAddedCallback { get; }

        public Func<Lazy<CriteriaOperator>, DevExpress.Xpf.Core.FilteringUI.AllowedGroupFilters> AllowedGroupFilters { get; }

        public Action<NodeModelBase> RemoveNode { get; }

        public Func<Lazy<CriteriaOperator>, bool> CanExecuteRemoveAction { get; }

        public Func<Lazy<CriteriaOperator>, GroupNodeModelChildMenuOptions> GetChildMenuOptions { get; }
    }
}

