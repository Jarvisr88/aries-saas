namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public sealed class GroupValueNode<T> : ValueNodeBase<T>
    {
        private GroupNode<T>.GroupNodeCounters counters;
        private Lazy<ReadOnlyCollection<NodeBase<T>>> nodes;

        internal GroupValueNode(T value, Action beginChecking, Action endChecking, NodeParent<T> parent, Func<Task<ICollection<NodeBase<T>>>> getChildren) : base(value, beginChecking, endChecking, parent)
        {
            if (getChildren != null)
            {
                this.nodes = new Lazy<ReadOnlyCollection<NodeBase<T>>>(delegate {
                    ObservableCollectionCore<NodeBase<T>> childrenList = new ObservableCollectionCore<NodeBase<T>>();
                    ((GroupValueNode<T>) this).ProcessChildren(childrenList, getChildren);
                    return new ReadOnlyObservableCollection<NodeBase<T>>(childrenList);
                });
            }
        }

        internal void OnChildActualIsCheckedChanged(NodeBase<T> child, bool? oldValue)
        {
            if (this.NodesLoaded)
            {
                this.counters.OnChildActualIsCheckedChanged(child, oldValue);
                this.UpdateActualIsChecked();
            }
        }

        internal void OnChildIsCheckedChanged(NodeBase<T> child, bool? oldValue)
        {
            if (this.NodesLoaded)
            {
                this.counters.OnChildIsCheckedChanged(child, oldValue);
                base.SetIsCheckedInternal(this.counters.GetVisibleCheckedState(), NodeBase<T>.IsCheckedUpdateMode.Parent);
            }
        }

        [AsyncStateMachine(typeof(<ProcessChildren>d__9))]
        private void ProcessChildren(ObservableCollectionCore<NodeBase<T>> childrenList, Func<Task<ICollection<NodeBase<T>>>> getChildren)
        {
            <ProcessChildren>d__9<T> d__;
            d__.<>4__this = (GroupValueNode<T>) this;
            d__.childrenList = childrenList;
            d__.getChildren = getChildren;
            d__.<>t__builder = AsyncVoidMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ProcessChildren>d__9<T>>(ref d__);
        }

        protected override void SetIsCheckedOnChildren()
        {
            if (this.HasNodes && this.NodesLoaded)
            {
                this.counters.FlushVisibleCounters(base.IsChecked);
                foreach (NodeBase<T> base2 in this.Nodes)
                {
                    base2.SetIsCheckedInternal(base.IsChecked, NodeBase<T>.IsCheckedUpdateMode.Children);
                }
            }
        }

        protected override void UpdateActualIsChecked()
        {
            this.SetActualIsChecked((!this.HasNodes || !this.NodesLoaded) ? base.IsChecked : this.counters.GetCheckedState());
        }

        public override bool HasNodes =>
            this.nodes != null;

        public ReadOnlyCollection<NodeBase<T>> Nodes
        {
            get
            {
                if (this.nodes != null)
                {
                    return this.nodes.Value;
                }
                Lazy<ReadOnlyCollection<NodeBase<T>>> nodes = this.nodes;
                return null;
            }
        }

        private bool NodesLoaded =>
            (this.nodes == null) || (this.counters != null);

        [CompilerGenerated]
        private struct <ProcessChildren>d__9 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public Func<Task<ICollection<NodeBase<T>>>> getChildren;
            public ObservableCollectionCore<NodeBase<T>> childrenList;
            public GroupValueNode<T> <>4__this;
            private TaskAwaiter<ICollection<NodeBase<T>>> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<ICollection<NodeBase<T>>> awaiter;
                    ICollection<NodeBase<T>> is4;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<ICollection<NodeBase<T>>>();
                        this.<>1__state = num = -1;
                        goto TR_000F;
                    }
                    else
                    {
                        awaiter = this.getChildren().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_000F;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ICollection<NodeBase<T>>>, GroupValueNode<T>.<ProcessChildren>d__9>(ref awaiter, ref (GroupValueNode<T>.<ProcessChildren>d__9) ref this);
                        }
                    }
                    return;
                TR_000F:
                    is4 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<ICollection<NodeBase<T>>>();
                    ICollection<NodeBase<T>> is2 = is4;
                    GroupNode<T>.GroupNodeCounters counters = new GroupNode<T>.GroupNodeCounters();
                    this.childrenList.BeginUpdate();
                    IEnumerator<NodeBase<T>> enumerator = is2.GetEnumerator();
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            NodeBase<T> current = enumerator.Current;
                            this.childrenList.Add(current);
                            bool? isChecked = this.<>4__this.IsChecked;
                            bool flag = true;
                            if ((isChecked.GetValueOrDefault() == flag) ? (isChecked != null) : false)
                            {
                                current.IsChecked = true;
                            }
                            counters.OnChildAdded(current);
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (enumerator != null))
                        {
                            enumerator.Dispose();
                        }
                    }
                    this.childrenList.EndUpdate();
                    this.<>4__this.counters = counters;
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

