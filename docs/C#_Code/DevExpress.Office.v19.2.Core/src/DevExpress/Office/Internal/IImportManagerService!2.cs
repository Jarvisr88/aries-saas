namespace DevExpress.Office.Internal
{
    using System;
    using System.Collections.Generic;

    public interface IImportManagerService<TFormat, TResult>
    {
        IImporter<TFormat, TResult> GetImporter(TFormat format);
        List<IImporter<TFormat, TResult>> GetImporters();
        void RegisterImporter(IImporter<TFormat, TResult> importer);
        void UnregisterAllImporters();
        void UnregisterImporter(IImporter<TFormat, TResult> importer);
    }
}

