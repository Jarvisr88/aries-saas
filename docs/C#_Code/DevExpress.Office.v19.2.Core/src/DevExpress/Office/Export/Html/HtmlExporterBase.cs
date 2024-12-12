namespace DevExpress.Office.Export.Html
{
    using DevExpress.Office.Services;
    using DevExpress.Office.Services.Implementation;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Export.Web;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class HtmlExporterBase
    {
        protected const string htmlClipboardHeader = "Version:0.9\r\nStartHTML:>>>>>>>>>1\r\nEndHTML:>>>>>>>>>2\r\nStartFragment:>>>>>>>>>3\r\nEndFragment:>>>>>>>>>4";

        protected HtmlExporterBase(IServiceProvider serviceProvider)
        {
            Guard.ArgumentNotNull(serviceProvider, "serviceProvider");
            this.ServiceProvider = serviceProvider;
            this.StyleControl = new StyleWebControl();
        }

        protected virtual void CalculationClipboardHeaderValue(ChunkedStringBuilderClipboardWriter writer)
        {
            int length = "Version:0.9\r\nStartHTML:>>>>>>>>>1\r\nEndHTML:>>>>>>>>>2\r\nStartFragment:>>>>>>>>>3\r\nEndFragment:>>>>>>>>>4".Length;
            ChunkedStringBuilder stringBuilder = writer.GetStringBuilder();
            stringBuilder.Buffers[0].Replace(">>>>>>>>>1", length.ToString("D10"), 0, length);
            stringBuilder.Buffers[0].Replace(">>>>>>>>>2", writer.ByteCount.ToString("D10"), 0, length);
            stringBuilder.Buffers[0].Replace(">>>>>>>>>3", this.Fragment.StartFragment.ToString("D10"), 0, length);
            stringBuilder.Buffers[0].Replace(">>>>>>>>>4", this.Fragment.EndFragment.ToString("D10"), 0, length);
        }

        protected internal virtual void CreateHtmlDocument(DXHtmlTextWriter writer, DXWebControlBase body)
        {
            DXHtmlGenericControl control = new DXHtmlGenericControl(DXHtmlTextWriterTag.Html);
            if (!this.UseHtml5)
            {
                control.Attributes.Add("xmlns", "http://www.w3.org/1999/xhtml");
            }
            DXHtmlGenericControl child = new DXHtmlGenericControl(DXHtmlTextWriterTag.Head);
            control.Controls.Add(child);
            DXHtmlGenericControl control3 = new DXHtmlGenericControl(DXHtmlTextWriterTag.Meta);
            control3.Attributes.Add("http-equiv", "Content-Type");
            control3.Attributes.Add("content", $"text/html; charset={this.Encoding.WebName}");
            child.Controls.Add(control3);
            DXWebControlBase base2 = this.CreateTitle();
            child.Controls.Add(base2);
            if (this.ShouldWriteStyles())
            {
                this.WriteStyles(child);
            }
            control.Controls.Add(body);
            this.WriteHtmlDocumentPreamble(writer);
            writer.WriteLine();
            control.RenderControl(writer);
        }

        protected virtual DXHtmlTextWriter CreateHtmlTextWriter(TextWriter writer) => 
            new DXHtmlTextWriter(writer);

        protected virtual DXWebControlBase CreateTitle() => 
            new DXHtmlGenericControl(DXHtmlTextWriterTag.Title);

        public string Export()
        {
            ChunkedStringBuilder stringBuilder = new ChunkedStringBuilder();
            if (!this.ExportToClipboard)
            {
                using (ChunkedStringBuilderWriter writer2 = new ChunkedStringBuilderWriter(stringBuilder))
                {
                    this.Export(writer2);
                }
            }
            else
            {
                using (ChunkedStringBuilderClipboardWriter writer = new ChunkedStringBuilderClipboardWriter(stringBuilder))
                {
                    writer.WriteLine("Version:0.9\r\nStartHTML:>>>>>>>>>1\r\nEndHTML:>>>>>>>>>2\r\nStartFragment:>>>>>>>>>3\r\nEndFragment:>>>>>>>>>4");
                    this.Export(writer);
                    this.CalculationClipboardHeaderValue(writer);
                }
            }
            return stringBuilder.ToString();
        }

        public virtual void Export(TextWriter writer)
        {
            if (!this.EmbedImages)
            {
                this.ExportCore(writer);
            }
            else
            {
                IUriProvider provider = new DataStringUriProvider();
                IUriProviderService service = this.ServiceProvider.GetService(typeof(IUriProviderService)) as IUriProviderService;
                if (service != null)
                {
                    service.RegisterProvider(provider);
                }
                try
                {
                    this.ExportCore(writer);
                }
                finally
                {
                    if (service != null)
                    {
                        service.UnregisterProvider(provider);
                    }
                }
            }
            writer.Flush();
        }

        protected abstract void ExportBodyContent(DXWebControlBase root);
        protected virtual DXWebControlBase ExportBodyControl()
        {
            EmptyWebControl root = new EmptyWebControl();
            this.ExportBodyContent(root);
            DXWebControlBase body = new DXHtmlGenericControl(DXHtmlTextWriterTag.Body);
            this.SetupBodyTag(body);
            if (this.ExportToBodyTag && this.ShouldWriteStyles())
            {
                this.WriteStyles(body);
            }
            if (!this.ExportToClipboard)
            {
                body.Controls.Add(root);
            }
            else
            {
                this.Fragment = new DXHtmlClipboardFragment();
                this.Fragment.Controls.Add(root);
                body.Controls.Add(this.Fragment);
            }
            return body;
        }

        protected internal virtual void ExportCore(TextWriter writer)
        {
            using (DXHtmlTextWriter writer2 = this.CreateHtmlTextWriter(writer))
            {
                DXWebControlBase body = this.ExportBodyControl();
                if (this.ExportToBodyTag)
                {
                    body.RenderControl(writer2);
                }
                else
                {
                    this.CreateHtmlDocument(writer2, body);
                }
            }
        }

        protected internal string ExportCssProperiesToSeparateFile(StyleWebControl style)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                style.RenderStyles(writer);
                writer.Flush();
            }
            string styleText = sb.ToString();
            IUriProviderService service = this.ServiceProvider.GetService(typeof(IUriProviderService)) as IUriProviderService;
            return ((service == null) ? string.Empty : service.CreateCssUri(this.FilesPath, styleText, this.RelativeUri));
        }

        protected internal virtual IScriptContainer GetScriptContainer()
        {
            IScriptContainer service = this.ServiceProvider.GetService(typeof(IScriptContainer)) as IScriptContainer;
            return ((service == null) ? this.StyleControl : service);
        }

        protected void Initialize(string targetUri, bool useAbsolutePath)
        {
            this.FilesPath = !string.IsNullOrEmpty(targetUri) ? Path.Combine(Path.GetDirectoryName(targetUri), Path.GetFileNameWithoutExtension(targetUri) + "_files/") : "_files/";
            this.RelativeUri = !useAbsolutePath ? (Path.GetFileNameWithoutExtension(targetUri) + "_files/") : string.Empty;
            this.ScriptContainer = this.GetScriptContainer();
            this.ImageRepository = new ServiceBasedImageRepository(this.ServiceProvider, this.FilesPath, this.RelativeUri);
        }

        protected virtual void SetupBodyTag(DXWebControlBase body)
        {
        }

        protected internal bool ShouldWriteStyles() => 
            (this.StyleControl.Styles.Count > 0) || ((this.StyleControl.TagStyles.Count > 0) || !ReferenceEquals(this.ScriptContainer, this.StyleControl));

        protected internal virtual void WriteHtmlDocumentPreamble(DXHtmlTextWriter writer)
        {
            if (this.UseHtml5)
            {
                writer.Write("<!DOCTYPE html>");
            }
            else
            {
                writer.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            }
        }

        private void WriteStyleLink(DXWebControlBase control)
        {
            string str = this.ExportCssProperiesToSeparateFile(this.StyleControl);
            DXHtmlGenericControl child = new DXHtmlGenericControl(DXHtmlTextWriterTag.Link);
            child.Attributes.Add("rel", "stylesheet");
            child.Attributes.Add("type", "text/css");
            child.Attributes.Add("href", str);
            control.Controls.Add(child);
        }

        protected internal void WriteStyles(DXWebControlBase control)
        {
            if (this.ExportStylesAsStyleTag)
            {
                control.Controls.Add(this.StyleControl);
            }
            if (this.ExportStylesAsLink)
            {
                this.WriteStyleLink(control);
            }
        }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected StyleWebControl StyleControl { get; private set; }

        protected IScriptContainer ScriptContainer { get; private set; }

        protected IOfficeImageRepository ImageRepository { get; private set; }

        protected string FilesPath { get; private set; }

        protected string RelativeUri { get; private set; }

        protected abstract bool EmbedImages { get; }

        protected abstract bool ExportToBodyTag { get; }

        protected abstract bool ExportStylesAsStyleTag { get; }

        protected abstract bool ExportStylesAsLink { get; }

        protected abstract System.Text.Encoding Encoding { get; }

        protected virtual bool UseHtml5 =>
            false;

        protected virtual bool ExportToClipboard { get; set; }

        protected virtual DXHtmlClipboardFragment Fragment { get; set; }
    }
}

