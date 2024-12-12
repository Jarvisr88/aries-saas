namespace DMEWorks.CrystalReports
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.ReportAppServer.DataDefModel;
    using System;

    public abstract class DataSource
    {
        protected DataSource()
        {
        }

        internal abstract void ApplyLogonInfo(ReportDocument report);
        protected void DebugPrint(string prefix, ISCRConnectionInfo info)
        {
            this.DebugPrint(prefix + ".Attributes", info.Attributes);
        }

        protected void DebugPrint(string prefix, ISCRPropertyBag bag)
        {
            foreach (string str in bag.PropertyIDs)
            {
                object obj2 = bag[str];
                string str2 = $"{prefix}[{str}]";
                if (obj2 is ISCRPropertyBag)
                {
                    this.DebugPrint(str2, (ISCRPropertyBag) obj2);
                }
            }
        }

        protected void DebugPrint(string prefix, ISCRTable table)
        {
            this.DebugPrint(prefix + ".Attributes", table.Attributes);
            this.DebugPrint(prefix + ".ConnectionInfo", table.ConnectionInfo);
        }
    }
}

