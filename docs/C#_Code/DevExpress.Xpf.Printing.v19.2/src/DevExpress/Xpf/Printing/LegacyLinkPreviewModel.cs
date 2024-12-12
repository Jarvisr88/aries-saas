namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.XamlExport;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    [Obsolete("The DocumentPreview and corresponding xxxPreviewModel classes and interfaces are now obsolete. Use the DocumentPreviewControl class to display a print preview. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444")]
    public class LegacyLinkPreviewModel : PrintingSystemPreviewModel
    {
        private readonly IScaleService scaleService;
        private ILink link;

        public LegacyLinkPreviewModel() : this(null)
        {
        }

        public LegacyLinkPreviewModel(ILink link) : this(link, new PageSettingsConfiguratorService(), new PrintService(), new ExportSendService(), new HighlightingService(), new ScaleService())
        {
            base.OnSourceChanged();
        }

        internal LegacyLinkPreviewModel(ILink link, IPageSettingsConfiguratorService pageSettingsConfiguratorService, IPrintService printService, IExportSendService exportSendService, IHighlightingService highlightService, IScaleService scaleService) : base(pageSettingsConfiguratorService, printService, exportSendService, highlightService)
        {
            this.link = link;
            this.scaleService = scaleService;
            base.OnSourceChanged();
        }

        protected override bool CanScale(object parameter) => 
            base.BuildPagesComplete && (this.PrintingSystem.Document.CanChangePageSettings && (!base.IsExporting && !base.IsSaving));

        protected override bool CanSetWatermark(object parameter) => 
            false;

        protected override bool CanShowParametersPanel(object parameter) => 
            false;

        protected override void CreateDocument(bool buildPagesInBackground)
        {
            this.link.CreateDocument(buildPagesInBackground);
        }

        protected override void Export(ExportFormat format)
        {
            throw new NotImplementedException();
        }

        protected override void HookPrintingSystem()
        {
            base.HookPrintingSystem();
            this.PrintingSystem.ReplaceService<BackgroundPageBuildEngineStrategy>(new DispatcherPageBuildStrategy());
        }

        protected override void Scale(object parameter)
        {
            this.scaleService.Scale(this, this.DialogService.GetParentWindow());
        }

        protected override void SetWatermark(object parameter)
        {
            throw new NotImplementedException();
        }

        protected override FrameworkElement VisualizePage(int pageIndex) => 
            new BrickPageVisualizer(TextMeasurementSystem.GdiPlus).Visualize((PSPage) this.PrintingSystem.Pages[pageIndex], pageIndex, this.PrintingSystem.Pages.Count);

        public ILink Link
        {
            get => 
                this.link;
            set
            {
                if (!ReferenceEquals(this.link, value))
                {
                    base.OnSourceChanging();
                    this.link = value;
                    base.OnSourceChanged();
                }
            }
        }

        public override bool IsEmptyDocument =>
            !this.IsCreating && (this.PageCount == 0);

        public override DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel =>
            null;

        protected internal override PrintingSystemBase PrintingSystem =>
            (this.link != null) ? ((PrintingSystemBase) this.link.PrintingSystem) : null;

        protected override ExportOptions DocumentExportOptions
        {
            get => 
                this.PrintingSystem.ExportOptions;
            set
            {
                throw new NotImplementedException();
            }
        }

        protected override AvailableExportModes DocumentExportModes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override List<ExportOptionKind> DocumentHiddenOptions
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

