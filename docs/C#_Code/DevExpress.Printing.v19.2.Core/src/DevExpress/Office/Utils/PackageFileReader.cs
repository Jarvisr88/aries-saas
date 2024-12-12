namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Reader;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PackageFileReader : IDisposable
    {
        private const string pathDelimiter = @"\";
        private StructuredStorageReader structuredStorageReader;
        private PackageFileCollection packageFiles;
        private DevExpress.Office.Utils.PackageFileStreams packageFileStreams;
        private Dictionary<string, BinaryReader> packageFileReaders;

        public PackageFileReader(Stream stream) : this(stream, false)
        {
        }

        public PackageFileReader(Stream stream, bool keepOpen)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.packageFiles = this.GetPackageFiles(stream, keepOpen);
            this.packageFileStreams = new DevExpress.Office.Utils.PackageFileStreams();
            this.packageFileReaders = new Dictionary<string, BinaryReader>();
        }

        protected internal virtual Stream CreatePackageFileStreamCopy(string fileName)
        {
            PackageFile packageFile = this.GetPackageFile(fileName);
            return packageFile?.Stream;
        }

        public void Dispose()
        {
        }

        public IEnumerable<string> EnumerateFiles(string storageName, bool topStorageOnly)
        {
            List<string> list = new List<string>();
            if (this.PackageFiles != null)
            {
                string str = storageName.StartsWith(@"\") ? storageName : (@"\" + storageName);
                if (!str.EndsWith(@"\"))
                {
                    str = str + @"\";
                }
                int count = this.PackageFiles.Count;
                for (int i = 0; i < count; i++)
                {
                    string fileName = this.PackageFiles[i].FileName;
                    if (fileName.StartsWith(str, StringComparison.CurrentCultureIgnoreCase) && (!topStorageOnly || !fileName.Substring(str.Length).Contains(@"\")))
                    {
                        list.Add(fileName);
                    }
                }
            }
            return list;
        }

        public virtual BinaryReader GetCachedPackageFileReader(string fileName)
        {
            BinaryReader reader;
            string key = fileName.StartsWith(@"\") ? fileName : (@"\" + fileName);
            if (this.PackageFileReaders.TryGetValue(key, out reader))
            {
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                return reader;
            }
            Stream cachedPackageFileStream = this.GetCachedPackageFileStream(key);
            if (cachedPackageFileStream == null)
            {
                return null;
            }
            reader = new BinaryReader(cachedPackageFileStream);
            this.PackageFileReaders.Add(key, reader);
            return reader;
        }

        public virtual Stream GetCachedPackageFileStream(string fileName)
        {
            Stream stream;
            if (this.PackageFileStreams.TryGetValue(fileName, out stream))
            {
                stream.Seek(0L, SeekOrigin.Begin);
                return stream;
            }
            stream = this.CreatePackageFileStreamCopy(fileName);
            if (stream == null)
            {
                return null;
            }
            this.PackageFileStreams.Add(fileName, stream);
            return stream;
        }

        protected internal virtual PackageFile GetPackageFile(string fileName)
        {
            if (this.PackageFiles != null)
            {
                int count = this.PackageFiles.Count;
                for (int i = 0; i < count; i++)
                {
                    PackageFile file = this.PackageFiles[i];
                    if (string.Compare(file.FileName, fileName, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        return file;
                    }
                }
            }
            return null;
        }

        protected internal virtual PackageFileCollection GetPackageFiles(Stream stream, bool keepOpen)
        {
            this.structuredStorageReader = new StructuredStorageReader(stream, keepOpen);
            PackageFileCollection files = new PackageFileCollection();
            foreach (DirectoryEntry entry in this.structuredStorageReader.AllStreamEntries)
            {
                string path = entry.Path;
                Stream stream2 = this.structuredStorageReader.GetStream(path);
                files.Add(new PackageFile(path, stream2, (int) stream2.Length));
            }
            return files;
        }

        protected internal virtual Stream GetPackageFileStream(string fileName)
        {
            PackageFile packageFile = this.GetPackageFile(fileName);
            return packageFile?.Stream;
        }

        protected internal PackageFileCollection PackageFiles =>
            this.packageFiles;

        protected internal DevExpress.Office.Utils.PackageFileStreams PackageFileStreams =>
            this.packageFileStreams;

        protected internal Dictionary<string, BinaryReader> PackageFileReaders =>
            this.packageFileReaders;
    }
}

