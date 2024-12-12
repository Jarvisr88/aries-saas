namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;

    public class XlsSourceCommandIndex5 : XlsSourceCommandIndex
    {
        protected override XlsContentIndex CreateContent() => 
            new XlsContentIndex5();
    }
}

