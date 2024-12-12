namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraReports;
    using System;

    internal class ReportPreviewHelper : PreviewHelper
    {
        private readonly IReport report;

        public ReportPreviewHelper(IReport report)
        {
            Guard.ArgumentNotNull(report, "report");
            this.report = report;
        }

        protected override void CreateDocumentIfEmpty()
        {
            if (this.report.PrintingSystemBase.Document.IsEmpty)
            {
                this.report.CreateDocument(true);
            }
        }

        protected override void StopPageBuilding()
        {
            this.report.StopPageBuilding();
        }

        protected override object DocumentSource =>
            this.report;
    }
}

