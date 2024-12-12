namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class JBIG2HuffmanTreeBuilder
    {
        public void AddLowerRangeLine(JBIG2HuffmanTableLine line)
        {
            this.AddTableLine(line, () => new JBIG2HuffmanTreeLowerRangeLeaf(line.RangeLen, line.RangeLow));
        }

        public void AddOOBLine(JBIG2HuffmanTableLine line)
        {
            Func<IHuffmanTreeNode> createLeaf = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<IHuffmanTreeNode> local1 = <>c.<>9__6_0;
                createLeaf = <>c.<>9__6_0 = () => new JBIG2HuffmanTreeOOBLeaf();
            }
            this.AddTableLine(line, createLeaf);
        }

        public void AddRunCode(int? code, int codeLen, int value)
        {
            if (code != null)
            {
                this.RootNode.BuildTree(code.Value, codeLen, () => new JBIG2HuffmanTreeRunCodeLeaf(value));
            }
        }

        public void AddTableLine(JBIG2HuffmanTableLine line)
        {
            this.AddTableLine(line, () => new JBIG2HuffmanTreeLeaf(line.RangeLen, line.RangeLow));
        }

        private void AddTableLine(JBIG2HuffmanTableLine line, Func<IHuffmanTreeNode> createLeaf)
        {
            if (line.Code != null)
            {
                this.RootNode.BuildTree(line.Code.Value, line.Preflen, createLeaf);
            }
        }

        public JBIG2HuffmanTreeNode RootNode { get; } = new JBIG2HuffmanTreeNode()

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly JBIG2HuffmanTreeBuilder.<>c <>9 = new JBIG2HuffmanTreeBuilder.<>c();
            public static Func<IHuffmanTreeNode> <>9__6_0;

            internal IHuffmanTreeNode <AddOOBLine>b__6_0() => 
                new JBIG2HuffmanTreeOOBLeaf();
        }
    }
}

