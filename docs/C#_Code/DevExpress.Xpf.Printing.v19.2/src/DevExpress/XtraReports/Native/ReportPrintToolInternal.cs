namespace DevExpress.XtraReports.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class ReportPrintToolInternal : IReportPrintTool, IDisposable
    {
        private readonly IReport report;
        private Window previewWindow;
        private bool disposed;
        private bool? approved;

        public ReportPrintToolInternal(IReport report)
        {
            Guard.ArgumentNotNull(report, "report");
            this.report = report;
            report.PrintTool = this;
        }

        private void CreateDocumentIfEmpty(bool buildPagesInBackground)
        {
            if (this.report.PrintingSystemBase.Document.PageCount == 0)
            {
                this.report.CreateDocument(buildPagesInBackground);
            }
        }

        private Window CreatePreviewWindow(Window ownerWindow)
        {
            DocumentPreviewWindow window1 = new DocumentPreviewWindow();
            window1.Owner = ownerWindow;
            DocumentPreviewWindow window = window1;
            window.PreviewControl.DocumentSource = this.report;
            return window;
        }

        bool IReportPrintTool.ApproveParameters(Parameter[] parameters, bool requestParameters)
        {
            bool? nullable;
            this.approved = nullable = new bool?(this.approved != null);
            return nullable.Value;
        }

        void IReportPrintTool.ClosePreview()
        {
            if (this.previewWindow != null)
            {
                this.previewWindow.Close();
            }
        }

        void IReportPrintTool.CloseRibbonPreview()
        {
            throw new NotSupportedException();
        }

        bool? IReportPrintTool.ShowPageSetupDialog(object ownerWindow) => 
            new PageSettingsConfiguratorService().Configure(this.report.PrintingSystemBase.PageSettings, ownerWindow as Window);

        void IReportPrintTool.ShowPreview()
        {
            throw new NotSupportedException("Use ShowPreview(object ownerWindow) method instead.");
        }

        void IReportPrintTool.ShowPreview(object ownerWindow)
        {
            this.ShowPreview(ownerWindow as Window);
        }

        void IReportPrintTool.ShowPreviewDialog()
        {
            throw new NotSupportedException("Use ShowPreviewDialog(object ownerWindow) method instead.");
        }

        void IReportPrintTool.ShowPreviewDialog(object ownerWindow)
        {
            this.ShowPreviewDialog(ownerWindow as Window);
        }

        void IReportPrintTool.ShowRibbonPreview()
        {
            throw new NotSupportedException();
        }

        void IReportPrintTool.ShowRibbonPreview(object lookAndFeel)
        {
            throw new NotSupportedException();
        }

        void IReportPrintTool.ShowRibbonPreviewDialog()
        {
            throw new NotSupportedException();
        }

        void IReportPrintTool.ShowRibbonPreviewDialog(object lookAndFeel)
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && ReferenceEquals(this.report.PrintTool, this))
                {
                    this.report.PrintTool = null;
                }
                this.disposed = true;
            }
        }

        public void Print()
        {
            PrintHelper.PrintDirect(this.report);
        }

        public void Print(string printerName)
        {
            new XtraReportPreviewModel(this.report).PrintDirect(printerName);
        }

        public bool? PrintDialog() => 
            PrintHelper.Print(this.report);

        public void ShowPreview(Window ownerWindow)
        {
            this.ShowPreviewCore(ownerWindow, false);
        }

        private void ShowPreviewCore(Window ownerWindow, bool isModal)
        {
            if (ownerWindow == null)
            {
                throw new ArgumentNullException("ownerWindow");
            }
            this.CreateDocumentIfEmpty(true);
            this.previewWindow = this.CreatePreviewWindow(ownerWindow);
            if (isModal)
            {
                this.previewWindow.ShowDialog();
            }
            else
            {
                this.previewWindow.Show();
            }
        }

        public void ShowPreviewDialog(Window ownerWindow)
        {
            this.ShowPreviewCore(ownerWindow, true);
        }

        List<ParameterInfo> IReportPrintTool.ParametersInfo =>
            null;
    }
}

