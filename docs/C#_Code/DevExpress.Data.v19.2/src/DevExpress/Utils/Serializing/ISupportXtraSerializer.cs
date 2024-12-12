namespace DevExpress.Utils.Serializing
{
    using System;
    using System.IO;

    public interface ISupportXtraSerializer
    {
        void RestoreLayoutFromRegistry(string path);
        void RestoreLayoutFromStream(Stream stream);
        void RestoreLayoutFromXml(string xmlFile);
        void SaveLayoutToRegistry(string path);
        void SaveLayoutToStream(Stream stream);
        void SaveLayoutToXml(string xmlFile);
    }
}

