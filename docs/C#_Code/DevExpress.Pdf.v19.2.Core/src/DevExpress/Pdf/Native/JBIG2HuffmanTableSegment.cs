namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2HuffmanTableSegment : JBIG2SegmentData
    {
        private readonly IHuffmanTreeNode treeRoot;

        public JBIG2HuffmanTableSegment(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
            this.treeRoot = JBIG2HuffmanTableBuilder.BuildHuffmanTree(ParceTable(base.StreamHelper));
        }

        private static JBIG2HuffmanTableModel ParceTable(JBIG2StreamHelper streamHelper)
        {
            JBIG2HuffmanTableModel model = new JBIG2HuffmanTableModel();
            byte num = streamHelper.ReadByte();
            model.PrefixSize = ((num & 14) >> 1) + 1;
            int bitCount = ((num & 0x70) >> 4) + 1;
            int num3 = streamHelper.ReadInt32();
            int rangelow = streamHelper.ReadInt32();
            int num5 = num3;
            PdfBitReader reader = streamHelper.CreateBitReader();
            while (true)
            {
                int integer = reader.GetInteger(model.PrefixSize);
                int rangelen = reader.GetInteger(bitCount);
                model.AddLine(integer, rangelen, num5);
                num5 += 1 << (rangelen & 0x1f);
                if (num5 >= rangelow)
                {
                    model.AddLine(reader.GetInteger(model.PrefixSize), 0x20, num3 - 1);
                    model.AddLine(reader.GetInteger(model.PrefixSize), 0x20, rangelow);
                    model.HTOOB = (num & 1) != 0;
                    if (model.HTOOB)
                    {
                        model.Preflen.Add(reader.GetInteger(model.PrefixSize));
                    }
                    return model;
                }
            }
        }

        public IHuffmanTreeNode TreeRoot =>
            this.treeRoot;
    }
}

