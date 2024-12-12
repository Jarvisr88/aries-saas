namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct StreamingPropertyEnumerator : IEnumerator<XtraPropertyInfo>, IDisposable, IEnumerator
    {
        private int index;
        private IXtraPropertyCollection list;
        public StreamingPropertyEnumerator(IXtraPropertyCollection list)
        {
            this.index = -1;
            this.list = list;
        }

        public XtraPropertyInfo Current =>
            this.list[this.index];
        object IEnumerator.Current =>
            this.list[this.index];
        public void Dispose()
        {
            this.list = null;
        }

        public bool MoveNext()
        {
            int num = this.index + 1;
            this.index = num;
            return (num < this.list.Count);
        }

        public void Reset()
        {
            this.index = -1;
        }
    }
}

