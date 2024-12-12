namespace DevExpress.Data.PLinq
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class GetEnumerableEventArgs : EventArgs
    {
        public IEnumerable Source { get; set; }

        public object Tag { get; set; }
    }
}

