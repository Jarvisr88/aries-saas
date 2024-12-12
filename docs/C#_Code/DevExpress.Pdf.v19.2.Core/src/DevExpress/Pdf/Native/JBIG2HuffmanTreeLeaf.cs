﻿namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2HuffmanTreeLeaf : IHuffmanTreeNode
    {
        private readonly int rangelen;
        private readonly int rangelow;

        public JBIG2HuffmanTreeLeaf(int rangelen, int rangelow)
        {
            this.rangelen = rangelen;
            this.rangelow = rangelow;
        }

        public void BuildTree(int code, int curLen, Func<IHuffmanTreeNode> createLeaf)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        public int? DecodeValue(PdfBitReader reader) => 
            new int?(this.rangelow + reader.GetInteger(this.rangelen));
    }
}
