namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class XlTableStyleInfo : IXlTableStyleInfo
    {
        public XlTableStyleInfo()
        {
            this.Name = "TableStyleMedium2";
            this.ShowRowStripes = true;
        }

        public string Name { get; set; }

        public bool ShowColumnStripes { get; set; }

        public bool ShowRowStripes { get; set; }

        public bool ShowFirstColumn { get; set; }

        public bool ShowLastColumn { get; set; }
    }
}

