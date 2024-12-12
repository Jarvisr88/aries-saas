namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2SymbolDictionaryHuffmanDecoder : JBIG2HuffmanDecoder, IJBIG2SymbolDictionaryDecoder, IJBIG2Decoder
    {
        private readonly IHuffmanTreeNode sdhuffdh;
        private readonly IHuffmanTreeNode sdhuffdw;
        private readonly IHuffmanTreeNode sdhuffbmsize;
        private readonly IHuffmanTreeNode sdhuffagginst;
        private readonly IHuffmanTreeNode huffmanTableB1;

        public JBIG2SymbolDictionaryHuffmanDecoder(JBIG2StreamHelper sh, IHuffmanTreeNode sdhuffdh, IHuffmanTreeNode sdhuffdw, IHuffmanTreeNode sdhuffbmsize, IHuffmanTreeNode sdhuffagginst) : base(sh)
        {
            this.sdhuffdh = sdhuffdh;
            this.sdhuffdw = sdhuffdw;
            this.sdhuffbmsize = sdhuffbmsize;
            this.sdhuffagginst = sdhuffagginst;
            this.huffmanTableB1 = JBIG2HuffmanTableModel.StandardHuffmanTableA();
        }

        public int DecodeAI() => 
            base.Decode(this.sdhuffagginst);

        public int DecodeBmsize() => 
            base.Decode(this.sdhuffbmsize);

        public int DecodeDH() => 
            base.Decode(this.sdhuffdh);

        public int DecodeDW() => 
            base.Decode(this.sdhuffdw);

        public int DecodeEX() => 
            base.Decode(this.huffmanTableB1);

        public byte[] ReadBytes(int count) => 
            base.reader.ReadBytes(count);
    }
}

