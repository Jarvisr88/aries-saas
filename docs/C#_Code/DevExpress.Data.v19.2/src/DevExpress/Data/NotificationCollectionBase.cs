namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NotificationCollectionBase : CollectionBase
    {
        private int lockUpdate;

        public event CollectionChangeEventHandler CollectionChanged;

        public NotificationCollectionBase();
        public NotificationCollectionBase(CollectionChangeEventHandler collectionChanged);
        public void BeginUpdate();
        public void CancelUpdate();
        public void EndUpdate();
        protected override void OnClear();
        protected virtual void OnCollectionChanged(CollectionChangeEventArgs e);
        protected override void OnInsertComplete(int index, object value);
        protected override void OnRemoveComplete(int index, object value);
        internal void Reset();

        protected bool IsLockUpdate { get; }
    }
}

