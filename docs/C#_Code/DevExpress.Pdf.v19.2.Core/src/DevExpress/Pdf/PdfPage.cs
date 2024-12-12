namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfPage : PdfPageTreeObject
    {
        internal const string PageTreeNodeType = "Page";
        private const string lastModifiedDictionaryKey = "LastModified";
        private const string bleedBoxDictionaryKey = "BleedBox";
        private const string trimBoxDictionaryKey = "TrimBox";
        private const string artBoxDictionaryKey = "ArtBox";
        private const string contentsDictionaryKey = "Contents";
        private const string groupDictionaryKey = "Group";
        private const string thumbDictionaryKey = "Thumb";
        private const string articleBeadsDictionaryKey = "B";
        private const string displayDurationDictionaryKey = "Dur";
        private const string transDictionaryKey = "Trans";
        private const string annotsDictionaryKey = "Annots";
        private const string additionalActionsDictionaryKey = "AA";
        private const string structParentsDictionaryKey = "StructParents";
        private const string idDictionaryKey = "ID";
        private const string preferredZoomDictionaryKey = "PZ";
        private const string tabsDictionaryKey = "Tabs";
        private const string userUnitDictionaryKey = "UserUnit";
        private const byte space = 0x20;
        private readonly double userUnit;
        private readonly List<PdfAnnotation> annotations;
        private readonly PdfPageActions actions;
        private PdfRectangle bleedBox;
        private PdfRectangle trimBox;
        private PdfRectangle artBox;
        private DateTimeOffset? lastModified;
        private PdfTransparencyGroup transparencyGroup;
        private PdfImage thumbnailImage;
        private IList<int> articleBeadsObjectNumbers;
        private PdfBead[] articleBeads;
        private double displayDuration;
        private PdfPagePresentation trans;
        private PdfMetadata metadata;
        private int? structParents;
        private Dictionary<string, PdfPieceInfoEntry> pieceInfo;
        private byte[] id;
        private double? preferredZoomFactor;
        private PdfAnnotationTabOrder annotationTabOrder;
        private IList<PdfArrayCompressedData> contents;
        private PdfObjectReference contentsReference;
        private WeakReference commands;
        private PdfReaderDictionary dictionary;
        private bool ensuredAnnotations;
        private bool ensured;

        internal PdfPage(PdfPageTreeNode parent, PdfReaderDictionary dictionary) : base(parent, dictionary)
        {
            this.userUnit = 1.0;
            this.annotations = new List<PdfAnnotation>();
            this.displayDuration = -1.0;
            this.contents = new List<PdfArrayCompressedData>();
            this.dictionary = dictionary;
            if (base.Resources == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            base.MediaBox ??= PdfPaperSize.Letter;
            double? number = dictionary.GetNumber("UserUnit");
            this.userUnit = (number != null) ? number.GetValueOrDefault() : 1.0;
            this.bleedBox = dictionary.GetRectangle("BleedBox");
            this.trimBox = dictionary.GetRectangle("TrimBox");
            this.artBox = dictionary.GetRectangle("ArtBox");
            PdfObjectCollection objects = dictionary.Objects;
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("AA");
            if (dictionary2 != null)
            {
                this.actions = new PdfPageActions(dictionary2);
            }
            IList<object> array = dictionary.GetArray("B");
            if (array != null)
            {
                this.articleBeadsObjectNumbers = new List<int>();
                foreach (object obj2 in array)
                {
                    if (obj2 == null)
                    {
                        this.articleBeadsObjectNumbers.Add(-1);
                        continue;
                    }
                    PdfObjectReference reference = obj2 as PdfObjectReference;
                    if (reference == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                        continue;
                    }
                    this.articleBeadsObjectNumbers.Add(reference.Number);
                }
            }
            this.UpdateBoxes();
        }

        internal PdfPage(PdfDocumentCatalog documentCatalog, PdfRectangle mediaBox, PdfRectangle cropBox, int rotate) : base(documentCatalog, mediaBox, cropBox, rotate)
        {
            this.userUnit = 1.0;
            this.annotations = new List<PdfAnnotation>();
            this.displayDuration = -1.0;
            this.contents = new List<PdfArrayCompressedData>();
            base.Parent = documentCatalog.Pages.GetPageNode(documentCatalog.Objects, false);
        }

        private void AddCommands(byte[] commandsData, int index)
        {
            this.contents.Insert(index, new PdfArrayCompressedData(commandsData));
            this.commands = null;
        }

        internal void AddCommandsToEnd(byte[] commands)
        {
            this.EnsurePage();
            this.AddCommands(commands, this.contents.Count);
        }

        private PdfWritableConvertibleArray<PdfArrayCompressedData> CreateContentsArray(PdfObjectCollection objects) => 
            new PdfWritableConvertibleArray<PdfArrayCompressedData>(this.contents, compressedData => objects.AddStream(compressedData.CreateWriterStream(new PdfWriterDictionary(objects))));

        internal void EnsureAnnotations()
        {
            if (!this.ensuredAnnotations && (this.dictionary != null))
            {
                this.ensuredAnnotations = true;
                IList<object> array = this.dictionary.GetArray("Annots", true);
                if (array != null)
                {
                    foreach (object obj2 in array)
                    {
                        try
                        {
                            PdfAnnotation item = this.dictionary.Objects.GetAnnotation(this, obj2);
                            if (item != null)
                            {
                                this.annotations.Add(item);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                int num = 0;
                while (true)
                {
                    if (num >= this.annotations.Count)
                    {
                        if (this.ensured)
                        {
                            this.dictionary = null;
                        }
                        break;
                    }
                    try
                    {
                        this.annotations[num].Ensure();
                    }
                    catch
                    {
                    }
                    num++;
                }
            }
        }

        private void EnsurePage()
        {
            if (!this.ensured && (this.dictionary != null))
            {
                object obj2;
                this.ensured = true;
                PdfObjectCollection objects = this.dictionary.Objects;
                this.lastModified = this.dictionary.GetDate("LastModified");
                if (this.dictionary.TryGetValue("Contents", out obj2))
                {
                    obj2 = objects.TryResolve(obj2, null);
                    PdfReaderStream stream = obj2 as PdfReaderStream;
                    if (stream != null)
                    {
                        this.contents.Add(new PdfArrayCompressedData(stream));
                    }
                    else
                    {
                        IList<object> list = obj2 as IList<object>;
                        if (list == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        foreach (object obj3 in list)
                        {
                            stream = objects.TryResolve(obj3, null) as PdfReaderStream;
                            if (stream == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            this.contents.Add(new PdfArrayCompressedData(stream));
                        }
                    }
                }
                PdfReaderDictionary dictionary = this.dictionary.GetDictionary("Group");
                if (dictionary != null)
                {
                    this.transparencyGroup = new PdfTransparencyGroup(dictionary);
                }
                if (this.dictionary.TryGetValue("Thumb", out obj2))
                {
                    try
                    {
                        this.thumbnailImage = objects.GetXObject(obj2, null, "Image") as PdfImage;
                    }
                    catch
                    {
                    }
                }
                double? number = this.dictionary.GetNumber("Dur");
                this.displayDuration = (number != null) ? number.GetValueOrDefault() : -1.0;
                PdfReaderDictionary dictionary2 = this.dictionary.GetDictionary("Trans");
                if (dictionary2 != null)
                {
                    this.trans = new PdfPagePresentation(dictionary2);
                }
                this.metadata = this.dictionary.GetMetadata();
                this.pieceInfo = PdfPieceInfoEntry.Parse(this.dictionary);
                this.structParents = this.dictionary.GetInteger("StructParents");
                this.id = this.dictionary.GetBytes("ID");
                this.preferredZoomFactor = this.dictionary.GetNumber("PZ");
                this.annotationTabOrder = PdfEnumToStringConverter.Parse<PdfAnnotationTabOrder>(this.dictionary.GetName("Tabs"), true);
                if (this.ensuredAnnotations)
                {
                    this.dictionary = null;
                }
            }
        }

        protected internal override void FlushPageData(PdfObjectCollection objects)
        {
            base.FlushPageData(objects);
            int count = this.contents.Count;
            if (count > 1)
            {
                int nextObjectNumber = objects.GetNextObjectNumber();
                objects.AddItem(new PdfObjectContainer(nextObjectNumber, 0, this.CreateContentsArray(objects)), false);
                this.contentsReference = new PdfObjectReference(nextObjectNumber);
            }
            else if (count == 1)
            {
                this.contentsReference = objects.AddStream(this.contents[0].CreateWriterStream(new PdfWriterDictionary(objects)));
            }
            this.contents = null;
        }

        internal PdfPoint FromUserSpace(PdfPoint point, double factor, int angle)
        {
            PdfRectangle cropBox = base.CropBox;
            int num = NormalizeRotate(angle + base.Rotate);
            return ((num == 90) ? new PdfPoint(point.Y / factor, point.X / factor) : ((num == 180) ? new PdfPoint(cropBox.Width - (point.X / factor), point.Y / factor) : ((num == 270) ? new PdfPoint(cropBox.Width - (point.Y / factor), cropBox.Height - (point.X / factor)) : new PdfPoint(point.X / factor, cropBox.Height - (point.Y / factor)))));
        }

        internal byte[] GetCommandsData()
        {
            this.EnsurePage();
            List<byte> list = new List<byte>();
            foreach (PdfArrayCompressedData data in this.contents)
            {
                list.AddRange(data.UncompressedData);
                list.Add(0x20);
            }
            return list.ToArray();
        }

        internal PdfSize GetSize(int rotationAngle)
        {
            double width;
            double height;
            PdfRectangle cropBox = base.CropBox;
            int num3 = NormalizeRotate(base.Rotate + rotationAngle);
            if ((num3 != 90) && (num3 != 270))
            {
                width = cropBox.Width;
                height = cropBox.Height;
            }
            else
            {
                width = cropBox.Height;
                height = cropBox.Width;
            }
            return new PdfSize(Math.Max((double) 1.0, (double) (width * this.userUnit)), Math.Max((double) 1.0, (double) (height * this.userUnit)));
        }

        internal PdfPoint GetTopLeftCorner(int rotationAngle)
        {
            PdfRectangle cropBox = base.CropBox;
            int num = NormalizeRotate(rotationAngle + base.Rotate);
            return ((num == 90) ? new PdfPoint(cropBox.Left, cropBox.Bottom) : ((num == 180) ? cropBox.BottomRight : ((num == 270) ? new PdfPoint(cropBox.Right, cropBox.Top) : cropBox.TopLeft)));
        }

        internal void InsertCommandsAtBegin(byte[] commands)
        {
            this.EnsurePage();
            this.AddCommands(commands, 0);
        }

        internal bool RemoveAllWidgetAnnotations(PdfDocumentStateBase documentState, bool flatten)
        {
            this.EnsureAnnotations();
            return this.RemoveWidgetAnnotations(documentState, this.annotations, flatten);
        }

        internal bool RemoveWidgetAnnotations(PdfDocumentStateBase documentState, ICollection<PdfAnnotation> annotationCollection, bool flatten)
        {
            using (PdfWidgetAnnotationRemover remover = flatten ? new PdfWidgetAnnotationFlattener(this, documentState) : new PdfWidgetAnnotationRemover(this))
            {
                return remover.RemoveWidgets(annotationCollection);
            }
        }

        internal void ReplaceCommands(byte[] commands)
        {
            this.EnsurePage();
            this.contents.Clear();
            this.AddCommands(commands, 0);
        }

        internal PdfPoint ToUserSpace(PdfPoint point, double factor, int angle)
        {
            PdfRectangle cropBox = base.CropBox;
            int num = NormalizeRotate(angle + base.Rotate);
            return ((num == 90) ? new PdfPoint(point.Y * factor, point.X * factor) : ((num == 180) ? new PdfPoint((cropBox.Width - point.X) * factor, point.Y * factor) : ((num == 270) ? new PdfPoint((cropBox.Height - point.Y) * factor, (cropBox.Width - point.X) * factor) : new PdfPoint(point.X * factor, (cropBox.Height - point.Y) * factor))));
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            this.EnsurePage();
            this.EnsureAnnotations();
            object obj2 = base.ToWritableObject(objects);
            PdfWriterDictionary dictionary = obj2 as PdfWriterDictionary;
            if (dictionary != null)
            {
                dictionary.Add("Type", new PdfName("Page"));
                dictionary.AddNullable<DateTimeOffset>("LastModified", this.lastModified);
                PdfRectangle cropBox = base.CropBox;
                dictionary.Add("BleedBox", this.BleedBox, cropBox);
                dictionary.Add("TrimBox", this.TrimBox, cropBox);
                dictionary.Add("ArtBox", this.ArtBox, cropBox);
                if (this.contents == null)
                {
                    dictionary.Add("Contents", this.contentsReference);
                }
                else
                {
                    int count = this.contents.Count;
                    if (count > 1)
                    {
                        dictionary.Add("Contents", this.CreateContentsArray(objects));
                    }
                    else if (count == 1)
                    {
                        dictionary.Add("Contents", this.contents[0].CreateWriterStream(new PdfWriterDictionary(objects)));
                    }
                }
                dictionary.Add("Group", this.transparencyGroup);
                dictionary.Add("Thumb", this.thumbnailImage);
                dictionary.Add("Dur", this.displayDuration, -1.0);
                if (this.trans != null)
                {
                    dictionary.Add("Trans", this.trans.Write());
                }
                dictionary.AddList<PdfAnnotation>("Annots", this.annotations);
                PdfPieceInfoEntry.WritePieceInfo(dictionary, this.pieceInfo);
                if (this.actions != null)
                {
                    PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
                    dictionary2.Add("O", this.Actions.Opened);
                    dictionary2.Add("C", this.Actions.Closed);
                    dictionary.Add("AA", dictionary2);
                }
                dictionary.AddNullable<int>("StructParents", this.structParents);
                dictionary.Add("ID", this.id, null);
                dictionary.AddNullable<double>("PZ", this.preferredZoomFactor);
                if (this.annotationTabOrder != PdfAnnotationTabOrder.RowOrder)
                {
                    dictionary.AddEnumName<PdfAnnotationTabOrder>("Tabs", this.annotationTabOrder);
                }
                dictionary.Add("UserUnit", this.userUnit, 1.0);
                dictionary.AddList<PdfBead>("B", this.ArticleBeads);
                dictionary.Add("Metadata", this.metadata);
            }
            return obj2;
        }

        protected override void UpdateBoxes()
        {
            base.UpdateBoxes();
            PdfRectangle mediaBox = base.MediaBox;
            if (mediaBox != null)
            {
                PdfRectangle bleedBox = this.BleedBox;
                if (!mediaBox.Contains(bleedBox))
                {
                    this.BleedBox = mediaBox.Trim(bleedBox);
                }
                PdfRectangle trimBox = this.TrimBox;
                if (!mediaBox.Contains(trimBox))
                {
                    this.TrimBox = mediaBox.Trim(trimBox);
                }
                PdfRectangle artBox = this.ArtBox;
                if (!mediaBox.Contains(artBox))
                {
                    this.ArtBox = mediaBox.Trim(artBox);
                }
            }
        }

        public double UserUnit =>
            this.userUnit;

        public IList<PdfAnnotation> Annotations
        {
            get
            {
                this.EnsureAnnotations();
                return this.annotations;
            }
        }

        public PdfPageActions Actions =>
            this.actions;

        public PdfRectangle BleedBox
        {
            get => 
                this.bleedBox ?? base.CropBox;
            set
            {
                base.CheckBox(value, PdfCoreStringId.MsgIncorrectPageBleedBox);
                this.bleedBox = value;
            }
        }

        public PdfRectangle TrimBox
        {
            get => 
                this.trimBox ?? base.CropBox;
            set
            {
                base.CheckBox(value, PdfCoreStringId.MsgIncorrectPageTrimBox);
                this.trimBox = value;
            }
        }

        public PdfRectangle ArtBox
        {
            get => 
                this.artBox ?? base.CropBox;
            set
            {
                base.CheckBox(value, PdfCoreStringId.MsgIncorrectPageArtBox);
                this.artBox = value;
            }
        }

        public DateTimeOffset? LastModified
        {
            get
            {
                this.EnsurePage();
                return this.lastModified;
            }
            set
            {
                this.EnsurePage();
                this.lastModified = value;
            }
        }

        public PdfCommandList Commands
        {
            get
            {
                if (this.commands != null)
                {
                    object target = this.commands.Target;
                    if (this.commands.IsAlive)
                    {
                        return (PdfCommandList) target;
                    }
                }
                PdfCommandList content = PdfContentStreamParser.GetContent(base.Resources, this.GetCommandsData());
                this.commands = new WeakReference(content);
                return content;
            }
        }

        public PdfTransparencyGroup TransparencyGroup
        {
            get
            {
                this.EnsurePage();
                return this.transparencyGroup;
            }
        }

        public PdfImage ThumbnailImage
        {
            get
            {
                this.EnsurePage();
                return this.thumbnailImage;
            }
        }

        public IList<PdfBead> ArticleBeads
        {
            get
            {
                this.EnsurePage();
                if ((this.articleBeads == null) && (this.articleBeadsObjectNumbers != null))
                {
                    int count = this.articleBeadsObjectNumbers.Count;
                    if (count > 0)
                    {
                        this.articleBeads = new PdfBead[count];
                        PdfObjectCollection objects = base.DocumentCatalog.Objects;
                        for (int i = 0; i < count; i++)
                        {
                            PdfBead bead = this.articleBeads[i] = objects.GetResolvedObject<PdfBead>(this.articleBeadsObjectNumbers[i]);
                            if ((bead != null) && !ReferenceEquals(bead.Page, this))
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                        }
                    }
                    this.articleBeadsObjectNumbers = null;
                }
                return this.articleBeads;
            }
        }

        public double DisplayDuration
        {
            get
            {
                this.EnsurePage();
                return this.displayDuration;
            }
        }

        public PdfPagePresentation Trans
        {
            get
            {
                this.EnsurePage();
                return this.trans;
            }
        }

        public PdfMetadata Metadata
        {
            get
            {
                this.EnsurePage();
                return this.metadata;
            }
        }

        public int? StructParents
        {
            get
            {
                this.EnsurePage();
                return this.structParents;
            }
        }

        public byte[] ID
        {
            get
            {
                this.EnsurePage();
                return this.id;
            }
        }

        public double? PreferredZoomFactor
        {
            get
            {
                this.EnsurePage();
                return this.preferredZoomFactor;
            }
        }

        public PdfAnnotationTabOrder AnnotationTabOrder
        {
            get
            {
                this.EnsurePage();
                return this.annotationTabOrder;
            }
        }

        public IDictionary<string, PdfPieceInfoEntry> PieceInfo
        {
            get
            {
                this.EnsurePage();
                return this.pieceInfo;
            }
        }
    }
}

