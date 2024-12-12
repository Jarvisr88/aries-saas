namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class ColumnCollectionBase<TColumn> : ObservableCollectionCore<TColumn>, IColumnCollection, IList, ICollection, IEnumerable, INotifyCollectionChanged, ILockable, ISupportGetCachedIndex<ColumnBase> where TColumn: ColumnBase
    {
        private readonly DataControlBase owner;
        private readonly Dictionary<string, TColumn> fieldsNameCache;

        public ColumnCollectionBase(DataControlBase owner)
        {
            this.fieldsNameCache = new Dictionary<string, TColumn>();
            this.owner = owner;
        }

        [CompilerGenerated, DebuggerHidden]
        private void <>n__0(int index, TColumn item)
        {
            base.InsertItem(index, item);
        }

        protected override void ClearItems()
        {
            for (int i = 0; i < base.Count; i++)
            {
                this.OnRemoveItem(base[i]);
            }
            base.ClearItems();
            this.ResetColumnsByFieldsNameCache();
        }

        int ISupportGetCachedIndex<ColumnBase>.GetCachedIndex(ColumnBase column) => 
            base.GetCachedIndex((TColumn) column);

        ColumnBase IColumnCollection.GetColumnByName(string name) => 
            this.GetColumnByName(name);

        void IColumnCollection.OnColumnsChanged()
        {
            this.ResetColumnsByFieldsNameCache();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public override void EndUpdate()
        {
            base.EndUpdate();
            this.owner.OnColumnCollectionEndUpdate();
        }

        public TColumn GetColumnByFieldName(string fieldName)
        {
            TColumn local;
            if (string.IsNullOrEmpty(fieldName) || (base.Count == 0))
            {
                return default(TColumn);
            }
            if (!this.fieldsNameCache.TryGetValue(fieldName, out local))
            {
                using (IEnumerator<TColumn> enumerator = base.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        TColumn current = enumerator.Current;
                        if (current.FieldName == fieldName)
                        {
                            this.fieldsNameCache.Add(fieldName, current);
                            return current;
                        }
                    }
                }
            }
            return local;
        }

        public TColumn GetColumnByName(string name) => 
            ListHelper.Find<TColumn>(this, column => column.Name == name);

        protected override void InsertItem(int index, TColumn item)
        {
            item.InsertLocker.DoLockedAction(delegate {
                ((ColumnCollectionBase<TColumn>) this).OnInsertItem(item);
                ((ColumnCollectionBase<TColumn>) this).owner.OnColumnAdding(item);
                ((ColumnCollectionBase<TColumn>) this).<>n__0(index, item);
            });
            this.owner.OnColumnAdded(item);
            item.UpdateAutoFilterInitialized();
        }

        protected virtual void OnInsertItem(TColumn column)
        {
            this.owner.AddChild(column);
            column.OwnerControl = this.owner;
        }

        protected virtual void OnRemoveItem(TColumn column)
        {
            column.ClearBindingValues();
            column.OwnerControl = null;
            this.owner.RemoveChild(column);
        }

        protected override void RemoveItem(int index)
        {
            TColumn column = base[index];
            this.OnRemoveItem(column);
            this.ResetColumnsByFieldsNameCache();
            base.RemoveItem(index);
            this.owner.OnColumnRemoved(column);
        }

        protected void ResetColumnsByFieldsNameCache()
        {
            this.fieldsNameCache.Clear();
        }

        protected override void SetItem(int index, TColumn item)
        {
            this.ResetColumnsByFieldsNameCache();
            base.SetItem(index, item);
        }

        private Dictionary<string, TColumn> FieldsNameCache =>
            this.fieldsNameCache;

        public TColumn this[string fieldName] =>
            this.GetColumnByFieldName(fieldName);

        DataControlBase IColumnCollection.Owner =>
            this.owner;

        ColumnBase IColumnCollection.this[string fieldName] =>
            this[fieldName];

        ColumnBase IColumnCollection.this[int index] =>
            base[index];
    }
}

