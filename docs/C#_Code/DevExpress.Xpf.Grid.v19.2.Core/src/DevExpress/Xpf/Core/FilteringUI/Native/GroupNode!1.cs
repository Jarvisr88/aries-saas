namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class GroupNode<T> : NodeBase<T>
    {
        private readonly GroupNodeCounters<T> counters;
        private readonly List<NodeBase<T>> ChildNodes;

        internal GroupNode(T value, Action beginChecking, Action endChecking, GroupNode<T> parent) : this(value, beginChecking, endChecking, parent?.AsNodeParent)
        {
            this.counters = new GroupNodeCounters<T>();
            this.ChildNodes = new List<NodeBase<T>>();
            this.<Nodes>k__BackingField = this.ChildNodes.AsReadOnly();
            this.<AsNodeParent>k__BackingField = new NodeParent<T>(new Action<NodeBase<T>>(this.OnChildIsVisibleChanged), new Action<NodeBase<T>, int?>(this.OnChildCountSummaryChanged), new Action<NodeBase<T>>(this.AddChild), new Action<NodeBase<T>, bool?>(this.OnChildIsCheckedChanged), new Action<NodeBase<T>, bool?>(this.OnChildActualIsCheckedChanged));
        }

        internal void AddChild(NodeBase<T> node)
        {
            this.ChildNodes.Add(node);
            this.counters.OnChildAdded(node);
            this.UpdateIsChecked();
            base.SetActualIsChecked(this.counters.GetCheckedState());
            if (node.IsVisible)
            {
                int? nullable1;
                int? count = base.Count;
                int? nullable2 = node.Count;
                if ((count != null) & (nullable2 != null))
                {
                    nullable1 = new int?(count.GetValueOrDefault() + nullable2.GetValueOrDefault());
                }
                else
                {
                    nullable1 = null;
                }
                this.Count = nullable1;
            }
        }

        protected internal override IEnumerable<NodeBase<T>> GetChildren() => 
            this.Nodes;

        protected internal override IEnumerable<NodeBase<T>> GetChildrenFast() => 
            this.Nodes;

        internal override bool HasVisibleChildren() => 
            this.counters.HasVisibleNodes();

        internal void OnChildActualIsCheckedChanged(NodeBase<T> node, bool? oldValue)
        {
            this.counters.OnChildActualIsCheckedChanged(node, oldValue);
            this.UpdateActualIsChecked();
        }

        internal void OnChildCountSummaryChanged(NodeBase<T> child, int? oldCount)
        {
            if (child.IsVisible)
            {
                int? nullable1;
                int? nullable6;
                int? count = base.Count;
                int? nullable3 = child.Count;
                int? nullable4 = oldCount;
                if ((nullable3 != null) & (nullable4 != null))
                {
                    nullable1 = new int?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault());
                }
                else
                {
                    nullable1 = null;
                }
                int? nullable2 = nullable1;
                if ((count != null) & (nullable2 != null))
                {
                    nullable6 = new int?(count.GetValueOrDefault() + nullable2.GetValueOrDefault());
                }
                else
                {
                    nullable4 = null;
                    nullable6 = nullable4;
                }
                this.Count = nullable6;
            }
        }

        internal void OnChildIsCheckedChanged(NodeBase<T> node, bool? oldValue)
        {
            this.counters.OnChildIsCheckedChanged(node, oldValue);
            this.UpdateIsChecked();
        }

        internal void OnChildIsVisibleChanged(NodeBase<T> node)
        {
            int? nullable3;
            int? nullable1;
            int? nullable5;
            this.counters.OnChildIsVisibleChanged(node);
            this.UpdateIsChecked();
            int? count = base.Count;
            if (node.IsVisible)
            {
                nullable1 = node.Count;
            }
            else
            {
                nullable3 = node.Count;
                if (nullable3 != null)
                {
                    nullable1 = new int?(-nullable3.GetValueOrDefault());
                }
                else
                {
                    nullable1 = null;
                }
            }
            int? nullable2 = nullable1;
            if ((count != null) & (nullable2 != null))
            {
                nullable5 = new int?(count.GetValueOrDefault() + nullable2.GetValueOrDefault());
            }
            else
            {
                nullable3 = null;
                nullable5 = nullable3;
            }
            this.Count = nullable5;
        }

        protected override void SetIsCheckedOnChildren()
        {
            this.counters.FlushVisibleCounters(base.IsChecked);
            Func<NodeBase<T>, bool> predicate = <>c<T>.<>9__22_0;
            if (<>c<T>.<>9__22_0 == null)
            {
                Func<NodeBase<T>, bool> local1 = <>c<T>.<>9__22_0;
                predicate = <>c<T>.<>9__22_0 = x => x.IsVisible;
            }
            foreach (NodeBase<T> base2 in this.GetChildren().Where<NodeBase<T>>(predicate))
            {
                base2.SetIsCheckedInternal(base.IsChecked, NodeBase<T>.IsCheckedUpdateMode.Children);
            }
        }

        protected override void UpdateActualIsChecked()
        {
            bool? newIsChecked = (this.ChildNodes.Count == 0) ? base.IsChecked : this.counters.GetCheckedState();
            base.SetActualIsChecked(newIsChecked);
        }

        private void UpdateIsChecked()
        {
            base.SetIsCheckedInternal(this.counters.GetVisibleCheckedState(), NodeBase<T>.IsCheckedUpdateMode.Parent);
        }

        public override bool HasNodes =>
            true;

        public ReadOnlyCollection<NodeBase<T>> Nodes { get; }

        internal NodeParent<T> AsNodeParent { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupNode<T>.<>c <>9;
            public static Func<NodeBase<T>, bool> <>9__22_0;

            static <>c()
            {
                GroupNode<T>.<>c.<>9 = new GroupNode<T>.<>c();
            }

            internal bool <SetIsCheckedOnChildren>b__22_0(NodeBase<T> x) => 
                x.IsVisible;
        }

        internal class GroupNodeCounters
        {
            private int count;
            private int visibleCount;
            private int nullCount;
            private int visibleNullCount;
            private int checkedCount;
            private int visibleCheckedCount;

            public void FlushVisibleCounters(bool? isChecked)
            {
                Guard.ArgumentNotNull(isChecked, "VisibleIsChecked");
                this.visibleCheckedCount = isChecked.Value ? this.visibleCount : 0;
                this.visibleNullCount = 0;
            }

            public bool? GetCheckedState()
            {
                Guard.ArgumentNonNegative(this.count, "count");
                Guard.ArgumentNonNegative(this.checkedCount, "checkedCount");
                Guard.ArgumentNonNegative(this.nullCount, "nullCount");
                if ((this.count != 0) && (this.checkedCount == this.count))
                {
                    return true;
                }
                if ((this.checkedCount == 0) && (this.nullCount == 0))
                {
                    return false;
                }
                return null;
            }

            public bool? GetVisibleCheckedState()
            {
                Guard.ArgumentNonNegative(this.visibleCount, "visibleCount");
                Guard.ArgumentNonNegative(this.visibleCheckedCount, "visibleCheckedCount");
                Guard.ArgumentNonNegative(this.visibleNullCount, "visibleNullCount");
                if ((this.count != 0) && (this.visibleCheckedCount == this.visibleCount))
                {
                    return true;
                }
                if ((this.visibleCheckedCount == 0) && (this.visibleNullCount == 0))
                {
                    return false;
                }
                return null;
            }

            public bool HasVisibleNodes()
            {
                Guard.ArgumentNonNegative(this.visibleCount, "visibleCount");
                return (this.visibleCount > 0);
            }

            public void OnChildActualIsCheckedChanged(NodeBase<T> childNode, bool? oldValue)
            {
                GroupNode<T>.GroupNodeCounters.OnChildActualIsCheckedChangedCore(childNode.ActualIsChecked, oldValue, this.count, ref this.checkedCount, ref this.nullCount);
            }

            private static void OnChildActualIsCheckedChangedCore(bool? isChecked, bool? oldIsChecked, int totalCount, ref int checkedCount, ref int nullCount)
            {
                if ((isChecked != null) && isChecked.Value)
                {
                    Guard.ArgumentIsInRange(0, totalCount - 1, checkedCount, "checkedCount");
                    checkedCount++;
                }
                if (isChecked == null)
                {
                    Guard.ArgumentIsInRange(0, totalCount - 1, nullCount, "nullCount");
                    nullCount++;
                }
                if ((oldIsChecked != null) && oldIsChecked.Value)
                {
                    Guard.ArgumentPositive(checkedCount, "checkedCount");
                    checkedCount--;
                }
                if (oldIsChecked == null)
                {
                    Guard.ArgumentPositive(nullCount, "nullCount");
                    nullCount--;
                }
            }

            public void OnChildAdded(NodeBase<T> child)
            {
                this.count++;
                if (child.IsVisible)
                {
                    this.visibleCount++;
                }
                this.OnChildIsCheckedChanged(child, false);
                this.OnChildActualIsCheckedChanged(child, false);
            }

            public void OnChildIsCheckedChanged(NodeBase<T> childNode, bool? oldValue)
            {
                if (childNode.IsVisible)
                {
                    GroupNode<T>.GroupNodeCounters.OnChildActualIsCheckedChangedCore(childNode.IsChecked, oldValue, this.visibleCount, ref this.visibleCheckedCount, ref this.visibleNullCount);
                }
            }

            public void OnChildIsVisibleChanged(NodeBase<T> childNode)
            {
                if (!childNode.IsVisible)
                {
                    Guard.ArgumentPositive(this.visibleCount, "visibleCount");
                    this.visibleCount--;
                    Guard.ArgumentNonNegative(this.visibleCount, "visibleCount");
                    if ((childNode.IsChecked != null) && childNode.IsChecked.Value)
                    {
                        Guard.ArgumentPositive(this.visibleCheckedCount, "visibleCount");
                        this.visibleCheckedCount--;
                    }
                }
                else
                {
                    Guard.ArgumentIsInRange(0, this.count - 1, this.visibleCount, "visibleCount");
                    this.visibleCount++;
                    if (childNode.IsChecked == null)
                    {
                        Guard.ArgumentIsInRange(0, this.count - 1, this.visibleNullCount, "visibleNullCount");
                        this.visibleNullCount++;
                    }
                    else if (childNode.IsChecked.Value)
                    {
                        Guard.ArgumentIsInRange(0, this.count - 1, this.visibleCheckedCount, "visibleCheckedCount");
                        this.visibleCheckedCount++;
                    }
                }
            }
        }
    }
}

