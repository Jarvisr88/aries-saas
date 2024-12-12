namespace DevExpress.SpreadsheetSource
{
    using System;

    public interface ISpreadsheetSourceOptions
    {
        string Password { get; set; }

        bool SkipEmptyRows { get; set; }

        bool SkipHiddenRows { get; set; }

        bool SkipHiddenColumns { get; set; }
    }
}

