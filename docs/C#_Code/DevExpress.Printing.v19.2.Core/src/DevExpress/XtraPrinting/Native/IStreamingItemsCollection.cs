namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;

    internal interface IStreamingItemsCollection : IStreamingPropertyCollection, IXtraPropertyCollection, ICollection, IEnumerable
    {
        void IncreaseStartIndex(int index);
        void SetItemsSource(ICollection collection);
    }
}

