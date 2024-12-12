namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ComplexColumnInfoCollection : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        public int Add(string name);
        public int IndexOf(string name);
        protected override void OnInsertComplete(int index, object item);
        protected override void OnRemoveComplete(int index, object item);
        protected virtual void RaiseCollectionChanged(CollectionChangeEventArgs e);
        public void Remove(string name);

        public ComplexColumnInfo this[int index] { get; }
    }
}

