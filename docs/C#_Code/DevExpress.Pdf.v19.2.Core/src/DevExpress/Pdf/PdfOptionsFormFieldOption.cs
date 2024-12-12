namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOptionsFormFieldOption
    {
        private readonly string text;
        private readonly string exportText;

        internal PdfOptionsFormFieldOption(object value)
        {
            byte[] buffer = value as byte[];
            if (buffer != null)
            {
                this.text = PdfDocumentReader.ConvertToTextString(buffer);
                this.exportText = this.text;
            }
            else
            {
                IList<object> list = value as IList<object>;
                if ((list == null) || (list.Count != 2))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                byte[] buffer2 = list[0] as byte[];
                byte[] buffer3 = list[1] as byte[];
                if ((buffer2 == null) || (buffer3 == null))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.text = PdfDocumentReader.ConvertToTextString(buffer2);
                this.exportText = PdfDocumentReader.ConvertToTextString(buffer3);
            }
        }

        internal PdfOptionsFormFieldOption(string text, string exportText)
        {
            this.text = text;
            this.exportText = exportText;
        }

        internal object Write()
        {
            if (this.text == this.exportText)
            {
                return this.text;
            }
            return new object[] { this.text, this.exportText };
        }

        public string Text =>
            this.text;

        public string ExportText =>
            this.exportText;
    }
}

