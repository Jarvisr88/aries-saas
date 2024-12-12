namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;

    public interface IXlsSourceCommandFactory
    {
        IXlsSourceCommand CreateCommand(XlReader reader);
        IXlsSourceCommand CreateCommand(int typeCode);
    }
}

