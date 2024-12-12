namespace DevExpress.Office.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public abstract class ExportManagerService<TFormat, TResult> : IExportManagerService<TFormat, TResult>
    {
        private readonly Dictionary<TFormat, IExporter<TFormat, TResult>> exporters;

        protected ExportManagerService()
        {
            this.exporters = new Dictionary<TFormat, IExporter<TFormat, TResult>>();
            this.RegisterNativeFormats();
        }

        public virtual IExporter<TFormat, TResult> GetExporter(TFormat format) => 
            this.Exporters[format];

        public virtual List<IExporter<TFormat, TResult>> GetExporters()
        {
            List<IExporter<TFormat, TResult>> list = new List<IExporter<TFormat, TResult>>();
            list.AddRange(this.Exporters.Values);
            return list;
        }

        public virtual void RegisterExporter(IExporter<TFormat, TResult> Exporter)
        {
            Guard.ArgumentNotNull(Exporter, "Exporter");
            this.Exporters.Add(Exporter.Format, Exporter);
        }

        protected internal abstract void RegisterNativeFormats();
        public void UnregisterAllExporters()
        {
            this.Exporters.Clear();
        }

        public virtual void UnregisterExporter(IExporter<TFormat, TResult> Exporter)
        {
            if (Exporter != null)
            {
                this.Exporters.Remove(Exporter.Format);
            }
        }

        public Dictionary<TFormat, IExporter<TFormat, TResult>> Exporters =>
            this.exporters;
    }
}

