namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;

    public class XlsContentRowPageBreaks : XlsContentPageBreaksBase
    {
        public XlsContentRowPageBreaks(IXlPageBreaks breaks) : base(breaks)
        {
        }

        protected override int MaxCount =>
            0x402;

        protected override int EndValue =>
            0xff;
    }
}

