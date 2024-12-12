namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;

    public class BarContainerControlCollection : ObservableCollection<BarContainerControl>
    {
        private int updating;
        private BarManager manager;

        public BarContainerControlCollection(BarManager manager);
        public void BeginUpdate();
        protected override void ClearItems();
        public void EndUpdate();
        private IEnumerable<BarContainerControl> Find(Func<BarContainerControl, bool> predicate);
        public BarContainerControl FindByType(BarContainerType dock);
        protected override void InsertItem(int index, BarContainerControl container);
        protected internal virtual void OnBarsChanged();
        protected virtual void OnInsertItem(BarContainerControl container);
        protected virtual void OnRemoveItem(BarContainerControl container);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, BarContainerControl item);

        public bool IsUpdating { get; }

        public BarManager Manager { get; }

        public BarContainerControl this[string name] { get; }
    }
}

