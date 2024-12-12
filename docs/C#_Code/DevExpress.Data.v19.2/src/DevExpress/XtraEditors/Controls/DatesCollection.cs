namespace DevExpress.XtraEditors.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DatesCollection : CollectionBase
    {
        public DatesCollection();
        public DatesCollection(IDatesCollectionOwner owner);
        public int Add(DateTime obj);
        public void AddRange(DatesCollection dates);
        public void BeginUpdate();
        public void CancelUpdate();
        public bool Contains(DateTime obj);
        public void EndUpdate();
        protected virtual DateTime ExtractDate(DateTime dt);
        public bool IsContinuous();
        protected override void OnClearComplete();
        protected virtual void OnCollectionChanged();
        protected override void OnInsertComplete(int index, object value);
        protected override void OnRemoveComplete(int index, object value);
        protected override void OnSetComplete(int index, object oldValue, object newValue);
        public void Remove(DateTime obj);
        public void RemoveRange(DatesCollection dates);

        protected IDatesCollectionOwner Owner { get; private set; }

        protected int LockUpdateCount { get; set; }

        protected bool IsLockUpdate { get; }

        public DateTime this[int index] { get; }
    }
}

