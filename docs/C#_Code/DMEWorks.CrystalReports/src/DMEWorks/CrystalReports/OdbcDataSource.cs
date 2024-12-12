namespace DMEWorks.CrystalReports
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.ReportAppServer.ClientDoc;
    using CrystalDecisions.ReportAppServer.Controllers;
    using CrystalDecisions.ReportAppServer.DataDefModel;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class OdbcDataSource : DataSource
    {
        private readonly string _dsnName;
        private readonly string _database;
        private readonly string _userID;
        private readonly string _password;

        public OdbcDataSource(string dsnName, string database, string userID, string password)
        {
            this._dsnName = dsnName;
            this._database = database;
            this._userID = userID;
            this._password = password;
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
            vVal.Add("Database", this.Database);
            vVal.Add("DSN", this.DsnName);
            vVal.Add("UseDSNProperties", "False");
            PropertyBag bag2 = (PropertyBag) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("EC75982C-37CF-4F20-8736-92901B9FCAD7")));
            bag2.Add("Database DLL", "crdb_odbc.dll");
            bag2.Add("QE_DatabaseName", this.Database);
            bag2.Add("QE_DatabaseType", "ODBC (RDO)");
            bag2.Add("QE_LogonProperties", vVal);
            bag2.Add("QE_ServerDescription", this.DsnName);
            bag2.Add("QE_SQLDB", "True");
            bag2.Add("SSO Enabled", "False");
            ConnectionInfo info1 = (ConnectionInfo) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("0F0EEB0E-21BE-4BE8-98EB-79B21E5B4DA5")));
            info1.Attributes = bag2;
            info1.Kind = CrConnectionInfoKindEnum.crConnectionInfoKindCRQE;
            info1.UserName = this.UserID;
            info1.Password = this.Password;
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
                controller.LogonEx(this.DsnName, this.Database, this.UserID, this.Password);
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

        public string DsnName =>
            this._dsnName;

        public string Database =>
            this._database;

        public string UserID =>
            this._userID;

        public string Password =>
            this._password;
    }
}

