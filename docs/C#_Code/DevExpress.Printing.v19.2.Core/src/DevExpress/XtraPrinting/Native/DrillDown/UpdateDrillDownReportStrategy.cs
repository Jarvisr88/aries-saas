namespace DevExpress.XtraPrinting.Native.DrillDown
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports;
    using System;

    internal class UpdateDrillDownReportStrategy : IUpdateDrillDownReportStrategy
    {
        public virtual void Update(IReport report, PrintingSystemBase oldPrintingSystem);
    }
}

