namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class TreeExtensions
    {
        private static IEnumerable<T> FlattenFromWithin<T>(this IEnumerable<T> rootPath, Func<T, IList<T>> getChildren, Func<IList<T>, T, int> indedOf, Func<IImmutableStack<T>, Func<T, IList<T>>, Func<IList<T>, T, int>, IImmutableStack<T>> getNextElement) where T: class
        {
            Func<IList<T>, T, int> func1 = indedOf;
            if (indedOf == null)
            {
                Func<IList<T>, T, int> local1 = indedOf;
                func1 = <>c__3<T>.<>9__3_0;
                if (<>c__3<T>.<>9__3_0 == null)
                {
                    Func<IList<T>, T, int> local2 = <>c__3<T>.<>9__3_0;
                    func1 = <>c__3<T>.<>9__3_0 = (list, item) => list.IndexOf(item);
                }
            }
            indedOf = func1;
            IImmutableStack<T> originalStack = rootPath.ToImmutableStack<T>();
            Func<IImmutableStack<T>, IImmutableStack<T>> next = delegate (IImmutableStack<T> x) {
                IImmutableStack<T> stack = getNextElement(x, getChildren, indedOf);
                return (stack.Peek() == originalStack.Peek()) ? null : stack;
            };
            IEnumerable<IImmutableStack<T>> enumerable1 = LinqExtensions.Unfold<IImmutableStack<T>>(originalStack, next, <>c__3<T>.<>9__3_2 ??= x => ReferenceEquals(x, null));
            if (<>c__3<T>.<>9__3_3 == null)
            {
                IEnumerable<IImmutableStack<T>> local4 = LinqExtensions.Unfold<IImmutableStack<T>>(originalStack, next, <>c__3<T>.<>9__3_2 ??= x => ReferenceEquals(x, null));
                enumerable1 = (IEnumerable<IImmutableStack<T>>) (<>c__3<T>.<>9__3_3 = x => x.Peek());
            }
            return ((IEnumerable<IImmutableStack<T>>) <>c__3<T>.<>9__3_3).Select<IImmutableStack<T>, T>(((Func<IImmutableStack<T>, T>) enumerable1));
        }

        public static IEnumerable<T> FlattenFromWithinBackward<T>(this IEnumerable<T> rootPath, Func<T, IList<T>> getChildren, Func<IList<T>, T, int> indedOf = null) where T: class => 
            rootPath.FlattenFromWithin<T>(getChildren, indedOf, new Func<IImmutableStack<T>, Func<T, IList<T>>, Func<IList<T>, T, int>, IImmutableStack<T>>(TreeExtensions.GetPrevElementInHierarchyCore<T>));

        public static IEnumerable<T> FlattenFromWithinForward<T>(this IEnumerable<T> rootPath, Func<T, IList<T>> getChildren, Func<IList<T>, T, int> indedOf = null) where T: class
        {
            Func<IImmutableStack<T>, Func<T, IList<T>>, Func<IList<T>, T, int>, IImmutableStack<T>> getNextElement = <>c__1<T>.<>9__1_0;
            if (<>c__1<T>.<>9__1_0 == null)
            {
                Func<IImmutableStack<T>, Func<T, IList<T>>, Func<IList<T>, T, int>, IImmutableStack<T>> local1 = <>c__1<T>.<>9__1_0;
                getNextElement = <>c__1<T>.<>9__1_0 = (s, gc, io) => GetNextElementInHierarchyCore<T>(s, gc, io, false);
            }
            return rootPath.FlattenFromWithin<T>(getChildren, indedOf, getNextElement);
        }

        public static TResult FoldTree<T, TResult>(T root, Func<T, IEnumerable<T>> getChildren, Func<T, IEnumerable<TResult>, TResult> combineWithChildren)
        {
            IEnumerable<TResult> enumerable = from x in getChildren(root) select FoldTree<T, TResult>(x, getChildren, combineWithChildren);
            return combineWithChildren(root, enumerable);
        }

        private static IImmutableStack<T> GetNextElementInHierarchyCore<T>(IImmutableStack<T> rootStack, Func<T, IList<T>> getChildren, Func<IList<T>, T, int> indedOf, bool skipChildren) where T: class
        {
            T arg = rootStack.Peek();
            IList<T> source = getChildren(arg);
            if (!skipChildren && source.Any<T>())
            {
                return rootStack.Push<T>(source.First<T>());
            }
            IImmutableStack<T> stack = rootStack.Pop();
            T local2 = stack.FirstOrDefault<T>();
            if (local2 == null)
            {
                return rootStack;
            }
            IList<T> list2 = getChildren(local2);
            int num = indedOf(list2, arg);
            return ((num >= (list2.Count - 1)) ? GetNextElementInHierarchyCore<T>(stack, getChildren, indedOf, true) : stack.Push<T>(list2[num + 1]));
        }

        private static IImmutableStack<T> GetPrevElementInHierarchyCore<T>(IImmutableStack<T> rootStack, Func<T, IList<T>> getChildren, Func<IList<T>, T, int> indedOf) where T: class
        {
            T arg = rootStack.Peek();
            Func<T, IEnumerable<T>> func = delegate (T element) {
                Func<T, T> <>9__1;
                Func<T, T> next = <>9__1;
                if (<>9__1 == null)
                {
                    Func<T, T> local1 = <>9__1;
                    next = <>9__1 = x => getChildren(x).LastOrDefault<T>();
                }
                return LinqExtensions.Unfold<T>(element, next, <>c__5<T>.<>9__5_2 ??= x => (x == null));
            };
            IImmutableStack<T> source = rootStack.Pop();
            T local2 = source.FirstOrDefault<T>();
            if (local2 == null)
            {
                return ImmutableStack.Empty<T>().PushMultiple<T>(func(arg));
            }
            IList<T> list = getChildren(local2);
            int num = indedOf(list, arg);
            return ((num <= 0) ? source : source.PushMultiple<T>(func(list[num - 1])));
        }

        public static TreeWrapper<TNew, TValue> TransformTree<T, TNew, TValue, TState>(T root, TState state, Func<T, IEnumerable<T>> getChildren, Func<TreeWrapper<TNew, TValue>[], T, TState, TValue> getValue, Func<T, TNew> getItem, Func<TState, T, TState> getFirstChildState, Func<TState, T, TState> advanceChildState)
        {
            TState childrenState = getFirstChildState(state, root);
            TreeWrapper<TNew, TValue>[] children = getChildren(root).Select<T, TreeWrapper<TNew, TValue>>(delegate (T child) {
                TreeWrapper<TNew, TValue> wrapper = TransformTree<T, TNew, TValue, TState>(child, childrenState, getChildren, getValue, getItem, getFirstChildState, advanceChildState);
                childrenState = advanceChildState(childrenState, child);
                return wrapper;
            }).ToArray<TreeWrapper<TNew, TValue>>();
            return new TreeWrapper<TNew, TValue>(getItem(root), getValue(children, root, state), children);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<T> where T: class
        {
            public static readonly TreeExtensions.<>c__1<T> <>9;
            public static Func<IImmutableStack<T>, Func<T, IList<T>>, Func<IList<T>, T, int>, IImmutableStack<T>> <>9__1_0;

            static <>c__1()
            {
                TreeExtensions.<>c__1<T>.<>9 = new TreeExtensions.<>c__1<T>();
            }

            internal IImmutableStack<T> <FlattenFromWithinForward>b__1_0(IImmutableStack<T> s, Func<T, IList<T>> gc, Func<IList<T>, T, int> io) => 
                TreeExtensions.GetNextElementInHierarchyCore<T>(s, gc, io, false);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<T> where T: class
        {
            public static readonly TreeExtensions.<>c__3<T> <>9;
            public static Func<IList<T>, T, int> <>9__3_0;
            public static Func<IImmutableStack<T>, bool> <>9__3_2;
            public static Func<IImmutableStack<T>, T> <>9__3_3;

            static <>c__3()
            {
                TreeExtensions.<>c__3<T>.<>9 = new TreeExtensions.<>c__3<T>();
            }

            internal int <FlattenFromWithin>b__3_0(IList<T> list, T item) => 
                list.IndexOf(item);

            internal bool <FlattenFromWithin>b__3_2(IImmutableStack<T> x) => 
                ReferenceEquals(x, null);

            internal T <FlattenFromWithin>b__3_3(IImmutableStack<T> x) => 
                x.Peek();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<T> where T: class
        {
            public static readonly TreeExtensions.<>c__5<T> <>9;
            public static Func<T, bool> <>9__5_2;

            static <>c__5()
            {
                TreeExtensions.<>c__5<T>.<>9 = new TreeExtensions.<>c__5<T>();
            }

            internal bool <GetPrevElementInHierarchyCore>b__5_2(T x) => 
                x == null;
        }
    }
}

