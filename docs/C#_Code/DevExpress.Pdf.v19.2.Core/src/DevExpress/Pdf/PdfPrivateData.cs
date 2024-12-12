namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class PdfPrivateData : PdfObject
    {
        private readonly PdfPrivateData parent;
        private readonly int objectNumber;
        private readonly byte[] rawData;
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly Dictionary<string, object> dictionary;

        internal PdfPrivateData(PdfPrivateData parent, PdfReaderDictionary readerDictionary) : base(readerDictionary.Number)
        {
            this.dictionary = new Dictionary<string, object>();
            this.parent = parent;
            this.objectNumber = readerDictionary.Number;
            PdfObjectCollection objects = readerDictionary.Objects;
            if (objects != null)
            {
                this.documentCatalog = objects.DocumentCatalog;
            }
            this.dictionary = readerDictionary;
        }

        internal PdfPrivateData(PdfPrivateData parent, PdfReaderStream readerStream) : this(parent, readerStream.Dictionary)
        {
            this.rawData = readerStream.DecryptedData;
        }

        internal PdfWriterDictionary CreateWriterDictionary(PdfObjectCollection collection)
        {
            PdfObjectCollection objects = this.Objects;
            return PdfElementsDictionaryWriter.Write(this.dictionary, value => this.ToWritableObject(collection, TryResolve(this, objects, value)));
        }

        internal void Remove(string key)
        {
            this.dictionary.Remove(key);
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = this.CreateWriterDictionary(collection) ?? new PdfWriterDictionary(collection);
            return ((this.rawData == null) ? ((object) dictionary) : ((object) new PdfWriterStream(dictionary, this.rawData)));
        }

        private object ToWritableObject(PdfObjectCollection collection, object obj)
        {
            PdfPrivateData data = obj as PdfPrivateData;
            if (data != null)
            {
                return collection.AddObject((PdfObject) data);
            }
            IList<object> list = obj as IList<object>;
            PdfObjectCollection objects = this.Objects;
            return ((list == null) ? obj : new PdfWritableConvertibleArray<object>(list, value => this.ToWritableObject(collection, TryResolve(this, objects, value))));
        }

        internal static object TryResolve(PdfPrivateData parent, PdfObjectCollection collection, object value)
        {
            if (collection != null)
            {
                PdfObjectReference reference = value as PdfObjectReference;
                if (reference != null)
                {
                    if (parent != null)
                    {
                        int number = reference.Number;
                        for (PdfPrivateData data = parent; data != null; data = data.parent)
                        {
                            if (data.objectNumber == number)
                            {
                                return data;
                            }
                        }
                    }
                    object resolvedObject = TryResolve(parent, collection, collection.TryResolve(value, null));
                    collection.GetObject<PdfObject>(reference, o => resolvedObject as PdfObject);
                    return resolvedObject;
                }
            }
            PdfReaderDictionary readerDictionary = value as PdfReaderDictionary;
            if (readerDictionary != null)
            {
                return ((readerDictionary.Count == 0) ? null : new PdfPrivateData(parent, readerDictionary));
            }
            PdfReaderStream readerStream = value as PdfReaderStream;
            if (readerStream != null)
            {
                return new PdfPrivateData(parent, readerStream);
            }
            if (value is double)
            {
                return new PdfDouble(value);
            }
            byte[] buffer = value as byte[];
            if (buffer != null)
            {
                return buffer;
            }
            IList<object> list = value as IList<object>;
            if (list == null)
            {
                return value;
            }
            List<object> list2 = new List<object>(list.Count);
            foreach (object obj2 in list)
            {
                list2.Add(TryResolve(parent, collection, obj2));
            }
            return list2;
        }

        public byte[] RawData =>
            this.rawData;

        public object this[string key] =>
            TryResolve(this, this.Objects, this.dictionary[key]);

        private PdfObjectCollection Objects =>
            this.documentCatalog?.Objects;
    }
}

