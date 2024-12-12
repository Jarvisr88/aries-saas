namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2TextRegionHuffmanDecoder : JBIG2HuffmanDecoder, IJBIG2TextRegionDecoder, IJBIG2Decoder
    {
        private readonly JBIG2TextRegionHuffmanTables tables;
        private readonly int sbstripsBitNumber;
        private readonly IHuffmanTreeNode rootID;

        public JBIG2TextRegionHuffmanDecoder(JBIG2StreamHelper sh, JBIG2TextRegionHuffmanTables huffmanTables, int sbstrips, IHuffmanTreeNode root) : base(sh)
        {
            this.tables = huffmanTables;
            this.sbstripsBitNumber = (int) Math.Ceiling(Math.Log((double) sbstrips, 2.0));
            this.rootID = root;
        }

        public int DecodeDS() => 
            base.Decode(this.tables.Sbhuffds);

        public int DecodeDT() => 
            base.Decode(this.tables.Sbhuffdt);

        public int DecodeFS() => 
            base.Decode(this.tables.Sbhufffs);

        public int DecodeID() => 
            base.Decode(this.rootID);

        public int DecodeIT() => 
            base.reader.GetInteger(this.sbstripsBitNumber);

        public int DecodeRDH() => 
            base.Decode(this.tables.Sbhuffrdh);

        public int DecodeRDW() => 
            base.Decode(this.tables.Sbhuffrdw);

        public int DecodeRDX() => 
            base.Decode(this.tables.Sbhuffrdx);

        public int DecodeRDY() => 
            base.Decode(this.tables.Sbhuffrdy);

        public int DecodeRI() => 
            base.reader.GetBit();

        public int DecodeRSize() => 
            base.Decode(this.tables.Sbhuffrsize);

        public void IgnoreExtendedBits()
        {
            base.reader.ReadBytes(0);
        }
    }
}

