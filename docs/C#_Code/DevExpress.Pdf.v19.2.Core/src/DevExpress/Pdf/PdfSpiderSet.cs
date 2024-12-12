namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class PdfSpiderSet : PdfObject
    {
        private const string dictionaryName = "SpiderContentSet";
        private const string idKey = "ID";
        private const string contentTypeKey = "CT";
        private const string timeStampKey = "TS";
        private const string sourceInformationKey = "SI";
        private const string pagesKey = "O";
        private readonly byte[] id;
        private readonly List<PdfPage> pageSet;
        private readonly List<PdfSourceInformation> sourceInformation;
        private readonly string contentType;
        private readonly DateTimeOffset? timeStamp;

        protected PdfSpiderSet(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.pageSet = new List<PdfPage>();
            this.sourceInformation = new List<PdfSourceInformation>();
            this.id = dictionary.GetBytes("ID");
            IList<object> array = dictionary.GetArray("O");
            object obj2 = null;
            if ((this.id == null) || ((array == null) || !dictionary.TryGetValue("SI", out obj2)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfObjectCollection objects = dictionary.Objects;
            PdfDocumentCatalog documentCatalog = objects.DocumentCatalog;
            int num = -1;
            IList<PdfPage> pages = documentCatalog.Pages;
            foreach (object obj3 in array)
            {
                PdfPage item = documentCatalog.FindPage(obj3);
                if (item != null)
                {
                    int index = pages.IndexOf(item);
                    if (index <= num)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    num = index;
                    this.pageSet.Add(item);
                }
            }
            Func<PdfReaderDictionary, PdfSourceInformation> create = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<PdfReaderDictionary, PdfSourceInformation> local1 = <>c.<>9__24_0;
                create = <>c.<>9__24_0 = siDictionary => new PdfSourceInformation(siDictionary);
            }
            this.sourceInformation.Add(objects.GetObject<PdfSourceInformation>(obj2, create));
            this.contentType = dictionary.GetString("CT");
            this.timeStamp = dictionary.GetDate("TS");
        }

        internal static PdfSpiderSet Create(PdfObjectCollection objects, object value)
        {
            Func<PdfReaderDictionary, PdfSpiderSet> create = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<PdfReaderDictionary, PdfSpiderSet> local1 = <>c.<>9__6_0;
                create = <>c.<>9__6_0 = delegate (PdfReaderDictionary dictionary) {
                    string name = dictionary.GetName("S");
                    if (name == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    if (name == "SPS")
                    {
                        return new PdfPageSet(dictionary);
                    }
                    if (name == "SIS")
                    {
                        return new PdfImageSet(dictionary);
                    }
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;
                };
            }
            return objects.GetObject<PdfSpiderSet>(value, create);
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Type", new PdfName("SpiderContentSet"));
            dictionary.Add("S", new PdfName(this.SubType));
            dictionary.Add("ID", this.id, null);
            dictionary.Add("CT", this.contentType, null);
            dictionary.Add("TS", this.timeStamp, null);
            if (this.sourceInformation.Count > 0)
            {
                object obj2 = (this.sourceInformation.Count != 1) ? ((object) new PdfWritableObjectArray((IEnumerable<PdfObject>) this.sourceInformation, collection)) : ((object) collection.AddObject((PdfObject) this.sourceInformation[0]));
                dictionary.Add("SI", obj2);
            }
            List<object> list = new List<object>();
            foreach (PdfPage page in this.pageSet)
            {
                list.Add(collection.AddObject((PdfObject) page));
            }
            dictionary.Add("O", list);
            return dictionary;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            this.CreateDictionary(collection);

        public byte[] ID =>
            this.id;

        public IEnumerable<PdfPage> PageSet =>
            this.pageSet;

        public IEnumerable<PdfSourceInformation> SourceInformation =>
            this.sourceInformation;

        public string ContentType =>
            this.contentType;

        public DateTimeOffset? TimeStamp =>
            this.timeStamp;

        protected abstract string SubType { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfSpiderSet.<>c <>9 = new PdfSpiderSet.<>c();
            public static Func<PdfReaderDictionary, PdfSpiderSet> <>9__6_0;
            public static Func<PdfReaderDictionary, PdfSourceInformation> <>9__24_0;

            internal PdfSourceInformation <.ctor>b__24_0(PdfReaderDictionary siDictionary) => 
                new PdfSourceInformation(siDictionary);

            internal PdfSpiderSet <Create>b__6_0(PdfReaderDictionary dictionary)
            {
                string name = dictionary.GetName("S");
                if (name == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                if (name == "SPS")
                {
                    return new PdfPageSet(dictionary);
                }
                if (name == "SIS")
                {
                    return new PdfImageSet(dictionary);
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
                return null;
            }
        }
    }
}

