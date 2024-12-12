namespace DevExpress.Pdf.Native
{
    using System;

    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class PdfDefaultFieldAttribute : Attribute
    {
        private readonly object value;
        private readonly bool canUsed;

        public PdfDefaultFieldAttribute(object value) : this(value, true)
        {
        }

        public PdfDefaultFieldAttribute(object value, bool canUsed)
        {
            this.value = value;
            this.canUsed = canUsed;
        }

        public object Value =>
            this.value;

        public bool CanUsed =>
            this.canUsed;
    }
}

