namespace DMEWorks.CrystalReports
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.ReportAppServer.ClientDoc;
    using CrystalDecisions.ReportAppServer.Controllers;
    using CrystalDecisions.ReportAppServer.DataDefModel;
    using System;
    using System.Runtime.InteropServices;

    internal static class Utilities
    {
        public static void FixAliases(ReportDocument report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            if (report.IsSubreport)
            {
                throw new NotSupportedException("cannot be used on subreport");
            }
            FixAliases(report.get_ReportClientDocument());
        }

        private static void FixAliases(ISCDReportClientDocument report)
        {
            FixAliases(report.DatabaseController);
            foreach (string str in report.SubreportController.GetSubreportNames())
            {
                FixAliases(report.SubreportController.GetSubreport(str).DatabaseController);
            }
        }

        private static void FixAliases(DatabaseController controller)
        {
            ISCRTables tables = controller.Database.Tables;
            for (int i = 0; i < tables.Count; i++)
            {
                ISCRTable table = tables[i];
                string str = table.Alias ?? "";
                if (str.StartsWith("tbl_", StringComparison.OrdinalIgnoreCase))
                {
                    controller.ModifyTableAlias(table, str.Substring(3, str.Length - 3));
                }
                else if (str.StartsWith("view_", StringComparison.OrdinalIgnoreCase))
                {
                    controller.ModifyTableAlias(table, str.Substring(4, str.Length - 4));
                }
            }
        }
    }
}

