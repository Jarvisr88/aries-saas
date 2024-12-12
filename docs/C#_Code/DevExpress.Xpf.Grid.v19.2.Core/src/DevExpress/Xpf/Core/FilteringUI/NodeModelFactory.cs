namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NodeModelFactory
    {
        public NodeModelFactory(Func<CriteriaOperator, LeafNodeModel> createLeaf, Func<CriteriaOperator, IList<NodeModelBase>, bool, GroupNodeModel> createGroup, Func<CriteriaOperator, CustomExpressionNodeModel> createCustom)
        {
            this.<CreateLeaf>k__BackingField = createLeaf;
            this.<CreateGroup>k__BackingField = createGroup;
            this.<CreateCustom>k__BackingField = createCustom;
        }

        public Func<CriteriaOperator, LeafNodeModel> CreateLeaf { get; }

        public Func<CriteriaOperator, IList<NodeModelBase>, bool, GroupNodeModel> CreateGroup { get; }

        public Func<CriteriaOperator, CustomExpressionNodeModel> CreateCustom { get; }
    }
}

