namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfShadingPattern : PdfPattern
    {
        private const string shadingDictionaryKey = "Shading";
        private const string graphicsStateDictionaryKey = "ExtGState";
        private readonly PdfShading shading;
        private readonly PdfGraphicsStateParameters graphicsState;

        internal PdfShadingPattern(PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            if (!dictionary.TryGetValue("Shading", out obj2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.shading = dictionary.Objects.GetObject<PdfShading>(obj2, new Func<object, PdfShading>(PdfShading.Parse));
            this.graphicsState = dictionary.GetGraphicsStateParameters("ExtGState");
        }

        internal PdfShadingPattern(PdfShading shading, PdfTransformationMatrix matrix) : base(matrix)
        {
            this.shading = shading;
        }

        protected override PdfWriterDictionary GetDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.GetDictionary(objects);
            dictionary.Add("ExtGState", this.graphicsState);
            dictionary.Add("Shading", this.shading);
            return dictionary;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.GetDictionary(objects);

        public PdfShading Shading =>
            this.shading;

        public PdfGraphicsStateParameters GraphicsState =>
            this.graphicsState;

        protected override int PatternType =>
            2;
    }
}

