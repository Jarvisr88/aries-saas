namespace DevExpress.XtraReports.ReportGeneration
{
    using DevExpress.Utils;
    using System;

    internal class ReportGeneratorOptionsUtils
    {
        public static DefaultBoolean GetActualOptionValue(DefaultBoolean currentValue, bool condition) => 
            condition ? DefaultBoolean.False : currentValue;
    }
}

