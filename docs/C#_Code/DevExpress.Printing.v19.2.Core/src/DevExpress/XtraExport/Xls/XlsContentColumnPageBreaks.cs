namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;

    public class XlsContentColumnPageBreaks : XlsContentPageBreaksBase
    {
        public XlsContentColumnPageBreaks(IXlPageBreaks breaks) : base(breaks)
        {
        }

        protected override int MaxCount =>
            0xff;

        protected override int EndValue =>
            0xffff;
    }
}

