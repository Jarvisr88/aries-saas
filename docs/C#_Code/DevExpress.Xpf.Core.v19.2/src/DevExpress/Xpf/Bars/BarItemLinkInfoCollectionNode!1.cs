namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarItemLinkInfoCollectionNode<TElement> where TElement: class, IBarItemLinkInfo
    {
        private BarItemLinkBase root;
        private BarItemLinkInfoCollectionNode<TElement> parent;
        private TElement linkInfo;
        private bool isDestroyed;
        private bool allowRecycling;
        private BaseBarItemLinkInfoFactory<TElement> factory;

        public BarItemLinkInfoCollectionNode(BarItemLinkBase root, BarItemLinkInfoCollectionNode<TElement> parent, BaseBarItemLinkInfoFactory<TElement> factory, bool allowRecycling);
        protected BarItemLinkInfoCollectionNode<TElement> CreateNode(BarItemLinkBase linkBase);
        public virtual void Destroy();
        [IteratorStateMachine(typeof(BarItemLinkInfoCollectionNode<>.<EnumerateItems>d__38))]
        public virtual IEnumerable<TElement> EnumerateItems();
        public int GetIndex();
        public TElement GetLinkInfo();
        protected virtual bool GetShouldCreateLinkInfo();
        public virtual void Initialize();
        protected IDisposable LockCollectionChanges();
        protected internal virtual void OnChildNodeChanged(BarItemLinkInfoCollectionNode<TElement> node);
        private void OnIsRemovedChanged(object sender, RoutedEventArgs e);
        protected virtual void OnItemChanged(object sender, ValueChangedEventArgs<BarItem> e);
        protected void UnlockCollectionChanges();

        public BarItemLinkInfoCollectionNode<TElement> ParentNode { get; }

        public BarItemLinkInfoCollectionRootNode<TElement> RootNode { get; }

        public BarItemLinkBase LinkBase { get; }

        public BarItemLink Link { get; }

        public BarItem Item { get; }

        protected bool IsDestroyed { get; }

        public bool ShouldCreateLinkInfo { get; private set; }

        protected BaseBarItemLinkInfoFactory<TElement> Factory { [DebuggerStepThrough] get; }

        protected bool AllowRecycling { [DebuggerStepThrough] get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkInfoCollectionNode<TElement>.<>c <>9;
            public static Func<BarItemLinkInfoCollectionNode<TElement>, BarItemLinkInfoCollectionRootNode<TElement>> <>9__6_0;
            public static Func<BarItemLink, BarItem> <>9__12_0;

            static <>c();
            internal BarItem <get_Item>b__12_0(BarItemLink x);
            internal BarItemLinkInfoCollectionRootNode<TElement> <get_RootNode>b__6_0(BarItemLinkInfoCollectionNode<TElement> x);
        }

        [CompilerGenerated]
        private sealed class <EnumerateItems>d__38 : IEnumerable<TElement>, IEnumerable, IEnumerator<TElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TElement <>2__current;
            private int <>l__initialThreadId;
            public BarItemLinkInfoCollectionNode<TElement> <>4__this;

            [DebuggerHidden]
            public <EnumerateItems>d__38(int <>1__state);
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

