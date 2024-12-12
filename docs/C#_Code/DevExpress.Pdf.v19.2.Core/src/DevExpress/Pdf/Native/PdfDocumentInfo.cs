namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class PdfDocumentInfo : PdfObject
    {
        private static HashSet<string> excludedKeys;
        private const string titleDictionaryKey = "Title";
        private const string authorDictionaryKey = "Author";
        private const string subjectDictionaryKey = "Subject";
        private const string keywordsDictionaryKey = "Keywords";
        private const string creatorDictionaryKey = "Creator";
        private const string producerDictionaryKey = "Producer";
        private const string creationDateDictionaryKey = "CreationDate";
        private const string modDateDictionaryKey = "ModDate";
        private const string trappedDictionaryKey = "Trapped";
        private const string trueTrappedValue = "True";
        private const string falseTrappedValue = "False";
        private const string dateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
        private readonly PdfCustomPropertiesDictionary customPropertiesDictionary;
        private string title;
        private string author;
        private string subject;
        private string keywords;
        private string creator;
        private string producer;
        private string additionalMetadata;
        private DateTimeOffset? creationDate;
        private DateTimeOffset? modDate;
        private DefaultBoolean trapped;

        static PdfDocumentInfo()
        {
            HashSet<string> set1 = new HashSet<string>();
            set1.Add("Title");
            set1.Add("Author");
            set1.Add("Subject");
            set1.Add("Keywords");
            set1.Add("Creator");
            set1.Add("Producer");
            set1.Add("CreationDate");
            set1.Add("ModDate");
            set1.Add("Trapped");
            excludedKeys = set1;
        }

        public PdfDocumentInfo()
        {
            this.customPropertiesDictionary = new PdfCustomPropertiesDictionary();
            this.trapped = DefaultBoolean.Default;
        }

        public PdfDocumentInfo(PdfReaderDictionary dictionary)
        {
            object obj2;
            this.customPropertiesDictionary = new PdfCustomPropertiesDictionary();
            this.trapped = DefaultBoolean.Default;
            this.title = dictionary.GetString("Title");
            this.author = dictionary.GetString("Author");
            this.subject = dictionary.GetString("Subject");
            this.keywords = dictionary.GetString("Keywords");
            this.creator = dictionary.GetString("Creator");
            this.producer = dictionary.GetString("Producer");
            this.creationDate = dictionary.GetDate("CreationDate");
            this.modDate = dictionary.GetDate("ModDate");
            if (!dictionary.TryGetValue("Trapped", out obj2))
            {
                goto TR_000A;
            }
            else if (!(obj2 as bool))
            {
                string str;
                PdfName name = obj2 as PdfName;
                if (name != null)
                {
                    str = name.Name;
                }
                else
                {
                    byte[] buffer = obj2 as byte[];
                    if (buffer == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    str = PdfDocumentReader.ConvertToString(buffer);
                }
                uint num = <PrivateImplementationDetails>.ComputeStringHash(str);
                if (num > 0x811c9dc5)
                {
                    if (num > 0x9b759fb9)
                    {
                        if (num == 0xcc200e59)
                        {
                            if (str == "Unknown")
                            {
                                goto TR_000C;
                            }
                        }
                        else if ((num == 0xcdde5e05) && (str == "True"))
                        {
                            goto TR_000F;
                        }
                    }
                    else if (num == 0x977555f8)
                    {
                        if (str == "False")
                        {
                            goto TR_0012;
                        }
                    }
                    else if ((num == 0x9b759fb9) && (str == "unknown"))
                    {
                        goto TR_000C;
                    }
                    goto TR_000D;
                }
                else
                {
                    if (num == 0xb069958)
                    {
                        if (str == "false")
                        {
                            goto TR_0012;
                        }
                    }
                    else if (num == 0x4db211e5)
                    {
                        if (str == "true")
                        {
                            goto TR_000F;
                        }
                    }
                    else if ((num == 0x811c9dc5) && ((str != null) && (str.Length == 0)))
                    {
                        goto TR_000C;
                    }
                    goto TR_000D;
                }
            }
            else
            {
                this.trapped = ((bool) obj2) ? DefaultBoolean.True : DefaultBoolean.False;
                goto TR_000A;
            }
            goto TR_000C;
        TR_000A:
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                if (!excludedKeys.Contains(pair.Key))
                {
                    byte[] bytes = dictionary.GetBytes(pair.Key, true);
                    if (bytes != null)
                    {
                        this.customPropertiesDictionary.Add(pair.Key, bytes);
                    }
                }
            }
            return;
        TR_000C:
            this.trapped = DefaultBoolean.Default;
            goto TR_000A;
        TR_000D:
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            goto TR_000A;
        TR_000F:
            this.trapped = DefaultBoolean.True;
            goto TR_000A;
        TR_0012:
            this.trapped = DefaultBoolean.False;
            goto TR_000A;
        }

        internal PdfMetadata GetMetadata(PdfCompatibility compatibility)
        {
            PdfXmpMetadataBuilder builder = new PdfXmpMetadataBuilder();
            switch (compatibility)
            {
                case PdfCompatibility.PdfA1b:
                    builder.AppendPdfACompatibilityElement(1);
                    break;

                case PdfCompatibility.PdfA2b:
                    builder.AppendPdfACompatibilityElement(2);
                    break;

                case PdfCompatibility.PdfA3b:
                    builder.AppendPdfACompatibilityElement(3);
                    break;

                default:
                    break;
            }
            builder.AppendPdfElement("Producer", this.producer);
            builder.AppendPdfElement("Keywords", this.keywords);
            if (this.creationDate != null)
            {
                builder.AppendXmpElement("CreateDate", this.creationDate.Value.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture));
            }
            if (this.modDate != null)
            {
                string str = this.modDate.Value.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
                builder.AppendXmpElement("ModifyDate", str);
                builder.AppendXmpElement("MetadataDate", str);
            }
            builder.AppendXmpElement("CreatorTool", this.creator);
            builder.AppendDcElement("creator", "Seq", false, this.author);
            builder.AppendDcElement("title", "Alt", true, this.title);
            builder.AppendDcElement("description", "Alt", true, this.subject);
            if (this.keywords != null)
            {
                builder.AppendDcStartElement("subject");
                builder.AppendRdfStartElement("Bag");
                char[] separator = new char[] { ';', ',' };
                string[] strArray = this.keywords.Split(separator);
                int index = 0;
                while (true)
                {
                    if (index >= strArray.Length)
                    {
                        builder.AppendEndElement();
                        builder.AppendEndElement();
                        break;
                    }
                    builder.AppendRdfElement("li", strArray[index].Trim());
                    index++;
                }
            }
            return new PdfMetadata(builder.GetXml(this.additionalMetadata));
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Title", this.title, null);
            dictionary.Add("Author", this.author, null);
            dictionary.Add("Subject", this.subject, null);
            dictionary.Add("Keywords", this.keywords, null);
            dictionary.Add("Creator", this.creator, null);
            dictionary.Add("Producer", this.producer, null);
            dictionary.AddNullable<DateTimeOffset>("CreationDate", this.creationDate);
            dictionary.AddNullable<DateTimeOffset>("ModDate", this.modDate);
            DefaultBoolean trapped = this.trapped;
            if (trapped == DefaultBoolean.True)
            {
                dictionary.Add("Trapped", new PdfName("True"));
            }
            else if (trapped == DefaultBoolean.False)
            {
                dictionary.Add("Trapped", new PdfName("False"));
            }
            foreach (KeyValuePair<string, byte[]> pair in this.customPropertiesDictionary.BlobDictionary)
            {
                if (pair.Value != null)
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            }
            return ((dictionary.Count > 0) ? dictionary : null);
        }

        public string Title
        {
            get => 
                this.title;
            set => 
                this.title = value;
        }

        public string Author
        {
            get => 
                this.author;
            set => 
                this.author = value;
        }

        public string Subject
        {
            get => 
                this.subject;
            set => 
                this.subject = value;
        }

        public string Keywords
        {
            get => 
                this.keywords;
            set => 
                this.keywords = value;
        }

        public string Creator
        {
            get => 
                this.creator;
            set => 
                this.creator = value;
        }

        public string Producer
        {
            get => 
                this.producer;
            set => 
                this.producer = value;
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.creationDate;
            set => 
                this.creationDate = value;
        }

        public DateTimeOffset? ModDate
        {
            get => 
                this.modDate;
            set => 
                this.modDate = value;
        }

        public DefaultBoolean Trapped
        {
            get => 
                this.trapped;
            set => 
                this.trapped = value;
        }

        public string AdditionalMetadata
        {
            get => 
                this.additionalMetadata;
            set => 
                this.additionalMetadata = value;
        }

        public IDictionary<string, string> CustomProperties =>
            this.customPropertiesDictionary;
    }
}

