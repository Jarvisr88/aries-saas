namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using System;

    public class PSMultiplePageWebControl : PSWebControlBase
    {
        public PSMultiplePageWebControl(Document document, IImageRepository imageRepository, HtmlExportOptionsBase options) : base(document, imageRepository, options)
        {
        }

        protected override void CreatePages()
        {
            AddPageControls(this, base.options);
        }
    }
}

