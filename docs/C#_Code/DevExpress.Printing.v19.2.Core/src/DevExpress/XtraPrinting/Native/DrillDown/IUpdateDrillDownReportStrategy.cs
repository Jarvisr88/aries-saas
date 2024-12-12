namespace DevExpress.XtraPrinting.Native.DrillDown
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports;
    using System;

    internal interface IUpdateDrillDownReportStrategy
    {
        void Update(IReport report, PrintingSystemBase oldPrintingSystem);
    }
}

