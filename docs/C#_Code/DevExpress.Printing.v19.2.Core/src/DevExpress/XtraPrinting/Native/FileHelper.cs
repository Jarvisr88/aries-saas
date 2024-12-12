namespace DevExpress.XtraPrinting.Native
{
    using System;

    public static class FileHelper
    {
        public static void CopyFile(string src, string dest);
        public static string GetTempFileName();
        public static string SetValidExtension(string fileName, string primaryExt, string[] extensions);
    }
}

