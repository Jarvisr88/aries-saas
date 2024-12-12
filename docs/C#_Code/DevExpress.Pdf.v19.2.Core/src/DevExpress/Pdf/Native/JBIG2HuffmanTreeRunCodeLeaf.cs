namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2HuffmanTreeRunCodeLeaf : IHuffmanTreeNode
    {
        private readonly int value;

        public JBIG2HuffmanTreeRunCodeLeaf(int value)
        {
            this.value = value;
        }

        public void BuildTree(int code, int curLen, Func<IHuffmanTreeNode> createLeaf)
        {
            throw new NotImplementedException();
        }

        public int? DecodeValue(PdfBitReader reader) => 
            new int?(this.value);
    }
}

