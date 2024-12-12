namespace DevExpress.SpreadsheetSource.Xls
{
    using System;

    public class XlsSourceCommandContinue : XlsSourceCommandRecordBase
    {
        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (contentBuilder.DataCollector != null)
            {
                contentBuilder.DataCollector.PutData(base.Data, contentBuilder);
            }
        }
    }
}

