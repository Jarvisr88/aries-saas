namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfHuffmanTreeLeaf : PdfHuffmanTreeNode
    {
        private readonly int runLength;

        public PdfHuffmanTreeLeaf(int runLength)
        {
            this.runLength = runLength;
        }

        public int RunLength =>
            this.runLength;
    }
}

