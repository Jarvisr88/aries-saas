namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsSourceCommandString5 : XlsSourceCommandBase
    {
        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.Value = contentBuilder.ReadString2(reader);
            XlsSourceCommandFormula formula = contentBuilder.CommandFactory.CreateCommand(6) as XlsSourceCommandFormula;
            if (formula != null)
            {
                formula.StringValue = this.Value;
                formula.Execute(contentBuilder.DataReader);
            }
        }

        public string Value { get; private set; }
    }
}

