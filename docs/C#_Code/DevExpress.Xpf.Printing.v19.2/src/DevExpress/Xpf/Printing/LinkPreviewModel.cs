namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    [Obsolete("The DocumentPreview and corresponding xxxPreviewModel classes and interfaces are now obsolete. Use the DocumentPreviewControl class to display a print preview. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444")]
    public class LinkPreviewModel : PrintingSystemPreviewModel
    {
        private static readonly TimeSpan blockingExportDelay = TimeSpan.FromMilliseconds(100.0);
        private DevExpress.Xpf.Printing.LinkBase link;

        public LinkPreviewModel() : this(null)
        {
        }

        public LinkPreviewModel(DevExpress.Xpf.Printing.LinkBase link)
        {
            this.link = link;
            base.OnSourceChanged();
        }

        protected override bool CanScale(object parameter) => 
            false;

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
            if (this.Link != null)
            {
                this.Link.CreateDocumentFinished += new EventHandler<EventArgs>(this.Link_CreateDocumentFinished);
            }
        }

        private void Link_CreateDocumentFinished(object sender, EventArgs e)
        {
            base.ToggleDocumentMap();
        }

        protected override void Scale(object parameter)
        {
            throw new NotSupportedException();
        }

        protected override void SetWatermark(object parameter)
        {
            throw new NotImplementedException();
        }

        protected override void UnhookPrintingSystem()
        {
            base.UnhookPrintingSystem();
            if (this.Link != null)
            {
                this.Link.CreateDocumentFinished -= new EventHandler<EventArgs>(this.Link_CreateDocumentFinished);
            }
        }

        protected override FrameworkElement VisualizePage(int pageIndex) => 
            this.link.VisualizePage(pageIndex);

        public DevExpress.Xpf.Printing.LinkBase Link
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

        protected internal override PrintingSystemBase PrintingSystem =>
            this.link?.PrintingSystem;

        protected override ExportOptions DocumentExportOptions
        {
            get => 
                this.PrintingSystem.ExportOptions;
            set => 
                this.PrintingSystem.ExportOptions.Assign(value);
        }

        protected override AvailableExportModes DocumentExportModes
        {
            get
            {
                AvailableExportModes availableExportModes = this.PrintingSystem.Document.AvailableExportModes;
                return new AvailableExportModes(availableExportModes.Rtf, availableExportModes.Docx, availableExportModes.Html.Exclude<HtmlExportMode>(HtmlExportMode.DifferentFiles), availableExportModes.Image.Exclude<ImageExportMode>(ImageExportMode.DifferentFiles), availableExportModes.Xls.Exclude<XlsExportMode>(XlsExportMode.DifferentFiles), availableExportModes.Xlsx.Exclude<XlsxExportMode>(XlsxExportMode.DifferentFiles));
            }
        }

        protected override List<ExportOptionKind> DocumentHiddenOptions =>
            this.PrintingSystem.ExportOptions.HiddenOptions;

        public override bool IsEmptyDocument =>
            !this.IsCreating && (this.PageCount == 0);

        public override DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel =>
            null;
    }
}

