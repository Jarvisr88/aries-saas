namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;

    internal sealed class LinearFilterTree : SimpleFilterTreeBase
    {
        private LinearFilterTree(FilterTreeClient client, FilterTreeKind kind) : base(client, kind)
        {
        }

        protected override Action<GroupNode<NodeValueInfo>, FilterValueInfo> GetRegularValueProcessor(CriteriaOperator filter) => 
            delegate (GroupNode<NodeValueInfo> root, FilterValueInfo filterValue) {
                ValueNode<NodeValueInfo> node = base.CreateAndAddRegualValueNode(root, filterValue.Value, filterValue.GetDisplayText, base.EditSettingsLazy);
                node.SetCount(filterValue.Count);
                node.IsChecked = new bool?(base.IsRegularValueChecked(filterValue.Value));
            };

        public static FilterTreeBase Linear(FilterTreeClient client) => 
            new LinearFilterTree(client, FilterTreeKind.Linear);

        public static FilterTreeBase StringLinear(FilterTreeClient client) => 
            new LinearFilterTree(client, FilterTreeKind.StringLinear);

        internal override bool HasGroupedNodes =>
            false;
    }
}

