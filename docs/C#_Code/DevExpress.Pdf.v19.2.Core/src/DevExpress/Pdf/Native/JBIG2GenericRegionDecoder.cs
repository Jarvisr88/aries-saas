namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JBIG2GenericRegionDecoder
    {
        private readonly JBIG2Decoder decoder;
        private readonly JBIG2Image image;

        protected JBIG2GenericRegionDecoder(JBIG2Image image, JBIG2Decoder decoder)
        {
            this.decoder = decoder;
            this.image = image;
        }

        public abstract void Decode();

        protected JBIG2Decoder Decoder =>
            this.decoder;

        protected JBIG2Image Image =>
            this.image;
    }
}

