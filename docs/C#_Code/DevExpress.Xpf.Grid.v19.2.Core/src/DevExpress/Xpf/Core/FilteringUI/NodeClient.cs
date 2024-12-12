namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal class NodeClient
    {
        public NodeClient(Func<string, Func<NodeModelBase>, FilterModelClient> createFilterModelClient, Func<string, Type> getColumnType, string defaultColumnName, Func<CriteriaOperator, NodeClientColumnsInfo> getColumns, Func<CriteriaOperator, IReadOnlyCollection<string>> getParameters, Func<CriteriaOperator, AllowedOperandTypes> getAllowedOperandTypes, bool? showCounts, OperandListObserver<NodeModelBase> operandListObserver, Action<NodeModelBase> removeNode, DevExpress.Xpf.Core.FilteringUI.SubstituteOperatorMenuItems substituteOperatorMenuItems, Func<FilterModelBase, DataTemplate> selectTemplate, Func<Lazy<CriteriaOperator>, string, bool> canExecuteRemoveAction)
        {
            Guard.ArgumentNotNull(createFilterModelClient, "createFilterModelClient");
            Guard.ArgumentNotNull(getColumnType, "getColumnType");
            Guard.ArgumentNotNull(getColumns, "getColumns");
            Guard.ArgumentNotNull(operandListObserver, "operandListObserver");
            Guard.ArgumentNotNull(removeNode, "removeNode");
            Guard.ArgumentNotNull(substituteOperatorMenuItems, "substituteOperatorMenuItems");
            Guard.ArgumentNotNull(selectTemplate, "selectTemplate");
            Guard.ArgumentNotNull(canExecuteRemoveAction, "canExecuteRemoveAction");
            this.<CreateFilterModelClient>k__BackingField = createFilterModelClient;
            this.<GetColumns>k__BackingField = getColumns;
            this.<GetParameters>k__BackingField = getParameters;
            this.<GetAllowedOperandTypes>k__BackingField = getAllowedOperandTypes;
            this.<GetColumnType>k__BackingField = getColumnType;
            this.<DefaultColumnName>k__BackingField = defaultColumnName;
            this.<ShowCounts>k__BackingField = showCounts;
            this.<OperandListObserver>k__BackingField = operandListObserver;
            this.<RemoveNode>k__BackingField = removeNode;
            this.<SubstituteOperatorMenuItems>k__BackingField = substituteOperatorMenuItems;
            this.<SelectTemplate>k__BackingField = selectTemplate;
            this.<CanExecuteRemoveAction>k__BackingField = canExecuteRemoveAction;
        }

        public Func<string, Func<NodeModelBase>, FilterModelClient> CreateFilterModelClient { get; }

        public Func<CriteriaOperator, NodeClientColumnsInfo> GetColumns { get; }

        public Func<CriteriaOperator, IReadOnlyCollection<string>> GetParameters { get; }

        public Func<CriteriaOperator, AllowedOperandTypes> GetAllowedOperandTypes { get; }

        public Func<string, Type> GetColumnType { get; }

        public string DefaultColumnName { get; }

        public bool? ShowCounts { get; }

        public OperandListObserver<NodeModelBase> OperandListObserver { get; }

        public Action<NodeModelBase> RemoveNode { get; }

        public DevExpress.Xpf.Core.FilteringUI.SubstituteOperatorMenuItems SubstituteOperatorMenuItems { get; }

        public Func<FilterModelBase, DataTemplate> SelectTemplate { get; }

        public Func<Lazy<CriteriaOperator>, string, bool> CanExecuteRemoveAction { get; }
    }
}

