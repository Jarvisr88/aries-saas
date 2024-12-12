namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfHuffmanTreeBranch : PdfHuffmanTreeNode
    {
        private PdfHuffmanTreeNode zero;
        private PdfHuffmanTreeNode one;

        public void Fill(IDictionary<string, int> dictionary)
        {
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                this.Fill(pair.Key, pair.Value);
            }
        }

        public void Fill(string sequence, int runLength)
        {
            bool flag;
            if (string.IsNullOrEmpty(sequence))
            {
                throw new ArgumentException("sequence");
            }
            char ch = sequence[0];
            if (ch == '0')
            {
                flag = false;
            }
            else
            {
                if (ch != '1')
                {
                    throw new ArgumentException("sequence");
                }
                flag = true;
            }
            string str = sequence.Remove(0, 1);
            if (string.IsNullOrEmpty(str))
            {
                if (flag)
                {
                    if (this.one != null)
                    {
                        throw new ArgumentException("sequence");
                    }
                    this.one = new PdfHuffmanTreeLeaf(runLength);
                }
                else
                {
                    if (this.zero != null)
                    {
                        throw new ArgumentException("sequence");
                    }
                    this.zero = new PdfHuffmanTreeLeaf(runLength);
                }
            }
            else
            {
                PdfHuffmanTreeBranch one;
                if (flag)
                {
                    if (this.one == null)
                    {
                        one = new PdfHuffmanTreeBranch();
                        this.one = one;
                    }
                    else
                    {
                        one = this.one as PdfHuffmanTreeBranch;
                        if (one == null)
                        {
                            throw new ArgumentException("sequence");
                        }
                    }
                }
                else if (this.zero == null)
                {
                    one = new PdfHuffmanTreeBranch();
                    this.zero = one;
                }
                else
                {
                    one = this.zero as PdfHuffmanTreeBranch;
                    if (one == null)
                    {
                        throw new ArgumentException("sequence");
                    }
                }
                one.Fill(str, runLength);
            }
        }

        public PdfHuffmanTreeNode Zero =>
            this.zero;

        public PdfHuffmanTreeNode One =>
            this.one;
    }
}

