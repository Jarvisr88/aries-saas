namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class FilterEditorUniqueValuesHelper
    {
        private static OperandValue TrueCriteria = new OperandValue(true);
        private static OperandValue FalseCriteria = new OperandValue(false);

        private static CriteriaOperator BuildTreeFilterWithSubstitutedNode(NodeModelBase root, NodeModelBase substitutedNode, CriteriaOperator substitutedNodeFilter) => 
            root.BuildEvaluableFilter(delegate (IList<NodeModelBase> nodes) {
                Func<NodeModelBase, CriteriaOperator> <>9__1;
                Func<NodeModelBase, CriteriaOperator> selector = <>9__1;
                if (<>9__1 == null)
                {
                    Func<NodeModelBase, CriteriaOperator> local1 = <>9__1;
                    selector = <>9__1 = x => ReferenceEquals(x, substitutedNode) ? substitutedNodeFilter : BuildTreeFilterWithSubstitutedNode(x, substitutedNode, substitutedNodeFilter);
                }
                return nodes.Select<NodeModelBase, CriteriaOperator>(selector);
            });

        internal static CriteriaOperator BuildUniqueValuesFilter(NodeModelBase root, NodeModelBase target)
        {
            if ((root == null) || (target == null))
            {
                return null;
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { BuildTreeFilterWithSubstitutedNode(root, target, TrueCriteria), BuildTreeFilterWithSubstitutedNode(root, target, FalseCriteria) };
            CriteriaOperator objA = SimplifyOperator(new GroupOperator(GroupOperatorType.Or, operands));
            return (!Equals(objA, TrueCriteria) ? objA : null);
        }

        private static GroupOperator CloneAndRemove(GroupOperator groupOperator, OperandValue value)
        {
            GroupOperator @operator = groupOperator.Clone();
            @operator.Operands.Remove(value);
            return @operator;
        }

        private static CriteriaOperator SimplifyOperator(CriteriaOperator criteria)
        {
            // Unresolved stack state at '000002AF'
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterEditorUniqueValuesHelper.<>c <>9 = new FilterEditorUniqueValuesHelper.<>c();
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_1;
            public static Func<GroupOperator, CriteriaOperator> <>9__3_0;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_2;
            public static Predicate<GroupOperator> <>9__3_3;
            public static Func<GroupOperator, CriteriaOperator> <>9__3_4;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_5;
            public static Predicate<GroupOperator> <>9__3_6;
            public static Func<GroupOperator, CriteriaOperator> <>9__3_7;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_8;
            public static Predicate<GroupOperator> <>9__3_9;
            public static Func<GroupOperator, CriteriaOperator> <>9__3_10;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_11;
            public static Predicate<GroupOperator> <>9__3_12;
            public static Func<GroupOperator, CriteriaOperator> <>9__3_13;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_14;
            public static Predicate<UnaryOperator> <>9__3_15;
            public static Func<UnaryOperator, CriteriaOperator> <>9__3_16;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_17;
            public static Predicate<UnaryOperator> <>9__3_18;
            public static Func<UnaryOperator, CriteriaOperator> <>9__3_19;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_20;
            public static Predicate<UnaryOperator> <>9__3_21;
            public static Func<UnaryOperator, CriteriaOperator> <>9__3_22;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__3_23;

            internal CriteriaOperator <SimplifyOperator>b__3_0(GroupOperator x)
            {
                Func<CriteriaOperator, CriteriaOperator> selector = <>9__3_1;
                if (<>9__3_1 == null)
                {
                    Func<CriteriaOperator, CriteriaOperator> local1 = <>9__3_1;
                    selector = <>9__3_1 = y => FilterEditorUniqueValuesHelper.SimplifyOperator(y);
                }
                return new GroupOperator(x.OperatorType, x.Operands.Select<CriteriaOperator, CriteriaOperator>(selector));
            }

            internal CriteriaOperator <SimplifyOperator>b__3_1(CriteriaOperator y) => 
                FilterEditorUniqueValuesHelper.SimplifyOperator(y);

            internal CriteriaOperator <SimplifyOperator>b__3_10(GroupOperator _) => 
                FilterEditorUniqueValuesHelper.FalseCriteria;

            internal CriteriaOperator <SimplifyOperator>b__3_11(CriteriaOperator x) => 
                x;

            internal bool <SimplifyOperator>b__3_12(GroupOperator x) => 
                x.Operands.Contains(FilterEditorUniqueValuesHelper.FalseCriteria) && (x.OperatorType == GroupOperatorType.Or);

            internal CriteriaOperator <SimplifyOperator>b__3_13(GroupOperator x) => 
                FilterEditorUniqueValuesHelper.CloneAndRemove(x, FilterEditorUniqueValuesHelper.FalseCriteria);

            internal CriteriaOperator <SimplifyOperator>b__3_14(CriteriaOperator x) => 
                x;

            internal bool <SimplifyOperator>b__3_15(UnaryOperator u) => 
                u.OperatorType == UnaryOperatorType.Not;

            internal CriteriaOperator <SimplifyOperator>b__3_16(UnaryOperator x) => 
                new UnaryOperator(UnaryOperatorType.Not, FilterEditorUniqueValuesHelper.SimplifyOperator(x.Operand));

            internal CriteriaOperator <SimplifyOperator>b__3_17(CriteriaOperator x) => 
                x;

            internal bool <SimplifyOperator>b__3_18(UnaryOperator u) => 
                (u.OperatorType == UnaryOperatorType.Not) && Equals(u.Operand, FilterEditorUniqueValuesHelper.TrueCriteria);

            internal CriteriaOperator <SimplifyOperator>b__3_19(UnaryOperator x) => 
                FilterEditorUniqueValuesHelper.FalseCriteria;

            internal CriteriaOperator <SimplifyOperator>b__3_2(CriteriaOperator x) => 
                x;

            internal CriteriaOperator <SimplifyOperator>b__3_20(CriteriaOperator x) => 
                x;

            internal bool <SimplifyOperator>b__3_21(UnaryOperator u) => 
                (u.OperatorType == UnaryOperatorType.Not) && Equals(u.Operand, FilterEditorUniqueValuesHelper.FalseCriteria);

            internal CriteriaOperator <SimplifyOperator>b__3_22(UnaryOperator x) => 
                FilterEditorUniqueValuesHelper.TrueCriteria;

            internal CriteriaOperator <SimplifyOperator>b__3_23(CriteriaOperator x) => 
                x;

            internal bool <SimplifyOperator>b__3_3(GroupOperator x) => 
                x.Operands.Contains(FilterEditorUniqueValuesHelper.TrueCriteria) && (x.OperatorType == GroupOperatorType.And);

            internal CriteriaOperator <SimplifyOperator>b__3_4(GroupOperator x) => 
                FilterEditorUniqueValuesHelper.CloneAndRemove(x, FilterEditorUniqueValuesHelper.TrueCriteria);

            internal CriteriaOperator <SimplifyOperator>b__3_5(CriteriaOperator x) => 
                x;

            internal bool <SimplifyOperator>b__3_6(GroupOperator x) => 
                x.Operands.Contains(FilterEditorUniqueValuesHelper.TrueCriteria) && (x.OperatorType == GroupOperatorType.Or);

            internal CriteriaOperator <SimplifyOperator>b__3_7(GroupOperator _) => 
                FilterEditorUniqueValuesHelper.TrueCriteria;

            internal CriteriaOperator <SimplifyOperator>b__3_8(CriteriaOperator x) => 
                x;

            internal bool <SimplifyOperator>b__3_9(GroupOperator x) => 
                x.Operands.Contains(FilterEditorUniqueValuesHelper.FalseCriteria) && (x.OperatorType == GroupOperatorType.And);
        }
    }
}

