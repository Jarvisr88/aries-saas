namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MergedEnumerator : IEnumerator, IDisposable
    {
        private readonly IEnumerator<IEnumerator> sourceEnumerator;

        public MergedEnumerator(params IEnumerator[] args);
        public void Dispose();
        public bool MoveNext();
        public void Reset();

        public object Current { get; }

        object IEnumerator.Current { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MergedEnumerator.<>c <>9;
            public static Func<IEnumerator, bool> <>9__1_0;

            static <>c();
            internal bool <.ctor>b__1_0(IEnumerator x);
        }
    }
}

