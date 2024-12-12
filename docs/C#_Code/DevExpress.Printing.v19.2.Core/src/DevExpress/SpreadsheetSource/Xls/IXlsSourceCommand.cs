namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;

    public interface IXlsSourceCommand
    {
        void Execute(XlsSourceDataReader dataReader);
        void Execute(XlsSpreadsheetSource contentBuilder);
        void Read(XlReader reader, XlsSpreadsheetSource contentBuilder);

        bool IsSubstreamBound { get; }
    }
}

