namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfEmptySoftMask : PdfSoftMask
    {
        internal const string Name = "None";
        private static readonly PdfEmptySoftMask instance = new PdfEmptySoftMask();

        private PdfEmptySoftMask() : base(null)
        {
        }

        protected internal override bool IsSame(PdfSoftMask softMask) => 
            ReferenceEquals(softMask, instance);

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.Write(objects);

        protected internal override object Write(PdfObjectCollection objects) => 
            new PdfName("None");

        public static PdfEmptySoftMask Instance =>
            instance;
    }
}

