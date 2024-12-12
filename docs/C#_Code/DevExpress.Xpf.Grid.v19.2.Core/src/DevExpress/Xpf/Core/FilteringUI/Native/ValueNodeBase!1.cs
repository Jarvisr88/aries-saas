namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;

    public abstract class ValueNodeBase<T> : NodeBase<T>
    {
        internal ValueNodeBase(T value, Action beginChecking, Action endChecking, NodeParent<T> parent) : base(value, beginChecking, endChecking, parent)
        {
        }

        internal void SetCount(int? count)
        {
            base.Count = count;
        }
    }
}

