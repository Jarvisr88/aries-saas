namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;

    public class PSWebControl : PSWebControlBase
    {
        private DXHtmlTable contentTable;

        public PSWebControl(Document document, IImageRepository imageRepository, HtmlExportOptionsBase options) : base(document, imageRepository, options)
        {
        }

        protected override void CreatePages()
        {
            this.contentTable = base.CreateHtmlLayoutTable(new HtmlLayoutBuilder(base.document, base.htmlExportContext), base.options.TableLayout);
            base.AddChildrenControl(this.contentTable);
        }

        public DXHtmlTable ContentTable =>
            this.contentTable;
    }
}

