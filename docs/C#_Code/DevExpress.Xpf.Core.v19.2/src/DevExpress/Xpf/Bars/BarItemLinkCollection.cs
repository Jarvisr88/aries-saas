namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class BarItemLinkCollection : SimpleLinkCollection, INotifyLockableChanged, ILockable
    {
        private ILinksHolder holder;
        private readonly Locker insertItemLocker;
        private readonly Locker removeItemLocker;
        internal bool lockLinkPropertyChanges;
        private readonly Locker addItemLocker;
        private BarItemLinkBase link;
        private int lockCount;

        event EventHandler INotifyLockableChanged.OnBeginUpdate;

        event EventHandler INotifyLockableChanged.OnEndUpdate;

        private event EventHandler onBeginUpdate;

        private event EventHandler onEndUpdate;

        public BarItemLinkCollection();
        public BarItemLinkCollection(ILinksHolder holder);
        public BarItemLinkBase Add(BarItem item);
        protected override void ClearItems();
        protected void ClearLink(BarItemLinkBase link);
        public virtual bool Contains(BarItem item);
        void ILockable.BeginUpdate();
        void ILockable.EndUpdate();
        public BarItemLinkBase Insert(int index, BarItem item);
        protected override void InsertItem(int index, BarItemLinkBase itemLink);
        protected internal IDisposable LockItemInsertationToCommonCollection();
        protected internal IDisposable LockItemRemovalFromCommonCollection();
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e);
        internal void RaiseReset();
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, BarItemLinkBase item);
        private void UpdateItemLinkOnAddToCollection(BarItemLinkBase itemLink);

        protected virtual bool EnableLinkCollectionLogic { get; }

        [Description("Refers to the container that displays the contents of the current collection.")]
        public ILinksHolder Holder { get; }

        public BarItemLink this[string name] { get; }

        bool ILockable.IsLockUpdate { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkCollection.<>c <>9;
            public static Action<BarItemLinkBase> <>9__4_1;

            static <>c();
            internal void <.ctor>b__4_1(BarItemLinkBase x);
        }

        private class UpdateHasVisibleLinksAction : IAggregateAction, IAction
        {
            public bool CanAggregate(IAction action);
            public void Execute();
        }
    }
}

