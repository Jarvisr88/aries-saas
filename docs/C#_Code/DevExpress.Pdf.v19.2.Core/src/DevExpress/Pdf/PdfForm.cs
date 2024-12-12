namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfForm : PdfXObject
    {
        internal const string Type = "Form";
        private const string bBoxDictionaryKey = "BBox";
        private const string matrixDictionaryKey = "Matrix";
        private const string resourceDictionaryString = "Resources";
        private const string lastModifiedDictionaryKey = "LastModified";
        private const string structParentsDictionaryKey = "StructParents";
        private readonly Dictionary<string, PdfPieceInfoEntry> pieceInfo;
        private readonly DateTimeOffset? lastModified;
        private readonly int? structParents;
        private readonly PdfDocumentCatalog documentCatalog;
        private PdfArrayCompressedData commandsData;
        private PdfCommandList commands;
        private PdfRectangle bBox;
        private PdfTransformationMatrix matrix;
        private PdfResources resources;

        internal PdfForm(PdfForm form)
        {
            this.bBox = form.bBox;
            this.resources = form.resources;
            this.pieceInfo = form.pieceInfo;
            this.commandsData = form.commandsData;
            this.commands = new PdfCommandList(form.Commands);
            this.matrix = form.matrix;
            this.documentCatalog = form.documentCatalog;
        }

        internal PdfForm(PdfDocumentCatalog documentCatalog, PdfRectangle bBox)
        {
            if (bBox == null)
            {
                throw new ArgumentNullException("bBox");
            }
            this.documentCatalog = documentCatalog;
            this.bBox = bBox;
            this.resources = new PdfResources(documentCatalog, true);
            this.matrix = new PdfTransformationMatrix();
            this.commandsData = new PdfArrayCompressedData(new byte[0]);
        }

        internal PdfForm(PdfReaderStream stream, PdfResources parentResources) : base(stream.Dictionary)
        {
            this.commandsData = new PdfArrayCompressedData(stream);
            PdfReaderDictionary dictionary = stream.Dictionary;
            int? integer = dictionary.GetInteger("FormType");
            if ((integer != null) && (integer.Value != 1))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.resources = dictionary.GetActualResources("Resources", parentResources, false, true);
            this.resources ??= new PdfResources(dictionary.Objects.DocumentCatalog, parentResources, false, false, null);
            this.documentCatalog = dictionary.Objects.DocumentCatalog;
            this.pieceInfo = PdfPieceInfoEntry.Parse(dictionary);
            this.lastModified = dictionary.GetDate("LastModified");
            this.structParents = dictionary.GetInteger("StructParents");
            PdfRectangle rectangle = dictionary.GetRectangle("BBox");
            PdfForm form1 = this;
            if (rectangle == null)
            {
                form1 = (PdfForm) new PdfRectangle(0.0, 0.0, 0.0, 0.0);
            }
            rectangle.bBox = (PdfRectangle) form1;
            this.matrix = new PdfTransformationMatrix(dictionary.GetArray("Matrix"));
        }

        internal static PdfForm Create(PdfReaderStream stream, PdfResources parentResources)
        {
            PdfReaderDictionary groupDictionary = stream.Dictionary.GetDictionary("Group");
            return ((groupDictionary == null) ? new PdfForm(stream, parentResources) : new PdfGroupForm(stream, groupDictionary, parentResources));
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.AddName("Subtype", "Form");
            dictionary.Add("BBox", this.bBox);
            if (!this.matrix.IsDefault)
            {
                dictionary.Add("Matrix", this.matrix.Data);
            }
            dictionary.Add("Resources", this.resources);
            PdfPieceInfoEntry.WritePieceInfo(dictionary, this.pieceInfo);
            dictionary.AddNullable<DateTimeOffset>("LastModified", this.lastModified);
            dictionary.AddNullable<int>("StructParents", this.structParents);
            return dictionary;
        }

        internal PdfResources CreateEmptyResources()
        {
            this.resources = new PdfResources(this.documentCatalog, true);
            return this.resources;
        }

        protected override PdfStream CreateStream(PdfObjectCollection objects) => 
            this.commandsData.CreateWriterStream(this.CreateDictionary(objects));

        internal PdfTransformationMatrix GetTrasformationMatrix(PdfRectangle annotationRect)
        {
            PdfRectangle bBox = this.BBox;
            double left = bBox.Left;
            double right = bBox.Right;
            double top = bBox.Top;
            double bottom = bBox.Bottom;
            PdfPoint point = this.matrix.Transform(new PdfPoint(left, bottom));
            double x = point.X;
            double num6 = point.X;
            double y = point.Y;
            double num8 = point.Y;
            PdfPoint[] pointArray1 = new PdfPoint[] { this.matrix.Transform(new PdfPoint(left, top)), this.matrix.Transform(new PdfPoint(right, top)), this.matrix.Transform(new PdfPoint(right, bottom)) };
            foreach (PdfPoint point2 in pointArray1)
            {
                double num14 = point2.X;
                x = PdfMathUtils.Min(x, num14);
                num6 = PdfMathUtils.Max(num6, num14);
                double num15 = point2.Y;
                y = PdfMathUtils.Min(y, num15);
                num8 = PdfMathUtils.Max(num8, num15);
            }
            double num9 = num6 - x;
            double num10 = num8 - y;
            if (num9 == 0.0)
            {
                num9 = 1.0;
            }
            if (num10 == 0.0)
            {
                num10 = 1.0;
            }
            double a = annotationRect.Width / num9;
            double d = annotationRect.Height / num10;
            return new PdfTransformationMatrix(a, 0.0, 0.0, d, annotationRect.Left - (x * a), annotationRect.Bottom - (y * d));
        }

        internal void ReplaceCommands(byte[] data)
        {
            this.commandsData = new PdfArrayCompressedData(data);
            this.commands = null;
        }

        public Dictionary<string, PdfPieceInfoEntry> PieceInfo =>
            this.pieceInfo;

        public DateTimeOffset? LastModified =>
            this.lastModified;

        public int? StructParents =>
            this.structParents;

        public PdfCommandList Commands
        {
            get
            {
                this.commands ??= ((this.commandsData == null) ? new PdfCommandList() : PdfContentStreamParser.GetContent(this.resources, this.commandsData.UncompressedData));
                return this.commands;
            }
        }

        public PdfRectangle BBox
        {
            get => 
                this.bBox;
            internal set => 
                this.bBox = value;
        }

        public PdfTransformationMatrix Matrix
        {
            get => 
                this.matrix;
            internal set => 
                this.matrix = value;
        }

        internal PdfResources Resources =>
            this.resources;

        internal PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;
    }
}

