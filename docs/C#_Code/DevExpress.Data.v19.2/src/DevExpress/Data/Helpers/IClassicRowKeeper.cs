namespace DevExpress.Data.Helpers
{
    using System;
    using System.IO;

    public interface IClassicRowKeeper : IDisposable
    {
        void Clear();
        void ClearSelection();
        bool GroupsRestoreFromStream(Stream stream);
        void GroupsSaveToStream(Stream stream);
        bool Restore();
        bool RestoreIncremental();
        bool RestoreStream();
        void Save();
        void SaveOnFilter();
        void SaveOnRefresh(bool isEndUpdate);
    }
}

