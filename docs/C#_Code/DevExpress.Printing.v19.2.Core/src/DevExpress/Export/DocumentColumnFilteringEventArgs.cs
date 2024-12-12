namespace DevExpress.Export
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class DocumentColumnFilteringEventArgs
    {
        public IXlFilterCriteria Filter { get; set; }

        public string ColumnFieldName { get; internal set; }

        public int ColumnPosition { get; internal set; }
    }
}

