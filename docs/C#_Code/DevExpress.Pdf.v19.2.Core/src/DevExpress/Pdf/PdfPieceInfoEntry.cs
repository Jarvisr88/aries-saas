namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfPieceInfoEntry
    {
        private const string lastModifiedKey = "LastModified";
        private const string privateKey = "Private";
        private const string pieceInfoKey = "PieceInfo";
        private readonly PdfDocumentCatalog catalog;
        private readonly DateTimeOffset? lastModified;
        private object data;
        private object dataValue;

        internal PdfPieceInfoEntry(PdfReaderDictionary dictionary)
        {
            this.lastModified = dictionary.GetDate("LastModified");
            dictionary.TryGetValue("Private", out this.dataValue);
            this.catalog = dictionary.Objects.DocumentCatalog;
        }

        internal static Dictionary<string, PdfPieceInfoEntry> Parse(PdfReaderDictionary dictionary)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("PieceInfo");
            if (dictionary2 == null)
            {
                return null;
            }
            Dictionary<string, PdfPieceInfoEntry> dictionary3 = new Dictionary<string, PdfPieceInfoEntry>();
            foreach (KeyValuePair<string, object> pair in dictionary2)
            {
                PdfReaderDictionary dictionary4 = dictionary2.Objects.TryResolve(pair.Value, null) as PdfReaderDictionary;
                if (dictionary4 != null)
                {
                    dictionary3.Add(pair.Key, new PdfPieceInfoEntry(dictionary4));
                }
            }
            return ((dictionary3.Count == 0) ? null : dictionary3);
        }

        internal PdfDictionary Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddNullable<DateTimeOffset>("LastModified", this.lastModified);
            dictionary.AddIfPresent("Private", this.WriteObject(objects, this.Data));
            return dictionary;
        }

        private object WriteObject(PdfObjectCollection objects, object obj)
        {
            PdfPrivateData data = obj as PdfPrivateData;
            if (data != null)
            {
                return objects.AddObject((PdfObject) data);
            }
            IEnumerable<object> enumerable = obj as IEnumerable<object>;
            return ((enumerable == null) ? obj : new PdfWritableConvertibleArray<object>(enumerable, value => this.WriteObject(objects, value)));
        }

        internal static void WritePieceInfo(PdfWriterDictionary dictionary, Dictionary<string, PdfPieceInfoEntry> pieceInfo)
        {
            dictionary.AddIfPresent("PieceInfo", PdfElementsDictionaryWriter.Write(pieceInfo, value => ((PdfPieceInfoEntry) value).Write(dictionary.Objects)));
        }

        public DateTimeOffset? LastModified =>
            this.lastModified;

        public object Data
        {
            get
            {
                if (this.dataValue != null)
                {
                    this.data = PdfPrivateData.TryResolve(null, this.catalog.Objects, this.dataValue);
                    this.dataValue = null;
                }
                return this.data;
            }
        }
    }
}

