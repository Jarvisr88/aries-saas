namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPatternColorSpace : PdfCustomColorSpace
    {
        internal const string Name = "Pattern";
        private readonly PdfColorSpace alternateColorSpace;

        internal PdfPatternColorSpace() : this(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB))
        {
        }

        internal PdfPatternColorSpace(PdfColorSpace alternateColorSpace)
        {
            this.alternateColorSpace = alternateColorSpace;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfName name = new PdfName("Pattern");
            return new object[] { name, this.alternateColorSpace.Write(collection) };
        }

        protected internal override PdfColor Transform(PdfColor color) => 
            this.alternateColorSpace.Transform(color);

        protected internal override object Write(PdfObjectCollection collection)
        {
            PdfDeviceColorSpace alternateColorSpace = this.alternateColorSpace as PdfDeviceColorSpace;
            return (((alternateColorSpace == null) || (alternateColorSpace.Kind != PdfDeviceColorSpaceKind.RGB)) ? base.Write(collection) : new PdfName("Pattern"));
        }

        public PdfColorSpace AlternateColorSpace =>
            this.alternateColorSpace;

        public override int ComponentsCount =>
            this.alternateColorSpace.ComponentsCount;
    }
}

