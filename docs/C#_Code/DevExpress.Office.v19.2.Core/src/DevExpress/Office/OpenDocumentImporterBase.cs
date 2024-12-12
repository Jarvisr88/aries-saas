namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Zip;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public abstract class OpenDocumentImporterBase : DestinationAndXmlBasedImporter, IDisposable
    {
        private PackageFileCollection packageFiles;
        private readonly Dictionary<string, OfficeNativeImage> packageImages;

        protected OpenDocumentImporterBase(IDocumentModel documentModel) : base(documentModel)
        {
            this.packageImages = new Dictionary<string, OfficeNativeImage>();
        }

        protected internal virtual void ApplyDefaultProperties()
        {
        }

        protected internal override void BeforeImportMainDocument()
        {
            base.BeforeImportMainDocument();
            this.ApplyDefaultProperties();
            this.ImportStyles();
        }

        public override bool ConvertToBool(string value)
        {
            bool result = false;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            this.ThrowInvalidFile();
            return false;
        }

        protected internal abstract Destination CreateRootStyleDestination();
        protected internal override XmlReaderSettings CreateXmlReaderSettings()
        {
            XmlReaderSettings settings = base.CreateXmlReaderSettings();
            settings.IgnoreWhitespace = false;
            return settings;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposePackageFiles();
            }
        }

        protected internal virtual void DisposePackageFiles()
        {
            if (this.PackageFiles != null)
            {
                int count = this.PackageFiles.Count;
                for (int i = 0; i < count; i++)
                {
                    this.PackageFiles[i].Stream.Dispose();
                }
                this.PackageFiles.Clear();
                this.packageFiles = null;
            }
        }

        protected internal virtual PackageFile GetPackageFile(string fileName)
        {
            if (this.PackageFiles != null)
            {
                fileName = fileName.Replace('\\', '/');
                if (fileName.StartsWith("./"))
                {
                    fileName = fileName.Substring(2);
                }
                else if (fileName.StartsWith("/"))
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

        protected internal virtual PackageFileCollection GetPackageFiles(Stream stream)
        {
            InternalZipFileCollection files = InternalZipArchive.Open(stream);
            int count = files.Count;
            PackageFileCollection files2 = new PackageFileCollection();
            for (int i = 0; i < count; i++)
            {
                InternalZipFile file = files[i];
                files2.Add(new PackageFile(file.FileName.Replace('\\', '/'), file.FileDataStream, (int) file.UncompressedSize));
            }
            return files2;
        }

        public virtual Stream GetPackageFileStream(string fileName)
        {
            PackageFile packageFile = this.GetPackageFile(fileName);
            return packageFile?.Stream;
        }

        public virtual XmlReader GetPackageFileXmlReader(string fileName)
        {
            Stream packageFileStream = this.GetPackageFileStream(fileName);
            return ((packageFileStream != null) ? this.CreateXmlReader(packageFileStream) : null);
        }

        protected internal virtual void ImportStyles()
        {
            string fileName = "styles.xml";
            XmlReader packageFileXmlReader = this.GetPackageFileXmlReader(fileName);
            if (packageFileXmlReader != null)
            {
                this.ImportStylesCore(packageFileXmlReader);
            }
        }

        protected internal virtual void ImportStylesCore(XmlReader reader)
        {
            if (this.ReadToRootElement(reader, "document-styles", "urn:oasis:names:tc:opendocument:xmlns:office:1.0"))
            {
                base.DestinationStack = new Stack<Destination>();
                this.ImportContent(reader, this.CreateRootStyleDestination());
            }
        }

        public virtual OfficeImage LookUpImageByFileName(string fileName)
        {
            OfficeNativeImage image;
            if (this.packageImages.TryGetValue(fileName, out image))
            {
                return new OfficeReferenceImage(base.DocumentModel, image);
            }
            Stream imageStreamByFileName = this.LookupImageStreamByFileName(fileName);
            if (imageStreamByFileName == null)
            {
                return null;
            }
            try
            {
                imageStreamByFileName.Seek(0L, SeekOrigin.Begin);
                OfficeReferenceImage image2 = base.DocumentModel.CreateImage(imageStreamByFileName);
                this.packageImages.Add(fileName, image2.NativeRootImage);
                return image2;
            }
            catch
            {
                return null;
            }
        }

        protected internal virtual Stream LookupImageStreamByFileName(string fileName)
        {
            PackageFile packageFile = this.GetPackageFile(fileName);
            if (packageFile == null)
            {
                return null;
            }
            byte[] buffer = new byte[packageFile.StreamLength];
            packageFile.Stream.Read(buffer, 0, buffer.Length);
            return new MemoryStream(buffer);
        }

        public virtual void OpenPackage(Stream stream)
        {
            this.packageFiles = this.GetPackageFiles(stream);
        }

        protected override void PrepareOfficeTheme()
        {
        }

        protected internal override void ProcessCurrentDestination(XmlReader reader)
        {
            if (base.DestinationStack.Count > 0)
            {
                base.ProcessCurrentDestination(reader);
            }
        }

        public override string ReadAttribute(XmlReader reader, string attributeName)
        {
            throw new InvalidOperationException();
        }

        public override string ReadAttribute(XmlReader reader, string attributeName, string ns)
        {
            throw new InvalidOperationException();
        }

        public override void ThrowInvalidFile()
        {
            throw new Exception("Invalid OpenDocument file");
        }

        public PackageFileCollection PackageFiles =>
            this.packageFiles;

        public Dictionary<string, OfficeNativeImage> PackageImageStreams =>
            this.packageImages;

        public override string RelationsNamespace =>
            string.Empty;

        public override string DocumentRootFolder { get; set; }

        public override OpenXmlRelationCollection DocumentRelations =>
            null;
    }
}

