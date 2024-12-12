namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PdfCommandList : List<PdfCommand>
    {
        public PdfCommandList()
        {
        }

        public PdfCommandList(IEnumerable<PdfCommand> commands) : base(commands)
        {
        }

        internal byte[] ToByteArray(PdfResources resources)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfDocumentStream writer = PdfDocumentStream.CreateStreamForWriting(stream);
                foreach (PdfCommand command in this)
                {
                    command.Write(resources, writer);
                }
                return stream.ToArray();
            }
        }
    }
}

