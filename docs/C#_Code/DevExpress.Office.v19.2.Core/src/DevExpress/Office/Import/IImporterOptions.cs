namespace DevExpress.Office.Import
{
    using DevExpress.Office;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public interface IImporterOptions : ISupportsCopyFrom<IImporterOptions>
    {
        string SourceUri { get; set; }
    }
}

