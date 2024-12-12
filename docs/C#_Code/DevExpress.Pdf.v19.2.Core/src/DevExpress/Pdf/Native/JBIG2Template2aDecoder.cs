namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2Template2aDecoder : JBIG2GenericRegionDecoder
    {
        public JBIG2Template2aDecoder(JBIG2Image image, JBIG2Decoder decoder) : base(image, decoder)
        {
        }

        public override void Decode()
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }
    }
}

