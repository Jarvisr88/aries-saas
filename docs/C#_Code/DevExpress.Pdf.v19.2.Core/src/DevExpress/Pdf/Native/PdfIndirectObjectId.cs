namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfIndirectObjectId : IPdfObjectId, IEquatable<PdfIndirectObjectId>
    {
        private readonly int number;
        private readonly Guid collectionId;
        public PdfIndirectObjectId(Guid collectionId, int number)
        {
            this.number = number;
            this.collectionId = collectionId;
        }

        public bool Equals(PdfIndirectObjectId other) => 
            (this.number == other.number) && (this.collectionId == other.collectionId);
    }
}

