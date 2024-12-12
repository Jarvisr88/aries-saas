namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCMapTreeBranch<T> : PdfCMapTreeNode<T>
    {
        private readonly Dictionary<byte, PdfCMapTreeNode<T>> children;
        private readonly T defaultValue;

        public PdfCMapTreeBranch(T defaultValue)
        {
            this.children = new Dictionary<byte, PdfCMapTreeNode<T>>();
            this.defaultValue = defaultValue;
        }

        public override void Fill(byte[] code, int position, T value)
        {
            int num = code.Length - position;
            if (num > 0)
            {
                PdfCMapTreeNode<T> node;
                byte key = code[position];
                if (!this.children.TryGetValue(key, out node))
                {
                    if (num == 1)
                    {
                        this.children[key] = new PdfCMapTreeLeaf<T>(value);
                    }
                    else
                    {
                        PdfCMapTreeBranch<T> branch = new PdfCMapTreeBranch<T>(this.defaultValue);
                        branch.Fill(code, position + 1, value);
                        this.children[key] = branch;
                    }
                }
                else if (num == 1)
                {
                    this.children[key] = new PdfCMapTreeLeaf<T>(value);
                }
                else if (!node.IsFinal)
                {
                    node.Fill(code, position + 1, value);
                }
            }
        }

        public override PdfCMapFindResult<T> Find(byte[] code, int position)
        {
            PdfCMapTreeNode<T> node;
            if (((code.Length - position) <= 0) || !this.children.TryGetValue(code[position], out node))
            {
                return new PdfCMapFindResult<T>(this.defaultValue, 0);
            }
            PdfCMapFindResult<T> result = node.Find(code, position + 1);
            return new PdfCMapFindResult<T>(result.Value, result.CodeLength + 1);
        }

        public override bool IsFinal =>
            false;
    }
}

