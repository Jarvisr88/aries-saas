namespace DevExpress.XtraReports
{
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class ReportExtensions
    {
        public static bool ApproveParameters(this IReport report, IReportPrintTool tool)
        {
            List<Parameter> list = new List<Parameter>();
            Predicate<Parameter> condition = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Predicate<Parameter> local1 = <>c.<>9__0_0;
                condition = <>c.<>9__0_0 = p => true;
            }
            report.CollectParameters(list, condition);
            if (((tool != null) ? tool.ApproveParameters(list.ToArray(), report.RequestParameters) : true) || !report.RequestParameters)
            {
                return true;
            }
            Func<Parameter, bool> predicate = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<Parameter, bool> local2 = <>c.<>9__0_1;
                predicate = <>c.<>9__0_1 = p => p.Visible;
            }
            return !list.Any<Parameter>(predicate);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReportExtensions.<>c <>9 = new ReportExtensions.<>c();
            public static Predicate<Parameter> <>9__0_0;
            public static Func<Parameter, bool> <>9__0_1;

            internal bool <ApproveParameters>b__0_0(Parameter p) => 
                true;

            internal bool <ApproveParameters>b__0_1(Parameter p) => 
                p.Visible;
        }
    }
}

