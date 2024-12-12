namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Net.Mail;

    public class MailDocumentBuilder : HtmlDocumentBuilder
    {
        private string partId;

        public MailDocumentBuilder(MailMessageExportOptions options) : this(options, HtmlDocumentBuilder.defaultBgColor)
        {
        }

        public MailDocumentBuilder(MailMessageExportOptions options, Color bgColor) : base(options, bgColor)
        {
            this.partId = string.Empty;
            this.partId = Guid.NewGuid().ToString();
        }

        public AlternateView CreateDocument(Document document, IImageRepository imageRepository)
        {
            PSWebControlBase webControl = base.CreateWebControl(document, imageRepository);
            if (webControl == null)
            {
                return null;
            }
            StringWriter writer = new StringWriter();
            DXHtmlTextWriter writer2 = new DXHtmlTextWriter(writer);
            base.CreateDocumentCore(writer2, webControl, imageRepository);
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(writer.ToString(), null, "text/html");
            Dictionary<long, DevExpress.XtraPrinting.Export.Web.ImageInfo> imagesTable = ((MailImageRepository) imageRepository).ImagesTable;
            foreach (long num in imagesTable.Keys)
            {
                this.WriteImage(alternateView, writer2, imagesTable[num]);
            }
            return alternateView;
        }

        protected internal override IImageRepository CreateImageRepository(Stream stream) => 
            new MailImageRepository();

        private void WriteImage(AlternateView alternateView, DXHtmlTextWriter writer, DevExpress.XtraPrinting.Export.Web.ImageInfo imageInfo)
        {
            MemoryStream contentStream = new MemoryStream(HtmlImageHelper.ImageToArray(imageInfo.Image));
            LinkedResource item = new LinkedResource(contentStream, "image/" + HtmlImageHelper.GetMimeType(imageInfo.Image));
            item.ContentId = imageInfo.ContentId;
            alternateView.LinkedResources.Add(item);
        }
    }
}

