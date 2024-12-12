namespace DevExpress.SpreadsheetSource.Csv
{
    using System;

    public interface ICsvSourceValueConverter
    {
        object Convert(string text, int columnIndex);
    }
}

