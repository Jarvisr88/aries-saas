namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class XlTableFormattingBase
    {
        protected XlTableFormattingBase()
        {
        }

        public XlDifferentialFormatting DataFormatting { get; set; }

        public XlDifferentialFormatting HeaderRowFormatting { get; set; }

        public XlDifferentialFormatting TotalRowFormatting { get; set; }
    }
}

