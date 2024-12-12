namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class JBIG2SegmentHeader
    {
        private const int pageAssociationSizeFlagMask = 0x40;
        private readonly List<int> referredToSegments;
        private readonly int number;
        private readonly byte flags;
        private readonly int pageAssociation;
        private readonly int dataLength;
        private readonly JBIG2SegmentData data;

        public JBIG2SegmentHeader(Stream stream, JBIG2Image image)
        {
            int num2;
            this.referredToSegments = new List<int>();
            JBIG2StreamHelper streamHelper = new JBIG2StreamHelper(stream);
            this.number = streamHelper.ReadInt32();
            this.flags = streamHelper.ReadByte();
            byte num = streamHelper.ReadByte();
            if ((num & 0xe0) != 0xe0)
            {
                num2 = num >> 5;
            }
            else
            {
                stream.Position -= 1L;
                num2 = streamHelper.ReadInt32() & 0x1fffffff;
                stream.Position += (num2 + 1) / 8;
            }
            int count = (this.Number <= 0x100) ? 1 : ((this.Number <= 0x10000) ? 2 : 4);
            if (num2 > 0xf4240)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            for (int i = 0; i < num2; i++)
            {
                this.ReferredToSegments.Add(streamHelper.ReadInt(count));
            }
            this.pageAssociation = streamHelper.ReadInt(((this.Flags & 0x40) == 0x40) ? 4 : 1);
            this.dataLength = streamHelper.ReadInt32();
            this.data = JBIG2SegmentData.Create(streamHelper, this, image);
        }

        public void Process()
        {
            this.data.Process();
        }

        public int Number =>
            this.number;

        public byte Flags =>
            this.flags;

        public int PageAssociation =>
            this.pageAssociation;

        public int DataLength =>
            this.dataLength;

        public JBIG2SegmentData Data =>
            this.data;

        public List<int> ReferredToSegments =>
            this.referredToSegments;

        public bool EndOfFile =>
            (this.Flags & 0x3f) == 0x33;
    }
}

