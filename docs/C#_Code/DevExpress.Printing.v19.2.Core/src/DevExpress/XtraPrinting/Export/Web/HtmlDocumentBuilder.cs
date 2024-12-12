namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Net;
    using System.Text;

    public class HtmlDocumentBuilder : HtmlDocumentBuilderBase
    {
        private const string FilesDirSuffix = "_files";
        private const int bufferSize = 0x400;
        protected static readonly Color defaultBgColor = Color.White;
        protected HtmlExportOptionsBase options;
        private Color bgColor;
        private Margins bodyMargins;
        private bool rightToLeftLayout;

        public HtmlDocumentBuilder(HtmlExportOptionsBase options) : this(options, defaultBgColor)
        {
        }

        public HtmlDocumentBuilder(HtmlExportOptionsBase options, Color bgColor)
        {
            this.bgColor = defaultBgColor;
            this.options = options;
            this.bgColor = bgColor;
            this.bodyMargins = new Margins(10, 10, 10, 10);
        }

        public void CreateDocument(Document document, Stream stream, IImageRepository imageRepository)
        {
            PSWebControlBase webControl = this.CreateWebControl(document, imageRepository);
            if (webControl != null)
            {
                this.rightToLeftLayout = webControl.RightToLeftLayout;
                using (StreamWriter writer = CreateNonClosableStreamWriter(stream))
                {
                    DXHtmlTextWriter writer2 = this.ShouldCreateCompressedWriter ? new CompressedHtmlTextWriter(writer) : new DXHtmlTextWriter(writer);
                    this.CreateDocumentCore(writer2, webControl, imageRepository);
                }
            }
        }

        public override void CreateDocumentCore(DXHtmlTextWriter writer, DXWebControlBase webControl, IImageRepository imageRepository)
        {
            string str;
            TextWriter innerWriter = writer.InnerWriter;
            using (StringWriter writer3 = new StringWriter())
            {
                writer.InnerWriter = writer3;
                base.CreateDocumentCore(writer, webControl, imageRepository);
                writer.Flush();
                str = writer3.ToString();
            }
            writer.InnerWriter = innerWriter;
            if (!this.options.ExportOnlyDocumentBody)
            {
                this.WriteHeader(writer, webControl as PSWebControlBase);
            }
            writer.Write(str);
            if (!this.options.ExportOnlyDocumentBody)
            {
                this.WriteFooter(writer);
            }
            writer.Flush();
        }

        protected internal virtual IImageRepository CreateImageRepository(Stream stream)
        {
            if (!this.options.EmbedImagesInHTML && !(stream is FileStream))
            {
                return new InMemoryHtmlImageRepository("_files");
            }
            string filePath = string.Empty;
            if (stream is FileStream)
            {
                filePath = ((FileStream) stream).Name;
            }
            return CreatePSImageRepository(filePath, this.options.EmbedImagesInHTML);
        }

        private static StreamWriter CreateNonClosableStreamWriter(Stream stream) => 
            new StreamWriter(stream, new UTF8Encoding(false, true), 0x400, true);

        internal static ImageRepositoryBase CreatePSImageRepository(string filePath, bool embedImagesInHTML)
        {
            if (embedImagesInHTML)
            {
                return new CssImageRepository();
            }
            string str = string.IsNullOrEmpty(filePath) ? string.Empty : Path.GetDirectoryName(filePath);
            string str2 = Path.GetFileNameWithoutExtension(filePath) + "_files";
            DeleteExistingDirectory(Path.Combine(str, str2));
            return new HtmlImageRepository(str, str2);
        }

        protected PSWebControlBase CreateWebControl(Document document, IImageRepository imageRepository)
        {
            HtmlExportMode exportMode = this.options.ExportMode;
            return ((exportMode == HtmlExportMode.SingleFile) ? ((PSWebControlBase) new PSWebControl(document, imageRepository, this.options)) : ((exportMode == HtmlExportMode.SingleFilePageByPage) ? ((PSWebControlBase) new PSMultiplePageWebControl(document, imageRepository, this.options)) : null));
        }

        private static void DeleteExistingDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        protected virtual void WriteFooter(DXHtmlTextWriter writer)
        {
            writer.WriteEndTag("body");
            writer.WriteLine();
            writer.WriteEndTag("html");
        }

        protected virtual void WriteHeader(DXHtmlTextWriter writer, PSWebControlBase psWebControl)
        {
            writer.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            writer.WriteLine();
            writer.Write("<!-- saved from url=(0016)http://localhost -->");
            writer.WriteLine();
            writer.WriteFullBeginTag(this.rightToLeftLayout ? "html dir=\"rtl\"" : "html");
            writer.WriteLine();
            writer.WriteFullBeginTag("head");
            writer.WriteLine();
            writer.Indent++;
            writer.WriteFullBeginTag("title");
            writer.Write(this.EncodedTitle);
            writer.WriteEndTag("title");
            writer.WriteLine();
            if (!string.IsNullOrEmpty(this.EncodedCharSet))
            {
                writer.Write($"<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset={this.EncodedCharSet}"/>");
                writer.WriteLine();
            }
            if (psWebControl != null)
            {
                psWebControl.Styles.RenderControl(writer);
            }
            writer.Indent--;
            writer.WriteEndTag("head");
            writer.WriteLine();
            string tagName = $"body leftMargin={this.bodyMargins.Left} topMargin={this.bodyMargins.Top} rightMargin={this.bodyMargins.Right} bottomMargin={this.bodyMargins.Bottom}";
            if (this.options.ExportMode == HtmlExportMode.SingleFile)
            {
                tagName = tagName + $" style="background-color:{HtmlConvert.ToHtml(this.bgColor)}"";
            }
            writer.WriteFullBeginTag(tagName);
            writer.WriteLine();
        }

        public Margins BodyMargins
        {
            get => 
                this.bodyMargins;
            set => 
                this.bodyMargins = value;
        }

        protected string EncodedCharSet =>
            DXHttpUtility.HtmlAttributeEncode(this.options.CharacterSet);

        protected string EncodedTitle =>
            string.IsNullOrEmpty(this.options.Title) ? "XtraExport" : WebUtility.HtmlEncode(this.options.Title);

        protected bool Compressed =>
            this.options.RemoveSecondarySymbols;

        protected virtual bool ShouldCreateCompressedWriter =>
            this.Compressed;
    }
}

