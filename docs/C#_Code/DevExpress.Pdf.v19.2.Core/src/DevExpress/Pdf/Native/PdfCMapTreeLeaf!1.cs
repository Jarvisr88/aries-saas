namespace DevExpress.Pdf.Native
{
    using System;

    internal class PdfCMapTreeLeaf<T> : PdfCMapTreeNode<T>
    {
        private readonly T value;

        public PdfCMapTreeLeaf(T value)
        {
            this.value = value;
        }

        public override void Fill(byte[] code, int position, T value)
        {
        }

        public override PdfCMapFindResult<T> Find(byte[] code, int position) => 
            new PdfCMapFindResult<T>(this.value, 0);

        public override bool IsFinal =>
            true;
    }
}

