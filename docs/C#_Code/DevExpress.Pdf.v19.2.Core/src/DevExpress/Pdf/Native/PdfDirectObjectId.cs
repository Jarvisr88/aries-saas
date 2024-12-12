namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfDirectObjectId : IPdfObjectId, IEquatable<PdfDirectObjectId>
    {
        private readonly PdfObject obj;
        public PdfDirectObjectId(PdfObject obj)
        {
            this.obj = obj;
        }

        public bool Equals(PdfDirectObjectId other) => 
            ReferenceEquals(this.obj, other.obj);
    }
}

