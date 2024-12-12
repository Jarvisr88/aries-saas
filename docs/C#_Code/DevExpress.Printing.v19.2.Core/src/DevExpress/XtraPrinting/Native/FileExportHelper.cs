namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using System;

    internal static class FileExportHelper
    {
        public static void Do(string filePath, Action1<Stream> action);
    }
}

