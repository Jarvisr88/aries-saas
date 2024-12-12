namespace DevExpress.Pdf.Native
{
    using System;

    public interface IHuffmanTreeNode
    {
        void BuildTree(int code, int curLen, Func<IHuffmanTreeNode> createLeaf);
        int? DecodeValue(PdfBitReader reader);
    }
}

