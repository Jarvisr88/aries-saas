namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.FilteringUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class NodeBase<T> : BindableFast
    {
        private readonly Action beginChecking;
        private readonly Action endChecking;
        private bool? isCheckedCore;
        private bool isVisibleCore;
        private int? countCore;
        private bool shouldUpdateCountProperty;
        private bool isExpanded;

        internal NodeBase(T value, Action beginChecking, Action endChecking, NodeParent<T> parent)
        {
            this.isCheckedCore = false;
            this.beginChecking = beginChecking;
            this.endChecking = endChecking;
            this.<Value>k__BackingField = value;
            this.ActualIsChecked = false;
            this.Count = 0;
            this.IsVisible = true;
            this.<Parent>k__BackingField = parent;
            NodeParent<T> parent1 = this.Parent;
            if (parent1 == null)
            {
                NodeParent<T> local1 = parent1;
            }
            else
            {
                parent1.OnChildCreated((NodeBase<T>) this);
            }
        }

        protected internal virtual IEnumerable<NodeBase<T>> GetChildren() => 
            Enumerable.Empty<NodeBase<T>>();

        protected internal virtual IEnumerable<NodeBase<T>> GetChildrenFast() => 
            null;

        internal virtual bool HasVisibleChildren() => 
            false;

        internal void RaiseCountChangedIfNeeded()
        {
            if (this.shouldUpdateCountProperty)
            {
                this.shouldUpdateCountProperty = false;
                if (this.IsVisible)
                {
                    base.RaisePropertyChanged("Count");
                }
            }
        }

        protected void SetActualIsChecked(bool? newIsChecked)
        {
            bool? actualIsChecked = this.ActualIsChecked;
            bool? nullable2 = newIsChecked;
            if ((actualIsChecked.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((actualIsChecked != null) != (nullable2 != null)) : true)
            {
                bool? nullable3 = this.ActualIsChecked;
                this.ActualIsChecked = newIsChecked;
                NodeParent<T> parent = this.Parent;
                if (parent == null)
                {
                    NodeParent<T> local1 = parent;
                }
                else
                {
                    parent.OnChildActualIsCheckedChanged((NodeBase<T>) this, nullable3);
                }
            }
        }

        internal void SetIsCheckedInternal(bool? value, IsCheckedUpdateMode<T> updateMode)
        {
            bool? isChecked = this.IsChecked;
            bool? nullable3 = value;
            if (!((isChecked.GetValueOrDefault() == nullable3.GetValueOrDefault()) ? ((isChecked != null) == (nullable3 != null)) : false))
            {
                bool? nullable = this.IsChecked;
                base.SetValue<bool?>(ref this.isCheckedCore, value, "IsChecked");
                if (updateMode.HasFlag(IsCheckedUpdateMode<T>.Children))
                {
                    this.SetIsCheckedOnChildren();
                }
                if (updateMode.HasFlag(IsCheckedUpdateMode<T>.Parent))
                {
                    NodeParent<T> parent = this.Parent;
                    if (parent == null)
                    {
                        NodeParent<T> local1 = parent;
                    }
                    else
                    {
                        parent.OnChildIsCheckedChanged((NodeBase<T>) this, nullable);
                    }
                }
                this.UpdateActualIsChecked();
            }
        }

        protected virtual void SetIsCheckedOnChildren()
        {
        }

        protected abstract void UpdateActualIsChecked();

        public T Value { get; }

        internal NodeParent<T> Parent { get; }

        public bool? IsChecked
        {
            get => 
                this.isCheckedCore;
            set
            {
                bool? isChecked = this.IsChecked;
                bool? nullable2 = value;
                if (!((isChecked.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((isChecked != null) == (nullable2 != null)) : false))
                {
                    Guard.ArgumentNotNull(value, "value");
                    if (this.beginChecking == null)
                    {
                        Action beginChecking = this.beginChecking;
                    }
                    else
                    {
                        this.beginChecking();
                    }
                    this.SetIsCheckedInternal(value, IsCheckedUpdateMode<T>.ParentAndChildren);
                    if (this.endChecking == null)
                    {
                        Action endChecking = this.endChecking;
                    }
                    else
                    {
                        this.endChecking();
                    }
                }
            }
        }

        internal bool? ActualIsChecked { get; private set; }

        public bool IsVisible
        {
            get => 
                this.isVisibleCore;
            internal set
            {
                if (this.isVisibleCore != value)
                {
                    this.isVisibleCore = value;
                    NodeParent<T> parent = this.Parent;
                    if (parent == null)
                    {
                        NodeParent<T> local1 = parent;
                    }
                    else
                    {
                        parent.OnChildIsVisibleChanged((NodeBase<T>) this);
                    }
                }
            }
        }

        public int? Count
        {
            get => 
                this.countCore;
            protected set
            {
                int? countCore = this.countCore;
                int? nullable3 = value;
                if (!((countCore.GetValueOrDefault() == nullable3.GetValueOrDefault()) ? ((countCore != null) == (nullable3 != null)) : false))
                {
                    int? nullable = this.countCore;
                    this.countCore = value;
                    this.shouldUpdateCountProperty = true;
                    NodeParent<T> parent = this.Parent;
                    if (parent == null)
                    {
                        NodeParent<T> local1 = parent;
                    }
                    else
                    {
                        parent.OnChildCountSummaryChanged((NodeBase<T>) this, nullable);
                    }
                }
            }
        }

        public bool ShowCount =>
            this.Count != null;

        public bool IsExpanded
        {
            get => 
                this.isExpanded;
            set => 
                base.SetValue<bool>(ref this.isExpanded, value, "IsExpanded");
        }

        public abstract bool HasNodes { get; }

        internal enum IsCheckedUpdateMode
        {
            public const NodeBase<T>.IsCheckedUpdateMode Parent = NodeBase<T>.IsCheckedUpdateMode.Parent;,
            public const NodeBase<T>.IsCheckedUpdateMode Children = NodeBase<T>.IsCheckedUpdateMode.Children;,
            public const NodeBase<T>.IsCheckedUpdateMode ParentAndChildren = NodeBase<T>.IsCheckedUpdateMode.ParentAndChildren;
        }
    }
}

