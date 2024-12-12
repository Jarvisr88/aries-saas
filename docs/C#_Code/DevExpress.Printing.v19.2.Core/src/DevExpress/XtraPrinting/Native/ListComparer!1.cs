namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    internal class ListComparer<T> : IComparer<IList<T>>
    {
        public int Compare(IList<T> x, IList<T> y);
    }
}

