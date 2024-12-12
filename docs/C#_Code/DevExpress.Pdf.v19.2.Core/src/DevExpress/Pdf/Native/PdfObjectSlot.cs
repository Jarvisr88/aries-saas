namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfObjectSlot : PdfObjectPointer
    {
        public PdfObjectSlot(int number, int generation, long offset) : base(number, generation, offset)
        {
        }
    }
}

