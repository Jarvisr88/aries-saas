namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class BarItemCollection : ObservableCollection<BarItem>
    {
        private int updating;

        public event BeforeRemoveBarItemEventHandler AfterInsert;

        public event BeforeRemoveBarItemEventHandler BeforeRemove;

        public event EventHandler OnBeginUpdate;

        public event EventHandler OnEndUpdate;

        public void BeginUpdate();
        protected override void ClearItems();
        public void EndUpdate();
        protected override void InsertItem(int index, BarItem barItem);
        protected internal virtual void OnBarItemCollectionChanged();
        protected internal virtual void OnInsertItem(BarItem barItem, int index);
        protected virtual void OnRemoveItem(BarItem barItem, int index);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, BarItem item);

        public BarItem this[string name] { get; }

        [Description("Returns true if the BeginUpdate method has been called, but the corresponding EndUpdate method hasn't been called yet.")]
        public bool IsUpdating { get; }
    }
}

