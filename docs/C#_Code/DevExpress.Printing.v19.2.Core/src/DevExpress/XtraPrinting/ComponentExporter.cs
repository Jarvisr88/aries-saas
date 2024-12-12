namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrintingLinks;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ComponentExporter : IDisposable
    {
        private bool rightToLeftLayout;
        private DevExpress.XtraPrinting.PrintingSystemBase printingSystem;
        private DevExpress.XtraPrinting.PrintingSystemBase outerPrintingSystem;
        private PageSettings pageSettings;
        private IPrintable component;
        private PrintableComponentLinkBase linkBase;

        public event EventHandler<ProgressEventArgs> Progress;

        public ComponentExporter(IPrintable component)
        {
            this.component = component;
        }

        public ComponentExporter(IPrintable component, DevExpress.XtraPrinting.PrintingSystemBase printingSystem)
        {
            this.component = component;
            this.outerPrintingSystem = printingSystem;
            this.outerPrintingSystem.AfterChange += new ChangeEventHandler(this.OnAfterChange);
        }

        private void ApplyPageSettings(PrintableComponentLinkBase printableLink)
        {
            if (IsActualPageSettings(this.pageSettings) && (printableLink != null))
            {
                printableLink.PaperKind = this.pageSettings.PaperSize.Kind;
                if ((this.pageSettings.PaperSize.Kind == PaperKind.Custom) && ((this.pageSettings.PaperSize.Width != 0) && (this.pageSettings.PaperSize.Height != 0)))
                {
                    printableLink.CustomPaperSize = new Size(this.pageSettings.PaperSize.Width, this.pageSettings.PaperSize.Height);
                }
                printableLink.PaperName = this.pageSettings.PaperSize.PaperName;
                printableLink.Landscape = this.pageSettings.Landscape;
                printableLink.Margins = this.pageSettings.Margins;
            }
        }

        public void ClearDocument()
        {
            this.LinkBase.ClearDocument();
        }

        public void CreateDocument()
        {
            this.LinkBase.CreateDocument();
        }

        protected virtual PrintableComponentLinkBase CreateLink() => 
            new PrintableComponentLinkBase();

        public void Dispose()
        {
            if (this.linkBase == null)
            {
                PrintableComponentLinkBase linkBase = this.linkBase;
            }
            else
            {
                this.linkBase.Dispose();
            }
            if (this.printingSystem != null)
            {
                this.printingSystem.Dispose();
                this.printingSystem.AfterChange -= new ChangeEventHandler(this.OnAfterChange);
            }
            if (this.outerPrintingSystem != null)
            {
                this.outerPrintingSystem.AfterChange -= new ChangeEventHandler(this.OnAfterChange);
            }
        }

        public virtual void Export(ExportTarget target, Stream stream)
        {
            switch (target)
            {
                case ExportTarget.Xls:
                    this.LinkBase.ExportToXls(stream);
                    return;

                case ExportTarget.Xlsx:
                    this.LinkBase.ExportToXlsx(stream);
                    return;

                case ExportTarget.Html:
                    this.LinkBase.ExportToHtml(stream);
                    return;

                case ExportTarget.Mht:
                    this.LinkBase.ExportToMht(stream);
                    return;

                case ExportTarget.Pdf:
                    this.LinkBase.ExportToPdf(stream);
                    return;

                case ExportTarget.Text:
                    this.LinkBase.ExportToText(stream);
                    return;

                case ExportTarget.Rtf:
                    this.LinkBase.ExportToRtf(stream);
                    return;

                case ExportTarget.Csv:
                    this.LinkBase.ExportToCsv(stream);
                    return;

                case ExportTarget.Image:
                    this.LinkBase.ExportToImage(stream);
                    return;

                case ExportTarget.Docx:
                    this.LinkBase.ExportToDocx(stream);
                    return;
            }
            throw new ArgumentOutOfRangeException("target");
        }

        public virtual void Export(ExportTarget target, string filePath)
        {
            switch (target)
            {
                case ExportTarget.Xls:
                    this.LinkBase.ExportToXls(filePath);
                    return;

                case ExportTarget.Xlsx:
                    this.LinkBase.ExportToXlsx(filePath);
                    return;

                case ExportTarget.Html:
                    this.LinkBase.ExportToHtml(filePath);
                    return;

                case ExportTarget.Mht:
                    this.LinkBase.ExportToMht(filePath);
                    return;

                case ExportTarget.Pdf:
                    this.LinkBase.ExportToPdf(filePath);
                    return;

                case ExportTarget.Text:
                    this.LinkBase.ExportToText(filePath);
                    return;

                case ExportTarget.Rtf:
                    this.LinkBase.ExportToRtf(filePath);
                    return;

                case ExportTarget.Csv:
                    this.LinkBase.ExportToCsv(filePath);
                    return;

                case ExportTarget.Image:
                    this.LinkBase.ExportToImage(filePath);
                    return;

                case ExportTarget.Docx:
                    this.LinkBase.ExportToDocx(filePath);
                    return;
            }
            throw new ArgumentOutOfRangeException("target");
        }

        public virtual void Export(ExportTarget target, Stream stream, ExportOptionsBase options)
        {
            switch (target)
            {
                case ExportTarget.Xls:
                    this.LinkBase.ExportToXls(stream, (XlsExportOptions) options);
                    return;

                case ExportTarget.Xlsx:
                    this.LinkBase.ExportToXlsx(stream, (XlsxExportOptions) options);
                    return;

                case ExportTarget.Html:
                    this.LinkBase.ExportToHtml(stream, (HtmlExportOptions) options);
                    return;

                case ExportTarget.Mht:
                    this.LinkBase.ExportToMht(stream, (MhtExportOptions) options);
                    return;

                case ExportTarget.Pdf:
                    this.LinkBase.ExportToPdf(stream, (PdfExportOptions) options);
                    return;

                case ExportTarget.Text:
                    this.LinkBase.ExportToText(stream, (TextExportOptions) options);
                    return;

                case ExportTarget.Rtf:
                    this.LinkBase.ExportToRtf(stream, (RtfExportOptions) options);
                    return;

                case ExportTarget.Csv:
                    this.LinkBase.ExportToCsv(stream, (CsvExportOptions) options);
                    return;

                case ExportTarget.Image:
                    this.LinkBase.ExportToImage(stream, (ImageExportOptions) options);
                    return;

                case ExportTarget.Docx:
                    this.LinkBase.ExportToDocx(stream, (DocxExportOptions) options);
                    return;
            }
            throw new ArgumentOutOfRangeException("target");
        }

        public virtual void Export(ExportTarget target, string filePath, ExportOptionsBase options)
        {
            switch (target)
            {
                case ExportTarget.Xls:
                    this.LinkBase.ExportToXls(filePath, (XlsExportOptions) options);
                    return;

                case ExportTarget.Xlsx:
                    this.LinkBase.ExportToXlsx(filePath, (XlsxExportOptions) options);
                    return;

                case ExportTarget.Html:
                    this.LinkBase.ExportToHtml(filePath, (HtmlExportOptions) options);
                    return;

                case ExportTarget.Mht:
                    this.LinkBase.ExportToMht(filePath, (MhtExportOptions) options);
                    return;

                case ExportTarget.Pdf:
                    this.LinkBase.ExportToPdf(filePath, (PdfExportOptions) options);
                    return;

                case ExportTarget.Text:
                    this.LinkBase.ExportToText(filePath, (TextExportOptions) options);
                    return;

                case ExportTarget.Rtf:
                    this.LinkBase.ExportToRtf(filePath, (RtfExportOptions) options);
                    return;

                case ExportTarget.Csv:
                    this.LinkBase.ExportToCsv(filePath, (CsvExportOptions) options);
                    return;

                case ExportTarget.Image:
                    this.LinkBase.ExportToImage(filePath, (ImageExportOptions) options);
                    return;

                case ExportTarget.Docx:
                    this.LinkBase.ExportToDocx(filePath, (DocxExportOptions) options);
                    return;
            }
            throw new ArgumentOutOfRangeException("target");
        }

        private static bool IsActualPageSettings(PageSettings pageSettings)
        {
            if (pageSettings == null)
            {
                return false;
            }
            try
            {
                PaperSize paperSize = pageSettings.PaperSize;
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void OnAfterChange(object sender, ChangeEventArgs e)
        {
            if (e.EventName == "ProgressPositionChanged")
            {
                int position = (int) e.ValueOf("ProgressPosition");
                int maximum = (int) e.ValueOf("ProgressMaximum");
                if (this.Progress != null)
                {
                    this.Progress(this, new ProgressEventArgs(position, maximum));
                }
            }
        }

        public void SetPageSettings(PageSettings newPageSettings)
        {
            this.pageSettings = (newPageSettings != null) ? ((PageSettings) newPageSettings.Clone()) : null;
            this.ApplyPageSettings(this.linkBase);
        }

        public bool RightToLeftLayout
        {
            get => 
                this.rightToLeftLayout;
            set
            {
                this.rightToLeftLayout = value;
                if (this.linkBase != null)
                {
                    this.linkBase.RightToLeftLayout = this.rightToLeftLayout;
                }
            }
        }

        public IPrintable Component
        {
            get => 
                this.component;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                this.component = value;
                if (this.linkBase != null)
                {
                    this.linkBase.Component = this.component;
                }
            }
        }

        public PrintingSystemActivity Activity =>
            this.LinkBase.Activity;

        protected PrintableComponentLinkBase LinkBase
        {
            get
            {
                if (this.linkBase == null)
                {
                    this.linkBase = this.CreateLink();
                    this.linkBase.PrintingSystemBase = this.PrintingSystemBase;
                    this.linkBase.Component = this.component;
                    this.linkBase.RightToLeftLayout = this.RightToLeftLayout;
                    this.ApplyPageSettings(this.linkBase);
                }
                return this.linkBase;
            }
        }

        public DevExpress.XtraPrinting.PrintingSystemBase PrintingSystemBase =>
            (this.outerPrintingSystem != null) ? this.outerPrintingSystem : this.InnerPrintingSystem;

        private DevExpress.XtraPrinting.PrintingSystemBase InnerPrintingSystem
        {
            get
            {
                if (this.printingSystem == null)
                {
                    this.printingSystem = new DevExpress.XtraPrinting.PrintingSystemBase();
                    this.printingSystem.AfterChange += new ChangeEventHandler(this.OnAfterChange);
                }
                return this.printingSystem;
            }
        }

        public bool IsDocumentEmpty =>
            this.PrintingSystemBase.Document.IsEmpty;
    }
}

