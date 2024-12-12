namespace DevExpress.SpreadsheetSource.Xls
{
    using System;

    public interface IXlsSourceDataCollector
    {
        void PutData(byte[] data, XlsSpreadsheetSource contentBuilder);
    }
}

