namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfSignatureFormFieldLock : PdfObject
    {
        private const string actionDictionaryKey = "Action";
        private const string fieldsDictionaryKey = "Fields";
        private readonly PdfSignatureFormFieldLockRange range;
        private readonly IList<string> fieldNames;

        internal PdfSignatureFormFieldLock(PdfReaderDictionary dictionary)
        {
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.range = dictionary.GetEnumName<PdfSignatureFormFieldLockRange>("Action");
            Func<object, string> create = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<object, string> local1 = <>c.<>9__8_0;
                create = <>c.<>9__8_0 = delegate (object value) {
                    byte[] buffer = value as byte[];
                    if (buffer == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return PdfDocumentReader.ConvertToString(buffer);
                };
            }
            this.fieldNames = dictionary.GetArray<string>("Fields", create);
            if (this.range != PdfSignatureFormFieldLockRange.All)
            {
                if (this.fieldNames == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else if (this.fieldNames != null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddEnumName<PdfSignatureFormFieldLockRange>("Action", this.range);
            if (this.fieldNames != null)
            {
                dictionary.Add("Fields", new PdfWritableArray(this.fieldNames));
            }
            return dictionary;
        }

        public PdfSignatureFormFieldLockRange Range =>
            this.range;

        public IList<string> FieldNames =>
            this.fieldNames;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfSignatureFormFieldLock.<>c <>9 = new PdfSignatureFormFieldLock.<>c();
            public static Func<object, string> <>9__8_0;

            internal string <.ctor>b__8_0(object value)
            {
                byte[] buffer = value as byte[];
                if (buffer == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfDocumentReader.ConvertToString(buffer);
            }
        }
    }
}

