namespace DevExpress.Mvvm
{
    using System;
    using System.IO;

    public interface IFileInfo
    {
        StreamWriter AppendText();
        FileInfo CopyTo(string destinationFileName, bool overwrite);
        FileStream Create();
        StreamWriter CreateText();
        void Delete();
        void MoveTo(string destinationFileName);
        FileStream Open(FileMode mode, FileAccess access, FileShare share);
        FileStream OpenRead();
        StreamReader OpenText();
        FileStream OpenWrite();

        long Length { get; }

        string DirectoryName { get; }

        string Name { get; }

        bool Exists { get; }

        FileAttributes Attributes { get; set; }
    }
}

