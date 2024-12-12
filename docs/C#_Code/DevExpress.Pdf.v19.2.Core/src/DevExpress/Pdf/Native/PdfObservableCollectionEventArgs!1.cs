namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfObservableCollectionEventArgs<T> : EventArgs
    {
        private readonly T item;

        public PdfObservableCollectionEventArgs(T item)
        {
            this.item = item;
        }

        public T Item =>
            this.item;
    }
}

