namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class BarItemInplaceLinksHolderCollectionNode<TElement> : BarItemLinkInfoCollectionNode<TElement> where TElement: class, IBarItemLinkInfo
    {
        private BarItemLinkInfoCollectionExpandableNode<TElement> child;
        private IInplaceLinksHolder holder;

        public BarItemInplaceLinksHolderCollectionNode(BarItemLinkBase root, BarItemLinkInfoCollectionNode<TElement> parent, BaseBarItemLinkInfoFactory<TElement> factory, bool allowRecycling);
        [DebuggerHidden, CompilerGenerated]
        private IEnumerable<TElement> <>n__0();
        public override void Destroy();
        [IteratorStateMachine(typeof(BarItemInplaceLinksHolderCollectionNode<>.<EnumerateItems>d__9))]
        public override IEnumerable<TElement> EnumerateItems();
        public override void Initialize();
        protected virtual void OnHolderActualLinksChanged(object sender, ValueChangedEventArgs<BarItemLinkCollection> e);
        protected virtual void OnHolderIsExpandedChanged(object sender, ValueChangedEventArgs<bool?> e);

        private IInplaceLinksHolder Holder { get; }

        [CompilerGenerated]
        private sealed class <EnumerateItems>d__9 : IEnumerable<TElement>, IEnumerable, IEnumerator<TElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TElement <>2__current;
            private int <>l__initialThreadId;
            public BarItemInplaceLinksHolderCollectionNode<TElement> <>4__this;
            private IEnumerator<TElement> <>7__wrap1;

            [DebuggerHidden]
            public <EnumerateItems>d__9(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            TElement IEnumerator<TElement>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

