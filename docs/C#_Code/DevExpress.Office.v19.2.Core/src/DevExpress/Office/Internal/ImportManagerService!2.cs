namespace DevExpress.Office.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public abstract class ImportManagerService<TFormat, TResult> : IImportManagerService<TFormat, TResult>
    {
        private readonly Dictionary<TFormat, IImporter<TFormat, TResult>> importers;

        protected ImportManagerService()
        {
            this.importers = new Dictionary<TFormat, IImporter<TFormat, TResult>>();
            this.RegisterNativeFormats();
        }

        public virtual IImporter<TFormat, TResult> GetImporter(TFormat format)
        {
            IImporter<TFormat, TResult> importer;
            return (!this.importers.TryGetValue(format, out importer) ? null : importer);
        }

        public virtual List<IImporter<TFormat, TResult>> GetImporters()
        {
            List<IImporter<TFormat, TResult>> list = new List<IImporter<TFormat, TResult>>();
            list.AddRange(this.importers.Values);
            return list;
        }

        public virtual void RegisterImporter(IImporter<TFormat, TResult> importer)
        {
            Guard.ArgumentNotNull(importer, "importer");
            this.importers.Add(importer.Format, importer);
        }

        protected internal abstract void RegisterNativeFormats();
        public void UnregisterAllImporters()
        {
            this.importers.Clear();
        }

        public virtual void UnregisterImporter(IImporter<TFormat, TResult> importer)
        {
            if (importer != null)
            {
                this.importers.Remove(importer.Format);
            }
        }

        public Dictionary<TFormat, IImporter<TFormat, TResult>> Importers =>
            this.importers;
    }
}

