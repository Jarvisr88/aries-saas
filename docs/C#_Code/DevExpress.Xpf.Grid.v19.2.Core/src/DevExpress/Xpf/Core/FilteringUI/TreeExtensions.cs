namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class TreeExtensions
    {
        public static IEnumerable<L> FlattenLeaves<L, G>(this IEnumerable<Tree<L, G>> forest)
        {
            Func<Tree<L, G>, IEnumerable<L>> selector = <>c__6<L, G>.<>9__6_0;
            if (<>c__6<L, G>.<>9__6_0 == null)
            {
                Func<Tree<L, G>, IEnumerable<L>> local1 = <>c__6<L, G>.<>9__6_0;
                selector = <>c__6<L, G>.<>9__6_0 = delegate (Tree<L, G> tree) {
                    Func<L, IEnumerable<L>> func1 = <>c__6<L, G>.<>9__6_1;
                    if (<>c__6<L, G>.<>9__6_1 == null)
                    {
                        Func<L, IEnumerable<L>> local1 = <>c__6<L, G>.<>9__6_1;
                        func1 = <>c__6<L, G>.<>9__6_1 = leaf => leaf.Yield<L>();
                    }
                    return tree.Match<IEnumerable<L>>(func1, <>c__6<L, G>.<>9__6_2 ??= (_, children) => children.FlattenLeaves<L, G>());
                };
            }
            return forest.SelectMany<Tree<L, G>, L>(selector);
        }

        public static IEnumerable<Tree<L, G>> FlattenLeavesContainers<L, G>(this IEnumerable<Tree<L, G>> forest)
        {
            Func<Tree<L, G>, IEnumerable<Tree<L, G>>> getItems = <>c__7<L, G>.<>9__7_0;
            if (<>c__7<L, G>.<>9__7_0 == null)
            {
                Func<Tree<L, G>, IEnumerable<Tree<L, G>>> local1 = <>c__7<L, G>.<>9__7_0;
                getItems = <>c__7<L, G>.<>9__7_0 = node => node.GetChildren<L, G>();
            }
            Func<Tree<L, G>, bool> predicate = <>c__7<L, G>.<>9__7_1;
            if (<>c__7<L, G>.<>9__7_1 == null)
            {
                Func<Tree<L, G>, bool> local2 = <>c__7<L, G>.<>9__7_1;
                predicate = <>c__7<L, G>.<>9__7_1 = delegate (Tree<L, G> node) {
                    Func<L, bool> leaf = <>c__7<L, G>.<>9__7_2;
                    if (<>c__7<L, G>.<>9__7_2 == null)
                    {
                        Func<L, bool> local1 = <>c__7<L, G>.<>9__7_2;
                        leaf = <>c__7<L, G>.<>9__7_2 = _ => true;
                    }
                    return node.MatchLeaf<L, G, bool>(leaf, null);
                };
            }
            return forest.Flatten<Tree<L, G>>(getItems).Where<Tree<L, G>>(predicate);
        }

        public static IList<Tree<L, G>> GetChildren<L, G>(this Tree<L, G> tree)
        {
            Func<G, IList<Tree<L, G>>, IList<Tree<L, G>>> group = <>c__8<L, G>.<>9__8_0;
            if (<>c__8<L, G>.<>9__8_0 == null)
            {
                Func<G, IList<Tree<L, G>>, IList<Tree<L, G>>> local1 = <>c__8<L, G>.<>9__8_0;
                group = <>c__8<L, G>.<>9__8_0 = (_, children) => children;
            }
            return tree.MatchGroup<L, G, IList<Tree<L, G>>>(group, (<>c__8<L, G>.<>9__8_1 ??= ((Func<IList<Tree<L, G>>>) (() => new Tree<L, G>[0]))));
        }

        private static T GetValueOrDefault<T>(Func<T> getValue)
        {
            if (getValue != null)
            {
                return getValue();
            }
            return default(T);
        }

        public static Tree<L, G> HierarchicalWhere<L, G>(this Tree<L, G> tree, Func<L, bool> leafPredicate, Func<G, IEnumerable<Tree<L, G>>, bool> groupPredicate) => 
            tree.Match<Tree<L, G>>(leaf => leafPredicate(leaf) ? tree : null, delegate (G groupValue, IList<Tree<L, G>> children) {
                Tree<L, G> tree = Tree<L, G>.CreateGroup(groupValue, children.HierarchicalWhere<L, G>(leafPredicate, groupPredicate).ToArray<Tree<L, G>>());
                return tree.MatchGroup<L, G, bool>(((Func<G, IList<Tree<L, G>>, bool>) groupPredicate), null) ? tree : null;
            });

        public static IEnumerable<Tree<L, G>> HierarchicalWhere<L, G>(this IEnumerable<Tree<L, G>> forest, Func<L, bool> leafPredicate, Func<G, IEnumerable<Tree<L, G>>, bool> groupPredicate)
        {
            Func<Tree<L, G>, bool> predicate = <>c__4<L, G>.<>9__4_1;
            if (<>c__4<L, G>.<>9__4_1 == null)
            {
                Func<Tree<L, G>, bool> local1 = <>c__4<L, G>.<>9__4_1;
                predicate = <>c__4<L, G>.<>9__4_1 = tree => tree != null;
            }
            return (from tree in forest select tree.HierarchicalWhere<L, G>(leafPredicate, groupPredicate)).Where<Tree<L, G>>(predicate);
        }

        public static T MatchGroup<L, G, T>(this Tree<L, G> tree, Func<G, IList<Tree<L, G>>, T> group, Func<T> otherwise = null) => 
            tree.Match<T>(_ => GetValueOrDefault<T>(otherwise), group);

        public static T MatchLeaf<L, G, T>(this Tree<L, G> tree, Func<L, T> leaf, Func<T> otherwise = null) => 
            tree.Match<T>(leaf, (_, __) => GetValueOrDefault<T>(otherwise));

        public static Tree<T, G> Transform<L, G, T>(this Tree<L, G> tree, Func<L, T> transformLeaf)
        {
            Func<G, IEnumerable<Tree<T, G>>, Tree<T, G>> transformGroup = <>c__3<L, G, T>.<>9__3_1;
            if (<>c__3<L, G, T>.<>9__3_1 == null)
            {
                Func<G, IEnumerable<Tree<T, G>>, Tree<T, G>> local1 = <>c__3<L, G, T>.<>9__3_1;
                transformGroup = <>c__3<L, G, T>.<>9__3_1 = (group, children) => Tree<T, G>.CreateGroup(group, children.ToArray<Tree<T, G>>());
            }
            return tree.Transform<L, G, Tree<T, G>>(leaf => Tree<T, G>.CreateLeaf(transformLeaf(leaf)), transformGroup);
        }

        public static IEnumerable<Tree<T, G>> Transform<L, G, T>(this IEnumerable<Tree<L, G>> forest, Func<L, T> transformLeaf) => 
            from tree in forest select tree.Transform<L, G, T>(transformLeaf);

        public static T Transform<L, G, T>(this Tree<L, G> tree, Func<L, T> transformLeaf, Func<G, IEnumerable<T>, T> transformGroup) => 
            tree.Match<T>(leaf => transformLeaf(leaf), delegate (G group, IList<Tree<L, G>> children) {
                Func<Tree<L, G>, T> <>9__2;
                Func<Tree<L, G>, T> selector = <>9__2;
                if (<>9__2 == null)
                {
                    Func<Tree<L, G>, T> local1 = <>9__2;
                    selector = <>9__2 = x => x.Transform<L, G, T>(transformLeaf, transformGroup);
                }
                return transformGroup(group, children.Select<Tree<L, G>, T>(selector));
            });

        public static IEnumerable<T> Transform<L, G, T>(this IEnumerable<Tree<L, G>> forest, Func<L, T> transformLeaf, Func<G, IEnumerable<T>, T> transformGroup) => 
            from tree in forest select tree.Transform<L, G, T>(transformLeaf, transformGroup);

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<L, G, T>
        {
            public static readonly DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__3<L, G, T> <>9;
            public static Func<G, IEnumerable<Tree<T, G>>, Tree<T, G>> <>9__3_1;

            static <>c__3()
            {
                DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__3<L, G, T>.<>9 = new DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__3<L, G, T>();
            }

            internal Tree<T, G> <Transform>b__3_1(G group, IEnumerable<Tree<T, G>> children) => 
                Tree<T, G>.CreateGroup(group, children.ToArray<Tree<T, G>>());
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__4<L, G>
        {
            public static readonly DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__4<L, G> <>9;
            public static Func<Tree<L, G>, bool> <>9__4_1;

            static <>c__4()
            {
                DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__4<L, G>.<>9 = new DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__4<L, G>();
            }

            internal bool <HierarchicalWhere>b__4_1(Tree<L, G> tree) => 
                tree != null;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__6<L, G>
        {
            public static readonly DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G> <>9;
            public static Func<L, IEnumerable<L>> <>9__6_1;
            public static Func<G, IList<Tree<L, G>>, IEnumerable<L>> <>9__6_2;
            public static Func<Tree<L, G>, IEnumerable<L>> <>9__6_0;

            static <>c__6()
            {
                DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G>.<>9 = new DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G>();
            }

            internal IEnumerable<L> <FlattenLeaves>b__6_0(Tree<L, G> tree)
            {
                Func<L, IEnumerable<L>> func1 = DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G>.<>9__6_1;
                if (DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G>.<>9__6_1 == null)
                {
                    Func<L, IEnumerable<L>> local1 = DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G>.<>9__6_1;
                    func1 = DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G>.<>9__6_1 = leaf => leaf.Yield<L>();
                }
                return tree.Match<IEnumerable<L>>(func1, DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__6<L, G>.<>9__6_2 ??= (_, children) => children.FlattenLeaves<L, G>());
            }

            internal IEnumerable<L> <FlattenLeaves>b__6_1(L leaf) => 
                leaf.Yield<L>();

            internal IEnumerable<L> <FlattenLeaves>b__6_2(G _, IList<Tree<L, G>> children) => 
                children.FlattenLeaves<L, G>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__7<L, G>
        {
            public static readonly DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__7<L, G> <>9;
            public static Func<Tree<L, G>, IEnumerable<Tree<L, G>>> <>9__7_0;
            public static Func<L, bool> <>9__7_2;
            public static Func<Tree<L, G>, bool> <>9__7_1;

            static <>c__7()
            {
                DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__7<L, G>.<>9 = new DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__7<L, G>();
            }

            internal IEnumerable<Tree<L, G>> <FlattenLeavesContainers>b__7_0(Tree<L, G> node) => 
                node.GetChildren<L, G>();

            internal bool <FlattenLeavesContainers>b__7_1(Tree<L, G> node)
            {
                Func<L, bool> leaf = DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__7<L, G>.<>9__7_2;
                if (DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__7<L, G>.<>9__7_2 == null)
                {
                    Func<L, bool> local1 = DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__7<L, G>.<>9__7_2;
                    leaf = DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__7<L, G>.<>9__7_2 = _ => true;
                }
                return node.MatchLeaf<L, G, bool>(leaf, null);
            }

            internal bool <FlattenLeavesContainers>b__7_2(L _) => 
                true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__8<L, G>
        {
            public static readonly DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__8<L, G> <>9;
            public static Func<G, IList<Tree<L, G>>, IList<Tree<L, G>>> <>9__8_0;
            public static Func<IList<Tree<L, G>>> <>9__8_1;

            static <>c__8()
            {
                DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__8<L, G>.<>9 = new DevExpress.Xpf.Core.FilteringUI.TreeExtensions.<>c__8<L, G>();
            }

            internal IList<Tree<L, G>> <GetChildren>b__8_0(G _, IList<Tree<L, G>> children) => 
                children;

            internal IList<Tree<L, G>> <GetChildren>b__8_1() => 
                new Tree<L, G>[0];
        }
    }
}

