namespace DevExpress.Office.Export
{
    using DevExpress.Office;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public interface IExporterOptions : ISupportsCopyFrom<IExporterOptions>
    {
        string TargetUri { get; set; }
    }
}

