namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDefaultHalftone : PdfHalftone
    {
        internal const string Id = "Default";
        private static readonly PdfDefaultHalftone instance = new PdfDefaultHalftone();

        private PdfDefaultHalftone() : base(string.Empty)
        {
        }

        protected internal override object CreateWriteableObject(PdfObjectCollection objects) => 
            new PdfName("Default");

        protected internal override bool IsSame(PdfHalftone halftone) => 
            ReferenceEquals(halftone, instance);

        public static PdfDefaultHalftone Instance =>
            instance;
    }
}

