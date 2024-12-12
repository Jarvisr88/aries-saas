namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class StreamingDocumentSerializationOptions : DocumentSerializationOptions
    {
        private IStreamingDocument streamingDocument;

        public StreamingDocumentSerializationOptions(IStreamingDocument document, int pageCount) : base((Document) document, pageCount)
        {
            this.streamingDocument = document;
        }

        public StreamingDocumentSerializationOptions(Document document, int pageCount) : base(document, pageCount)
        {
            this.streamingDocument = document as IStreamingDocument;
        }

        [XtraSerializableProperty, DefaultValue((float) 0f)]
        public float MaxBrickRight { get; set; }

        [XtraSerializableProperty]
        public bool BuildContinuousInfo { get; set; }

        [XtraSerializableProperty, DefaultValue(-1)]
        public int LastStoredID { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Content, 9)]
        public override BookmarkNode RootBookmark =>
            (this.streamingDocument != null) ? this.streamingDocument.RootBookmark : base.RootBookmark;
    }
}

