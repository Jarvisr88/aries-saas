namespace DevExpress.XtraReports.UI
{
    using DevExpress.XtraReports;
    using System;

    public abstract class ReportPrintContext
    {
        protected ReportPrintContext()
        {
        }

        public abstract IReportPrintTool CreateTool(IReport report);
    }
}

