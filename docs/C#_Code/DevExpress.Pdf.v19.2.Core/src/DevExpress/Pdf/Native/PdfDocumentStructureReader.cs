namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf.Localization;
    using System;

    internal abstract class PdfDocumentStructureReader
    {
        public const byte LineFeed = 10;
        public const byte CarriageReturn = 13;
        public const byte Comment = 0x25;
        protected static readonly PdfTokenDescription EofToken;
        protected static readonly PdfTokenDescription TrailerToken;
        private readonly PdfObjectCollection objects;
        private readonly PdfDocumentStream documentStream;
        private PdfObjectReference rootObjectReference;

        static PdfDocumentStructureReader()
        {
            byte[] token = new byte[] { 0x25, 0x25, 0x45, 0x4f, 70 };
            EofToken = new PdfTokenDescription(token);
            byte[] buffer2 = new byte[] { 0x74, 0x72, 0x61, 0x69, 0x6c, 0x65, 0x72 };
            TrailerToken = new PdfTokenDescription(buffer2);
        }

        protected PdfDocumentStructureReader(PdfDocumentStream documentStream)
        {
            this.documentStream = documentStream;
            this.objects = new PdfObjectCollection(documentStream);
        }

        internal static void ThrowIncorrectDataException()
        {
            throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectPdfData));
        }

        protected virtual void UpdateTrailer(PdfReaderDictionary trailerDictionary, PdfObjectCollection objects)
        {
            PdfObjectReference objectReference = trailerDictionary.GetObjectReference("Root");
            if (objectReference != null)
            {
                this.rootObjectReference = objectReference;
            }
        }

        protected PdfObjectCollection Objects =>
            this.objects;

        protected PdfObjectReference RootObjectReference =>
            this.rootObjectReference;

        protected PdfDocumentStream DocumentStream =>
            this.documentStream;
    }
}

