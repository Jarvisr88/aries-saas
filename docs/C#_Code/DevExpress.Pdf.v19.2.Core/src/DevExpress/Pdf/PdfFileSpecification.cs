namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Text;

    public class PdfFileSpecification : PdfObject
    {
        private const string dictionaryType = "Filespec";
        private const string fileSystemDictionaryKey = "FS";
        private const string fileNameDictionaryKey = "F";
        private const string unicodeFileNameDictionaryKey = "UF";
        private const string embeddedFileDictionaryKey = "EF";
        private const string descriptionDictionaryKey = "Desc";
        private const string collectionItemDictionaryKey = "CI";
        private const string relationshipDictionaryKey = "AFRelationship";
        private const string indexDictionaryKey = "Index";
        private readonly string fileSystem;
        private readonly int index;
        private string fileName;
        private string description;
        private PdfAssociatedFileRelationship relationship;
        private PdfFileAttachment attachment;
        private PdfFileSpecificationData fileSpecificationData;

        private PdfFileSpecification(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.fileSystem = dictionary.GetName("FS");
            string text1 = dictionary.GetString("UF");
            string text5 = text1;
            if (text1 == null)
            {
                string local1 = text1;
                string text2 = dictionary.GetString("F");
                text5 = text2;
                if (text2 == null)
                {
                    string local2 = text2;
                    string text3 = dictionary.GetString("DOS");
                    text5 = text3;
                    if (text3 == null)
                    {
                        string local3 = text3;
                        string text4 = dictionary.GetString("Mac");
                        text5 = text4;
                        if (text4 == null)
                        {
                            string local4 = text4;
                            text5 = dictionary.GetString("Unix");
                        }
                    }
                }
            }
            this.fileName = text5;
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("EF");
            if (dictionary2 == null)
            {
                this.fileSpecificationData = new PdfFileSpecificationData();
            }
            else
            {
                PdfReaderDictionary streamDictionary = dictionary2.GetStreamDictionary("F");
                PdfReaderDictionary dictionary6 = streamDictionary;
                if (streamDictionary == null)
                {
                    PdfReaderDictionary local5 = streamDictionary;
                    PdfReaderDictionary dictionary5 = dictionary2.GetStreamDictionary("DOS");
                    dictionary6 = dictionary5;
                    if (dictionary5 == null)
                    {
                        PdfReaderDictionary local6 = dictionary5;
                        dictionary6 = dictionary2.GetStreamDictionary("Unix");
                    }
                }
                PdfReaderDictionary dictionary4 = dictionary6;
                if (dictionary4 == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.fileSpecificationData = new PdfFileSpecificationData(dictionary4);
            }
            this.description = dictionary.GetTextString("Desc");
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("CI");
            if (dictionary3 != null)
            {
                int? integer = dictionary3.GetInteger("Index");
                this.index = (integer != null) ? integer.GetValueOrDefault() : 0;
            }
            this.relationship = dictionary.GetEnumName<PdfAssociatedFileRelationship>("AFRelationship");
        }

        internal PdfFileSpecification(string fileName)
        {
            this.fileName = fileName;
            this.fileSpecificationData = new PdfFileSpecificationData();
        }

        internal static PdfFileSpecification Parse(object value)
        {
            PdfReaderDictionary dictionary = value as PdfReaderDictionary;
            if (dictionary != null)
            {
                return new PdfFileSpecification(dictionary);
            }
            byte[] buffer = value as byte[];
            if (buffer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return new PdfFileSpecification(PdfDocumentReader.ConvertToString(buffer));
        }

        internal static PdfFileSpecification Parse(PdfReaderDictionary dictionary, string key, bool required)
        {
            object obj2;
            if (dictionary.TryGetValue(key, out obj2))
            {
                return dictionary.Objects.GetFileSpecification(obj2);
            }
            if (required)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "Filespec");
            dictionary.AddName("FS", this.fileSystem);
            dictionary.Add("F", Encoding.UTF8.GetBytes(this.fileName));
            dictionary.Add("UF", this.fileName);
            if (this.fileSpecificationData.HasData)
            {
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
                dictionary2.Add("F", this.fileSpecificationData);
                dictionary.Add("EF", dictionary2);
            }
            dictionary.AddIfPresent("Desc", this.description);
            if (this.index != 0)
            {
                PdfWriterDictionary dictionary3 = new PdfWriterDictionary(objects);
                dictionary3.Add("Index", this.index);
                dictionary.Add("CI", dictionary3);
            }
            dictionary.AddEnumName<PdfAssociatedFileRelationship>("AFRelationship", this.relationship);
            return dictionary;
        }

        public string FileSystem =>
            this.fileSystem;

        public int Index =>
            this.index;

        public string FileName
        {
            get => 
                this.fileName;
            internal set => 
                this.fileName = value;
        }

        public string MimeType
        {
            get => 
                this.fileSpecificationData.MimeType;
            internal set => 
                this.fileSpecificationData.MimeType = value;
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.fileSpecificationData.CreationDate;
            internal set => 
                this.fileSpecificationData.CreationDate = value;
        }

        public DateTimeOffset? ModificationDate
        {
            get => 
                this.fileSpecificationData.ModificationDate;
            internal set => 
                this.fileSpecificationData.ModificationDate = value;
        }

        public byte[] FileData
        {
            get => 
                this.fileSpecificationData.Data;
            internal set => 
                this.fileSpecificationData.Data = value;
        }

        public int Size
        {
            get
            {
                int? size = this.fileSpecificationData.Size;
                return ((size != null) ? size.GetValueOrDefault() : 0);
            }
        }

        public string Description
        {
            get => 
                this.description;
            internal set => 
                this.description = value;
        }

        public PdfAssociatedFileRelationship Relationship
        {
            get => 
                this.relationship;
            internal set => 
                this.relationship = value;
        }

        internal PdfFileAttachment Attachment
        {
            get
            {
                this.attachment ??= new PdfFileAttachment(this);
                return this.attachment;
            }
            set => 
                this.attachment = value;
        }
    }
}

