namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class JBIG2HuffmanTreeNode : IHuffmanTreeNode
    {
        public void BuildTree(int code, int curLen, Func<IHuffmanTreeNode> createLeaf)
        {
            if (curLen == 1)
            {
                if ((code & 1) != 0)
                {
                    this.Right = createLeaf();
                }
                else
                {
                    this.Left = createLeaf();
                }
            }
            else if ((code & (1 << ((curLen - 1) & 0x1f))) != 0)
            {
                this.Right ??= new JBIG2HuffmanTreeNode();
                this.Right.BuildTree(code, --curLen, createLeaf);
            }
            else
            {
                this.Left ??= new JBIG2HuffmanTreeNode();
                this.Left.BuildTree(code, --curLen, createLeaf);
            }
        }

        public int? DecodeValue(PdfBitReader reader) => 
            (reader.GetBit() != 1) ? this.Left.DecodeValue(reader) : this.Right.DecodeValue(reader);

        public IHuffmanTreeNode Left { get; set; }

        public IHuffmanTreeNode Right { get; set; }
    }
}

