namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal class FilterEditorNodeModelFactory
    {
        public FilterEditorNodeModelFactory(FilterEditorNodeModelFactoryOptions options, NodeTreeListener listener)
        {
            this.<Options>k__BackingField = options;
            this.<Listener>k__BackingField = listener;
        }

        private CustomExpressionNodeClient CreateCustomNodeClient(Func<GroupNodeModel> rootAccessor) => 
            new CustomExpressionNodeClient(() => this.Context.GetColumnForest(ColumnForestFilterMode.FilterEditorVisibleOnly).FlattenLeaves<VisualFilteringColumn, HeaderAppearanceAccessor>(), propertyName => this.GetColumn(propertyName).Type, delegate (NodeModelBase node) {
                this.RemoveNode(node, rootAccessor);
            }, x => this.Options.CanRemove(x, null));

        private NodeModelFactory CreateFactory(Action onNodesChanged, NodeClient nodeClient, CustomExpressionNodeClient customNodeClient) => 
            delegate (Lazy<NodeModelFactory> factoryAccessor) {
                Func<CriteriaOperator, CustomExpressionNodeModel> <>9__3;
                Func<CriteriaOperator, LeafNodeModel> <>9__1;
                GroupNodeModelClient groupClient = this.CreateGroupNodeClient(onNodesChanged, nodeClient, factoryAccessor);
                Func<CriteriaOperator, LeafNodeModel> createLeaf = <>9__1;
                if (<>9__1 == null)
                {
                    Func<CriteriaOperator, LeafNodeModel> local1 = <>9__1;
                    createLeaf = <>9__1 = filter => LeafNodeModel.Create(nodeClient, filter);
                }
                Func<CriteriaOperator, CustomExpressionNodeModel> createCustom = <>9__3;
                if (<>9__3 == null)
                {
                    Func<CriteriaOperator, CustomExpressionNodeModel> local2 = <>9__3;
                    createCustom = <>9__3 = filter => CustomExpressionNodeModel.Create(customNodeClient, filter);
                }
                return new NodeModelFactory(createLeaf, (filter, operandModels, isRoot) => isRoot ? GroupNodeModel.CreateRoot(groupClient, filter, operandModels) : GroupNodeModel.CreateSubGroup(groupClient, filter, operandModels), createCustom);
            }.WithReturnValue<NodeModelFactory>();

        private FilterModelClient CreateFilterModelClient(string propertyName, Func<NodeModelBase> nodeAccessor, Action onNodesChanged, Func<GroupNodeModel> rootAccessor)
        {
            Func<string, CriteriaOperator, CountsIncludeMode, string, Task<UniqueValues>> getGroupUniqueValues = <>c.<>9__14_1;
            if (<>c.<>9__14_1 == null)
            {
                Func<string, CriteriaOperator, CountsIncludeMode, string, Task<UniqueValues>> local1 = <>c.<>9__14_1;
                getGroupUniqueValues = <>c.<>9__14_1 = delegate (string valuesPropertyName, CriteriaOperator filter, CountsIncludeMode countsIncludeMode, string filterPropertyName) {
                    throw new InvalidOperationException();
                };
            }
            return new FilterModelClient(propertyName, new Func<string, FilteringColumn>(this.GetColumn), countsIncludeMode => this.Context.GetUniqueValues(propertyName, FilterEditorUniqueValuesHelper.BuildUniqueValuesFilter(rootAccessor(), nodeAccessor()), countsIncludeMode == CountsIncludeMode.Include, FilterValuesThrottleMode.Forbid), getGroupUniqueValues, null, delegate (Lazy<CriteriaOperator> _) {
                onNodesChanged();
            }, new Func<CriteriaOperator, CriteriaOperator>(this.Context.SubstituteWholeFilter), null, () => this.Context.GetFormatConditionFilters(propertyName));
        }

        private GroupNodeModelClient CreateGroupNodeClient(Action onNodesChanged, NodeClient nodeClient, Lazy<NodeModelFactory> factoryAccessor) => 
            new GroupNodeModelClient(factoryAccessor, onNodesChanged, this.Listener.OnNodeAdded, this.Options.GetAllowedGroupFilters, nodeClient.RemoveNode, x => this.Options.CanRemove(x, null), this.Options.GetChildMenuOptions);

        private NodeClient CreateNodeClient(Func<GroupNodeModel> rootAccessor, Action onNodesChanged) => 
            new NodeClient((propertyName, nodeAccessor) => this.CreateFilterModelClient(propertyName, nodeAccessor, onNodesChanged, rootAccessor), propertyName => this.GetColumn(propertyName).Type, this.Options.PropertyName, new Func<CriteriaOperator, NodeClientColumnsInfo>(this.GetColumns), this.Options.GetParameters, this.Options.GetAllowedOperandTypes, this.Options.ShowCounts, new OperandListObserver<NodeModelBase>(this.Listener.OnOperandAdding, this.Listener.OnOperandRemoving), delegate (NodeModelBase node) {
                this.RemoveNode(node, rootAccessor);
            }, (items, filter, propertyName) => this.Options.SubstituteOperatorMenuItems(items, filter, propertyName), this.Options.SelectTemplate, this.Options.CanRemove);

        private Action CreateNodesChangedCallback(Func<GroupNodeModel> rootAccessor) => 
            delegate {
                GroupNodeModel singleElement = rootAccessor();
                if (singleElement != null)
                {
                    Func<NodeModelBase, IEnumerable<NodeModelBase>> getItems = <>c.<>9__10_1;
                    if (<>c.<>9__10_1 == null)
                    {
                        Func<NodeModelBase, IEnumerable<NodeModelBase>> local1 = <>c.<>9__10_1;
                        getItems = <>c.<>9__10_1 = x => x.GetChildren();
                    }
                    foreach (GroupNodeModel model2 in singleElement.Yield<NodeModelBase>().Flatten<NodeModelBase>(getItems).OfType<GroupNodeModel>())
                    {
                        model2.Update();
                    }
                    this.Listener.OnTreeChanged();
                }
            };

        public NodeModelFactory Factory(Func<GroupNodeModel> rootAccessor)
        {
            Action onNodesChanged = this.CreateNodesChangedCallback(rootAccessor);
            return this.CreateFactory(onNodesChanged, this.CreateNodeClient(rootAccessor, onNodesChanged), this.CreateCustomNodeClient(rootAccessor));
        }

        private FilteringColumn GetColumn(string propertyName)
        {
            FilteringColumn column = this.Context.GetColumn(propertyName);
            if (column != null)
            {
                return column;
            }
            Type type1 = typeof(string);
            if (<>c.<>9__15_0 == null)
            {
                Type local2 = typeof(string);
                type1 = (Type) (<>c.<>9__15_0 = (Func<BaseEditSettings>) (() => null));
            }
            Func<object, string> func1 = <>c.<>9__15_2 ??= ((Func<object, string>) (_ => null));
            if (<>c.<>9__15_3 == null)
            {
                Func<object, string> local5 = <>c.<>9__15_2 ??= ((Func<object, string>) (_ => null));
                func1 = (Func<object, string>) (<>c.<>9__15_3 = (Func<string>) (() => null));
            }
            return new FilteringColumn(propertyName ?? string.Empty, (Type) <>c.<>9__15_0, ColumnFilterMode.Value, false, (Func<BaseEditSettings>) type1, <>c.<>9__15_1 ??= () => false, (Func<object, string>) new Func<FilterRestrictions>(FilterRestrictions.All), (Func<FilterRestrictions>) <>c.<>9__15_3, (Func<string>) func1, <>c.<>9__15_4 ??= (_, __, ___, ____) => UniqueValues.Empty, <>c.<>9__15_5 ??= () => false, <>c.<>9__15_6 ??= ((Func<SummaryFilterInfo[], object[]>) (_ => null)), <>c.<>9__15_7 ??= ((Func<PredefinedFilterCollection>) (() => null)));
        }

        private NodeClientColumnsInfo GetColumns(CriteriaOperator filter)
        {
            IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>> enumerable1 = ((this.Options.PropertySelectorMode == PropertySelectorMode.List) ? this.Context.GetColumnForest(ColumnForestFilterMode.FilterEditorVisibleOnly).FlattenLeavesContainers<VisualFilteringColumn, HeaderAppearanceAccessor>() : this.Context.GetColumnForest(ColumnForestFilterMode.FilterEditorVisibleOnly)).HierarchicalWhere<VisualFilteringColumn, HeaderAppearanceAccessor>(<>c.<>9__13_0 ??= column => !string.IsNullOrEmpty(column.Name), <>c.<>9__13_1 ??= (_, children) => children.Any<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>());
            if (<>c.<>9__13_2 == null)
            {
                IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>> local3 = ((this.Options.PropertySelectorMode == PropertySelectorMode.List) ? this.Context.GetColumnForest(ColumnForestFilterMode.FilterEditorVisibleOnly).FlattenLeavesContainers<VisualFilteringColumn, HeaderAppearanceAccessor>() : this.Context.GetColumnForest(ColumnForestFilterMode.FilterEditorVisibleOnly)).HierarchicalWhere<VisualFilteringColumn, HeaderAppearanceAccessor>(<>c.<>9__13_0 ??= column => !string.IsNullOrEmpty(column.Name), <>c.<>9__13_1 ??= (_, children) => children.Any<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>());
                enumerable1 = (IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>) (<>c.<>9__13_2 = delegate (VisualFilteringColumn column) {
                    HeaderAppearance appearance = column.GetHeaderAppearance();
                    return FieldItem.CreateLeaf(appearance?.Content, appearance?.Selector, column.Name);
                });
            }
            FieldItem[] itemArray = ((IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>) <>c.<>9__13_2).Transform<VisualFilteringColumn, HeaderAppearanceAccessor, FieldItem>(((Func<VisualFilteringColumn, FieldItem>) enumerable1), (<>c.<>9__13_3 ??= delegate (HeaderAppearanceAccessor getHeaderAppearance, IEnumerable<FieldItem> children) {
                HeaderAppearance appearance = getHeaderAppearance();
                return FieldItem.CreateGroup(appearance?.Content, appearance?.Selector, children.ToArray<FieldItem>());
            })).ToArray<FieldItem>();
            return this.Options.GetColumns(itemArray, filter);
        }

        private void RemoveNode(NodeModelBase node, Func<GroupNodeModel> rootAccessor)
        {
            GroupNodeModel objB = rootAccessor();
            if ((node != null) && !ReferenceEquals(node, objB))
            {
                this.Listener.OnRemovingNode(node);
                GroupNodeModel parent = node.GetParent(objB);
                if (parent != null)
                {
                    parent.RemoveChild(node);
                }
            }
        }

        private FilterEditorNodeModelFactoryOptions Options { get; }

        private NodeTreeListener Listener { get; }

        private FilteringUIContext Context =>
            this.Options.Context;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterEditorNodeModelFactory.<>c <>9 = new FilterEditorNodeModelFactory.<>c();
            public static Func<NodeModelBase, IEnumerable<NodeModelBase>> <>9__10_1;
            public static Func<VisualFilteringColumn, bool> <>9__13_0;
            public static Func<HeaderAppearanceAccessor, IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>, bool> <>9__13_1;
            public static Func<VisualFilteringColumn, FieldItem> <>9__13_2;
            public static Func<HeaderAppearanceAccessor, IEnumerable<FieldItem>, FieldItem> <>9__13_3;
            public static Func<string, CriteriaOperator, CountsIncludeMode, string, Task<UniqueValues>> <>9__14_1;
            public static Func<BaseEditSettings> <>9__15_0;
            public static Func<bool> <>9__15_1;
            public static Func<object, string> <>9__15_2;
            public static Func<string> <>9__15_3;
            public static Func<CriteriaOperator, bool, CountsIncludeMode, FilterValuesThrottleMode, Task<UniqueValues>> <>9__15_4;
            public static Func<bool> <>9__15_5;
            public static Func<SummaryFilterInfo[], object[]> <>9__15_6;
            public static Func<PredefinedFilterCollection> <>9__15_7;

            internal Task<UniqueValues> <CreateFilterModelClient>b__14_1(string valuesPropertyName, CriteriaOperator filter, CountsIncludeMode countsIncludeMode, string filterPropertyName)
            {
                throw new InvalidOperationException();
            }

            internal IEnumerable<NodeModelBase> <CreateNodesChangedCallback>b__10_1(NodeModelBase x) => 
                x.GetChildren();

            internal BaseEditSettings <GetColumn>b__15_0() => 
                null;

            internal bool <GetColumn>b__15_1() => 
                false;

            internal string <GetColumn>b__15_2(object _) => 
                null;

            internal string <GetColumn>b__15_3() => 
                null;

            internal Task<UniqueValues> <GetColumn>b__15_4(CriteriaOperator _, bool __, CountsIncludeMode ___, FilterValuesThrottleMode ____) => 
                UniqueValues.Empty;

            internal bool <GetColumn>b__15_5() => 
                false;

            internal object[] <GetColumn>b__15_6(SummaryFilterInfo[] _) => 
                null;

            internal PredefinedFilterCollection <GetColumn>b__15_7() => 
                null;

            internal bool <GetColumns>b__13_0(VisualFilteringColumn column) => 
                !string.IsNullOrEmpty(column.Name);

            internal bool <GetColumns>b__13_1(HeaderAppearanceAccessor _, IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>> children) => 
                children.Any<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>();

            internal FieldItem <GetColumns>b__13_2(VisualFilteringColumn column)
            {
                HeaderAppearance appearance = column.GetHeaderAppearance();
                return FieldItem.CreateLeaf(appearance?.Content, appearance?.Selector, column.Name);
            }

            internal FieldItem <GetColumns>b__13_3(HeaderAppearanceAccessor getHeaderAppearance, IEnumerable<FieldItem> children)
            {
                HeaderAppearance appearance = getHeaderAppearance();
                return FieldItem.CreateGroup(appearance?.Content, appearance?.Selector, children.ToArray<FieldItem>());
            }
        }
    }
}

