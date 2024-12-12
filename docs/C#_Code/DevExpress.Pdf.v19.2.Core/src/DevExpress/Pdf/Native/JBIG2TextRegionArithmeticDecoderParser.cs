namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JBIG2TextRegionArithmeticDecoderParser : JBIG2TextRegionParser<JBIG2Decoder>
    {
        public JBIG2TextRegionArithmeticDecoderParser(JBIG2StreamHelper streamHelper, JBIG2RegionSegmentInfo regionInfo, int flags, IList<JBIG2SymbolDictionary> sdReferred) : base(streamHelper, regionInfo, flags, sdReferred)
        {
            this.<Decoder>k__BackingField = JBIG2Decoder.Create(streamHelper, base.Glyphs.Count);
        }

        public JBIG2TextRegionArithmeticDecoderParser(int refaggninst, int sdrtemplate, int[] at, List<JBIG2Image> glyphs, JBIG2Decoder decoder, JBIG2RegionSegmentInfo regionInfo) : base(refaggninst, sdrtemplate, at, glyphs, regionInfo)
        {
            this.<Decoder>k__BackingField = decoder;
        }

        protected override void RefinementDecode(JBIG2Image ib, int dx, int dy, JBIG2Image refImage)
        {
            new JBIG2RefinementRegion(ib, dx, dy, base.Sbrtemplate, base.Sbrat, this.Decoder, refImage).Process();
        }

        protected override JBIG2Decoder Decoder { get; }
    }
}

