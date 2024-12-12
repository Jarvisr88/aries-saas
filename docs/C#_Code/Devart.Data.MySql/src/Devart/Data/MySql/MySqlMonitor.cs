namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;

    [aa("MySqlMonitor_Description"), ToolboxItem(true), Designer("Devart.Common.Design.DbMonitorDesigner, Devart.Data.Design")]
    public class MySqlMonitor : DbMonitorHelper
    {
        private static WeakReference a;

        internal static void a(MonitorTracePoint A_0, DbConnection A_1);
        internal static void a(MonitorTracePoint A_0, IDbConnection A_1);
        internal static void a(MonitorTracePoint A_0, object A_1);
        internal static void a(Exception A_0, object A_1);
        internal static void a(MonitorTracePoint A_0, IDbCommand A_1, string A_2);
        internal static void a(MonitorTracePoint A_0, IDbCommand A_1, string A_2, int A_3);
        internal static void b(MonitorTracePoint A_0, DbConnection A_1);
        internal static void b(MonitorTracePoint A_0, IDbConnection A_1);
        internal static void c(MonitorTracePoint A_0, IDbConnection A_1);
        protected override void GetParameterInfo(IDbDataParameter parameter, out string name, out string dbType, out string direction, out string value);

        internal static MySqlMonitor SingletonInstance { get; set; }

        public override bool IsActive { get; set; }

        protected override string ProductName { get; }

        protected override bool DbMonitorAppAvailable { get; }
    }
}

