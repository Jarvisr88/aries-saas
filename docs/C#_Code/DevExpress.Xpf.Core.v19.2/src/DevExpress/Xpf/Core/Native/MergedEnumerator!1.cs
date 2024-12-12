namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MergedEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private readonly IEnumerator<IEnumerator<T>> sourceEnumerator;

        public MergedEnumerator(params IEnumerator<T>[] args);
        public void Dispose();
        public bool MoveNext();
        public void Reset();

        public T Current { get; }

        object IEnumerator.Current { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MergedEnumerator<T>.<>c <>9;
            public static Func<IEnumerator<T>, bool> <>9__1_0;

            static <>c();
            internal bool <.ctor>b__1_0(IEnumerator<T> x);
        }
    }
}

