namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting.Export.Pdf.Compression;
    using System;
    using System.IO;

    public class PdfFlateStream : PdfStream
    {
        public PdfFlateStream()
        {
        }

        public PdfFlateStream(bool useLength1) : base(useLength1)
        {
        }

        protected override void FillAttributes(MemoryStream actualData)
        {
            base.FillAttributes(actualData);
            base.Attributes.Add("Filter", "FlateDecode");
        }

        protected override void WriteContent(StreamWriter writer)
        {
            MemoryStream actualData = Deflater.DeflateStream((MemoryStream) base.Data);
            try
            {
                base.WriteStream(writer, actualData);
            }
            finally
            {
                actualData.Close();
            }
        }
    }
}

