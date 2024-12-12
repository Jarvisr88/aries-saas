namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class CheckedTreeHelper
    {
        internal static IEnumerable<NodeBase<T>> GetDepthFirstNodes<T>(NodeBase<T> node)
        {
            NodeBase<T>[] source = new NodeBase<T>[] { node };
            Func<NodeBase<T>, IEnumerable<NodeBase<T>>> getItems = <>c__0<T>.<>9__0_0;
            if (<>c__0<T>.<>9__0_0 == null)
            {
                Func<NodeBase<T>, IEnumerable<NodeBase<T>>> local1 = <>c__0<T>.<>9__0_0;
                getItems = <>c__0<T>.<>9__0_0 = x => x.GetChildrenFast();
            }
            return source.Flatten<NodeBase<T>>(getItems);
        }

        public static List<NodeBase<T>> GetTopmostCheckedNodes<T>(NodeBase<T> node)
        {
            List<NodeBase<T>> list = new List<NodeBase<T>>();
            if (node.ActualIsChecked != null)
            {
                if (node.ActualIsChecked.Value)
                {
                    list.Add(node);
                }
                return list;
            }
            foreach (NodeBase<T> base2 in ((GroupNode<T>) node).Nodes)
            {
                List<NodeBase<T>> topmostCheckedNodes = GetTopmostCheckedNodes<T>(base2);
                List<NodeBase<T>> collection = topmostCheckedNodes;
                if (topmostCheckedNodes == null)
                {
                    List<NodeBase<T>> local1 = topmostCheckedNodes;
                    collection = new List<NodeBase<T>>();
                }
                list.AddRange(collection);
            }
            Guard.ArgumentPositive(list.Count, "result");
            return list;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<T>
        {
            public static readonly CheckedTreeHelper.<>c__0<T> <>9;
            public static Func<NodeBase<T>, IEnumerable<NodeBase<T>>> <>9__0_0;

            static <>c__0()
            {
                CheckedTreeHelper.<>c__0<T>.<>9 = new CheckedTreeHelper.<>c__0<T>();
            }

            internal IEnumerable<NodeBase<T>> <GetDepthFirstNodes>b__0_0(NodeBase<T> x) => 
                x.GetChildrenFast();
        }
    }
}

