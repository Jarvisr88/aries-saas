namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class FilterEditorNavigationPager<TNode> where TNode: class
    {
        private static TNode ByOffsetCore(IEnumerable<NodeOffsetRecord<TNode>> records, Func<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>, bool> predicate)
        {
            NodeOffsetRecord<TNode> local1 = records.Aggregate<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>>(null, (acc, current) => ((acc == null) || predicate(current, acc)) ? current : acc);
            if (local1 != null)
            {
                return local1.Node;
            }
            NodeOffsetRecord<TNode> local2 = local1;
            return default(TNode);
        }

        private static TNode GetTarget(TNode focusedNode, Func<IList<NodeOffsetRecord<TNode>>> getRecords, double viewportHeight, NavigationDirection<TNode> direction)
        {
            if (focusedNode != null)
            {
                IList<NodeOffsetRecord<TNode>> list = getRecords();
                if (list.Count == 0)
                {
                    return default(TNode);
                }
                if (direction == NavigationDirection<TNode>.Down)
                {
                    TNode local2 = FilterEditorNavigationPager<TNode>.MaxByOffset(from record in list
                        where FilterEditorNavigationPager<TNode>.IsOnCurrentPage(record, viewportHeight)
                        select record);
                    return ((local2 == focusedNode) ? FilterEditorNavigationPager<TNode>.MaxByOffset(from record in list
                        where FilterEditorNavigationPager<TNode>.IsOnNextPage(record, viewportHeight)
                        select record) : local2);
                }
                if (direction == NavigationDirection<TNode>.Up)
                {
                    TNode local3 = FilterEditorNavigationPager<TNode>.MinByOffset(from record in list
                        where FilterEditorNavigationPager<TNode>.IsOnCurrentPage(record, viewportHeight)
                        select record);
                    return ((local3 == focusedNode) ? FilterEditorNavigationPager<TNode>.MinByOffset(from record in list
                        where FilterEditorNavigationPager<TNode>.IsOnPreviousPage(record, viewportHeight)
                        select record) : local3);
                }
            }
            return default(TNode);
        }

        private static bool IsOnCurrentPage(NodeOffsetRecord<TNode> record, double viewportHeight) => 
            (record.VerticalOffset >= 0.0) && (record.VerticalOffset <= viewportHeight);

        private static bool IsOnNextPage(NodeOffsetRecord<TNode> record, double viewportHeight) => 
            (record.VerticalOffset > viewportHeight) && (record.VerticalOffset <= (viewportHeight * 2.0));

        private static bool IsOnPreviousPage(NodeOffsetRecord<TNode> record, double viewportHeight) => 
            (record.VerticalOffset < 0.0) && (record.VerticalOffset >= -viewportHeight);

        private static TNode MaxByOffset(IEnumerable<NodeOffsetRecord<TNode>> records)
        {
            Func<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>, bool> predicate = <>c<TNode>.<>9__6_0;
            if (<>c<TNode>.<>9__6_0 == null)
            {
                Func<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>, bool> local1 = <>c<TNode>.<>9__6_0;
                predicate = <>c<TNode>.<>9__6_0 = (x, y) => x.VerticalOffset > y.VerticalOffset;
            }
            return FilterEditorNavigationPager<TNode>.ByOffsetCore(records, predicate);
        }

        private static TNode MinByOffset(IEnumerable<NodeOffsetRecord<TNode>> records)
        {
            Func<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>, bool> predicate = <>c<TNode>.<>9__7_0;
            if (<>c<TNode>.<>9__7_0 == null)
            {
                Func<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>, bool> local1 = <>c<TNode>.<>9__7_0;
                predicate = <>c<TNode>.<>9__7_0 = (x, y) => x.VerticalOffset < y.VerticalOffset;
            }
            return FilterEditorNavigationPager<TNode>.ByOffsetCore(records, predicate);
        }

        public static NavigationPager<TNode> Pager(TNode focusedNode, Func<IList<NodeOffsetRecord<TNode>>> getRecords, double viewportHeight) => 
            new NavigationPager<TNode>(() => FilterEditorNavigationPager<TNode>.GetTarget(focusedNode, getRecords, viewportHeight, NavigationDirection<TNode>.Up), () => FilterEditorNavigationPager<TNode>.GetTarget(focusedNode, getRecords, viewportHeight, NavigationDirection<TNode>.Down));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterEditorNavigationPager<TNode>.<>c <>9;
            public static Func<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>, bool> <>9__6_0;
            public static Func<NodeOffsetRecord<TNode>, NodeOffsetRecord<TNode>, bool> <>9__7_0;

            static <>c()
            {
                FilterEditorNavigationPager<TNode>.<>c.<>9 = new FilterEditorNavigationPager<TNode>.<>c();
            }

            internal bool <MaxByOffset>b__6_0(NodeOffsetRecord<TNode> x, NodeOffsetRecord<TNode> y) => 
                x.VerticalOffset > y.VerticalOffset;

            internal bool <MinByOffset>b__7_0(NodeOffsetRecord<TNode> x, NodeOffsetRecord<TNode> y) => 
                x.VerticalOffset < y.VerticalOffset;
        }

        private enum NavigationDirection
        {
            public const FilterEditorNavigationPager<TNode>.NavigationDirection Up = FilterEditorNavigationPager<TNode>.NavigationDirection.Up;,
            public const FilterEditorNavigationPager<TNode>.NavigationDirection Down = FilterEditorNavigationPager<TNode>.NavigationDirection.Down;
        }
    }
}

