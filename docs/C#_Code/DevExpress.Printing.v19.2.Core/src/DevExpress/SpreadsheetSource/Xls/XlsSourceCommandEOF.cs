namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;

    public class XlsSourceCommandEOF : XlsSourceCommandBase
    {
        public override void Execute(XlsSourceDataReader dataReader)
        {
            dataReader.EndContent();
        }

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.ClearDataCollectors();
            contentBuilder.EndContent();
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
        }

        public override bool IsSubstreamBound =>
            true;
    }
}

