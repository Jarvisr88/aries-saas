namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class StringHierarchyFilterTree : SimpleFilterTreeBase
    {
        public StringHierarchyFilterTree(FilterTreeClient client) : base(client, FilterTreeKind.StringHierarchy)
        {
        }

        private void CreateAndAddStringNode(GroupNode<NodeValueInfo> group, FilterValueInfo filterValue, Func<string> getDisplayText)
        {
            ValueNode<NodeValueInfo> node = base.CreateAndAddRegualValueNode(group, filterValue.Value, getDisplayText, base.EditSettingsLazy);
            node.SetCount(filterValue.Count);
            node.IsChecked = new bool?(base.IsRegularValueChecked(filterValue.Value));
        }

        protected override Action<GroupNode<NodeValueInfo>, FilterValueInfo> GetRegularValueProcessor(CriteriaOperator filter)
        {
            string currentString = null;
            GroupNode<NodeValueInfo> currentGroupNode = null;
            return delegate (GroupNode<NodeValueInfo> root, FilterValueInfo filterValue) {
                string arg = (string) filterValue.Value;
                Func<string, string> func = <>c.<>9__3_1 ??= x => x.Substring(0, 1);
                string newGroupKey = func(arg);
                if (string.IsNullOrEmpty(currentString) || (newGroupKey != func(currentString)))
                {
                    currentGroupNode = this.CreateAndAddGroupNode(root, newGroupKey, () => newGroupKey);
                }
                if (currentString != arg)
                {
                    currentString = arg;
                    this.CreateAndAddStringNode(currentGroupNode, filterValue, filterValue.GetDisplayText);
                }
            };
        }

        internal override bool HasGroupedNodes =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StringHierarchyFilterTree.<>c <>9 = new StringHierarchyFilterTree.<>c();
            public static Func<string, string> <>9__3_1;

            internal string <GetRegularValueProcessor>b__3_1(string x) => 
                x.Substring(0, 1);
        }
    }
}

