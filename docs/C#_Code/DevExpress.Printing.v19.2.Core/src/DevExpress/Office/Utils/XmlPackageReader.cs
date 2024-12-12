namespace DevExpress.Office.Utils
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.Xml;

    public class XmlPackageReader : IDisposable
    {
        private PackageFileCollection packageFiles;

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.packageFiles != null))
            {
                int count = this.packageFiles.Count;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= count)
                    {
                        this.packageFiles.Clear();
                        this.packageFiles = null;
                        break;
                    }
                    this.packageFiles[num2].Stream.Dispose();
                    num2++;
                }
            }
        }

        public PackageFile GetPackageFile(string fileName)
        {
            if (this.PackageFiles != null)
            {
                fileName = fileName.Replace('\\', '/');
                if (fileName.StartsWith("/", StringComparison.Ordinal))
                {
                    fileName = fileName.Substring(1);
                }
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

        public Stream GetPackageFileStream(string fileName) => 
            this.GetPackageFileStream(fileName, false);

        private Stream GetPackageFileStream(string fileName, bool returnSeekableStream)
        {
            PackageFile packageFile = this.GetPackageFile(fileName);
            if (packageFile == null)
            {
                return null;
            }
            if (!returnSeekableStream)
            {
                return packageFile.Stream;
            }
            MemoryStream seekableStream = packageFile.SeekableStream;
            seekableStream.Seek(0L, SeekOrigin.Begin);
            return seekableStream;
        }

        public XmlReader GetPackageFileXmlReader(string fileName, XmlReaderSettings settings) => 
            this.GetPackageFileXmlReader(fileName, settings, false);

        public XmlReader GetPackageFileXmlReader(string fileName, XmlReaderSettings settings, bool createSeekableStream)
        {
            Stream packageFileStream = this.GetPackageFileStream(fileName, createSeekableStream);
            return ((packageFileStream != null) ? XmlReader.Create(packageFileStream, settings) : null);
        }

        public void OpenPackage(Stream stream)
        {
            InternalZipFileCollection files = InternalZipArchive.Open(stream);
            int count = files.Count;
            this.packageFiles = new PackageFileCollection();
            for (int i = 0; i < count; i++)
            {
                InternalZipFile file = files[i];
                this.packageFiles.Add(new PackageFile(file.FileName.Replace('\\', '/'), file.FileDataStream, (int) file.UncompressedSize));
            }
        }

        public PackageFileCollection PackageFiles =>
            this.packageFiles;
    }
}

