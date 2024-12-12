namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;

    public sealed class ValueNode<T> : ValueNodeBase<T>
    {
        internal ValueNode(T value, Action beginChecking, Action endChecking, NodeParent<T> parent) : base(value, beginChecking, endChecking, parent)
        {
        }

        protected override void UpdateActualIsChecked()
        {
            base.SetActualIsChecked(base.IsChecked);
        }

        public override bool HasNodes =>
            false;
    }
}

