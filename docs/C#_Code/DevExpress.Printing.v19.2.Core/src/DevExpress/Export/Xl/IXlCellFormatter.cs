namespace DevExpress.Export.Xl
{
    using System;

    internal interface IXlCellFormatter
    {
        string GetFormattedValue(IXlCell cell);
    }
}

