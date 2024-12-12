namespace DMEWorks.CrystalReports
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.ReportAppServer.ClientDoc;
    using CrystalDecisions.ReportAppServer.Controllers;
    using CrystalDecisions.ReportAppServer.DataDefModel;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class FileDataSource : DataSource
    {
        private readonly string _fileName;

        public FileDataSource(string fileName)
        {
            this._fileName = fileName;
        }

        internal override void ApplyLogonInfo(ReportDocument report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            if (report.IsSubreport)
            {
                throw new NotSupportedException("cannot be used on subreport");
            }
            this.ModifyConnectionInfo(report.get_ReportClientDocument());
        }

        private ConnectionInfo CreateReplacement()
        {
            PropertyBag vVal = (PropertyBag) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("EC75982C-37CF-4F20-8736-92901B9FCAD7")));
            vVal.Add("File Path ", this.FileName);
            vVal.Add("Internal Connection ID", Guid.NewGuid().ToString("B"));
            PropertyBag bag2 = (PropertyBag) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("EC75982C-37CF-4F20-8736-92901B9FCAD7")));
            bag2.Add("Database DLL", "crdb_adoplus.dll");
            bag2.Add("QE_DatabaseName", "");
            bag2.Add("QE_DatabaseType", "ADO.NET (XML)");
            bag2.Add("QE_LogonProperties", vVal);
            bag2.Add("QE_ServerDescription", "");
            bag2.Add("QE_SQLDB", "False");
            bag2.Add("SSO Enabled", "False");
            ConnectionInfo info1 = (ConnectionInfo) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("0F0EEB0E-21BE-4BE8-98EB-79B21E5B4DA5")));
            info1.Attributes = bag2;
            info1.Kind = CrConnectionInfoKindEnum.crConnectionInfoKindCRQE;
            info1.UserName = "";
            info1.Password = "";
            return info1;
        }

        private void ModifyConnectionInfo(ISCDReportClientDocument report)
        {
            List<DatabaseController>.Enumerator enumerator2;
            List<DatabaseController> list = new List<DatabaseController> {
                report.DatabaseController
            };
            foreach (string str in report.SubreportController.GetSubreportNames())
            {
                list.Add(report.SubreportController.GetSubreport(str).DatabaseController);
            }
            using (enumerator2 = list.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    foreach (ISCRTable table1 in enumerator2.Current.Database.Tables)
                    {
                    }
                }
            }
            ConnectionInfo newConnection = this.CreateReplacement();
            foreach (DatabaseController controller in list)
            {
                foreach (ConnectionInfo info2 in controller.GetConnectionInfos(null))
                {
                    controller.ReplaceConnection(info2, newConnection, null, CrDBOptionsEnum.crDBOptionIgnoreCurrentTableQualifiers);
                }
            }
            using (enumerator2 = list.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    foreach (ISCRTable table2 in enumerator2.Current.Database.Tables)
                    {
                    }
                }
            }
        }

        public string FileName =>
            this._fileName;
    }
}

