namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfUnclosedPathAnnotation : PdfPathAnnotation
    {
        private const string lineEndingDictionaryKey = "LE";
        private readonly PdfAnnotationLineEndingStyle startLineEnding;
        private readonly PdfAnnotationLineEndingStyle finishLineEnding;

        protected PdfUnclosedPathAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            IList<object> array = dictionary.GetArray("LE");
            if (array != null)
            {
                if (array.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.startLineEnding = ParseLineEnding(array[0]);
                this.finishLineEnding = ParseLineEnding(array[1]);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            if ((this.startLineEnding != PdfAnnotationLineEndingStyle.None) || (this.finishLineEnding != PdfAnnotationLineEndingStyle.None))
            {
                object[] objArray1 = new object[] { new PdfName(PdfEnumToStringConverter.Convert<PdfAnnotationLineEndingStyle>(this.startLineEnding, false)), new PdfName(PdfEnumToStringConverter.Convert<PdfAnnotationLineEndingStyle>(this.finishLineEnding, false)) };
                dictionary["LE"] = objArray1;
            }
            return dictionary;
        }

        private static PdfAnnotationLineEndingStyle ParseLineEnding(object value)
        {
            PdfName name = value as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfEnumToStringConverter.Parse<PdfAnnotationLineEndingStyle>(name.Name, false);
        }

        public PdfAnnotationLineEndingStyle StartLineEnding =>
            this.startLineEnding;

        public PdfAnnotationLineEndingStyle FinishLineEnding =>
            this.finishLineEnding;
    }
}

