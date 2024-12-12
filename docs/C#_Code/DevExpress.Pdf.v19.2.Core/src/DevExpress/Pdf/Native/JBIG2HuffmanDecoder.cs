namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class JBIG2HuffmanDecoder : IJBIG2Decoder
    {
        protected readonly PdfBitReader reader;

        protected JBIG2HuffmanDecoder(JBIG2StreamHelper sh)
        {
            this.reader = sh.CreateBitReader();
        }

        protected int Decode(IHuffmanTreeNode root)
        {
            int? nullable = root.DecodeValue(this.reader);
            this.LastCode = nullable == null;
            return nullable.GetValueOrDefault();
        }

        public bool LastCode { get; private set; }
    }
}

