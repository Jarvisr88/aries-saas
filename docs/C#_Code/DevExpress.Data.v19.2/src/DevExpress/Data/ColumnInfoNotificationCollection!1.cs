namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class ColumnInfoNotificationCollection<T> : NotificationCollectionBase, IEnumerable<T>, IEnumerable where T: class
    {
        private readonly DataControllerBase controller;

        protected ColumnInfoNotificationCollection(DataControllerBase controller, CollectionChangeEventHandler collectionChanged);
        protected abstract bool IsColumnInfoUsed(int index, IList<DataColumnInfo> unusedColumns);
        protected internal bool RemoveUnusedColumns(IList<DataColumnInfo> unusedColumns);
        [IteratorStateMachine(typeof(ColumnInfoNotificationCollection<>.<System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__6))]
        IEnumerator<T> IEnumerable<T>.GetEnumerator();

        public T this[int index] { get; }

        public DataControllerBase Controller { get; }

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__6 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public ColumnInfoNotificationCollection<T> <>4__this;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__6(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            T IEnumerator<T>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

