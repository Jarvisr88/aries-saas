namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.CompilerServices;

    [ToolboxItem(false), DesignTimeVisible(false)]
    public class LinkBase : Component, ILink2, ILink, ICommandHandler, IDocumentSource
    {
        protected DevExpress.XtraPrinting.PrintingSystemBase ps;
        protected bool fLandscape;
        protected System.Drawing.Printing.Margins fMargins;
        protected System.Drawing.Printing.Margins fMinMargins;
        protected BrickModifier skipModifier;
        protected System.Drawing.Printing.PaperKind fPaperKind;
        protected Size fCustomPaperSize;
        private GraphicsUnit initialPageUnit;
        private bool initialDip;
        private bool rightToLeftLayout;
        private DevExpress.XtraPrinting.VerticalContentSplitting fVerticalContentSplitting;
        private LinkBase owner;
        protected DevExpress.XtraPrinting.PageHeaderFooter pageHF;
        protected bool fEnablePageDialog;
        protected string fPaperName;
        private PrintingSystemActivity activity;
        private int subreportLevel;
        private static readonly object BeforeCreateEvent = new object();
        private static readonly object AfterCreateEvent = new object();
        private static readonly object CreateMarginalHeaderEvent = new object();
        private static readonly object CreateMarginalFooterEvent = new object();
        private static readonly object CreateInnerPageHeaderEvent = new object();
        private static readonly object CreateInnerPageFooterEvent = new object();
        private static readonly object CreateReportHeaderEvent = new object();
        private static readonly object CreateReportFooterEvent = new object();
        private static readonly object CreateDetailHeaderEvent = new object();
        private static readonly object CreateDetailFooterEvent = new object();
        private static readonly object CreateDetailEvent = new object();

        [Description("Occurs after all sections of the document have been generated."), Category("Report Area")]
        public event EventHandler AfterCreateAreas
        {
            add
            {
                base.Events.AddHandler(AfterCreateEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(AfterCreateEvent, value);
            }
        }

        [Description("Occurs before any section of the document is generated."), Category("Report Area")]
        public event EventHandler BeforeCreateAreas
        {
            add
            {
                base.Events.AddHandler(BeforeCreateEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(BeforeCreateEvent, value);
            }
        }

        [Description("Occurs when a detail section of the document is being generated."), Category("Report Area")]
        public virtual event CreateAreaEventHandler CreateDetailArea
        {
            add
            {
                base.Events.AddHandler(CreateDetailEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateDetailEvent, value);
            }
        }

        [Description("Occurs when a detail footer section of the document is being generated."), Category("Report Area")]
        public virtual event CreateAreaEventHandler CreateDetailFooterArea
        {
            add
            {
                base.Events.AddHandler(CreateDetailFooterEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateDetailFooterEvent, value);
            }
        }

        [Description("Occurs when a detail header section of the document is being generated."), Category("Report Area")]
        public virtual event CreateAreaEventHandler CreateDetailHeaderArea
        {
            add
            {
                base.Events.AddHandler(CreateDetailHeaderEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateDetailHeaderEvent, value);
            }
        }

        [Description("Occurs when an inner page footer section of the document is being generated."), Category("Report Area")]
        public virtual event CreateAreaEventHandler CreateInnerPageFooterArea
        {
            add
            {
                base.Events.AddHandler(CreateInnerPageFooterEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateInnerPageFooterEvent, value);
            }
        }

        [Description("Occurs when an inner page header section of the document is being generated."), Category("Report Area")]
        public virtual event CreateAreaEventHandler CreateInnerPageHeaderArea
        {
            add
            {
                base.Events.AddHandler(CreateInnerPageHeaderEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateInnerPageHeaderEvent, value);
            }
        }

        [Description("Occurs when a marginal page footer section of the document is being generated."), Category("Report Area")]
        public event CreateAreaEventHandler CreateMarginalFooterArea
        {
            add
            {
                base.Events.AddHandler(CreateMarginalFooterEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateMarginalFooterEvent, value);
            }
        }

        [Description("Occurs when a marginal page header section of the document is being generated."), Category("Report Area")]
        public event CreateAreaEventHandler CreateMarginalHeaderArea
        {
            add
            {
                base.Events.AddHandler(CreateMarginalHeaderEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateMarginalHeaderEvent, value);
            }
        }

        [Description("Occurs when a report footer section of the document is being generated."), Category("Report Area")]
        public event CreateAreaEventHandler CreateReportFooterArea
        {
            add
            {
                base.Events.AddHandler(CreateReportFooterEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateReportFooterEvent, value);
            }
        }

        [Description("Occurs when a report header section of the document is being generated."), Category("Report Area")]
        public event CreateAreaEventHandler CreateReportHeaderArea
        {
            add
            {
                base.Events.AddHandler(CreateReportHeaderEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(CreateReportHeaderEvent, value);
            }
        }

        public LinkBase()
        {
            this.fMargins = XtraPageSettingsBase.DefaultMargins;
            this.fMinMargins = XtraPageSettingsBase.DefaultMinMargins;
            this.fPaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.fCustomPaperSize = Size.Empty;
            this.fVerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
            this.pageHF = new DevExpress.XtraPrinting.PageHeaderFooter();
            this.fEnablePageDialog = true;
            this.fPaperName = XtraPageSettingsBase.DefaultPaperName;
            this.RtfReportHeader = string.Empty;
            this.RtfReportFooter = string.Empty;
        }

        public LinkBase(DevExpress.XtraPrinting.PrintingSystemBase ps) : this()
        {
            this.ps = ps;
        }

        public LinkBase(IContainer container) : this()
        {
            container.Add(this);
        }

        public virtual void AddSubreport(PointF offset)
        {
            if (offset.Y > 0f)
            {
                this.InsertSpanBeforeSubreport(offset.Y);
            }
            DocumentBand band = this.ps.Document.AddReportContainer();
            band.AddBand(new DocumentBand(DocumentBandKind.ReportHeader));
            DocumentBand docBand = new DocumentBandContainer();
            band.AddBand(docBand);
            band.AddBand(new DocumentBand(DocumentBandKind.ReportFooter));
            this.AddSubreportInternal(docBand, PointF.Empty);
        }

        public virtual void AddSubreport(DevExpress.XtraPrinting.PrintingSystemBase ps, DocumentBand band, PointF offset)
        {
            this.ps = ps;
            this.AddSubreportInternal(band, offset);
        }

        private void AddSubreportInternal(DocumentBand docBand, PointF offset)
        {
            if (this.ps != null)
            {
                BrickGraphics graph = this.ps.Graph;
                BrickModifier modifier = graph.Modifier;
                this.subreportLevel++;
                try
                {
                    this.ps.BeginSubreportInternal(docBand, offset);
                    this.BeforeCreate();
                    this.CreateReportArea(new CreateAreaDelegate(this.CreateReportHeader), graph, BrickModifier.None | BrickModifier.ReportHeader);
                    BrickModifier skipArea = this.SkipArea;
                    this.SkipArea |= this.InternalSkipArea;
                    this.CreateReportArea(new CreateAreaDelegate(this.CreateDetailHeader), graph, BrickModifier.DetailHeader);
                    this.CreateReportArea(new CreateAreaDelegate(this.CreateDetail), graph, BrickModifier.Detail);
                    this.CreateReportArea(new CreateAreaDelegate(this.CreateDetailFooter), graph, BrickModifier.DetailFooter);
                    this.SkipArea = skipArea;
                    this.CreateReportArea(new CreateAreaDelegate(this.CreateReportFooter), graph, BrickModifier.None | BrickModifier.ReportFooter);
                    this.AfterCreate();
                    this.ps.EndSubreport();
                }
                finally
                {
                    graph.Modifier = modifier;
                    this.subreportLevel--;
                }
            }
        }

        protected virtual void AfterCreate()
        {
            this.ps.Graph.PageUnit = this.initialPageUnit;
            this.ps.Graph.DeviceIndependentPixel = this.initialDip;
        }

        protected virtual void ApplyPageSettings()
        {
            if (!this.ApplyPageSettingsCore())
            {
                this.PrintingSystemBase.PageSettings.Assign(this.GetValidMargins(this.fMargins, new System.Drawing.Printing.Margins(0, 0, 0, 0)), this.fPaperKind, this.fPaperName, this.fLandscape);
            }
        }

        protected bool ApplyPageSettingsCore() => 
            XtraPageSettingsBase.ApplyPageSettings(this.ps.PageSettings, this.fPaperKind, this.fCustomPaperSize, this.GetValidMargins(this.fMargins, this.fMinMargins), this.fMinMargins, this.fLandscape, this.fPaperName);

        protected virtual void BeforeCreate()
        {
            this.initialPageUnit = this.ps.Graph.PageUnit;
            this.initialDip = this.ps.Graph.DeviceIndependentPixel;
            if ((base.DesignMode || this.EnablePageDialog) && (this.PrintingSystemBase.Document is PSLinkDocument))
            {
                this.EnableCommand(PrintingSystemCommand.EditPageHF, true);
            }
        }

        internal virtual void BeforeDestroy()
        {
            this.DisableCommands();
        }

        public virtual bool CanHandleCommand(PrintingSystemCommand command, IPrintControl printControl) => 
            false;

        protected virtual bool CanHandleCommandInternal(PrintingSystemCommand command, IPrintControl printControl) => 
            command == PrintingSystemCommand.EditPageHF;

        public void ClearDocument()
        {
            this.PrintingSystemBase.ClearContent();
        }

        protected virtual void CreateDetail(BrickGraphics graph)
        {
            this.OnCreateDetail(new CreateAreaEventArgs(graph));
        }

        protected virtual void CreateDetailFooter(BrickGraphics graph)
        {
            this.OnCreateDetailFooter(new CreateAreaEventArgs(graph));
        }

        protected virtual void CreateDetailHeader(BrickGraphics graph)
        {
            this.OnCreateDetailHeader(new CreateAreaEventArgs(graph));
        }

        public virtual void CreateDocument()
        {
            this.ExecuteActivity(PrintingSystemActivity.Preparing, () => this.CreateDocument(false));
        }

        public virtual void CreateDocument(DevExpress.XtraPrinting.PrintingSystemBase ps)
        {
            if (ps != null)
            {
                this.ps = ps;
                this.CreateDocument();
            }
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void CreateDocument(bool buildForInstantPreview)
        {
            if (this.owner != null)
            {
                this.owner.CreateDocument();
            }
            else if ((this.ps != null) && !this.ps.Locked)
            {
                if (this.ps.RaisingAfterChange)
                {
                    this.ClearDocument();
                    this.ps.DelayedAction |= PrintingSystemAction.CreateDocument;
                }
                else
                {
                    ((ISupportInitialize) this.ps).BeginInit();
                    PrintingDocument document = this.ps.Document as PrintingDocument;
                    if (document != null)
                    {
                        document.VerticalContentSplitting = this.VerticalContentSplitting;
                    }
                    if (!this.ps.PageSettings.IsPreset)
                    {
                        this.ApplyPageSettings();
                    }
                    this.ps.PageSettings.IsPreset = false;
                    this.ps.Document.RightToLeftLayout = this.RightToLeftLayout;
                    this.ps.Begin();
                    this.OnBeforeCreate(EventArgs.Empty);
                    this.BeforeCreate();
                    try
                    {
                        this.CreateReportArea(new CreateAreaDelegate(this.CreateMarginalHeader), this.ps.Graph, BrickModifier.MarginalHeader);
                        this.CreateReportArea(new CreateAreaDelegate(this.CreateMarginalFooter), this.ps.Graph, BrickModifier.MarginalFooter);
                        this.CreateReportArea(new CreateAreaDelegate(this.CreateInnerPageHeader), this.ps.Graph, BrickModifier.InnerPageHeader);
                        this.CreateReportArea(new CreateAreaDelegate(this.CreateInnerPageFooter), this.ps.Graph, BrickModifier.InnerPageFooter);
                        this.CreateReportArea(new CreateAreaDelegate(this.CreateReportHeader), this.ps.Graph, BrickModifier.None | BrickModifier.ReportHeader);
                        this.CreateDocumentCore();
                        this.CreateReportArea(new CreateAreaDelegate(this.CreateReportFooter), this.ps.Graph, BrickModifier.None | BrickModifier.ReportFooter);
                        this.OnAfterCreate(EventArgs.Empty);
                        this.AfterCreate();
                        this.ps.End(this, buildForInstantPreview);
                    }
                    finally
                    {
                        ((ISupportInitialize) this.ps).EndInit();
                    }
                }
            }
        }

        protected virtual void CreateDocumentCore()
        {
            BrickModifier skipArea = this.SkipArea;
            this.SkipArea |= this.InternalSkipArea;
            this.CreateReportArea(new CreateAreaDelegate(this.CreateDetailHeader), this.ps.Graph, BrickModifier.DetailHeader);
            this.CreateReportArea(new CreateAreaDelegate(this.CreateDetail), this.ps.Graph, BrickModifier.Detail);
            this.CreateReportArea(new CreateAreaDelegate(this.CreateDetailFooter), this.ps.Graph, BrickModifier.DetailFooter);
            this.SkipArea = skipArea;
        }

        protected virtual void CreateInnerPageFooter(BrickGraphics graph)
        {
            this.DrawBrick(graph, this.InnerPageFooter);
            this.OnCreateInnerPageFooter(new CreateAreaEventArgs(graph));
        }

        protected virtual void CreateInnerPageHeader(BrickGraphics graph)
        {
            this.DrawBrick(graph, this.InnerPageHeader);
            this.OnCreateInnerPageHeader(new CreateAreaEventArgs(graph));
        }

        protected virtual void CreateMarginalFooter(BrickGraphics graph)
        {
            if (this.pageHF != null)
            {
                this.pageHF.CreateMarginalFooter(graph, this.GetImageArray());
            }
            this.OnCreateMarginalFooter(new CreateAreaEventArgs(graph));
        }

        protected virtual void CreateMarginalHeader(BrickGraphics graph)
        {
            if (this.pageHF != null)
            {
                this.pageHF.CreateMarginalHeader(graph, this.GetImageArray());
            }
            this.OnCreateMarginalHeader(new CreateAreaEventArgs(graph));
        }

        private void CreateReportArea(CreateAreaDelegate proc, BrickGraphics gr, BrickModifier modifier)
        {
            gr.Modifier = modifier;
            if (this.EnableCreate(gr.Modifier))
            {
                proc(gr);
            }
        }

        protected virtual void CreateReportFooter(BrickGraphics graph)
        {
            if (this.AllowRtfHederFooter)
            {
                this.DrawBrick(graph, this.ReportFooter);
            }
            this.OnCreateReportFooter(new CreateAreaEventArgs(graph));
        }

        protected virtual void CreateReportHeader(BrickGraphics graph)
        {
            if (this.AllowRtfHederFooter)
            {
                this.DrawBrick(graph, this.ReportHeader);
            }
            this.OnCreateReportHeader(new CreateAreaEventArgs(graph));
        }

        protected virtual void DisableCommands()
        {
            this.EnableCommand(PrintingSystemCommand.EditPageHF, false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.ps != null))
            {
                this.ps.FinalizeLink(this);
                this.ps = null;
            }
            base.Dispose(disposing);
        }

        private void DrawBrick(BrickGraphics graph, string rtf)
        {
            if (!string.IsNullOrEmpty(rtf) && (this.ps != null))
            {
                IRichTextBrick brick = ((IPrintingSystem) this.ps).CreateRichTextBrick();
                VisualBrickHelper.SetBrickBounds((VisualBrick) brick, new RectangleF(0f, 0f, graph.ClientPageSize.Width, (float) brick.InfiniteHeight), graph.Dpi);
                ((VisualBrick) brick).Style = new BrickStyle(BorderSide.None, 0f, DXColor.Empty, DXColor.Empty, DXColor.Black, BrickStyle.DefaultFont, new BrickStringFormat());
                brick.RtfText = rtf;
                float height = GraphicsUnitConverter.Convert((float) brick.EffectiveHeight, (float) 300f, graph.Dpi);
                graph.DrawBrick((VisualBrick) brick, new RectangleF(0f, 0f, graph.ClientPageSize.Width, height));
            }
        }

        protected void EnableCommand(PrintingSystemCommand command, bool enabled)
        {
            this.PrintingSystemBase.SetCommandVisibility(command, enabled ? CommandVisibility.All : CommandVisibility.None, Priority.Low);
            this.PrintingSystemBase.EnableCommandInternal(command, enabled);
        }

        private bool EnableCreate(BrickModifier modifier) => 
            ((modifier & this.skipModifier) == BrickModifier.None) || (modifier == BrickModifier.Detail);

        internal void ExecuteActivity(PrintingSystemActivity activity, Action0 callback)
        {
            this.PrintingSystemBase.ResetCancelPending();
            this.activity = activity;
            this.OnStartActivity();
            try
            {
                callback();
            }
            finally
            {
                this.OnEndActivity();
            }
        }

        protected void ExecuteExport(PrintingSystemActivity activity, ExportOptionsBase options, Action0 callback)
        {
            if (!this.IsDocumentEmpty)
            {
                this.ExecuteActivity(activity, () => callback());
            }
            else
            {
                activity |= PrintingSystemActivity.Preparing;
                this.ExecuteActivity(activity, delegate {
                    this.CreateDocument(false);
                    if (!this.IsDocumentEmpty)
                    {
                        callback();
                    }
                });
            }
        }

        public void ExportToCsv(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToCsv(stream, this.ExportOptions.Csv);
        }

        public void ExportToCsv(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToCsv(filePath, this.ExportOptions.Csv);
        }

        public void ExportToCsv(Stream stream, CsvExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToCsv(stream, options));
        }

        public void ExportToCsv(string filePath, CsvExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToCsv(filePath, options));
        }

        public void ExportToDocx(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToDocx(stream, this.ExportOptions.Docx);
        }

        public void ExportToDocx(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToDocx(filePath, this.ExportOptions.Docx);
        }

        public void ExportToDocx(Stream stream, DocxExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToDocx(stream, options));
        }

        public void ExportToDocx(string filePath, DocxExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToDocx(filePath, options));
        }

        public void ExportToHtml(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToHtml(stream, this.ExportOptions.Html);
        }

        public void ExportToHtml(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToHtml(filePath, this.ExportOptions.Html);
        }

        public void ExportToHtml(Stream stream, HtmlExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToHtml(stream, options));
        }

        public void ExportToHtml(string filePath, HtmlExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToHtml(filePath, options));
        }

        public void ExportToImage(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToImage(stream, this.ExportOptions.Image);
        }

        public void ExportToImage(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToImage(filePath, this.ExportOptions.Image);
        }

        public void ExportToImage(Stream stream, ImageExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToImage(stream, options));
        }

        public void ExportToImage(string filePath, ImageExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToImage(filePath, options));
        }

        public void ExportToMht(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToMht(stream, this.ExportOptions.Mht);
        }

        public void ExportToMht(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToMht(filePath, this.ExportOptions.Mht);
        }

        public void ExportToMht(Stream stream, MhtExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToMht(stream, options));
        }

        public void ExportToMht(string filePath, MhtExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToMht(filePath, options));
        }

        public void ExportToPdf(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToPdf(stream, this.ExportOptions.Pdf);
        }

        public void ExportToPdf(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToPdf(filePath, this.ExportOptions.Pdf);
        }

        public void ExportToPdf(Stream stream, PdfExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToPdf(stream, options));
        }

        public void ExportToPdf(string filePath, PdfExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToPdf(filePath, options));
        }

        public void ExportToRtf(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToRtf(stream, this.ExportOptions.Rtf);
        }

        public void ExportToRtf(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToRtf(filePath, this.ExportOptions.Rtf);
        }

        public void ExportToRtf(Stream stream, RtfExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToRtf(stream, options));
        }

        public void ExportToRtf(string filePath, RtfExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToRtf(filePath, options));
        }

        public void ExportToText(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToText(stream, this.ExportOptions.Text);
        }

        public void ExportToText(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToText(filePath, this.ExportOptions.Text);
        }

        public void ExportToText(Stream stream, TextExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToText(stream, options));
        }

        public void ExportToText(string filePath, TextExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToText(filePath, options));
        }

        public void ExportToXls(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToXls(stream, this.ExportOptions.Xls);
        }

        public void ExportToXls(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToXls(filePath, this.ExportOptions.Xls);
        }

        public void ExportToXls(Stream stream, XlsExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToXls(stream, options));
        }

        public void ExportToXls(string filePath, XlsExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToXls(filePath, options));
        }

        public void ExportToXlsx(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.ExportToXlsx(stream, this.ExportOptions.Xlsx);
        }

        public void ExportToXlsx(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            this.ExportToXlsx(filePath, this.ExportOptions.Xlsx);
        }

        public void ExportToXlsx(Stream stream, XlsxExportOptions options)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToXlsx(stream, options));
        }

        public void ExportToXlsx(string filePath, XlsxExportOptions options)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");
            Guard.ArgumentNotNull(options, "options");
            this.ExecuteExport(PrintingSystemActivity.Exporting, options, () => this.PrintingSystemBase.ExportToXlsx(filePath, options));
        }

        protected virtual System.Drawing.Image[] GetImageArray() => 
            new System.Drawing.Image[0];

        private System.Drawing.Printing.Margins GetValidMargins(System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins)
        {
            if ((this.pageHF == null) || !this.pageHF.IncreaseMarginsByContent)
            {
                return margins;
            }
            System.Drawing.Image[] images = this.GetImageArray();
            SizeF ef = GraphicsUnitConverter.Convert(this.pageHF.MeasureMarginalHeader(this.ps.Graph, images), this.ps.Graph.Dpi, (float) 100f);
            SizeF ef2 = GraphicsUnitConverter.Convert(this.pageHF.MeasureMarginalFooter(this.ps.Graph, images), this.ps.Graph.Dpi, (float) 100f);
            return new System.Drawing.Printing.Margins(margins.Left, margins.Right, (int) Math.Max((float) margins.Top, ef.Height + minMargins.Top), (int) Math.Max((float) margins.Bottom, ef2.Height + minMargins.Bottom));
        }

        public virtual void HandleCommand(PrintingSystemCommand command, object[] args, IPrintControl printControl, ref bool handled)
        {
        }

        private void InsertSpanBeforeSubreport(float span)
        {
            this.ps.Document.AddReportContainer().AddBand(DocumentBand.CreateSpanBand(DocumentBandKind.ReportHeader, span));
        }

        protected virtual void OnAfterCreate(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[AfterCreateEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBeforeCreate(EventArgs e)
        {
            this.PrintingSystemBase.AddCommandHandler(this);
            EventHandler handler = (EventHandler) base.Events[BeforeCreateEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateDetail(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateDetailEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateDetailFooter(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateDetailFooterEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateDetailHeader(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateDetailHeaderEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateInnerPageFooter(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateInnerPageFooterEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateInnerPageHeader(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateInnerPageHeaderEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateMarginalFooter(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateMarginalFooterEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateMarginalHeader(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateMarginalHeaderEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateReportFooter(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateReportFooterEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnCreateReportHeader(CreateAreaEventArgs e)
        {
            CreateAreaEventHandler handler = (CreateAreaEventHandler) base.Events[CreateReportHeaderEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnEndActivity()
        {
        }

        protected virtual void OnStartActivity()
        {
        }

        private void RestorePageHeaderFooterCore(XtraSerializer serializer, object path)
        {
            serializer.DeserializeObject(this.PageHeaderFooter, path, "XtraPrintingPageHeaderFooter");
        }

        public void RestorePageHeaderFooterFromRegistry(string path)
        {
            this.RestorePageHeaderFooterCore(new RegistryXtraSerializer(), path);
        }

        public void RestorePageHeaderFooterFromStream(Stream stream)
        {
            this.RestorePageHeaderFooterCore(new XmlXtraSerializer(), stream);
        }

        public void RestorePageHeaderFooterFromXml(string xmlFile)
        {
            this.RestorePageHeaderFooterCore(new XmlXtraSerializer(), xmlFile);
        }

        private void SavePageHeaderFooterCore(XtraSerializer serializer, object path)
        {
            serializer.SerializeObject(this.PageHeaderFooter, path, "XtraPrintingPageHeaderFooter");
        }

        public void SavePageHeaderFooterToRegistry(string path)
        {
            this.SavePageHeaderFooterCore(new RegistryXtraSerializer(), path);
        }

        public void SavePageHeaderFooterToStream(Stream stream)
        {
            this.SavePageHeaderFooterCore(new XmlXtraSerializer(), stream);
        }

        public void SavePageHeaderFooterToXml(string xmlFile)
        {
            this.SavePageHeaderFooterCore(new XmlXtraSerializer(), xmlFile);
        }

        public virtual void SetDataObject(object data)
        {
        }

        private bool ShouldSerializeCustomPaperSize() => 
            this.fCustomPaperSize != Size.Empty;

        protected bool ShouldSerializeMargins() => 
            !this.fMargins.Equals(XtraPageSettingsBase.DefaultMargins);

        protected bool ShouldSerializeMinMargins() => 
            !this.fMinMargins.Equals(XtraPageSettingsBase.DefaultMinMargins);

        protected bool ShouldSerializePageHeaderFooter() => 
            (this.pageHF != null) && this.pageHF.ShouldSerialize();

        protected bool ShouldSerializePaperKind() => 
            !this.fPaperKind.Equals(System.Drawing.Printing.PaperKind.Letter);

        protected virtual void UpdatePageSettings()
        {
            try
            {
                this.fMargins = this.ps.PageSettings.Margins;
                this.fLandscape = this.ps.PageSettings.Landscape;
                this.fPaperKind = this.ps.PageSettings.PaperKind;
                this.fPaperName = this.PrintingSystemBase.PageSettings.PaperName;
            }
            catch
            {
            }
        }

        internal void UpdatePageSettingsInternal()
        {
            this.UpdatePageSettings();
        }

        protected virtual string InnerPageHeader =>
            string.Empty;

        protected virtual string InnerPageFooter =>
            string.Empty;

        protected virtual string ReportHeader =>
            this.RtfReportHeader;

        protected virtual string ReportFooter =>
            this.RtfReportFooter;

        [Browsable(false)]
        public PrintingSystemActivity Activity =>
            this.activity;

        private bool IsDocumentEmpty =>
            this.PrintingSystemBase.Document.IsEmpty;

        private DevExpress.XtraPrinting.ExportOptions ExportOptions =>
            this.PrintingSystemBase.ExportOptions;

        [Description("Gets or sets the name of the custom paper which is used in the printer that the document is going to be printed on."), Category("Page Layout"), DefaultValue("")]
        public string PaperName
        {
            get => 
                this.fPaperName;
            set => 
                this.fPaperName = value;
        }

        [Description("Enables the Header and Footer dialog used for editing a document page's headers and footers."), DefaultValue(true), Category("HeadersFooters")]
        public bool EnablePageDialog
        {
            get => 
                this.fEnablePageDialog;
            set => 
                this.fEnablePageDialog = value;
        }

        [Browsable(true), Description("Gets or sets the object used to fill the document's area that is occupied by the page header and page footer."), Category("HeadersFooters"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Editor("DevExpress.XtraPrinting.Design.PageHeaderFooterEditor,DevExpress.XtraPrinting.v19.2.Design, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public virtual object PageHeaderFooter
        {
            get => 
                this.pageHF;
            set
            {
                if (value is DevExpress.XtraPrinting.PageHeaderFooter)
                {
                    this.pageHF = (DevExpress.XtraPrinting.PageHeaderFooter) value;
                }
            }
        }

        [Description("Gets or sets the RTF text, which will be printed as a Report Header."), Category("HeadersFooters"), DefaultValue(""), Editor("DevExpress.XtraPrinting.Design.RichTextEditor,DevExpress.XtraPrinting.v19.2.Design, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string RtfReportHeader { get; set; }

        [Description("Gets or sets the RTF text, which will be printed as a Report Footer."), Category("HeadersFooters"), DefaultValue(""), Editor("DevExpress.XtraPrinting.Design.RichTextEditor,DevExpress.XtraPrinting.v19.2.Design, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string RtfReportFooter { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LinkBase Owner
        {
            get => 
                this.owner;
            set => 
                this.owner = value;
        }

        [Description("Gets the type of the object to be printed."), Browsable(false)]
        public virtual Type PrintableObjectType =>
            typeof(object);

        [Description("Gets or sets report areas that should be skipped."), Browsable(true), Category("Printing"), DefaultValue(0), Editor("DevExpress.XtraPrinting.Design.SelectAreaEditor,DevExpress.XtraPrinting.v19.2.Design, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public BrickModifier SkipArea
        {
            get => 
                this.skipModifier;
            set => 
                this.skipModifier = value;
        }

        [Category("Printing"), Description("Gets or sets a value indicating whether content bricks, which are outside the right page margin, should be split across pages, or moved in their entirety to the next page."), DefaultValue(1)]
        public DevExpress.XtraPrinting.VerticalContentSplitting VerticalContentSplitting
        {
            get => 
                this.fVerticalContentSplitting;
            set => 
                this.fVerticalContentSplitting = value;
        }

        [Category("Printing"), Description("Specifies the orientation of the document's content."), DefaultValue(false)]
        public bool RightToLeftLayout
        {
            get => 
                this.rightToLeftLayout;
            set => 
                this.rightToLeftLayout = value;
        }

        [Description("Gets or sets the type of paper for the document."), Category("Page Layout")]
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get => 
                this.fPaperKind;
            set => 
                this.fPaperKind = value;
        }

        [Description("Gets or sets a value indicating whether the page orientation is landscape."), Category("Page Layout"), DefaultValue(false)]
        public bool Landscape
        {
            get => 
                this.fLandscape;
            set => 
                this.fLandscape = value;
        }

        [Description("Specifies the minimum printer margin values."), Category("Page Layout")]
        public System.Drawing.Printing.Margins MinMargins
        {
            get => 
                this.fMinMargins;
            set => 
                this.fMinMargins = value;
        }

        [Description("Gets or sets the margins of a report page (measured in hundredths of an inch)."), Category("Page Layout")]
        public System.Drawing.Printing.Margins Margins
        {
            get => 
                this.fMargins;
            set => 
                this.fMargins = value;
        }

        [Browsable(false)]
        public Size CustomPaperSize
        {
            get => 
                this.fCustomPaperSize;
            set => 
                this.fCustomPaperSize = value;
        }

        [Browsable(false), DefaultValue((string) null)]
        public virtual DevExpress.XtraPrinting.PrintingSystemBase PrintingSystemBase
        {
            get => 
                this.ps;
            set => 
                this.ps = value;
        }

        protected virtual BrickModifier InternalSkipArea =>
            BrickModifier.None | BrickModifier.ReportFooter | BrickModifier.ReportHeader;

        private bool AllowRtfHederFooter =>
            (this.subreportLevel == 0) || ((this.subreportLevel == 1) && (this.Owner != null));

        IPrintingSystem ILink.PrintingSystem =>
            this.PrintingSystemBase;

        protected delegate void CreateAreaDelegate(BrickGraphics gr);
    }
}

