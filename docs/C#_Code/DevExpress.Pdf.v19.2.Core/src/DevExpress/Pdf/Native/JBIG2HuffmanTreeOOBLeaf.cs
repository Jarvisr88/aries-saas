namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2HuffmanTreeOOBLeaf : IHuffmanTreeNode
    {
        public void BuildTree(int code, int curLen, Func<IHuffmanTreeNode> createLeaf)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        public int? DecodeValue(PdfBitReader reader) => 
            null;
    }
}

