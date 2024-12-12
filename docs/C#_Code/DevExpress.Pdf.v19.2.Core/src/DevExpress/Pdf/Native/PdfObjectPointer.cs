namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfObjectPointer : PdfDocumentItem
    {
        private readonly long offset;
        private bool applyEncryption;

        protected PdfObjectPointer(int number, int generation, long offset) : base(number, generation)
        {
            this.offset = offset;
            this.applyEncryption = true;
        }

        internal bool ApplyEncryption
        {
            get => 
                this.applyEncryption;
            set => 
                this.applyEncryption = value;
        }

        internal long Offset =>
            this.offset;
    }
}

