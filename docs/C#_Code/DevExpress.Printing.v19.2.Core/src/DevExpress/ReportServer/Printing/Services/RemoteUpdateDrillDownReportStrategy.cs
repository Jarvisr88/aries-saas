namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.ReportServer.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native.DrillDown;
    using DevExpress.XtraReports;
    using System;

    internal class RemoteUpdateDrillDownReportStrategy : UpdateDrillDownReportStrategy
    {
        public override void Update(IReport report, PrintingSystemBase oldPrintingSystem)
        {
            base.Update(report, oldPrintingSystem);
            if ((report.PrintingSystemBase is RemotePrintingSystem) && (oldPrintingSystem is RemotePrintingSystem))
            {
                RemotePageList pages = (RemotePageList) report.PrintingSystemBase.Pages;
                try
                {
                    int i = 0;
                    while (true)
                    {
                        if ((i >= report.PrintingSystemBase.Pages.Count) || (i >= oldPrintingSystem.Pages.Count))
                        {
                            while (oldPrintingSystem.Pages.Count > 0)
                            {
                                oldPrintingSystem.Pages.RemoveAt(0);
                            }
                            break;
                        }
                        pages.ReplaceCachedPage(i, oldPrintingSystem.Pages[i]);
                        i++;
                    }
                }
                catch
                {
                }
            }
        }
    }
}

