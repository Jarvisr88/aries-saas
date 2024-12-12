namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Export.Web;
    using System;
    using System.Drawing;

    public class HtmlExportContext : ExportContext
    {
        private readonly IScriptContainer scriptContainer;
        private readonly IImageRepository imageRepository;
        private readonly HtmlExportOptionsBase options;
        private DevExpress.XtraPrinting.Export.Web.HtmlCellImageContentCreator htmlCellImageContentCreator;

        public HtmlExportContext(PrintingSystemBase ps, IScriptContainer scriptContainer, IImageRepository imageRepository);
        public HtmlExportContext(PrintingSystemBase ps, IScriptContainer scriptContainer, IImageRepository imageRepository, HtmlExportOptionsBase options);
        protected virtual DevExpress.XtraPrinting.Export.Web.HtmlCellImageContentCreator CreateCssHtmlCellImageContentCreator();
        protected virtual DevExpress.XtraPrinting.Export.Web.HtmlCellImageContentCreator CreateHtmlCellImageContentCreator();
        protected virtual DevExpress.XtraPrinting.Export.Web.HtmlCellImageContentCreator CreateMhtCellImageContentCreator();
        public override BrickViewData[] GetData(Brick brick, RectangleF rect, RectangleF clipRect);
        protected internal virtual void RegisterNavigationScript();

        internal override PageByPageExportOptionsBase ExportOptions { get; }

        public bool CrossReferenceAvailable { get; }

        public bool InlineCss { get; }

        public bool TableLayout { get; }

        public bool UseHRefHyperlinks { get; }

        public bool AllowURLsWithJSContent { get; }

        public HtmlExportMode MainExportMode { get; }

        public IScriptContainer ScriptContainer { get; }

        public IImageRepository ImageRepository { get; }

        public virtual bool CopyStyleWhenClipping { get; }

        protected bool InMhtContext { get; }

        public virtual bool ShouldBlockBookmarks { get; }

        public virtual bool IsPageExport { get; }

        public DevExpress.XtraPrinting.Export.Web.HtmlCellImageContentCreator HtmlCellImageContentCreator { get; }
    }
}

