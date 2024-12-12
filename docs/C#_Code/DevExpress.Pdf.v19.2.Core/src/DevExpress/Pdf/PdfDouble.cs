namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Globalization;

    public class PdfDouble : IPdfWritableObject
    {
        private readonly double value;

        public PdfDouble(object value)
        {
            this.value = (double) value;
        }

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            stream.WriteString(this.value.ToString("0.0###############", CultureInfo.InvariantCulture));
        }

        public double Value =>
            this.value;
    }
}

