namespace DevExpress.XtraExport.Implementation
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlsTableColumn : XlColumn
    {
        public XlsTableColumn(XlSheet sheet) : base(sheet)
        {
        }

        public int FormatIndex { get; set; }
    }
}

