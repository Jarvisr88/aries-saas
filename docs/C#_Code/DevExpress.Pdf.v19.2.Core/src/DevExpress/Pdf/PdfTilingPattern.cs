namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTilingPattern : PdfPattern
    {
        private const string paintTypeDictionaryKey = "PaintType";
        private const string tilingTypeDictionaryKey = "TilingType";
        private const string boundingBoxDictionaryKey = "BBox";
        private const string xStepDictionaryKey = "XStep";
        private const string yStepDictionaryKey = "YStep";
        private const string resourcesDictionaryKey = "Resources";
        private const int coloredPaintType = 1;
        private const int uncoloredPaintType = 2;
        private readonly bool colored;
        private readonly PdfTilingType tilingType;
        private readonly PdfRectangle boundingBox;
        private readonly double xStep;
        private readonly double yStep;
        private readonly PdfResources resources;
        private byte[] commandsData;
        private PdfCommandList commands;

        internal PdfTilingPattern(PdfReaderStream stream) : base(stream.Dictionary)
        {
            PdfReaderDictionary dictionary = stream.Dictionary;
            int? integer = dictionary.GetInteger("PaintType");
            PdfTilingType? defaultValue = null;
            this.tilingType = PdfEnumToValueConverter.Parse<PdfTilingType>(dictionary.GetInteger("TilingType"), defaultValue);
            this.boundingBox = dictionary.GetRectangle("BBox");
            double? number = dictionary.GetNumber("XStep");
            double? nullable3 = dictionary.GetNumber("YStep");
            this.resources = dictionary.GetResources("Resources", null, false, true);
            if ((integer == null) || ((this.boundingBox == null) || ((number == null) || ((nullable3 == null) || (this.resources == null)))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = integer.Value;
            if (num == 1)
            {
                this.colored = true;
            }
            else if (num != 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            else
            {
                this.colored = false;
            }
            this.xStep = number.Value;
            this.yStep = nullable3.Value;
            if ((this.xStep == 0.0) || (this.yStep == 0.0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.commandsData = stream.UncompressedData;
        }

        internal PdfTilingPattern(PdfTransformationMatrix matrix, PdfRectangle boundingBox, double xStep, double yStep, PdfDocumentCatalog documentCatalog) : this(matrix, boundingBox, xStep, yStep, true, documentCatalog)
        {
        }

        internal PdfTilingPattern(PdfTransformationMatrix matrix, PdfRectangle boundingBox, double xStep, double yStep, bool colored, PdfDocumentCatalog documentCatalog) : base(matrix)
        {
            this.boundingBox = boundingBox;
            this.xStep = xStep;
            this.yStep = yStep;
            this.colored = colored;
            this.tilingType = PdfTilingType.NoDistortion;
            this.resources = new PdfResources(documentCatalog, true);
            this.commands = new PdfCommandList();
        }

        protected override PdfWriterDictionary GetDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.GetDictionary(objects);
            dictionary.Add("PaintType", this.colored ? 1 : 2);
            dictionary.Add("TilingType", PdfEnumToValueConverter.Convert<PdfTilingType>(this.tilingType));
            dictionary.Add("BBox", this.boundingBox.ToWritableObject());
            dictionary.Add("XStep", this.xStep);
            dictionary.Add("YStep", this.yStep);
            dictionary.Add("Resources", this.resources);
            return dictionary;
        }

        internal void ReplaceCommands(byte[] commandsData)
        {
            this.commandsData = commandsData;
            this.commands = null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            PdfWriterStream.CreateCompressedStream(this.GetDictionary(objects), this.commandsData);

        public bool Colored =>
            this.colored;

        public PdfTilingType TilingType =>
            this.tilingType;

        public PdfRectangle BoundingBox =>
            this.boundingBox;

        public double XStep =>
            this.xStep;

        public double YStep =>
            this.yStep;

        public PdfCommandList Commands
        {
            get
            {
                this.commands ??= PdfContentStreamParser.GetContent(this.resources, this.commandsData);
                return this.commands;
            }
        }

        internal PdfResources Resources =>
            this.resources;

        protected override int PatternType =>
            1;
    }
}

