namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    public class MhtDocumentBuilder : HtmlDocumentBuilder
    {
        private string partId;
        private DXHtmlTextWriter htmlWriter;

        public MhtDocumentBuilder(MhtExportOptions options) : this(options, HtmlDocumentBuilder.defaultBgColor)
        {
        }

        public MhtDocumentBuilder(MhtExportOptions options, Color bgColor) : base(options, bgColor)
        {
            this.partId = string.Empty;
            this.partId = Guid.NewGuid().ToString();
        }

        public override void CreateDocumentCore(DXHtmlTextWriter writer, DXWebControlBase webControl, IImageRepository imageRepository)
        {
            this.WriteMhtHeader(writer);
            TextWriter writer2 = new Base64Writer(writer);
            this.htmlWriter = base.Compressed ? new CompressedHtmlTextWriter(writer2) : new DXHtmlTextWriter(writer2);
            base.CreateDocumentCore(this.htmlWriter, webControl, imageRepository);
            this.htmlWriter.Close();
            this.htmlWriter = null;
            this.WriteImages(writer, (MhtImageRepository) imageRepository);
            writer.Flush();
        }

        protected internal override IImageRepository CreateImageRepository(Stream stream) => 
            new MhtImageRepository();

        private void WriteImage(DXHtmlTextWriter writer, DevExpress.XtraPrinting.Export.Web.ImageInfo imageInfo)
        {
            writer.WriteLine("------=_NextPart_" + this.partId);
            writer.WriteLine("Content-Type: image/" + HtmlImageHelper.GetMimeType(imageInfo.Image));
            writer.WriteLine("Content-Transfer-Encoding: base64");
            writer.WriteLine("Content-ID: <" + imageInfo.ContentId + ">");
            writer.WriteLine();
            writer.Write(Convert.ToBase64String(HtmlImageHelper.ImageToArray(imageInfo.Image)));
            writer.WriteLine();
            writer.WriteLine();
        }

        private void WriteImages(DXHtmlTextWriter writer, MhtImageRepository imageRepository)
        {
            writer.WriteLine();
            writer.WriteLine();
            Dictionary<long, DevExpress.XtraPrinting.Export.Web.ImageInfo> imagesTable = imageRepository.ImagesTable;
            foreach (long num in imagesTable.Keys)
            {
                this.WriteImage(writer, imagesTable[num]);
            }
            writer.WriteLine("------=_NextPart_" + this.partId + "--");
            writer.WriteLine();
        }

        private void WriteMhtHeader(DXHtmlTextWriter writer)
        {
            writer.WriteLine("From: <Saved by DevExpress.XtraPrinting.v19.2>");
            writer.WriteLine("Subject: " + base.EncodedTitle);
            writer.WriteLine("MIME-Version: 1.0");
            writer.WriteLine("Content-Type: multipart/related;");
            writer.WriteLine("    boundary=\"----=_NextPart_" + this.partId + "\";");
            writer.WriteLine("    type=\"text/html\"");
            writer.WriteLine();
            writer.WriteLine("This is a multi-part message in MIME format.");
            writer.WriteLine();
            writer.WriteLine("------=_NextPart_" + this.partId);
            writer.WriteLine("Content-Type: text/html;");
            writer.WriteLine("    charset=\"" + base.EncodedCharSet + "\"");
            writer.WriteLine("Content-Transfer-Encoding: base64");
            writer.WriteLine("Content-Location: file://");
            writer.WriteLine();
        }

        protected override bool ShouldCreateCompressedWriter =>
            false;
    }
}

