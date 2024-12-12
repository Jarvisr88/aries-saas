namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public abstract class HtmlExportOptionsBase : PageByPageExportOptionsBase
    {
        private const int DefaultPageBorderWidth = 1;
        private static readonly Color DefaultPageBorderColor = Color.Black;
        private const bool DefaultEmbedImagesInHTML = false;
        private const bool DefaultExportWatermarks = true;
        private const bool DefaultRemoveSecondarySymbols = false;
        private const bool DefaultExportOnlyDocumentBody = false;
        private const bool DefaultTableLayout = true;
        private const bool DefaultInlineCss = false;
        private const bool DefaultUseHRefHyperlinks = false;
        private const bool DefaultAllowURLsWithJSContent = false;
        private Color pageBorderColor;
        private int pageBorderWidth;
        private const HtmlExportMode DefaultExportMode = HtmlExportMode.SingleFile;
        protected const string DefaultCharacterSet = "utf-8";
        protected const string DefaultTitle = "Document";
        private HtmlExportMode exportMode;
        private string characterSet;
        private string title;
        private bool removeSecondarySymbols;
        private bool exportOnlyDocumentBody;
        private bool tableLayout;
        private bool exportWatermarks;
        private bool inlineCss;
        private bool embedImagesInHTML;
        private bool useHRefHyperlinks;
        private bool allowURLsWithJSContent;

        public HtmlExportOptionsBase()
        {
            this.pageBorderColor = DefaultPageBorderColor;
            this.pageBorderWidth = 1;
            this.characterSet = "utf-8";
            this.title = "Document";
            this.tableLayout = true;
            this.exportWatermarks = true;
        }

        protected HtmlExportOptionsBase(HtmlExportOptionsBase source) : base(source)
        {
            this.pageBorderColor = DefaultPageBorderColor;
            this.pageBorderWidth = 1;
            this.characterSet = "utf-8";
            this.title = "Document";
            this.tableLayout = true;
            this.exportWatermarks = true;
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            HtmlExportOptionsBase base2 = (HtmlExportOptionsBase) source;
            this.exportMode = base2.ExportMode;
            this.characterSet = base2.CharacterSet;
            this.removeSecondarySymbols = base2.RemoveSecondarySymbols;
            this.title = base2.Title;
            this.pageBorderColor = base2.PageBorderColor;
            this.pageBorderWidth = base2.PageBorderWidth;
            this.embedImagesInHTML = base2.EmbedImagesInHTML;
            this.tableLayout = base2.TableLayout;
            this.exportWatermarks = base2.exportWatermarks;
            this.inlineCss = base2.inlineCss;
            this.useHRefHyperlinks = base2.useHRefHyperlinks;
            this.allowURLsWithJSContent = base2.allowURLsWithJSContent;
            this.exportOnlyDocumentBody = base2.exportOnlyDocumentBody;
        }

        protected internal override bool ShouldSerialize() => 
            this.ShouldSerializePageBorderColor() || ((this.pageBorderWidth != 1) || ((this.exportMode != HtmlExportMode.SingleFile) || ((this.characterSet != "utf-8") || ((this.title != "Document") || (this.removeSecondarySymbols || (this.embedImagesInHTML || (!this.tableLayout || (!this.exportWatermarks || (this.inlineCss || (this.useHRefHyperlinks || (this.allowURLsWithJSContent || base.ShouldSerialize())))))))))));

        private bool ShouldSerializePageBorderColor() => 
            this.PageBorderColor != DefaultPageBorderColor;

        [Description("Gets or sets the color of page borders when a document is exported to HTML page-by-page."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.PageBorderColor"), TypeConverter(typeof(HtmlPageBorderColorConverter)), XtraSerializableProperty]
        public Color PageBorderColor
        {
            get => 
                this.pageBorderColor;
            set => 
                this.pageBorderColor = value;
        }

        [Description("Gets or sets the width (in pixels) of page borders when a document is exported to HTML page-by-page."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.PageBorderWidth"), DefaultValue(1), TypeConverter(typeof(HtmlPageBorderWidthConverter)), XtraSerializableProperty]
        public int PageBorderWidth
        {
            get => 
                this.pageBorderWidth;
            set => 
                this.pageBorderWidth = value;
        }

        [Description("Gets or sets a value indicating how a document is exported to HTML."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.ExportMode"), DefaultValue(0), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public HtmlExportMode ExportMode
        {
            get => 
                this.exportMode;
            set => 
                this.exportMode = value;
        }

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase
        {
            get
            {
                switch (this.ExportMode)
                {
                    case HtmlExportMode.SingleFile:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFile;

                    case HtmlExportMode.SingleFilePageByPage:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;

                    case HtmlExportMode.DifferentFiles:
                        return DevExpress.XtraPrinting.ExportModeBase.DifferentFiles;
                }
                throw new NotSupportedException();
            }
        }

        [Description("Gets or sets the range of pages to be exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.PageRange"), TypeConverter(typeof(HtmlPageRangeConverter)), XtraSerializableProperty]
        public override string PageRange
        {
            get => 
                base.PageRange;
            set => 
                base.PageRange = value;
        }

        [Description("Gets or sets the encoding name used when exporting a document to HTML."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.CharacterSet"), TypeConverter(typeof(CharSetConverter)), DefaultValue("utf-8"), Localizable(true), XtraSerializableProperty]
        public string CharacterSet
        {
            get => 
                this.characterSet;
            set => 
                this.characterSet = value;
        }

        [Description("Gets or sets a title of the created HTML file"), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.Title"), DefaultValue("Document"), Localizable(true), XtraSerializableProperty]
        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        [Description("Gets or sets a value indicating whether secondary symbols should be removed from the resulting HTML file, to reduce its size."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.RemoveSecondarySymbols"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(false), XtraSerializableProperty]
        public bool RemoveSecondarySymbols
        {
            get => 
                this.removeSecondarySymbols;
            set => 
                this.removeSecondarySymbols = value;
        }

        [Description("Specifies whether or not images are embedded in HTML content."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptions.EmbedImagesInHTML"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(false), XtraSerializableProperty]
        public virtual bool EmbedImagesInHTML
        {
            get => 
                this.embedImagesInHTML;
            set => 
                this.embedImagesInHTML = value;
        }

        [Description("Determines whether to use the table or non-table layout in the resulting HTML file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.TableLayout"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), XtraSerializableProperty]
        public bool TableLayout
        {
            get => 
                this.tableLayout;
            set => 
                this.tableLayout = value;
        }

        [Description("Specifies whether to export watermarks to HTML along with the rest of the document content."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.ExportWatermarks"), DefaultValue(true), TypeConverter(typeof(HtmlExportWatermarksConverter)), XtraSerializableProperty]
        public bool ExportWatermarks
        {
            get => 
                this.exportWatermarks;
            set => 
                this.exportWatermarks = value;
        }

        [Description("Specifies whether the style properties are written to the <head> section of an HTML document, or they are defined at the same place where a specific style is assigned in a document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.InlineCss"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public virtual bool InlineCss
        {
            get => 
                this.inlineCss;
            set => 
                this.inlineCss = value;
        }

        [Description("Specifies whether or not the document navigation is implemented by using scripts."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.UseHRefHyperlinks"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public virtual bool UseHRefHyperlinks
        {
            get => 
                this.useHRefHyperlinks;
            set => 
                this.useHRefHyperlinks = value;
        }

        [Description("Gets or sets whether the JavaScript code can be placed in URLs in the resulting HTML document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptionsBase.AllowURLsWithJSContent"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool AllowURLsWithJSContent
        {
            get => 
                this.allowURLsWithJSContent;
            set => 
                this.allowURLsWithJSContent = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DXHelpExclude(true)]
        public override bool RasterizeImages
        {
            get => 
                true;
            set
            {
            }
        }

        internal virtual bool ExportOnlyDocumentBody
        {
            get => 
                this.exportOnlyDocumentBody;
            set => 
                this.exportOnlyDocumentBody = value;
        }
    }
}

