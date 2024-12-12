namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.DataNodes;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public abstract class TemplatedLink : DevExpress.Xpf.Printing.LinkBase
    {
        private const float buildPagesRange = 75f;
        private const float postBuildPagesRange = 25f;
        private IRootDataNode rootNode;
        public static readonly DependencyProperty ReportHeaderDataProperty = DependencyPropertyManager.Register("ReportHeaderData", typeof(object), typeof(TemplatedLink), new PropertyMetadata(null));
        public static readonly DependencyProperty ReportFooterDataProperty = DependencyPropertyManager.Register("ReportFooterData", typeof(object), typeof(TemplatedLink), new PropertyMetadata(null));
        public static readonly DependencyProperty PageHeaderDataProperty = DependencyPropertyManager.Register("PageHeaderData", typeof(object), typeof(TemplatedLink), new PropertyMetadata(null));
        public static readonly DependencyProperty PageFooterDataProperty = DependencyPropertyManager.Register("PageFooterData", typeof(object), typeof(TemplatedLink), new PropertyMetadata(null));
        public static readonly DependencyProperty TopMarginDataProperty = DependencyPropertyManager.Register("TopMarginData", typeof(object), typeof(TemplatedLink), new PropertyMetadata(null));
        public static readonly DependencyProperty BottomMarginDataProperty = DependencyPropertyManager.Register("BottomMarginData", typeof(object), typeof(TemplatedLink), new PropertyMetadata(null));

        private event EventHandler DocumentBuildingCompleted;

        protected TemplatedLink() : this(null, string.Empty)
        {
        }

        protected TemplatedLink(PrintingSystem ps) : this(ps, string.Empty)
        {
        }

        protected TemplatedLink(string documentName) : this(null, documentName)
        {
        }

        protected TemplatedLink(PrintingSystem ps, string documentName) : base(ps, documentName)
        {
        }

        protected override void AfterBuildPages()
        {
            base.PrintingSystem.Lock();
            this.BrickCollector.Clear();
            new DocumentMapBuilder().Build(base.PrintingSystem.Document);
            this.BandInitializer.Clear();
            if (!this.PreventDisposing)
            {
                Action<IDisposable> action = <>c.<>9__103_0;
                if (<>c.<>9__103_0 == null)
                {
                    Action<IDisposable> local1 = <>c.<>9__103_0;
                    action = <>c.<>9__103_0 = x => x.Dispose();
                }
                (this.RootNode as IDisposable).Do<IDisposable>(action);
            }
            base.AfterBuildPages();
        }

        protected virtual void Build(bool buildPagesInBackground)
        {
            this.RootNode = this.CreateRootNode();
            this.BuildCore();
            if (buildPagesInBackground)
            {
                this.RaiseDocumentBuildingCompleted();
            }
        }

        protected virtual void BuildCore()
        {
            VisualDataNodeBandManager manager = this.CreateVisualDataNodeBandManager();
            manager.Initialize(base.PrintingSystem.PrintingDocument.Root);
            if (manager.totalDetailCount == -1)
            {
                float[] ranges = new float[] { float.NaN, float.NaN };
                base.PrintingSystem.ProgressReflector.SetProgressRanges(ranges);
            }
            if (this.ColumnWidth > 0f)
            {
                float columnWidth = GraphicsUnitConverter.Convert(this.ColumnWidth, (float) 96f, (float) 300f);
                if (columnWidth > 0f)
                {
                    int columnCount = this.GetColumnCount(columnWidth);
                    if (columnCount > 0)
                    {
                        base.PrintingSystem.PrintingDocument.Root.MultiColumn = new MultiColumn(columnCount, columnWidth, this.ColumnLayout);
                    }
                }
            }
            base.PrintingSystem.PrintingDocument.Root.BandManager = manager;
        }

        private void BuildHeaderFooterBands()
        {
            this.BrickCollector.VisualTreeWalker = new VisualTreeWalker();
            DocumentBand root = base.PrintingSystem.PrintingDocument.Root;
            root.Bands.Add(this.CreateDocumentBand(DocumentBandKind.TopMargin, new Func<bool, RowViewInfo>(this.GetTopMarginRowViewInfo)));
            root.Bands.Add(this.CreateDocumentBand(DocumentBandKind.ReportHeader, new Func<bool, RowViewInfo>(this.GetReportHeaderRowViewInfo)));
            root.Bands.Add(this.CreateDocumentBand(DocumentBandKind.PageHeader, new Func<bool, RowViewInfo>(this.GetPageHeaderRowViewInfo)));
            root.Bands.Add(this.CreateDocumentBand(DocumentBandKind.PageFooter, new Func<bool, RowViewInfo>(this.GetPageFooterRowViewInfo)));
            DocumentBand item = this.CreateDocumentBand(DocumentBandKind.ReportFooter, new Func<bool, RowViewInfo>(this.GetReportFooterRowViewInfo));
            item.PrintAtBottom = this.PrintReportFooterAtBottom;
            root.Bands.Add(item);
            root.Bands.Add(this.CreateDocumentBand(DocumentBandKind.BottomMargin, new Func<bool, RowViewInfo>(this.GetBottomMarginRowViewInfo)));
        }

        private void ClearBandInitializer()
        {
            if (this.BandInitializer != null)
            {
                this.BandInitializer.Clear();
                Action<DevExpress.Xpf.Printing.BrickCollection.BrickCollector> action = <>c.<>9__86_0;
                if (<>c.<>9__86_0 == null)
                {
                    Action<DevExpress.Xpf.Printing.BrickCollection.BrickCollector> local1 = <>c.<>9__86_0;
                    action = <>c.<>9__86_0 = x => x.BrickUpdaters.Clear();
                }
                this.BrickCollector.Do<DevExpress.Xpf.Printing.BrickCollection.BrickCollector>(action);
                this.BandInitializer = null;
            }
            if (this.BrickCollector != null)
            {
                this.BrickCollector.Clear();
                this.BrickCollector.BrickUpdaters.Clear();
                this.BrickCollector = null;
            }
        }

        private void CreateBandInitializer(System.Windows.Size usablePageSize, bool rightToLeftLayout)
        {
            Action<DevExpress.Xpf.Printing.BrickCollection.BrickCollector> action = <>c.<>9__87_0;
            if (<>c.<>9__87_0 == null)
            {
                Action<DevExpress.Xpf.Printing.BrickCollection.BrickCollector> local1 = <>c.<>9__87_0;
                action = <>c.<>9__87_0 = x => x.BrickUpdaters.Clear();
            }
            this.BrickCollector.Do<DevExpress.Xpf.Printing.BrickCollection.BrickCollector>(action);
            this.BrickCollector = new DevExpress.Xpf.Printing.BrickCollection.BrickCollector(base.PrintingSystem);
            this.BandInitializer = new DocumentBandInitializer(this.BrickCollector, usablePageSize, rightToLeftLayout);
        }

        private DocumentBand CreateDocumentBand(DocumentBandKind bandKind, Func<bool, RowViewInfo> getRowViewInfo)
        {
            DocumentBand band = new DocumentBand(bandKind);
            this.BandInitializer.Initialize(band, getRowViewInfo);
            return band;
        }

        protected override void CreateDocumentCore(bool buildPagesInBackground, bool applyPageSettings)
        {
            base.PrintingSystem.Unlock();
            base.PrintingSystem.Graph.PageUnit = GraphicsUnit.Document;
            base.PrintingSystem.ClearContent();
            this.ClearBandInitializer();
            ImageRepository.Clear(RepositoryImageHelper.GetDocumentId(base.PrintingSystem));
            base.PrintingSystem.Begin();
            if (applyPageSettings)
            {
                XtraPageSettingsBase.ApplyPageSettings(base.PrintingSystem.PageSettings, base.PaperKind, base.CustomPaperSize, base.Margins, base.MinMargins, base.Landscape);
            }
            float[] ranges = new float[] { 75f, 25f };
            base.PrintingSystem.ProgressReflector.SetProgressRanges(ranges);
            if (!string.IsNullOrEmpty(base.DocumentName))
            {
                base.PrintingSystem.Document.Name = base.DocumentName;
            }
            base.PrintingSystem.PrintingDocument.VerticalContentSplitting = base.VerticalContentSplitting;
            base.PrintingSystem.Document.RightToLeftLayout = this.IsDocumentLayoutRightToLeft();
            this.CreateBandInitializer(DrawingConverter.ToSize(base.PrintingSystem.PageSettings.UsablePageSizeInPixels), this.IsDocumentLayoutRightToLeft());
            this.BuildHeaderFooterBands();
            if (buildPagesInBackground)
            {
                this.DocumentBuildingCompleted += new EventHandler(this.TemplatedLink_DocumentBuildingCompleted);
                this.Build(buildPagesInBackground);
            }
            else
            {
                this.Build(buildPagesInBackground);
                this.End(buildPagesInBackground);
            }
        }

        protected abstract IRootDataNode CreateRootNode();
        internal virtual VisualDataNodeBandManager CreateVisualDataNodeBandManager() => 
            new VisualDataNodeBandManager(this.RootNode, this.BandInitializer);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ClearBandInitializer();
            }
            base.Dispose(disposing);
        }

        internal virtual void End(bool buildPagesInBackground)
        {
            base.PrintingSystem.End(buildPagesInBackground);
        }

        internal RowViewInfo GetBottomMarginRowViewInfo(bool allowContentReuse) => 
            (this.BottomMarginTemplate == null) ? null : new RowViewInfo(this.BottomMarginTemplate, this.BottomMarginData);

        private int GetColumnCount(float columnWidth)
        {
            int num2 = (int) (GraphicsUnitConverter.Convert(base.PrintingSystem.PageSettings.UsablePageSize.Width, (float) 100f, (float) 300f) / columnWidth);
            return ((num2 > 0) ? num2 : 1);
        }

        internal RowViewInfo GetPageFooterRowViewInfo(bool allowContentReuse) => 
            (this.PageFooterTemplate == null) ? null : new RowViewInfo(this.PageFooterTemplate, this.PageFooterData);

        internal RowViewInfo GetPageHeaderRowViewInfo(bool allowContentReuse) => 
            (this.PageHeaderTemplate == null) ? null : new RowViewInfo(this.PageHeaderTemplate, this.PageHeaderData);

        internal RowViewInfo GetReportFooterRowViewInfo(bool allowContentReuse) => 
            (this.ReportFooterTemplate == null) ? null : new RowViewInfo(this.ReportFooterTemplate, this.ReportFooterData);

        internal RowViewInfo GetReportHeaderRowViewInfo(bool allowContentReuse) => 
            (this.ReportHeaderTemplate == null) ? null : new RowViewInfo(this.ReportHeaderTemplate, this.ReportHeaderData);

        internal RowViewInfo GetTopMarginRowViewInfo(bool allowContentReuse) => 
            (this.TopMarginTemplate == null) ? null : new RowViewInfo(this.TopMarginTemplate, this.TopMarginData);

        protected abstract bool IsDocumentLayoutRightToLeft();
        protected void RaiseDocumentBuildingCompleted()
        {
            if (this.DocumentBuildingCompleted != null)
            {
                this.DocumentBuildingCompleted(this, EventArgs.Empty);
            }
        }

        private void TemplatedLink_DocumentBuildingCompleted(object sender, EventArgs e)
        {
            this.DocumentBuildingCompleted -= new EventHandler(this.TemplatedLink_DocumentBuildingCompleted);
            this.End(base.BuildPagesInBackground);
        }

        [Description("Specifies whether the report footer is printed at the bottom of the page, or immediately after the report content.")]
        public bool PrintReportFooterAtBottom { get; set; }

        [Description("Gets or sets the width of a single column.")]
        public float ColumnWidth { get; set; }

        [Description("Gets or sets the column layout.")]
        public DevExpress.XtraPrinting.ColumnLayout ColumnLayout { get; set; }

        [Description("Specifies the top margin template for the document.")]
        public DataTemplate TopMarginTemplate { get; set; }

        [Description("Specifies the bottom margin template for the document.")]
        public DataTemplate BottomMarginTemplate { get; set; }

        [Description("Specifies the page header template for the document.")]
        public DataTemplate PageHeaderTemplate { get; set; }

        [Description("Specifies the page footer template for the document.")]
        public DataTemplate PageFooterTemplate { get; set; }

        [Description("Specifies the report header template for the document.")]
        public DataTemplate ReportHeaderTemplate { get; set; }

        [Description("Specifies the report footer template for the document.")]
        public DataTemplate ReportFooterTemplate { get; set; }

        [Description("Specifies the content for the document's report header. This is a dependency property.")]
        public object ReportHeaderData
        {
            get => 
                base.GetValue(ReportHeaderDataProperty);
            set => 
                base.SetValue(ReportHeaderDataProperty, value);
        }

        [Description("Specifies the content for the document's report footer. This is a dependency property.")]
        public object ReportFooterData
        {
            get => 
                base.GetValue(ReportFooterDataProperty);
            set => 
                base.SetValue(ReportFooterDataProperty, value);
        }

        [Description("Specifies the content for the document's page header. This is a dependency property.")]
        public object PageHeaderData
        {
            get => 
                base.GetValue(PageHeaderDataProperty);
            set => 
                base.SetValue(PageHeaderDataProperty, value);
        }

        [Description("Specifies the content for the document's page footer. This is a dependency property.")]
        public object PageFooterData
        {
            get => 
                base.GetValue(PageFooterDataProperty);
            set => 
                base.SetValue(PageFooterDataProperty, value);
        }

        [Description("Specifies the content for the document's top margin. This is a dependency property.")]
        public object TopMarginData
        {
            get => 
                base.GetValue(TopMarginDataProperty);
            set => 
                base.SetValue(TopMarginDataProperty, value);
        }

        [Description("Specifies the content for the document's bottom margin. This is a dependency property.")]
        public object BottomMarginData
        {
            get => 
                base.GetValue(BottomMarginDataProperty);
            set => 
                base.SetValue(BottomMarginDataProperty, value);
        }

        protected DocumentBandInitializer BandInitializer { get; private set; }

        protected DevExpress.Xpf.Printing.BrickCollection.BrickCollector BrickCollector { get; private set; }

        protected internal IRootDataNode RootNode
        {
            get => 
                this.rootNode;
            set
            {
                if ((value != null) && !ReferenceEquals(this.rootNode, value))
                {
                    this.rootNode = value;
                }
            }
        }

        internal bool PreventDisposing { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TemplatedLink.<>c <>9 = new TemplatedLink.<>c();
            public static Action<DevExpress.Xpf.Printing.BrickCollection.BrickCollector> <>9__86_0;
            public static Action<DevExpress.Xpf.Printing.BrickCollection.BrickCollector> <>9__87_0;
            public static Action<IDisposable> <>9__103_0;

            internal void <AfterBuildPages>b__103_0(IDisposable x)
            {
                x.Dispose();
            }

            internal void <ClearBandInitializer>b__86_0(DevExpress.Xpf.Printing.BrickCollection.BrickCollector x)
            {
                x.BrickUpdaters.Clear();
            }

            internal void <CreateBandInitializer>b__87_0(DevExpress.Xpf.Printing.BrickCollection.BrickCollector x)
            {
                x.BrickUpdaters.Clear();
            }
        }
    }
}

