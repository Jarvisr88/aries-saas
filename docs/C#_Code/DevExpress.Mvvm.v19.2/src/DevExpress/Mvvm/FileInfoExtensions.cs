namespace DevExpress.Mvvm
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public static class FileInfoExtensions
    {
        public static FileInfo CopyTo(this IFileInfo fileInfo, string destinationFileName)
        {
            Verify(fileInfo);
            return fileInfo.CopyTo(destinationFileName, false);
        }

        public static string GetFullName(this IFileInfo fileInfo)
        {
            Verify(fileInfo);
            return Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
        }

        public static FileStream Open(this IFileInfo fileInfo, FileMode mode)
        {
            Verify(fileInfo);
            return fileInfo.Open(mode, FileAccess.ReadWrite, FileShare.None);
        }

        public static FileStream Open(this IFileInfo fileInfo, FileMode mode, FileAccess access)
        {
            Verify(fileInfo);
            return fileInfo.Open(mode, access, FileShare.None);
        }

        internal static void Verify(IFileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException("fileInfo");
            }
        }
    }
}

