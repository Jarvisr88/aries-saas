namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class FileInfoWrapper : IFileInfo
    {
        private FileInfoWrapper(System.IO.DirectoryInfo directoryInfo)
        {
            this.<FileSystemInfo>k__BackingField = directoryInfo;
        }

        public FileInfoWrapper(System.IO.FileInfo fileInfo)
        {
            this.<FileSystemInfo>k__BackingField = fileInfo;
        }

        public static FileInfoWrapper Create(string fileName) => 
            new FileInfoWrapper(new System.IO.FileInfo(fileName));

        public static FileInfoWrapper CreateDirectoryWrapper(string directoryPath) => 
            new FileInfoWrapper(new System.IO.DirectoryInfo(directoryPath));

        StreamWriter IFileInfo.AppendText() => 
            this.Match<StreamWriter>(() => this.FileInfo.AppendText(), null);

        System.IO.FileInfo IFileInfo.CopyTo(string destFileName, bool overwrite) => 
            this.Match<System.IO.FileInfo>(() => this.FileInfo.CopyTo(destFileName, overwrite), null);

        FileStream IFileInfo.Create() => 
            this.Match<FileStream>(() => this.FileInfo.Create(), null);

        StreamWriter IFileInfo.CreateText() => 
            this.Match<StreamWriter>(() => this.FileInfo.CreateText(), null);

        void IFileInfo.Delete()
        {
            this.FileSystemInfo.Delete();
        }

        void IFileInfo.MoveTo(string destFileName)
        {
            this.FileInfo.Do<System.IO.FileInfo>(x => x.MoveTo(destFileName));
        }

        FileStream IFileInfo.Open(FileMode mode, FileAccess access, FileShare share) => 
            this.Match<FileStream>(() => this.FileInfo.Open(mode, access, share), null);

        FileStream IFileInfo.OpenRead() => 
            this.Match<FileStream>(() => this.FileInfo.OpenRead(), null);

        StreamReader IFileInfo.OpenText() => 
            this.Match<StreamReader>(() => this.FileInfo.OpenText(), null);

        FileStream IFileInfo.OpenWrite() => 
            this.Match<FileStream>(() => this.FileInfo.OpenWrite(), null);

        private T Match<T>(Func<T> file, Func<T> directory = null) where T: class => 
            this.FileInfo.Return<System.IO.FileInfo, T>(_ => file(), delegate {
                if (directory != null)
                {
                    return directory();
                }
                Func<T> local1 = directory;
                return default(T);
            });

        public System.IO.FileSystemInfo FileSystemInfo { get; }

        public System.IO.FileInfo FileInfo =>
            this.FileSystemInfo as System.IO.FileInfo;

        private System.IO.DirectoryInfo DirectoryInfo =>
            this.FileSystemInfo as System.IO.DirectoryInfo;

        string IFileInfo.DirectoryName =>
            this.Match<string>(() => this.FileInfo.DirectoryName, () => this.DirectoryInfo.Parent.FullName);

        bool IFileInfo.Exists =>
            this.FileSystemInfo.Exists;

        long IFileInfo.Length
        {
            get
            {
                Func<System.IO.FileInfo, long> evaluator = <>c.<>9__21_0;
                if (<>c.<>9__21_0 == null)
                {
                    Func<System.IO.FileInfo, long> local1 = <>c.<>9__21_0;
                    evaluator = <>c.<>9__21_0 = x => x.Length;
                }
                return this.FileInfo.Return<System.IO.FileInfo, long>(evaluator, (<>c.<>9__21_1 ??= () => 0L));
            }
        }

        string IFileInfo.Name =>
            this.FileSystemInfo.Name;

        FileAttributes IFileInfo.Attributes
        {
            get => 
                this.FileSystemInfo.Attributes;
            set => 
                this.FileSystemInfo.Attributes = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FileInfoWrapper.<>c <>9 = new FileInfoWrapper.<>c();
            public static Func<FileInfo, long> <>9__21_0;
            public static Func<long> <>9__21_1;

            internal long <DevExpress.Mvvm.IFileInfo.get_Length>b__21_0(FileInfo x) => 
                x.Length;

            internal long <DevExpress.Mvvm.IFileInfo.get_Length>b__21_1() => 
                0L;
        }
    }
}

