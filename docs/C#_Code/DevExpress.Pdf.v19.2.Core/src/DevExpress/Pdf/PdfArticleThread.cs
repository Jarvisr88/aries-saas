namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfArticleThread : PdfObject
    {
        private const string firstBeadDictionaryKey = "F";
        private const string threadInfoDictionaryKey = "I";
        private readonly PdfDocumentInfo threadInfo;
        private PdfBead firstBead;

        internal PdfArticleThread(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            PdfObjectCollection objects = dictionary.Objects;
            PdfObjectReference objectReference = dictionary.GetObjectReference("F");
            if (objectReference != null)
            {
                this.firstBead = this.CreateBead(objects, objectReference);
                if (this.firstBead != null)
                {
                    int objectNumber = this.firstBead.ObjectNumber;
                    int nextNumber = this.firstBead.NextNumber;
                    PdfBead firstBead = this.firstBead;
                    int num3 = objectNumber;
                    while (true)
                    {
                        if (nextNumber == objectNumber)
                        {
                            firstBead.Next = this.firstBead;
                            this.firstBead.Previous = firstBead;
                            break;
                        }
                        PdfBead bead2 = this.CreateBead(objects, new PdfObjectReference(nextNumber));
                        if (bead2.PrevNumber != num3)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        firstBead.Next = bead2;
                        bead2.Previous = firstBead;
                        firstBead = bead2;
                        num3 = nextNumber;
                        nextNumber = bead2.NextNumber;
                    }
                }
            }
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("I");
            this.threadInfo = (dictionary2 == null) ? new PdfDocumentInfo() : new PdfDocumentInfo(dictionary2);
        }

        private PdfBead CreateBead(PdfObjectCollection objects, PdfObjectReference reference) => 
            objects.GetObject<PdfBead>(reference, dictionary => new PdfBead(this, dictionary));

        internal static IList<PdfArticleThread> Parse(PdfObjectCollection objects, IList<object> array)
        {
            if (array == null)
            {
                return null;
            }
            List<PdfArticleThread> list = new List<PdfArticleThread>();
            foreach (object obj2 in array)
            {
                Func<PdfReaderDictionary, PdfArticleThread> create = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<PdfReaderDictionary, PdfArticleThread> local1 = <>c.<>9__2_0;
                    create = <>c.<>9__2_0 = dictionary => new PdfArticleThread(dictionary);
                }
                list.Add(objects.GetObject<PdfArticleThread>(obj2, create));
            }
            return list;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("F", this.firstBead);
            dictionary.AddIfPresent("I", collection.AddObject((PdfObject) this.threadInfo));
            return dictionary;
        }

        public string Title
        {
            get => 
                this.threadInfo.Title;
            set => 
                this.threadInfo.Title = value;
        }

        public string Author
        {
            get => 
                this.threadInfo.Author;
            set => 
                this.threadInfo.Author = value;
        }

        public string Subject
        {
            get => 
                this.threadInfo.Subject;
            set => 
                this.threadInfo.Subject = value;
        }

        public string Keywords
        {
            get => 
                this.threadInfo.Keywords;
            set => 
                this.threadInfo.Keywords = value;
        }

        public string Creator
        {
            get => 
                this.threadInfo.Creator;
            set => 
                this.threadInfo.Creator = value;
        }

        public string Producer
        {
            get => 
                this.threadInfo.Producer;
            set => 
                this.threadInfo.Producer = value;
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.threadInfo.CreationDate;
            set => 
                this.threadInfo.CreationDate = value;
        }

        public DateTimeOffset? ModDate
        {
            get => 
                this.threadInfo.ModDate;
            set => 
                this.threadInfo.ModDate = value;
        }

        public DefaultBoolean Trapped
        {
            get => 
                this.threadInfo.Trapped;
            set => 
                this.threadInfo.Trapped = value;
        }

        public PdfBead FirstBead
        {
            get => 
                this.firstBead;
            internal set => 
                this.firstBead = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfArticleThread.<>c <>9 = new PdfArticleThread.<>c();
            public static Func<PdfReaderDictionary, PdfArticleThread> <>9__2_0;

            internal PdfArticleThread <Parse>b__2_0(PdfReaderDictionary dictionary) => 
                new PdfArticleThread(dictionary);
        }
    }
}

