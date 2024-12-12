namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfObjectContainer : PdfObjectPointer
    {
        private readonly object value;

        public PdfObjectContainer(int number, int generation, object value) : this(number, generation, value, 0L)
        {
        }

        public PdfObjectContainer(int number, int generation, object value, long offset) : base(number, generation, offset)
        {
            this.value = value;
        }

        public object Value =>
            this.value;
    }
}

