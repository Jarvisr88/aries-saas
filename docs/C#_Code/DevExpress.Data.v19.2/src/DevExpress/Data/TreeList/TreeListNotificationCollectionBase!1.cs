namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class TreeListNotificationCollectionBase<T> : NotificationCollectionBase, IEnumerable<T>, IEnumerable where T: class
    {
        protected TreeListNotificationCollectionBase(CollectionChangeEventHandler collectionChanged);
        public void AddRange(IList<T> items);
        public void ClearAndAddRange(IList<T> items);
        [IteratorStateMachine(typeof(TreeListNotificationCollectionBase<>.<System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__5))]
        IEnumerator<T> IEnumerable<T>.GetEnumerator();

        public T this[int index] { get; }

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__5 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public TreeListNotificationCollectionBase<T> <>4__this;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<T>-GetEnumerator>d__5(int <>1__state);
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

