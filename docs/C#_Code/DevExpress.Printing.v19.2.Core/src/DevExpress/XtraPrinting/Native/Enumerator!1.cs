namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    internal struct Enumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private IEnumerator enumarator;
        public Enumerator(ICollection list);
        public void Dispose();
        public bool MoveNext();
        public T Current { get; }
        object IEnumerator.Current { get; }
        void IEnumerator.Reset();
    }
}

