namespace DevExpress.Pdf.Native
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class PdfFieldValueAttribute : Attribute
    {
        private readonly int value;

        public PdfFieldValueAttribute(int value)
        {
            this.value = value;
        }

        public int Value =>
            this.value;
    }
}

