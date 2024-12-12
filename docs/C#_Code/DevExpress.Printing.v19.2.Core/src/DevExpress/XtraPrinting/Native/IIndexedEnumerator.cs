namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;

    public interface IIndexedEnumerator : IEnumerator
    {
        int RealIndex { get; }
    }
}

