namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public abstract class NestedObjectEnumeratorBase : IEnumerator
    {
        protected static readonly IEnumerator EmptyEnumerator;
        private IEnumerator objects;
        protected NestedObjectEnumeratorBase.EnumStack stack;

        static NestedObjectEnumeratorBase();
        protected NestedObjectEnumeratorBase(object obj);
        protected abstract IEnumerator GetNestedObjects(object obj);
        [IteratorStateMachine(typeof(NestedObjectEnumeratorBase.<GetParents>d__8))]
        public IEnumerable<object> GetParents();
        public virtual bool MoveNext();
        public virtual void Reset();

        protected IEnumerator Enumerator { get; }

        public object CurrentParent { get; }

        public int Level { get; }

        object IEnumerator.Current { get; }

        [CompilerGenerated]
        private sealed class <GetParents>d__8 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            public NestedObjectEnumeratorBase <>4__this;
            private IEnumerator<IEnumerator> <en>5__1;

            [DebuggerHidden]
            public <GetParents>d__8(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        protected class EnumStack : Stack<IEnumerator>
        {
            public IEnumerator TopEnumerator { get; }

            public bool IsEmpty { get; }
        }
    }
}

