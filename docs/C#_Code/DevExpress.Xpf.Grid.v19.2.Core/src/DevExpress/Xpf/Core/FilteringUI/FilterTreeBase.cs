namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal abstract class FilterTreeBase
    {
        protected readonly FilterTreeClient client;
        private readonly FilterTreeKind kind;

        public FilterTreeBase(FilterTreeClient filterTreeClient, FilterTreeKind kind)
        {
            this.client = filterTreeClient;
            this.kind = kind;
        }

        public void Build(IList<FilterValueInfo> filterValues, CriteriaOperator filter, Func<CriteriaOperator, CriteriaOperator> substituteFilter)
        {
            this.RootNode = this.CreateRootNode();
            IList<FilterValueInfo> list1 = filterValues;
            if (filterValues == null)
            {
                IList<FilterValueInfo> local1 = filterValues;
                list1 = new List<FilterValueInfo>();
            }
            this.BuildTree(this.RootNode, list1, filter, substituteFilter);
            this.RootNode.IsExpanded = true;
        }

        internal CriteriaOperator BuildFilter() => 
            ((this.RootNode?.ActualIsChecked != null) || !this.CanBuildFilter()) ? null : this.BuildFilterCore();

        protected abstract CriteriaOperator BuildFilterCore();
        protected abstract void BuildTree(GroupNode<NodeValueInfo> root, IList<FilterValueInfo> filterValues, CriteriaOperator filter, Func<CriteriaOperator, CriteriaOperator> substituteFilter);
        internal abstract bool CanBuildFilter();
        protected ValueNode<NodeValueInfo> CreateAndAddCustomValueNode(GroupNode<NodeValueInfo> parent, object value, Func<string> getDisplayText) => 
            this.CreateAndAddValueNode(parent, value, getDisplayText, DisplayMode.DisplayText, null);

        protected GroupNode<NodeValueInfo> CreateAndAddGroupNode(GroupNode<NodeValueInfo> parent, object value, Func<string> getDisplayText) => 
            new GroupNode<NodeValueInfo>(new NodeValueInfo(value, getDisplayText, DisplayMode.DisplayText, null), null, this.ApplyFilter, parent);

        protected ValueNode<NodeValueInfo> CreateAndAddRegualValueNode(GroupNode<NodeValueInfo> parent, object value, Func<string> getDisplayText, Lazy<BaseEditSettings> editSettingsLazy) => 
            this.CreateAndAddValueNode(parent, value, getDisplayText, DisplayMode.Value, editSettingsLazy);

        private ValueNode<NodeValueInfo> CreateAndAddValueNode(GroupNode<NodeValueInfo> parent, object value, Func<string> getDisplayText, DisplayMode displayMode, Lazy<BaseEditSettings> editSettingsLazy) => 
            new ValueNode<NodeValueInfo>(new NodeValueInfo(value, getDisplayText, displayMode, editSettingsLazy), null, this.ApplyFilter, parent?.AsNodeParent);

        private GroupNode<NodeValueInfo> CreateRootNode()
        {
            Func<string> getDisplayText = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<string> local1 = <>c.<>9__24_0;
                getDisplayText = <>c.<>9__24_0 = () => SelectAllText;
            }
            NodeValueInfo info = new NodeValueInfo(null, getDisplayText, DisplayMode.DisplayText, null);
            return new GroupNode<NodeValueInfo>(info, this.client.BeginUpdate, delegate {
                if (this.client.EndUpdate == null)
                {
                    Action endUpdate = this.client.EndUpdate;
                }
                else
                {
                    this.client.EndUpdate();
                }
                Action applyFilter = this.ApplyFilter;
                if (applyFilter == null)
                {
                    Action local2 = applyFilter;
                }
                else
                {
                    applyFilter();
                }
            }, null);
        }

        internal static string SelectAllText =>
            EditorLocalizer.GetString(EditorStringId.FilterElementAllItem);

        internal static string SelectBlanksText =>
            EditorLocalizer.GetString(EditorStringId.FilterElementBlanksItem);

        internal static string EmptyStringText =>
            EditorLocalizer.GetString(EditorStringId.FilterElementEmptyItem);

        protected Action ApplyFilter =>
            this.client.ApplyFilter;

        protected string PropertyName =>
            this.client.PropertyName;

        protected DevExpress.Xpf.Core.FilteringUI.FilterRestrictions FilterRestrictions =>
            this.client.FilterRestrictions;

        internal GroupNode<NodeValueInfo> RootNode { get; private set; }

        internal abstract bool HasGroupedNodes { get; }

        internal virtual bool FetchSublevelChildrenOnExpand =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterTreeBase.<>c <>9 = new FilterTreeBase.<>c();
            public static Func<string> <>9__24_0;

            internal string <CreateRootNode>b__24_0() => 
                FilterTreeBase.SelectAllText;
        }
    }
}

