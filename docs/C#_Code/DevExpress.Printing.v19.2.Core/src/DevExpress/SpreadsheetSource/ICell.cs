namespace DevExpress.SpreadsheetSource
{
    using DevExpress.Export.Xl;
    using System;

    public interface ICell
    {
        int FieldIndex { get; }

        XlVariantValue Value { get; }
    }
}

