namespace DevExpress.XtraExport
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcelNumberFormat
    {
        public ExcelNumberFormat(int id, string formatString)
        {
            this.Id = id;
            this.FormatString = formatString;
        }

        public int Id { get; internal set; }

        public string FormatString { get; private set; }
    }
}

