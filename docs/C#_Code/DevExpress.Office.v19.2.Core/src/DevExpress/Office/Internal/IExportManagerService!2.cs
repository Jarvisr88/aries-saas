namespace DevExpress.Office.Internal
{
    using System;
    using System.Collections.Generic;

    public interface IExportManagerService<TFormat, TResult>
    {
        IExporter<TFormat, TResult> GetExporter(TFormat format);
        List<IExporter<TFormat, TResult>> GetExporters();
        void RegisterExporter(IExporter<TFormat, TResult> exporter);
        void UnregisterAllExporters();
        void UnregisterExporter(IExporter<TFormat, TResult> exporter);
    }
}

