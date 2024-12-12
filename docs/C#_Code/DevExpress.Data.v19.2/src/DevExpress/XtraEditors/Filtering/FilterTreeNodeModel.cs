namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.XtraEditors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.CompilerServices;

    public abstract class FilterTreeNodeModel : IDisposable, IIBoundPropertyCreator
    {
        private FilterControlFocusInfo focusInfo;
        private FilterControlAllowAggregateEditing allowAggregateEditing;
        private bool showGroupCommandsIcon;
        private bool sortProperties;
        private bool showIsNullOperatorsForStrings;
        private int maxOperandsCount;
        private IBoundProperty defaultProperty;
        private GroupNode rootNode;
        private object sourceControl;
        private IFilteredComponent filterSourceControl;
        private bool showOperandTypeIcon;
        private int updaterCounter;
        private FilterTreeNodeModel.FilterCriteriaSubscribers filterCriteriaSubscribers;
        private FilterModelPickManager pickManager;
        private SourceControlNotifier notifier;
        private IBoundPropertyCollection filterProperties;
        private bool werePropertiesCreatedByModel;

        public event EventHandler<CreateCriteriaCustomParseEventArgs> CreateCriteriaCustomParse;

        public event EventHandler<CreateCriteriaParseContextEventArgs> CreateCriteriaParseContext;

        public event FilterTreeNodeModel.NotifyControlDelegate OnNotifyControl;

        protected FilterTreeNodeModel();
        public GroupNode AddGroup(GroupNode parent);
        public virtual void AddParameter(string parameterName, Type parameterType);
        public void ApplyFilter();
        public void BeginUpdate();
        public void CancelUpdate();
        public virtual AggregateNode CreateAggregateNode();
        public virtual ClauseNode CreateClauseNode();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual Node CreateCriteriaByDefaultProperty();
        protected virtual ClauseNode CreateDefaultClauseNode(IBoundProperty property);
        public virtual ClauseNode CreateDefaultClauseNode(IBoundPropertyCollection currentFilterProperties);
        protected virtual ClauseNode CreateDefaultClauseNode(IBoundProperty property, IBoundPropertyCollection currentFilterProperties);
        private void CreateFilterColumnCollection();
        protected virtual FilterModelPickManager CreateFilterModelPickManager();
        public virtual GroupNode CreateGroupNode();
        public GroupNode CreateGroupNode(GroupNode parent);
        protected abstract IBoundPropertyCollection CreateIBoundPropertyCollection();
        protected virtual Node CreateNodeFromCriteria(CriteriaOperator criteria);
        public Node CreateNodeFromCriteria(CriteriaOperator criteria, List<CriteriaOperator> skippedCriteriaOperator);
        protected INodesFactoryEx CreateNodesFactory();
        public abstract IBoundProperty CreateProperty(object dataSource, string dataMember, string displayName, bool isList, PropertyDescriptor property);
        public void CreateTree(CriteriaOperator criteria);
        protected virtual CriteriaOperator CriteriaFromString(string value);
        public CriteriaOperator CriteriaParse(string value);
        public string CriteriaSerialize(CriteriaOperator criteria);
        protected virtual string CriteriaToString(CriteriaOperator criteria);
        public void Dispose();
        public bool DoAddElement();
        public virtual bool DoesAllowItemCollectionEditor(IBoundProperty property);
        public bool DoPasteElement(string clipboardText);
        public bool DoSwapPropertyValue();
        public void EndUpdate();
        public void EndUpdate(FilterChangedAction action);
        public void EndUpdate(FilterChangedAction action, Node node);
        private void FilterTreeNodeModel_Initialized(object sender, EventArgs e);
        protected abstract FilterColumnClauseClass GetClauseClass(IBoundProperty property);
        public virtual ClauseType GetDefaultOperation(IBoundPropertyCollection properties, OperandProperty operandProperty);
        public IBoundProperty GetDefaultProperty();
        public virtual Type GetFunctionType(string name);
        public abstract string GetLocalizedStringForFilterClauseBetweenAnd();
        public abstract string GetLocalizedStringForFilterEmptyEnter();
        public abstract string GetLocalizedStringForFilterEmptyParameter();
        public abstract string GetLocalizedStringForFilterEmptyValue();
        public virtual string GetMenuStringByFunctionType(object functionType);
        public abstract string GetMenuStringByType(Aggregate type);
        public abstract string GetMenuStringByType(ClauseType type);
        public abstract string GetMenuStringByType(GroupType type);
        protected virtual IList<IFilterParameter> GetParameters();
        public List<IFilterParameter> GetParametersByType(Type type);
        public List<ITreeSelectableItem> GetTreeItemsByProperties();
        public List<ITreeSelectableItem> GetTreeItemsByProperties(IEnumerable properties);
        public virtual bool IsValidClause(ClauseType clause, FilterColumnClauseClass clauseClass);
        public bool IsValidClause(IBoundProperty property, ClauseType clause);
        public virtual bool IsValidFunction(FunctionOperatorType functionType, FilterColumnClauseClass clauseClass);
        public virtual bool IsValidFunction(ICustomFunctionOperator function, Type propertyType);
        public bool IsValidFunction(IBoundProperty property, FunctionOperatorType functionType);
        public bool IsValidFunction(IBoundProperty property, ICustomFunctionOperator function);
        public void ModelChanged(FilterChangedEventArgs info);
        private void NotifyControl(FilterChangedEventArgs info);
        protected virtual void OnFocusInfoChanged();
        private void OnSourceControlChanged();
        private void OnSourceControlPropertiesChanged();
        public abstract void OnVisualChange(FilterChangedActionInternal action, Node node);
        public void RebuildElements();
        private void RebuildElements(Node node);
        protected void RecursiveVisitor(Node node, Action<Node> action);
        private static void RemoveInterfaceProperties(List<IBoundProperty> list, Type interfaceType);
        public void SetDefaultProperty(IBoundProperty property);
        protected abstract void SetFilterColumnsCollection(IBoundPropertyCollection propertyCollection);
        public abstract void SetParent(IBoundProperty property, IBoundProperty parent);
        private void SourceControl_DataSourceChanged(object sender, EventArgs e);
        public virtual CriteriaOperator ToCriteria(INode node);
        private void UpdateFilterSourceControl(bool resetCriteria);
        protected virtual void ValidateAdditionalOperands(IClauseNode node);

        public FilterModelPickManager PickManager { get; }

        public IBoundPropertyCollection FilterProperties { get; set; }

        public bool IsUpdating { get; }

        public bool ShowIsNullOperatorsForStrings { get; set; }

        public virtual bool ShowOperandTypeIcon { get; set; }

        public virtual IFilterParametersOwner ParametersOwner { get; }

        public object SourceControl { get; set; }

        public CriteriaOperator FilterCriteria { get; set; }

        private IFilteredComponent FilterSourceControl { get; set; }

        public GroupNode RootNode { get; set; }

        public FilterControlFocusInfo FocusInfo { get; set; }

        public FilterControlAllowAggregateEditing AllowAggregateEditing { get; set; }

        public bool SortProperties { get; set; }

        public bool ShowGroupCommandsIcon { get; set; }

        public int MaxOperandsCount { get; set; }

        public string FilterString { get; set; }

        public bool AllowCreateDefaultClause { get; set; }

        protected virtual bool SupportCustomFunctions { get; }

        public bool ShowParameterTypeIcon { get; }

        public virtual bool CanAddParameters { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterTreeNodeModel.<>c <>9;
            public static Comparison<ITreeSelectableItem> <>9__33_0;

            static <>c();
            internal int <GetTreeItemsByProperties>b__33_0(ITreeSelectableItem x, ITreeSelectableItem y);
        }

        private class FilterCriteriaSubscribers
        {
            private IFilteredComponentBase filteredComponentBase;
            private IFilteredDataSource filteredDataSource;
            private IBindingListView bindingListView;
            private DataView dataView;
            private DataTable dataTable;
            private Action<CriteriaOperator> onCriteriaChanged;

            public FilterCriteriaSubscribers(Action<CriteriaOperator> onCriteriaChanged);
            public void Clear();
            private void filteredComponentBase_RowFilterChanged(object sender, EventArgs e);
            public List<Type> GetKnownInterfaces(object sourceControl);
            public void PropagateToSourceControl(CriteriaOperator criteria);
            public void Set(object sourceControl, bool resetCriteria);
        }

        public delegate void NotifyControlDelegate(FilterChangedEventArgs info);
    }
}

