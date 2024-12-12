namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfType3Font : PdfSimpleFont
    {
        internal const string Name = "Type3";
        private const string fontBBoxDictionaryKey = "FontBBox";
        private const string fontMatrixDictionaryKey = "FontMatrix";
        private const string charProcsDictionaryKey = "CharProcs";
        private const string resourcesDictionaryKey = "Resources";
        private readonly PdfRectangle fontBBox;
        private readonly PdfTransformationMatrix fontMatrix;
        private readonly Dictionary<string, PdfCommandList> charProcs;
        private readonly Dictionary<string, byte[]> charProcsData;
        private readonly Dictionary<string, PdfType3FontGlyph> glyphs;
        private readonly PdfResources resources;
        private readonly double widthFactor;
        private readonly double heightFactor;

        internal PdfType3Font(int objectNumber, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfSimpleFontEncoding encoding, int firstChar, int lastChar, double[] widths, PdfReaderDictionary dictionary) : base(objectNumber, string.Empty, toUnicode, fontDescriptor, encoding, new int?(firstChar), new int?(lastChar), widths)
        {
            this.charProcs = new Dictionary<string, PdfCommandList>();
            this.charProcsData = new Dictionary<string, byte[]>();
            this.glyphs = new Dictionary<string, PdfType3FontGlyph>();
            this.fontBBox = dictionary.GetRectangle("FontBBox");
            IList<object> array = dictionary.GetArray("FontMatrix");
            if ((this.fontBBox == null) || (array == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.fontMatrix = new PdfTransformationMatrix(array);
            this.widthFactor = this.fontMatrix.Transform(new PdfPoint(1.0, 0.0)).X;
            this.heightFactor = this.fontMatrix.Transform(new PdfPoint(0.0, 1.0)).Y;
            this.resources = dictionary.GetResources("Resources", null, false, false);
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("CharProcs");
            if (dictionary2 != null)
            {
                PdfObjectCollection objects = dictionary2.Objects;
                foreach (KeyValuePair<string, object> pair in dictionary2)
                {
                    object obj2 = objects.TryResolve(pair.Value, null);
                    if (obj2 != null)
                    {
                        PdfReaderStream stream = obj2 as PdfReaderStream;
                        if (stream == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        byte[] uncompressedData = stream.UncompressedData;
                        this.charProcsData.Add(pair.Key, uncompressedData);
                        this.charProcs.Add(pair.Key, PdfType3FontContentStreamParser.ParseGlyph(this.resources, uncompressedData));
                    }
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("FontBBox", this.FontBBox);
            dictionary.Add("FontMatrix", this.fontMatrix.Data);
            dictionary.Add("CharProcs", PdfElementsDictionaryWriter.Write(this.charProcsData, value => objects.AddStream((byte[]) value)));
            if (this.resources != null)
            {
                dictionary.Add("Resources", this.resources);
            }
            return dictionary;
        }

        protected override PdfFontProgramFacade CreateFontProgramFacade() => 
            null;

        protected override PdfFontMetricsMetadata CreateValidatedMetrics()
        {
            double top = 0.0;
            double bottom = 0.0;
            PdfRectangle fontBBox = this.FontBBox;
            if (fontBBox != null)
            {
                top = fontBBox.Top;
                bottom = fontBBox.Bottom;
            }
            foreach (KeyValuePair<string, PdfCommandList> pair in this.CharProcs)
            {
                PdfCommandList list = pair.Value;
                if (list.Count > 0)
                {
                    PdfSetCacheDeviceCommand command = list[0] as PdfSetCacheDeviceCommand;
                    if (command != null)
                    {
                        PdfRectangle boundingBox = command.BoundingBox;
                        top = Math.Max(top, boundingBox.Top);
                        bottom = Math.Min(bottom, boundingBox.Bottom);
                    }
                    if ((top == 0.0) && (bottom == 0.0))
                    {
                        foreach (PdfCommand command2 in list)
                        {
                            PdfModifyTransformationMatrixCommand command3 = command2 as PdfModifyTransformationMatrixCommand;
                            if (command3 != null)
                            {
                                top = Math.Max(top, command3.Matrix.D);
                                break;
                            }
                        }
                    }
                }
            }
            return new PdfFontMetricsMetadata(top, bottom, 1.0 / this.WidthFactor);
        }

        internal PdfType3FontGlyph GetGlyph(string name)
        {
            PdfType3FontGlyph glyph;
            if (!this.glyphs.TryGetValue(name, out glyph))
            {
                PdfCommandList list;
                if (!this.charProcs.TryGetValue(name, out list))
                {
                    return null;
                }
                glyph = new PdfType3FontGlyph(list);
                this.glyphs.Add(name, glyph);
            }
            return glyph;
        }

        public PdfRectangle FontBBox =>
            this.fontBBox;

        public PdfTransformationMatrix FontMatrix =>
            this.fontMatrix;

        public IDictionary<string, PdfCommandList> CharProcs =>
            this.charProcs;

        protected internal override double WidthFactor =>
            this.widthFactor;

        protected internal override double HeightFactor =>
            this.heightFactor;

        protected internal override bool HasSizeAttributes =>
            false;

        protected internal override string Subtype =>
            "Type3";
    }
}

