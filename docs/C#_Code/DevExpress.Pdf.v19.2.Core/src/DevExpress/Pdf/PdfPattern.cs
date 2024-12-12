namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfPattern : PdfObject
    {
        private const string patternTypeDictionaryKey = "PatternType";
        private const string matrixDictionaryKey = "Matrix";
        private const int shadingPatternType = 2;
        private readonly PdfTransformationMatrix matrix;

        protected PdfPattern(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.matrix = new PdfTransformationMatrix(dictionary.GetArray("Matrix"));
        }

        protected PdfPattern(PdfTransformationMatrix matrix)
        {
            this.matrix = matrix;
        }

        protected virtual PdfWriterDictionary GetDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("PatternType", this.PatternType);
            if (!this.matrix.IsDefault)
            {
                dictionary.Add("Matrix", this.matrix.Data);
            }
            return dictionary;
        }

        internal static PdfPattern Parse(object value)
        {
            PdfReaderStream stream = value as PdfReaderStream;
            if (stream == null)
            {
                PdfReaderDictionary dictionary = value as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfShadingPattern(dictionary);
            }
            int? integer = stream.Dictionary.GetInteger("PatternType");
            if (integer != null)
            {
                int? nullable2 = integer;
                int num = 2;
                if ((nullable2.GetValueOrDefault() == num) ? (nullable2 != null) : false)
                {
                    return new PdfShadingPattern(stream.Dictionary);
                }
            }
            return new PdfTilingPattern(stream);
        }

        public PdfTransformationMatrix Matrix =>
            this.matrix;

        protected abstract int PatternType { get; }
    }
}

