namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RowsRange : IEnumerable<int>, IEnumerable
    {
        public RowsRange(int topRowHandle, int rowCount);
        [IteratorStateMachine(typeof(RowsRange.<System-Collections-Generic-IEnumerable<System-Int32>-GetEnumerator>d__9))]
        IEnumerator<int> IEnumerable<int>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();

        public int TopRowHandle { get; private set; }

        public int RowCount { get; private set; }

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<System-Int32>-GetEnumerator>d__9 : IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            public RowsRange <>4__this;
            private int <rowHandle>5__1;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<System-Int32>-GetEnumerator>d__9(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            int IEnumerator<int>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

