namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfXFAForm
    {
        private readonly string content;

        internal PdfXFAForm(byte[] data)
        {
            this.content = Encoding.UTF8.GetString(data, 0, data.Length);
        }

        internal PdfXFAForm(PdfObjectCollection collection, IList<object> array)
        {
            int count = array.Count;
            if ((count % 2) != 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            count /= 2;
            this.content = string.Empty;
            int num2 = 0;
            int num3 = 0;
            while (num2 < count)
            {
                byte[] buffer = array[num3++] as byte[];
                if (buffer == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                PdfReaderStream stream = collection.TryResolve(array[num3++], null) as PdfReaderStream;
                if (stream == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                if (num2 > 0)
                {
                    this.content = this.content + "\n";
                }
                byte[] uncompressedData = stream.UncompressedData;
                this.content = this.content + Encoding.UTF8.GetString(uncompressedData, 0, uncompressedData.Length);
                num2++;
            }
        }

        internal PdfWriterStream Write(PdfObjectCollection objects) => 
            PdfWriterStream.CreateCompressedStream(new PdfWriterDictionary(objects), Encoding.UTF8.GetBytes(this.content));

        public string Content =>
            this.content;
    }
}

