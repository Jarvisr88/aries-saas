namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal abstract class LoadedFilterTreeBase : FilterTreeBase
    {
        private readonly DevExpress.Xpf.Core.FilteringUI.BlankOperations BlankOperations;
        private readonly Type PropertyType;
        protected readonly Lazy<BaseEditSettings> EditSettingsLazy;
        private ValueNode<NodeValueInfo> blanksNode;
        private Func<object, bool> isRegualValueCheckedFunc;

        public LoadedFilterTreeBase(FilterTreeClient filterTreeClient, FilterTreeKind kind) : base(filterTreeClient, kind)
        {
            FilteringColumn column = base.client.GetColumn(base.PropertyName);
            this.BlankOperations = base.client.GetBlankOperations(base.PropertyName);
            this.PropertyType = column.Type;
            this.EditSettingsLazy = filterTreeClient.RootEditSettingsLazy;
        }

        protected sealed override CriteriaOperator BuildFilterCore() => 
            this.BuildRegularValuesFilter(base.RootNode) | FilterTreeHelper.BuildCustomValuesFilter(base.RootNode, base.PropertyName, x => !this.IsRegularValue(x));

        protected abstract CriteriaOperator BuildRegularValuesFilter(GroupNode<NodeValueInfo> root);
        protected override void BuildTree(GroupNode<NodeValueInfo> root, IList<FilterValueInfo> filterValues, CriteriaOperator filter, Func<CriteriaOperator, CriteriaOperator> substituteFilter)
        {
            bool? isChecked;
            List<CriteriaOperator> customItemsFilters = FilterTreeHelper.SplitCriteriaOperator(filter);
            this.isRegualValueCheckedFunc = FilterTreeHelper.CreateContainsValueFunc(base.PropertyName, this.PropertyType, filter, substituteFilter);
            Action<GroupNode<NodeValueInfo>, FilterValueInfo> regularValueProcessor = this.GetRegularValueProcessor(filter);
            foreach (FilterValueInfo info in filterValues)
            {
                if (!this.BlankOperations.IsBlankValue(info.Value))
                {
                    if (!this.BlankOperations.IsEmptyValue(info.Value))
                    {
                        if (this.IsRegularValue(info.Value))
                        {
                            regularValueProcessor(root, info);
                            continue;
                        }
                        this.ProcessCustomValue(customItemsFilters, root, info);
                        continue;
                    }
                    Func<string> getDisplayText = <>c.<>9__6_1;
                    if (<>c.<>9__6_1 == null)
                    {
                        Func<string> local2 = <>c.<>9__6_1;
                        getDisplayText = <>c.<>9__6_1 = () => FilterTreeBase.EmptyStringText;
                    }
                    ValueNode<NodeValueInfo> node = this.CreateAndAddCustomValueNode(root, info.Value, getDisplayText);
                    node.IsChecked = new bool?(this.IsRegularValueChecked(info.Value));
                    node.SetCount(info.Count);
                    continue;
                }
                if (this.blanksNode == null)
                {
                    if (this.CreateIsNullOperator() == null)
                    {
                        continue;
                    }
                    Func<string> getDisplayText = <>c.<>9__6_0;
                    if (<>c.<>9__6_0 == null)
                    {
                        Func<string> local1 = <>c.<>9__6_0;
                        getDisplayText = <>c.<>9__6_0 = () => FilterTreeBase.SelectBlanksText;
                    }
                    this.blanksNode = this.CreateAndAddCustomValueNode(root, null, getDisplayText);
                }
                if (this.blanksNode != null)
                {
                    int? nullable1;
                    int? count = this.blanksNode.Count;
                    int? nullable2 = info.Count;
                    if ((count != null) & (nullable2 != null))
                    {
                        nullable1 = new int?(count.GetValueOrDefault() + nullable2.GetValueOrDefault());
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    this.blanksNode.SetCount(nullable1);
                    isChecked = this.blanksNode.IsChecked;
                    this.blanksNode.IsChecked = new bool?(((isChecked != null) ? isChecked.GetValueOrDefault() : false) || this.IsRegularValueChecked(info.Value));
                }
            }
            this.ValidateNodes();
            foreach (NodeBase<NodeValueInfo> base2 in CheckedTreeHelper.GetDepthFirstNodes<NodeValueInfo>(base.RootNode))
            {
                isChecked = base2.IsChecked;
                base2.IsExpanded = isChecked == null;
            }
        }

        private bool CalcIsVisible(NodeBase<NodeValueInfo> node)
        {
            if (!this.IsCustomNode(node))
            {
                if (base.client.GetIsNodeVisible != null)
                {
                    return base.client.GetIsNodeVisible(node);
                }
                Func<NodeBase<NodeValueInfo>, bool> getIsNodeVisible = base.client.GetIsNodeVisible;
            }
            return true;
        }

        protected CriteriaOperator CreateIsNullOperator() => 
            this.BlankOperations.CreateIsNullOperator(base.PropertyName, base.FilterRestrictions.AllowedUnaryFilters);

        protected abstract Action<GroupNode<NodeValueInfo>, FilterValueInfo> GetRegularValueProcessor(CriteriaOperator filter);
        private static bool IsCustomFilterItemChecked(List<CriteriaOperator> customItemsFilters, CriteriaOperator customOperator) => 
            (customItemsFilters.Count != 0) ? FilterTreeHelper.SplitCriteriaOperator(customOperator).All<CriteriaOperator>(x => customItemsFilters.Contains(x)) : false;

        private bool IsCustomItemChecked(List<CriteriaOperator> customItemsFilters, object value)
        {
            if (value is CriteriaOperator)
            {
                return IsCustomFilterItemChecked(customItemsFilters, (CriteriaOperator) value);
            }
            if (!(value is ICustomItem))
            {
                return false;
            }
            ICustomItem item = (ICustomItem) value;
            return (!(item.EditValue is CriteriaOperator) ? (this.IsRegularValue(item.EditValue) && this.IsRegularValueChecked(item.EditValue)) : IsCustomFilterItemChecked(customItemsFilters, (CriteriaOperator) item.EditValue));
        }

        private bool IsCustomNode(NodeBase<NodeValueInfo> node) => 
            !this.IsRegularValue(node.Value.Value);

        protected virtual bool IsRegularValue(object value) => 
            HierarchyFilterBuilderHelper.IsBlankElement(value) || this.PropertyType.IsAssignableFrom(value.GetType());

        protected bool IsRegularValueChecked(object value) => 
            this.isRegualValueCheckedFunc(value);

        private void ProcessCustomValue(List<CriteriaOperator> customItemsFilters, GroupNode<NodeValueInfo> root, FilterValueInfo filterValue)
        {
            ValueNode<NodeValueInfo> node;
            if (filterValue.Value is CriteriaOperator)
            {
                CriteriaOperator op = (CriteriaOperator) filterValue.Value;
                node = base.CreateAndAddCustomValueNode(root, op, () => op.ToString());
            }
            else if (!(filterValue.Value is ICustomItem))
            {
                node = base.CreateAndAddCustomValueNode(root, filterValue.Value, () => filterValue.Value.ToString());
            }
            else
            {
                ICustomItem customItem = (ICustomItem) filterValue.Value;
                node = base.CreateAndAddCustomValueNode(root, customItem.EditValue, delegate {
                    object displayValue = customItem.DisplayValue;
                    if (displayValue != null)
                    {
                        return displayValue.ToString();
                    }
                    object local1 = displayValue;
                    return null;
                });
            }
            int? count = null;
            node.SetCount(count);
            node.IsChecked = new bool?(this.IsCustomItemChecked(customItemsFilters, filterValue.Value));
        }

        public void ValidateNodes()
        {
            this.ValidateNodesCore(base.RootNode, false);
        }

        private void ValidateNodesCore(NodeBase<NodeValueInfo> node, bool isParentVisible)
        {
            foreach (NodeBase<NodeValueInfo> base2 in node.GetChildren())
            {
                this.ValidateNodesCore(base2, isParentVisible || this.CalcIsVisible(base2));
            }
            node.IsVisible = (ReferenceEquals(node, base.RootNode) | isParentVisible) || node.HasVisibleChildren();
            node.RaiseCountChangedIfNeeded();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LoadedFilterTreeBase.<>c <>9 = new LoadedFilterTreeBase.<>c();
            public static Func<string> <>9__6_0;
            public static Func<string> <>9__6_1;

            internal string <BuildTree>b__6_0() => 
                FilterTreeBase.SelectBlanksText;

            internal string <BuildTree>b__6_1() => 
                FilterTreeBase.EmptyStringText;
        }
    }
}

