namespace DevExpress.SpreadsheetSource
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;
    using System.Reflection;

    public interface ISpreadsheetDataReader
    {
        void Close();
        bool GetBoolean(int index);
        DateTime GetDateTime(int index);
        string GetDisplayText(int index, CultureInfo culture);
        double GetDouble(int index);
        XlVariantValueType GetFieldType(int index);
        string GetString(int index);
        XlVariantValue GetValue(int index);
        bool Read();

        bool IsClosed { get; }

        int FieldsCount { get; }

        XlVariantValue this[int index] { get; }

        ICellCollection ExistingCells { get; }
    }
}

