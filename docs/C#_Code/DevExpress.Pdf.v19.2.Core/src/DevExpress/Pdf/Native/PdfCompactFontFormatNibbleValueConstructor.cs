namespace DevExpress.Pdf.Native
{
    using System;
    using System.Globalization;
    using System.Text;

    public class PdfCompactFontFormatNibbleValueConstructor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public bool AddNibble(int nibble)
        {
            switch (nibble)
            {
                case 10:
                    this.stringBuilder.Append('.');
                    break;

                case 11:
                    this.stringBuilder.Append('E');
                    break;

                case 12:
                    this.stringBuilder.Append("E-");
                    break;

                case 13:
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    break;

                case 14:
                    this.stringBuilder.Append('-');
                    break;

                case 15:
                    return true;

                default:
                    this.stringBuilder.Append(nibble.ToString());
                    break;
            }
            return false;
        }

        public double Result =>
            double.Parse(this.stringBuilder.ToString(), CultureInfo.InvariantCulture);
    }
}

