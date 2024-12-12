namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2GenericRegion : JBIG2SegmentData
    {
        private readonly JBIG2RegionSegmentInfo info;
        private readonly int[] gbat;
        private readonly bool mmr;
        private readonly int gbTemplate;
        private readonly bool tpgdon;
        private readonly JBIG2Decoder decoder;

        public JBIG2GenericRegion(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
            int dataLength = header.DataLength - 0x12;
            this.info = new JBIG2RegionSegmentInfo(base.StreamHelper);
            byte num2 = base.StreamHelper.ReadByte();
            if ((num2 & 1) == 0)
            {
                int length = ((num2 & 6) > 0) ? 2 : 8;
                this.gbat = base.StreamHelper.ReadAdaptiveTemplate(length);
                dataLength -= length;
            }
            this.mmr = (num2 & 1) > 0;
            this.gbTemplate = (num2 & 6) >> 1;
            this.tpgdon = ((num2 & 8) >> 3) > 0;
            base.DoCacheData(dataLength);
            this.decoder = JBIG2Decoder.Create(base.StreamHelper, 0);
        }

        internal JBIG2GenericRegion(JBIG2SegmentHeader header, int[] gbat, int gbTemplate, JBIG2Decoder decoder) : base(null, header, null)
        {
            this.gbat = gbat;
            this.gbTemplate = gbTemplate;
            this.decoder = decoder;
        }

        internal JBIG2GenericRegionDecoder CreateDecoder(JBIG2Image image)
        {
            if (this.mmr)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (this.tpgdon ? JBIG2TPGDONDecoder.Create(image, this.decoder, this.gbat, this.gbTemplate) : CreateDecoder(this.gbTemplate, image, this.decoder, this.gbat));
        }

        public static JBIG2GenericRegionDecoder CreateDecoder(int gbTemplate, JBIG2Image image, JBIG2Decoder decoder, int[] gbat)
        {
            switch (gbTemplate)
            {
                case 0:
                    return (((gbat[0] != 3) || ((gbat[1] != -1) || ((gbat[2] != -3) || ((gbat[3] != -1) || ((gbat[4] != 2) || ((gbat[5] != -2) || ((gbat[6] != -2) || (gbat[7] != -2)))))))) ? ((JBIG2GenericRegionDecoder) new JBIG2Template0Decoder(image, decoder, gbat)) : ((JBIG2GenericRegionDecoder) new JBIG2FastTemplate0Decoder(image, decoder)));

                case 1:
                    return new JBIG2Template1Decoder(image, decoder, gbat);

                case 2:
                    return (((gbat[0] != 3) || (gbat[1] != -1)) ? ((JBIG2GenericRegionDecoder) new JBIG2Template2Decoder(image, decoder)) : ((JBIG2GenericRegionDecoder) new JBIG2Template2aDecoder(image, decoder)));

                case 3:
                    return new JBIG2Template3Decoder(image, decoder, gbat);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public override void Process()
        {
            base.Process();
            JBIG2Image image = new JBIG2Image(this.info.Width, this.info.Height);
            this.CreateDecoder(image).Decode();
            base.Image.Composite(image, this.info.X, this.info.Y, this.info.ComposeOperator);
        }

        protected override bool CacheData =>
            false;
    }
}

