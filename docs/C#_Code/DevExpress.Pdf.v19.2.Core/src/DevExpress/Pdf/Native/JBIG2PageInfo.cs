namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2PageInfo : JBIG2SegmentData
    {
        private const int pageDefaultPixelValueMask = 4;

        public JBIG2PageInfo(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
            int width = streamHelper.ReadInt32();
            streamHelper.ReadInt32();
            streamHelper.ReadInt32();
            streamHelper.ReadInt16();
            image.SetDimensions(width, streamHelper.ReadInt32(), (streamHelper.ReadByte() & 4) != 0);
        }

        protected override bool CacheData =>
            false;
    }
}

