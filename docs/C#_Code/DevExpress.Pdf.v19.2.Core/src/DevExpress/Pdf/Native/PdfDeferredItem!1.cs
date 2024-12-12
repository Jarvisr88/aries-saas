namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfDeferredItem<U>
    {
        private readonly Func<object, U> create;
        private object value;
        private U item;

        public PdfDeferredItem(U item)
        {
            this.item = item;
        }

        public PdfDeferredItem(object value, Func<object, U> create)
        {
            this.value = value;
            this.create = create;
        }

        public U Item
        {
            get
            {
                if (this.value != null)
                {
                    this.item = this.create(this.value);
                    this.value = null;
                }
                return this.item;
            }
        }
    }
}

