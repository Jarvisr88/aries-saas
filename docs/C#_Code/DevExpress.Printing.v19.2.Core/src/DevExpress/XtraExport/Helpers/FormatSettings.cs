namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class FormatSettings
    {
        public string FormatString { get; set; }

        public DevExpress.Utils.FormatType FormatType { get; set; }

        public Type ActualDataType { get; set; }
    }
}

