namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing.Design;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [ListBindable(BindableSupport.No), Editor("DevExpress.Utils.Design.CalculatedColumnCollectionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
    public class CalculatedColumnCollection : CollectionBase, IList<CalculatedColumn>, ICollection<CalculatedColumn>, IEnumerable<CalculatedColumn>, IEnumerable
    {
        internal ComparativeSource owner;

        public CalculatedColumnCollection(ComparativeSource owner);
        public void Add(CalculatedColumn item);
        public void AddRange(CalculatedColumn[] calculatedColumns);
        public bool Contains(CalculatedColumn item);
        public void CopyTo(CalculatedColumn[] array, int arrayIndex);
        [IteratorStateMachine(typeof(CalculatedColumnCollection.<GetEnumerator>d__17))]
        public IEnumerator<CalculatedColumn> GetEnumerator();
        public int IndexOf(CalculatedColumn item);
        public void Insert(int index, CalculatedColumn item);
        protected override void OnClearComplete();
        public bool Remove(CalculatedColumn item);
        public void UpdateColumn(CalculatedColumn item);

        public ComparativeSource Owner { get; }

        public CalculatedColumn this[int index] { get; set; }

        public bool IsReadOnly { get; }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__17 : IEnumerator<CalculatedColumn>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private CalculatedColumn <>2__current;
            public CalculatedColumnCollection <>4__this;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__17(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            CalculatedColumn IEnumerator<CalculatedColumn>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

