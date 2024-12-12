namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.Office.Crypto;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public abstract class XlsxImporter : DestinationXmImporter, IDisposable
    {
        private string documentRootFolder;
        private Stack<OpenXmlRelationCollection> documentRelationsStack;
        private XmlPackageReader packageReader;
        private static Dictionary<string, string> strictNamespaces = CreateStrictNamespaces();
        private const string StrictOpenXMLOfficeDocumentPrefix = "http://purl.oclc.org/ooxml/officeDocument";
        private const string OpenXMLOfficeDocumentPrefix = "http://schemas.openxmlformats.org/officeDocument/2006";
        private bool strictOpenXml;

        protected XlsxImporter()
        {
        }

        private long CalculateInitialStreamPosition(Stream stream)
        {
            try
            {
                return stream.Position;
            }
            catch
            {
                return -1L;
            }
        }

        protected internal string CalculateRelationTarget(OpenXmlRelation relation, string rootFolder, string defaultFileName) => 
            OpenXmlImportRelationHelper.CalculateRelationTarget(relation, rootFolder, defaultFileName);

        public override bool ConvertToBool(string value)
        {
            if ((value == "1") || ((value == "on") || ((value == "true") || (value == "t"))))
            {
                return true;
            }
            if ((value != "0") && ((value != "off") && ((value != "false") && (value != "f"))))
            {
                base.ThrowInvalidFile($"Can't convert {value} to bool");
            }
            return false;
        }

        private static Dictionary<string, string> CreateStrictNamespaces() => 
            new Dictionary<string, string> { 
                { 
                    "http://schemas.openxmlformats.org/spreadsheetml/2006/main",
                    "http://purl.oclc.org/ooxml/spreadsheetml/main"
                },
                { 
                    "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument",
                    "http://purl.oclc.org/ooxml/officeDocument/relationships/officeDocument"
                },
                { 
                    "http://schemas.openxmlformats.org/officeDocument/2006/relationships",
                    "http://purl.oclc.org/ooxml/officeDocument/relationships"
                },
                { 
                    "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings",
                    "http://purl.oclc.org/ooxml/officeDocument/relationships/sharedStrings"
                },
                { 
                    "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles",
                    "http://purl.oclc.org/ooxml/officeDocument/relationships/styles"
                },
                { 
                    "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet",
                    "http://purl.oclc.org/ooxml/officeDocument/relationships/worksheet"
                }
            };

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposePackageFiles();
            }
        }

        protected internal void DisposePackageFiles()
        {
            if (this.packageReader != null)
            {
                this.packageReader.Dispose();
                this.packageReader = null;
            }
        }

        protected string GetNamespace(string name) => 
            (!this.strictOpenXml || !strictNamespaces.ContainsKey(name)) ? name : strictNamespaces[name];

        protected internal PackageFile GetPackageFile(string fileName) => 
            this.packageReader.GetPackageFile(fileName);

        protected internal Stream GetPackageFileStream(string fileName) => 
            this.packageReader.GetPackageFileStream(fileName);

        protected internal XmlReader GetPackageFileXmlReader(string fileName) => 
            this.packageReader.GetPackageFileXmlReader(fileName, this.CreateXmlReaderSettings());

        protected internal XmlReader GetPackageFileXmlReader(string fileName, XmlReaderSettings settings) => 
            this.packageReader.GetPackageFileXmlReader(fileName, settings);

        protected internal XmlReader GetPackageFileXmlReaderBasedSeekableStream(string fileName) => 
            this.packageReader.GetPackageFileXmlReader(fileName, this.CreateXmlReaderSettings(), true);

        public void Import(Stream stream)
        {
            this.Prepare();
            this.ImportWorkbook(stream);
        }

        protected internal OpenXmlRelationCollection ImportRelations(string fileName)
        {
            XmlReader packageFileXmlReader = this.GetPackageFileXmlReader(fileName);
            return ((packageFileXmlReader == null) ? new OpenXmlRelationCollection() : this.ImportRelationsCore(packageFileXmlReader));
        }

        protected internal OpenXmlRelationCollection ImportRelationsCore(XmlReader reader)
        {
            OpenXmlRelationCollection relations = new OpenXmlRelationCollection();
            if (this.ReadToRootElement(reader, "Relationships", this.PackageRelsNamespace))
            {
                this.ImportContent(reader, new RelationshipsDestination(this, relations));
            }
            return relations;
        }

        protected internal void ImportWorkbook(Stream stream)
        {
            this.OpenPackage(stream);
            OpenXmlRelationCollection relations = this.ImportRelations("_rels/.rels");
            string fileName = this.LookupRelationTargetByType(relations, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument", string.Empty, "workbook.xml");
            XmlReader packageFileXmlReader = this.GetPackageFileXmlReader(fileName);
            if (packageFileXmlReader == null)
            {
                base.ThrowInvalidFile("Can't get reader for workbook.xml");
            }
            if (!this.ReadToRootElement(packageFileXmlReader, "workbook", this.SpreadsheetNamespace))
            {
                base.ThrowInvalidFile("Can't find root element \"workbook\"");
            }
            this.documentRootFolder = Path.GetDirectoryName(fileName);
            this.documentRelationsStack.Push(this.ImportRelations(this.documentRootFolder + "/_rels/" + Path.GetFileName(fileName) + ".rels"));
            this.ImportMainDocument(packageFileXmlReader, stream);
        }

        protected internal string LookupRelationTargetById(OpenXmlRelationCollection relations, string id, string rootFolder, string defaultFileName) => 
            OpenXmlImportRelationHelper.LookupRelationTargetById(relations, id, rootFolder, defaultFileName);

        protected internal string LookupRelationTargetByType(OpenXmlRelationCollection relations, string type, string rootFolder, string defaultFileName)
        {
            string str = OpenXmlImportRelationHelper.LookupRelationTargetByType(relations, type, rootFolder, string.Empty);
            if (string.IsNullOrEmpty(str) && (!string.IsNullOrEmpty(type) && type.StartsWith("http://schemas.openxmlformats.org/officeDocument/2006")))
            {
                string str2 = type.Replace("http://schemas.openxmlformats.org/officeDocument/2006", "http://purl.oclc.org/ooxml/officeDocument");
                str = OpenXmlImportRelationHelper.LookupRelationTargetByType(relations, str2, rootFolder, string.Empty);
                if (!string.IsNullOrEmpty(str))
                {
                    this.strictOpenXml = true;
                }
            }
            if (string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(defaultFileName))
            {
                str = !string.IsNullOrEmpty(rootFolder) ? (rootFolder + "/" + defaultFileName) : defaultFileName;
            }
            return str;
        }

        private Stream OpenEncryptedStream(Stream stream)
        {
            PackageFileReader reader = new PackageFileReader(stream);
            Stream cachedPackageFileStream = reader.GetCachedPackageFileStream(@"\EncryptionInfo");
            if (cachedPackageFileStream == null)
            {
                return null;
            }
            Stream encryptedPackageStream = reader.GetCachedPackageFileStream(@"\EncryptedPackage");
            if (encryptedPackageStream == null)
            {
                return null;
            }
            string encryptionPassword = this.EncryptionPassword;
            EncryptionSessionError error = EncryptionSession.Load(cachedPackageFileStream).GetDecryptedStream(ref encryptionPassword, encryptedPackageStream, out stream);
            this.ThrowExceptionOnError(error);
            return stream;
        }

        protected void OpenPackage(Stream stream)
        {
            long position = this.CalculateInitialStreamPosition(stream);
            this.OpenPackageCore(stream);
            if ((this.packageReader.PackageFiles.Count <= 0) && this.SeekToInitialStreamPosition(stream, position))
            {
                Stream stream2 = this.OpenEncryptedStream(stream);
                if (stream2 == null)
                {
                    throw new InvalidFileException(InvalidFileError.WrongFormat, "Wrong file format!");
                }
                this.OpenPackageCore(stream2);
            }
        }

        protected internal void OpenPackageCore(Stream stream)
        {
            this.packageReader = new XmlPackageReader();
            this.packageReader.OpenPackage(stream);
        }

        private void Prepare()
        {
            this.documentRelationsStack = new Stack<OpenXmlRelationCollection>();
        }

        public override string ReadAttribute(XmlReader reader, string attributeName) => 
            reader.GetAttribute(attributeName);

        public override string ReadAttribute(XmlReader reader, string attributeName, string ns) => 
            reader.GetAttribute(attributeName, ns);

        private bool SeekToInitialStreamPosition(Stream stream, long position)
        {
            bool flag;
            try
            {
                if (!stream.CanSeek || (position < 0L))
                {
                    flag = false;
                }
                else
                {
                    stream.Seek(position, SeekOrigin.Begin);
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private void ThrowExceptionOnError(EncryptionSessionError error)
        {
            if (error == EncryptionSessionError.PasswordRequired)
            {
                throw new EncryptedFileException(EncryptedFileError.PasswordRequired, "Password required to open this file!");
            }
            if (error == EncryptionSessionError.WrongPassword)
            {
                throw new EncryptedFileException(EncryptedFileError.WrongPassword, "Wrong password!");
            }
            if (error == EncryptionSessionError.IntegrityCheckFailed)
            {
                throw new EncryptedFileException(EncryptedFileError.IntegrityCheckFailed, "Encrypted file integrity check failed!\r\nThis file may have been tampered with or corrupted.");
            }
        }

        protected internal bool StrictOpenXML =>
            this.strictOpenXml;

        protected internal string SpreadsheetNamespace =>
            this.GetNamespace("http://schemas.openxmlformats.org/spreadsheetml/2006/main");

        protected internal string RelationsNamespace =>
            this.GetNamespace("http://schemas.openxmlformats.org/officeDocument/2006/relationships");

        protected internal string OfficeDocumentNamespace =>
            this.GetNamespace("http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument");

        protected internal string RelsStylesNamespace =>
            this.GetNamespace("http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles");

        protected internal string PackageRelsNamespace =>
            this.GetNamespace("http://schemas.openxmlformats.org/package/2006/relationships");

        protected internal string RelsSharedStringsNamespace =>
            this.GetNamespace("http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings");

        protected internal string RelsWorksheetNamespace =>
            this.GetNamespace("http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet");

        public Stack<OpenXmlRelationCollection> DocumentRelationsStack =>
            this.documentRelationsStack;

        public OpenXmlRelationCollection DocumentRelations =>
            this.documentRelationsStack.Peek();

        public string DocumentRootFolder =>
            this.documentRootFolder;

        public abstract string EncryptionPassword { get; set; }
    }
}

