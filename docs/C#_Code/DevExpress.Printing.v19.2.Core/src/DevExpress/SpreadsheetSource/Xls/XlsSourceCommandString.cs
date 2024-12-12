namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandString : XlsSourceCommandDataCollectorBase
    {
        private XLUnicodeString value = new XLUnicodeString();

        protected override bool GetCompleted() => 
            this.value.Complete;

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.value = new XLUnicodeString();
            base.ReadCore(reader, contentBuilder);
        }

        protected override void ReadData(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.value.ReadData(reader);
            if (this.value.Complete)
            {
                XlsSourceCommandFormula formula = contentBuilder.CommandFactory.CreateCommand(6) as XlsSourceCommandFormula;
                if (formula != null)
                {
                    formula.StringValue = this.value.Value;
                    formula.Execute(contentBuilder.DataReader);
                }
            }
        }
    }
}

