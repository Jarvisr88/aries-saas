namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public interface IRemoteExportService
    {
        event ExceptionEventHandler Exception;

        void Export(ExportOptionsBase exportOptions, string fileName, Action<string> afterExport);
    }
}

