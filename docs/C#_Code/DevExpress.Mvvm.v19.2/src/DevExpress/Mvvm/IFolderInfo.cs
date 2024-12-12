namespace DevExpress.Mvvm
{
    using System;
    using System.IO;

    public interface IFolderInfo
    {
        void Delete();
        void MoveTo(string destinationDirectoryName);

        string Path { get; }

        string DirectoryName { get; }

        string Name { get; }

        bool Exists { get; }

        FileAttributes Attributes { get; set; }
    }
}

