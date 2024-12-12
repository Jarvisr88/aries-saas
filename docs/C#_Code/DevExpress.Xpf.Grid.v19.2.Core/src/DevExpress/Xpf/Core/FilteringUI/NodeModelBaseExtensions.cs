namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class NodeModelBaseExtensions
    {
        public static CriteriaOperator BuildEvaluableFilter(this NodeModelBase node, Func<IList<NodeModelBase>, IEnumerable<CriteriaOperator>> getChildrenFilters = null) => 
            BetweenDatesHelper.RemoveDateInRange(DatePeriodsFilterModelHelper.CompressFilter(node.BuildMinimizedFilter(getChildrenFilters)));

        public static CriteriaOperator BuildMinimizedFilter(this NodeModelBase node, Func<IList<NodeModelBase>, IEnumerable<CriteriaOperator>> getChildrenFilters = null)
        {
            Func<LeafNodeModel, CriteriaOperator> leaf = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<LeafNodeModel, CriteriaOperator> local1 = <>c.<>9__0_0;
                leaf = <>c.<>9__0_0 = delegate (LeafNodeModel l) {
                    Func<MultiFilterModel, CriteriaOperator> evaluator = <>c.<>9__0_1;
                    if (<>c.<>9__0_1 == null)
                    {
                        Func<MultiFilterModel, CriteriaOperator> local1 = <>c.<>9__0_1;
                        evaluator = <>c.<>9__0_1 = x => x.BuildFilter();
                    }
                    return l.MultiModel.With<MultiFilterModel, CriteriaOperator>(evaluator);
                };
            }
            Func<CustomExpressionNodeModel, CriteriaOperator> customExpression = <>c.<>9__0_5;
            if (<>c.<>9__0_5 == null)
            {
                Func<CustomExpressionNodeModel, CriteriaOperator> local2 = <>c.<>9__0_5;
                customExpression = <>c.<>9__0_5 = c => c.Filter;
            }
            return node.Match<CriteriaOperator>(leaf, delegate (GroupNodeModel g) {
                IEnumerable<CriteriaOperator> enumerable1;
                if (g.GroupOperator == null)
                {
                    return null;
                }
                if (getChildrenFilters != null)
                {
                    enumerable1 = getChildrenFilters(g.Children);
                }
                else
                {
                    Func<NodeModelBase, CriteriaOperator> selector = <>c.<>9__0_3;
                    if (<>c.<>9__0_3 == null)
                    {
                        Func<NodeModelBase, CriteriaOperator> local1 = <>c.<>9__0_3;
                        selector = <>c.<>9__0_3 = x => x.BuildMinimizedFilter(null);
                    }
                    enumerable1 = g.Children.Select<NodeModelBase, CriteriaOperator>(selector);
                }
                CriteriaOperator[] arg = enumerable1.Where<CriteriaOperator>((<>c.<>9__0_4 ??= y => !y.ReferenceEqualsNull())).ToArray<CriteriaOperator>();
                return g.GroupOperator.Factory(arg);
            }, customExpression);
        }

        public static IList<NodeModelBase> GetChildren(this NodeModelBase node)
        {
            Func<GroupNodeModel, IList<NodeModelBase>> group = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<GroupNodeModel, IList<NodeModelBase>> local1 = <>c.<>9__2_0;
                group = <>c.<>9__2_0 = g => g.Children;
            }
            return node.Match<IList<NodeModelBase>>(group, <>c.<>9__2_1 ??= _ => new List<NodeModelBase>());
        }

        public static GroupNodeModel GetParent(this NodeModelBase node, GroupNodeModel root)
        {
            GroupNodeModel model2;
            if ((node == null) || (root == null))
            {
                return null;
            }
            using (IEnumerator<NodeModelBase> enumerator = root.GetChildren().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        NodeModelBase current = enumerator.Current;
                        if (ReferenceEquals(current, node))
                        {
                            model2 = root;
                        }
                        else
                        {
                            Func<GroupNodeModel, GroupNodeModel> <>9__1;
                            Func<LeafNodeModel, GroupNodeModel> leaf = <>c.<>9__3_0;
                            if (<>c.<>9__3_0 == null)
                            {
                                Func<LeafNodeModel, GroupNodeModel> local1 = <>c.<>9__3_0;
                                leaf = <>c.<>9__3_0 = (Func<LeafNodeModel, GroupNodeModel>) (_ => null);
                            }
                            Func<GroupNodeModel, GroupNodeModel> group = <>9__1;
                            if (<>9__1 == null)
                            {
                                Func<GroupNodeModel, GroupNodeModel> local2 = <>9__1;
                                group = <>9__1 = g => node.GetParent(g);
                            }
                            GroupNodeModel model = current.Match<GroupNodeModel>(leaf, group, <>c.<>9__3_2 ??= ((Func<CustomExpressionNodeModel, GroupNodeModel>) (_ => null)));
                            if (model == null)
                            {
                                continue;
                            }
                            model2 = model;
                        }
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return model2;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NodeModelBaseExtensions.<>c <>9 = new NodeModelBaseExtensions.<>c();
            public static Func<MultiFilterModel, CriteriaOperator> <>9__0_1;
            public static Func<LeafNodeModel, CriteriaOperator> <>9__0_0;
            public static Func<NodeModelBase, CriteriaOperator> <>9__0_3;
            public static Func<CriteriaOperator, bool> <>9__0_4;
            public static Func<CustomExpressionNodeModel, CriteriaOperator> <>9__0_5;
            public static Func<GroupNodeModel, IList<NodeModelBase>> <>9__2_0;
            public static Func<NodeModelBase, IList<NodeModelBase>> <>9__2_1;
            public static Func<LeafNodeModel, GroupNodeModel> <>9__3_0;
            public static Func<CustomExpressionNodeModel, GroupNodeModel> <>9__3_2;

            internal CriteriaOperator <BuildMinimizedFilter>b__0_0(LeafNodeModel l)
            {
                Func<MultiFilterModel, CriteriaOperator> evaluator = <>9__0_1;
                if (<>9__0_1 == null)
                {
                    Func<MultiFilterModel, CriteriaOperator> local1 = <>9__0_1;
                    evaluator = <>9__0_1 = x => x.BuildFilter();
                }
                return l.MultiModel.With<MultiFilterModel, CriteriaOperator>(evaluator);
            }

            internal CriteriaOperator <BuildMinimizedFilter>b__0_1(MultiFilterModel x) => 
                x.BuildFilter();

            internal CriteriaOperator <BuildMinimizedFilter>b__0_3(NodeModelBase x) => 
                x.BuildMinimizedFilter(null);

            internal bool <BuildMinimizedFilter>b__0_4(CriteriaOperator y) => 
                !y.ReferenceEqualsNull();

            internal CriteriaOperator <BuildMinimizedFilter>b__0_5(CustomExpressionNodeModel c) => 
                c.Filter;

            internal IList<NodeModelBase> <GetChildren>b__2_0(GroupNodeModel g) => 
                g.Children;

            internal IList<NodeModelBase> <GetChildren>b__2_1(NodeModelBase _) => 
                new List<NodeModelBase>();

            internal GroupNodeModel <GetParent>b__3_0(LeafNodeModel _) => 
                null;

            internal GroupNodeModel <GetParent>b__3_2(CustomExpressionNodeModel _) => 
                null;
        }
    }
}

