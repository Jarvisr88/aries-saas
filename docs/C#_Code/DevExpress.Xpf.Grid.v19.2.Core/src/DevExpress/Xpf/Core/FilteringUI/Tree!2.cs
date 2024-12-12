namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class Tree<L, G>
    {
        private readonly Either<L, Group<L, G>> either;

        private Tree(Either<L, Group<L, G>> either)
        {
            this.either = either;
        }

        public static Tree<L, G> CreateGroup(G value, IList<Tree<L, G>> children) => 
            new Tree<L, G>(Either<L, Group<L, G>>.Right(new Group<L, G>(value, children)));

        public static Tree<L, G> CreateLeaf(L value) => 
            new Tree<L, G>(Either<L, Group<L, G>>.Left(value));

        public T Match<T>(Func<L, T> leaf, Func<G, IList<Tree<L, G>>, T> group) => 
            this.either.Match<T>(leaf, (Func<Group<L, G>, T>) (g => group(g.Value, g.Children)));

        private class Group
        {
            public Group(G value, IList<Tree<L, G>> children)
            {
                Guard.ArgumentNotNull(children, "children");
                this.<Value>k__BackingField = value;
                this.<Children>k__BackingField = children;
            }

            public G Value { get; }

            public IList<Tree<L, G>> Children { get; }
        }
    }
}

