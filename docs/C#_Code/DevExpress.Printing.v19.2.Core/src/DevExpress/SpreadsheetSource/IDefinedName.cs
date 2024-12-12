namespace DevExpress.SpreadsheetSource
{
    using DevExpress.Export.Xl;
    using System;

    public interface IDefinedName
    {
        string Name { get; }

        string Scope { get; }

        bool IsHidden { get; }

        XlCellRange Range { get; }

        string RefersTo { get; }

        string Comment { get; }
    }
}

