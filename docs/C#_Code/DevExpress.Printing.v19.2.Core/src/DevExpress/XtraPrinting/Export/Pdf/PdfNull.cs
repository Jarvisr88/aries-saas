namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfNull : PdfObject
    {
        protected override void WriteContent(StreamWriter writer)
        {
            writer.Write("null");
        }
    }
}

