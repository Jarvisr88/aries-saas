namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;

    internal interface IStreamingPropertyCollection : IXtraPropertyCollection, ICollection, IEnumerable
    {
        void AssignCachedContentIndex(int index);

        object Owner { get; }

        int CachedContentIndex { get; }

        bool HasCachedContentIndex { get; }
    }
}

