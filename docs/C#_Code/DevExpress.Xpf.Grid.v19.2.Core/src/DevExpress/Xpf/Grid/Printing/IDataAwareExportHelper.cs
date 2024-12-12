namespace DevExpress.Xpf.Grid.Printing
{
    using System;
    using System.IO;

    public interface IDataAwareExportHelper
    {
        void Export(Stream steam);
        void Export(string filePath);
    }
}

