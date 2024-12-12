namespace DevExpress.Utils.Url
{
    using System;
    using System.IO;

    public static class UnixPath
    {
        public static bool IsFullyQualified(string path) => 
            !IsPartiallyQualified(path);

        public static bool IsPartiallyQualified(string path) => 
            string.IsNullOrEmpty(path) || (path[0] != Path.DirectorySeparatorChar);
    }
}

