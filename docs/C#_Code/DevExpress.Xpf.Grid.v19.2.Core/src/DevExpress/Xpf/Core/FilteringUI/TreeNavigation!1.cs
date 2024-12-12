namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class TreeNavigation<T> where T: class
    {
        private readonly NavigationNodeClient<T> client;
        private TreeNavigationState<T> stateCore;

        public TreeNavigation(T root, NavigationNodeClient<T> client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            this.<Root>k__BackingField = root;
            this.client = client;
        }

        private static T[] Concat(T node, T[] nodes) => 
            node.Yield<T>().Concat<T>(nodes).ToArray<T>();

        private static T FindFirstNode(T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindFirstNodeWithValidIndices(TreeNavigation<T>.GetLinearNodeSequence(root, root, path => path.FlattenFromWithinForward<T>((Func<T, IList<T>>) client.GetChildren, null), client), client);

        private static T FindFirstNodeWithValidIndices(IEnumerable<T> nodes, NavigationNodeClient<T> client) => 
            nodes.FirstOrDefault<T>(x => client.GetPaths(x).Any<NavigationPath>());

        private static T FindNextNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindRelativeNode(state, root, path => path.FlattenFromWithinForward<T>((Func<T, IList<T>>) client.GetChildren, null), client);

        private static T[] FindNodePath(T target, T root, Func<T, T[]> getChildren) => 
            TreeNavigation<T>.FindNodePath(target, root, new T[0], getChildren);

        private static T[] FindNodePath(T target, T currentNode, T[] currentNodePath, Func<T, T[]> getChildren)
        {
            T[] local4;
            if (currentNode == target)
            {
                local4 = TreeNavigation<T>.Concat(currentNode, currentNodePath);
            }
            else
            {
                Func<T[], bool> predicate = <>c<T>.<>9__40_1;
                if (<>c<T>.<>9__40_1 == null)
                {
                    Func<T[], bool> local1 = <>c<T>.<>9__40_1;
                    predicate = <>c<T>.<>9__40_1 = nodePath => nodePath.Any<T>();
                }
                T[] local2 = (from node in getChildren(currentNode) select TreeNavigation<T>.FindNodePath(target, node, TreeNavigation<T>.Concat(currentNode, currentNodePath), getChildren)).FirstOrDefault<T[]>(predicate);
                local4 = local2;
                if (local2 == null)
                {
                    T[] local3 = local2;
                    return new T[0];
                }
            }
            return local4;
        }

        private static T FindPreviousNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindRelativeNode(state, root, path => path.FlattenFromWithinBackward<T>((Func<T, IList<T>>) client.GetChildren, null), client);

        private static T FindRelativeNode(TreeNavigationState<T> state, T root, Func<IEnumerable<T>, IEnumerable<T>> flatten, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindFirstNodeWithValidIndices(TreeNavigation<T>.GetLinearNodeSequence(state.Node, root, flatten, client).Skip<T>(1), client);

        private static IEnumerable<T> GetLinearNodeSequence(T node, T root, Func<IEnumerable<T>, IEnumerable<T>> flatten, NavigationNodeClient<T> client) => 
            flatten(TreeNavigation<T>.FindNodePath(node, root, client.GetChildren));

        private static bool HasPath(T node, NavigationPath path, NavigationNodeClient<T> client) => 
            client.GetPaths(node).Any<NavigationPath>(p => NavigationPathExtensions.AreSame(p, path));

        private static bool IsCurrentIndexValid(TreeNavigationState<T> state, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.HasPath(state.Node, state.Path, client);

        public bool IsNavigated(T node)
        {
            T local2;
            if (node == null)
            {
                return false;
            }
            TreeNavigationState<T> state = this.State;
            if (state != null)
            {
                local2 = state.Node;
            }
            else
            {
                TreeNavigationState<T> local1 = state;
                local2 = default(T);
            }
            return (local2 == node);
        }

        public void MoveDown()
        {
            if ((this.State != null) && (this.Root != null))
            {
                TreeNavigationState<T> state1 = TreeNavigation<T>.MoveToSameIndexOnNextNode(this.State, this.Root, this.client);
                TreeNavigationState<T> state = state1;
                if (state1 == null)
                {
                    TreeNavigationState<T> local1 = state1;
                    TreeNavigationState<T> state2 = TreeNavigation<T>.MoveToLastIndexOnNextNode(this.State, this.Root, this.client);
                    state = state2;
                    if (state2 == null)
                    {
                        TreeNavigationState<T> local2 = state2;
                        state = this.State;
                    }
                }
                this.State = state;
            }
        }

        public void MoveFirst()
        {
            if (this.Root != null)
            {
                this.State = TreeNavigation<T>.MoveToFirstIndexOnFirstNode(this.Root, this.client);
            }
        }

        public void MoveNext()
        {
            if ((this.State != null) && (this.Root != null))
            {
                TreeNavigationState<T> state1 = TreeNavigation<T>.MoveToNextIndexInsideNode(this.State, this.client.GetPaths);
                TreeNavigationState<T> state = state1;
                if (state1 == null)
                {
                    TreeNavigationState<T> local1 = state1;
                    TreeNavigationState<T> state2 = TreeNavigation<T>.MoveToFirstIndexOnNextNode(this.State, this.Root, this.client);
                    state = state2;
                    if (state2 == null)
                    {
                        TreeNavigationState<T> local2 = state2;
                        TreeNavigationState<T> state3 = TreeNavigation<T>.MoveToFirstIndexInsideNode(this.State.Node, this.client.GetPaths);
                        state = state3;
                        if (state3 == null)
                        {
                            TreeNavigationState<T> local3 = state3;
                            state = this.State;
                        }
                    }
                }
                this.State = state;
            }
        }

        public void MovePrevious()
        {
            if ((this.State != null) && (this.Root != null))
            {
                TreeNavigationState<T> state1 = TreeNavigation<T>.MoveToPreviousIndexInsideNode(this.State, this.client.GetPaths);
                TreeNavigationState<T> state = state1;
                if (state1 == null)
                {
                    TreeNavigationState<T> local1 = state1;
                    TreeNavigationState<T> state2 = TreeNavigation<T>.MoveToLastIndexOnPreviousNode(this.State, this.Root, this.client);
                    state = state2;
                    if (state2 == null)
                    {
                        TreeNavigationState<T> local2 = state2;
                        TreeNavigationState<T> state3 = TreeNavigation<T>.MoveToLastIndexInsideNode(this.State.Node, this.client.GetPaths);
                        state = state3;
                        if (state3 == null)
                        {
                            TreeNavigationState<T> local3 = state3;
                            state = this.State;
                        }
                    }
                }
                this.State = state;
            }
        }

        public void MoveToFirstIndex()
        {
            if ((this.Root != null) && (this.State != null))
            {
                this.MoveToFirstIndex(this.State.Node);
            }
        }

        public void MoveToFirstIndex(T node)
        {
            if ((this.Root != null) && (node != null))
            {
                TreeNavigationState<T> state1 = TreeNavigation<T>.MoveToFirstIndexInsideNode(node, this.client.GetPaths);
                TreeNavigationState<T> state = state1;
                if (state1 == null)
                {
                    TreeNavigationState<T> local1 = state1;
                    state = this.State;
                }
                this.State = state;
            }
        }

        private static TreeNavigationState<T> MoveToFirstIndexInsideNode(T node, Func<T, IReadOnlyCollection<NavigationPath>> getPaths) => 
            getPaths(node).LeftMost().With<NavigationPath, TreeNavigationState<T>>(index => new TreeNavigationState<T>(node, index));

        private static TreeNavigationState<T> MoveToFirstIndexOnFirstNode(T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindFirstNode(root, client).With<T, TreeNavigationState<T>>(node => new TreeNavigationState<T>(node, client.GetPaths(node).LeftMost()));

        private static TreeNavigationState<T> MoveToFirstIndexOnNextNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindNextNode(state, root, client).With<T, TreeNavigationState<T>>(node => new TreeNavigationState<T>(node, client.GetPaths(node).LeftMost()));

        private static TreeNavigationState<T> MoveToFirstIndexOnPreviousNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindPreviousNode(state, root, client).With<T, TreeNavigationState<T>>(node => new TreeNavigationState<T>(node, client.GetPaths(node).LeftMost()));

        public void MoveToLastIndex()
        {
            if ((this.Root != null) && (this.State != null))
            {
                this.MoveToLastIndex(this.State.Node);
            }
        }

        public void MoveToLastIndex(T node)
        {
            if ((this.Root != null) && (node != null))
            {
                TreeNavigationState<T> state1 = TreeNavigation<T>.MoveToLastIndexInsideNode(node, this.client.GetPaths);
                TreeNavigationState<T> state = state1;
                if (state1 == null)
                {
                    TreeNavigationState<T> local1 = state1;
                    state = this.State;
                }
                this.State = state;
            }
        }

        private static TreeNavigationState<T> MoveToLastIndexInsideNode(T node, Func<T, IReadOnlyCollection<NavigationPath>> getPaths) => 
            getPaths(node).RightMost().With<NavigationPath, TreeNavigationState<T>>(index => new TreeNavigationState<T>(node, index));

        private static TreeNavigationState<T> MoveToLastIndexOnNextNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindNextNode(state, root, client).With<T, TreeNavigationState<T>>(node => new TreeNavigationState<T>(node, client.GetPaths(node).RightMost()));

        private static TreeNavigationState<T> MoveToLastIndexOnPreviousNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindPreviousNode(state, root, client).With<T, TreeNavigationState<T>>(node => new TreeNavigationState<T>(node, client.GetPaths(node).RightMost()));

        public void MoveToNearestBranch()
        {
            if ((this.State != null) && (this.Root != null))
            {
                T node;
                NavigationNodeClient<T> client = new NavigationNodeClient<T>(this.client.GetPaths, delegate (T node) {
                    T local2;
                    TreeNavigationState<T> state1 = base.State;
                    if (state1 != null)
                    {
                        local2 = state1.Node;
                    }
                    else
                    {
                        TreeNavigationState<T> local1 = state1;
                        local2 = default(T);
                    }
                    return (local2 == node) ? new T[0] : base.client.GetChildren(node);
                }, this.client.SetIsFocused);
                TreeNavigationState<T> state = TreeNavigation<T>.MoveToFirstIndexOnNextNode(this.State, this.Root, client);
                if (state != null)
                {
                    node = state.Node;
                }
                else
                {
                    node = default(T);
                }
                if (node == TreeNavigation<T>.FindFirstNode(this.Root, client))
                {
                    state = TreeNavigation<T>.MoveToFirstIndexOnPreviousNode(this.State, this.Root, client);
                }
                if (state != null)
                {
                    this.State = state;
                }
            }
        }

        private static TreeNavigationState<T> MoveToNextIndexInsideNode(TreeNavigationState<T> state, Func<T, IReadOnlyCollection<NavigationPath>> getPaths) => 
            (from x in getPaths(state.Node)
                where x.IsToTheRight(state.Path)
                select x).LeftMost().With<NavigationPath, TreeNavigationState<T>>(index => new TreeNavigationState<T>(state.Node, index));

        private static TreeNavigationState<T> MoveToPreviousIndexInsideNode(TreeNavigationState<T> state, Func<T, IReadOnlyCollection<NavigationPath>> getPaths) => 
            (from x in getPaths(state.Node)
                where x.IsToTheLeft(state.Path)
                select x).RightMost().With<NavigationPath, TreeNavigationState<T>>(index => new TreeNavigationState<T>(state.Node, index));

        private static TreeNavigationState<T> MoveToSameIndexOnNextNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindNextNode(state, root, client).If<T>(node => TreeNavigation<T>.HasPath(node, state.Path, client)).With<T, TreeNavigationState<T>>(node => new TreeNavigationState<T>(node, state.Path));

        private static TreeNavigationState<T> MoveToSameIndexOnPreviousNode(TreeNavigationState<T> state, T root, NavigationNodeClient<T> client) => 
            TreeNavigation<T>.FindPreviousNode(state, root, client).If<T>(node => TreeNavigation<T>.HasPath(node, state.Path, client)).With<T, TreeNavigationState<T>>(node => new TreeNavigationState<T>(node, state.Path));

        public void MoveUp()
        {
            if ((this.State != null) && (this.Root != null))
            {
                TreeNavigationState<T> state1 = TreeNavigation<T>.MoveToSameIndexOnPreviousNode(this.State, this.Root, this.client);
                TreeNavigationState<T> state = state1;
                if (state1 == null)
                {
                    TreeNavigationState<T> local1 = state1;
                    TreeNavigationState<T> state2 = TreeNavigation<T>.MoveToLastIndexOnPreviousNode(this.State, this.Root, this.client);
                    state = state2;
                    if (state2 == null)
                    {
                        TreeNavigationState<T> local2 = state2;
                        state = this.State;
                    }
                }
                this.State = state;
            }
        }

        private void OnStateChanged(TreeNavigationState<T> oldValue, TreeNavigationState<T> newValue)
        {
            if ((oldValue != null) && !TreeNavigationState<T>.AreSame(oldValue, newValue))
            {
                this.client.SetIsFocused(oldValue, false);
            }
            if (newValue != null)
            {
                this.client.SetIsFocused(newValue, true);
            }
        }

        public void Restore()
        {
            if (this.Root != null)
            {
                if (this.State == null)
                {
                    this.MoveFirst();
                }
                else
                {
                    this.OnStateChanged(null, this.State);
                }
            }
        }

        public T Root { get; }

        public TreeNavigationState<T> State
        {
            get => 
                this.stateCore;
            set
            {
                if (!ReferenceEquals(this.stateCore, value))
                {
                    TreeNavigationState<T> stateCore = this.stateCore;
                    this.stateCore = value;
                    this.OnStateChanged(stateCore, this.stateCore);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TreeNavigation<T>.<>c <>9;
            public static Func<T[], bool> <>9__40_1;

            static <>c()
            {
                TreeNavigation<T>.<>c.<>9 = new TreeNavigation<T>.<>c();
            }

            internal bool <FindNodePath>b__40_1(T[] nodePath) => 
                nodePath.Any<T>();
        }
    }
}

