namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class BarItemLinkInfoCollectionExpandableNode<TElement> : BarItemLinkInfoCollectionNode<TElement> where TElement: class, IBarItemLinkInfo
    {
        private IList<BarItemLinkBase> source;
        private List<BarItemLinkInfoCollectionNode<TElement>> children;
        private Dictionary<BarItemLinkBase, BarItemLinkInfoCollectionNode<TElement>> childrenDictionary;

        public BarItemLinkInfoCollectionExpandableNode(BarItemLinkBase root, BarItemLinkInfoCollectionNode<TElement> parent, IList<BarItemLinkBase> source, BaseBarItemLinkInfoFactory<TElement> factory, bool allowRecycling);
        private void ClearChildren();
        public override void Destroy();
        [IteratorStateMachine(typeof(BarItemLinkInfoCollectionExpandableNode<>.<EnumerateItems>d__14))]
        public override IEnumerable<TElement> EnumerateItems();
        public override void Initialize();
        private void Insert(BarItemLinkBase element, int index);
        protected internal override void OnChildNodeChanged(BarItemLinkInfoCollectionNode<TElement> node);
        private void OnSourceCollectionBeginUpdate(object sender, EventArgs e);
        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        private void OnSourceCollectionEndUpdate(object sender, EventArgs e);
        private void Remove(BarItemLinkBase element, int index);
        private void Reset();

        [CompilerGenerated]
        private sealed class <EnumerateItems>d__14 : IEnumerable<TElement>, IEnumerable, IEnumerator<TElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TElement <>2__current;
            private int <>l__initialThreadId;
            public BarItemLinkInfoCollectionExpandableNode<TElement> <>4__this;
            private List<BarItemLinkInfoCollectionNode<TElement>>.Enumerator <>7__wrap1;
            private IEnumerator<TElement> <>7__wrap2;

            [DebuggerHidden]
            public <EnumerateItems>d__14(int <>1__state);
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

