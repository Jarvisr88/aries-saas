namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfCMapTreeNode<T>
    {
        protected PdfCMapTreeNode()
        {
        }

        public abstract void Fill(byte[] code, int position, T value);
        public abstract PdfCMapFindResult<T> Find(byte[] code, int position);

        public abstract bool IsFinal { get; }
    }
}

