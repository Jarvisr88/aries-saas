namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public class BarCollection : SimpleBarCollection<Bar>
    {
        private int updating;
        private BarManager manager;

        public BarCollection(BarManager manager);
        public void BeginUpdate();
        protected override void ClearItems();
        public void EndUpdate();
        private IEnumerable<Bar> Find(Func<Bar, bool> predicate);
        public Bar GetBarByCaption(string barCaption);
        protected override void InsertItem(int index, Bar bar);
        protected internal virtual void OnBarsChanged();
        protected virtual void OnInsertItem(Bar bar);
        protected virtual void OnRemoveItem(Bar bar);
        private void RaiseBarRegistratorChanged(Bar bar);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, Bar item);

        [Description("Returns true if the BeginUpdate method has been called, but the corresponding EndUpdate method hasn't been called yet.")]
        public bool IsUpdating { get; }

        [Description("Gets the BarManager that owns the current collection.")]
        public BarManager Manager { get; }

        public Bar this[string name] { get; }
    }
}

