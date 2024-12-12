namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class FilterEditorTreeBuilder
    {
        private static NodeModelBase BuildNode(CriteriaOperator filter, bool isRoot, NodeModelFactory factory) => 
            filter.Transform<GroupOperator, NodeModelBase>((Func<GroupOperator, NodeModelBase>) (groupOperator => factory.CreateGroup(filter, BuildNodes(groupOperator.Operands, factory), isRoot)), delegate (CriteriaOperator leafOperator) {
                LeafNodeModel local1 = factory.CreateLeaf(leafOperator);
                NodeModelBase base2 = local1;
                if (local1 == null)
                {
                    LeafNodeModel local2 = local1;
                    NodeModelBase base1 = CreateUnaryNotOperatorNode(filter, isRoot, factory);
                    base2 = base1;
                    if (base1 == null)
                    {
                        NodeModelBase local3 = base1;
                        base2 = factory.CreateCustom(leafOperator);
                    }
                }
                return base2;
            });

        private static IList<NodeModelBase> BuildNodes(IList<CriteriaOperator> operands, NodeModelFactory factory) => 
            (from x in operands select BuildNode(x, false, factory)).ToArray<NodeModelBase>();

        public static GroupNodeModel BuildRoot(FilteringUIContext context, Func<Func<GroupNodeModel>, NodeModelFactory> createFactory)
        {
            if (context == null)
            {
                return null;
            }
            Locker treeIsBuildingLocker = new Locker();
            GroupNodeModel result = null;
            treeIsBuildingLocker.DoLockedAction(delegate {
                Func<Lazy<GroupNodeModel>, GroupNodeModel> <>9__1;
                Func<Lazy<GroupNodeModel>, GroupNodeModel> func = <>9__1;
                if (<>9__1 == null)
                {
                    Func<Lazy<GroupNodeModel>, GroupNodeModel> local1 = <>9__1;
                    func = <>9__1 = delegate (Lazy<GroupNodeModel> root) {
                        NodeModelFactory factory = createFactory(() => treeIsBuildingLocker.IsLocked ? null : root.Value);
                        if (context.Filter.ReferenceEqualsNull())
                        {
                            return factory.CreateGroup(null, new NodeModelBase[0], true);
                        }
                        Func<GroupNodeModel, GroupNodeModel> func1 = <>c.<>9__0_3;
                        if (<>c.<>9__0_3 == null)
                        {
                            Func<GroupNodeModel, GroupNodeModel> local1 = <>c.<>9__0_3;
                            func1 = <>c.<>9__0_3 = group => group;
                        }
                        return BuildNode(DatePeriodsFilterModelHelper.ExtractFilter(BetweenDatesHelper.SubstituteDateInRange(context.Filter)), true, factory).Match<GroupNodeModel>(func1, node => factory.CreateGroup(null, node.YieldToArray<NodeModelBase>(), true));
                    };
                }
                result = func.WithReturnValue<GroupNodeModel>();
            });
            return result;
        }

        private static NodeModelBase CreateUnaryNotOperatorNode(CriteriaOperator filter, bool isRoot, NodeModelFactory factory)
        {
            Predicate<UnaryOperator> condition = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Predicate<UnaryOperator> local1 = <>c.<>9__2_0;
                condition = <>c.<>9__2_0 = unaryOperator => unaryOperator.OperatorType == UnaryOperatorType.Not;
            }
            Func<CriteriaOperator, NodeModelBase> otherwise = <>c.<>9__2_4;
            if (<>c.<>9__2_4 == null)
            {
                Func<CriteriaOperator, NodeModelBase> local2 = <>c.<>9__2_4;
                otherwise = <>c.<>9__2_4 = (Func<CriteriaOperator, NodeModelBase>) (_ => null);
            }
            return filter.Transform<UnaryOperator, NodeModelBase>(condition, delegate (UnaryOperator notOperator) {
                Func<GroupOperator, IList<CriteriaOperator>> func = <>c.<>9__2_2;
                if (<>c.<>9__2_2 == null)
                {
                    Func<GroupOperator, IList<CriteriaOperator>> local1 = <>c.<>9__2_2;
                    func = <>c.<>9__2_2 = groupInsideNotOperator => groupInsideNotOperator.Operands;
                }
                IList<CriteriaOperator> operands = notOperator.Operand.Transform<GroupOperator, IList<CriteriaOperator>>(func, <>c.<>9__2_3 ??= ((Func<CriteriaOperator, IList<CriteriaOperator>>) (leafInsideNotOperator => leafInsideNotOperator.YieldToArray<CriteriaOperator>())));
                return factory.CreateGroup(notOperator, BuildNodes(operands, factory), isRoot);
            }, otherwise);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterEditorTreeBuilder.<>c <>9 = new FilterEditorTreeBuilder.<>c();
            public static Func<GroupNodeModel, GroupNodeModel> <>9__0_3;
            public static Predicate<UnaryOperator> <>9__2_0;
            public static Func<GroupOperator, IList<CriteriaOperator>> <>9__2_2;
            public static Func<CriteriaOperator, IList<CriteriaOperator>> <>9__2_3;
            public static Func<CriteriaOperator, NodeModelBase> <>9__2_4;

            internal GroupNodeModel <BuildRoot>b__0_3(GroupNodeModel group) => 
                group;

            internal bool <CreateUnaryNotOperatorNode>b__2_0(UnaryOperator unaryOperator) => 
                unaryOperator.OperatorType == UnaryOperatorType.Not;

            internal IList<CriteriaOperator> <CreateUnaryNotOperatorNode>b__2_2(GroupOperator groupInsideNotOperator) => 
                groupInsideNotOperator.Operands;

            internal IList<CriteriaOperator> <CreateUnaryNotOperatorNode>b__2_3(CriteriaOperator leafInsideNotOperator) => 
                leafInsideNotOperator.YieldToArray<CriteriaOperator>();

            internal NodeModelBase <CreateUnaryNotOperatorNode>b__2_4(CriteriaOperator _) => 
                null;
        }
    }
}

