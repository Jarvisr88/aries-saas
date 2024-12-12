namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class CustomExpressionNodeClient
    {
        public CustomExpressionNodeClient(Func<IEnumerable<VisualFilteringColumn>> getColumns, Func<string, Type> getColumnType, Action<NodeModelBase> removeNode, Func<Lazy<CriteriaOperator>, bool> canExecuteRemoveAction)
        {
            Guard.ArgumentNotNull(getColumns, "getColumns");
            Guard.ArgumentNotNull(removeNode, "removeNode");
            Guard.ArgumentNotNull(getColumnType, "getColumnType");
            Guard.ArgumentNotNull(canExecuteRemoveAction, "canExecuteRemoveAction");
            this.<GetColumns>k__BackingField = getColumns;
            this.<GetColumnType>k__BackingField = getColumnType;
            this.<RemoveNode>k__BackingField = removeNode;
            this.<CanExecuteRemoveAction>k__BackingField = canExecuteRemoveAction;
        }

        public Func<IEnumerable<VisualFilteringColumn>> GetColumns { get; }

        public Action<NodeModelBase> RemoveNode { get; }

        public Func<Lazy<CriteriaOperator>, bool> CanExecuteRemoveAction { get; }

        public Func<string, Type> GetColumnType { get; }
    }
}

