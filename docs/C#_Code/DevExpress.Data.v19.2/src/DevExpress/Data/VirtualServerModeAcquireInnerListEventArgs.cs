namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class VirtualServerModeAcquireInnerListEventArgs : EventArgs
    {
        internal VirtualServerModeAcquireInnerListEventArgs Next(IList nextList);

        public IList InnerList { get; set; }

        public Action<IList> ReleaseAction { get; set; }

        public Func<IList, IEnumerable, IList> AddMoreRowsFunc { get; set; }

        public Func<IList, IEnumerable, IList> ClearAndAddRowsFunc { get; set; }
    }
}

