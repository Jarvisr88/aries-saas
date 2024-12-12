namespace DevExpress.SpreadsheetSource
{
    using System;
    using System.Runtime.CompilerServices;

    public class SpreadsheetSourceOptions : ISpreadsheetSourceOptions
    {
        public SpreadsheetSourceOptions()
        {
            this.SkipEmptyRows = true;
        }

        public string Password { get; set; }

        public bool SkipEmptyRows { get; set; }

        public bool SkipHiddenRows { get; set; }

        public bool SkipHiddenColumns { get; set; }
    }
}

