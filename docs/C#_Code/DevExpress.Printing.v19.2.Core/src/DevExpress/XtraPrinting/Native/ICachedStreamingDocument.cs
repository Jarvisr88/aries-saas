namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal interface ICachedStreamingDocument
    {
        event EventHandler DocumentSerialized;

        void OnDocumentSerialized();

        IStoredIDProvider StoredIdProvider { get; }
    }
}

