namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class PdfDocumentCatalog : PdfObject, IPdfBookmarkParent
    {
        private const string dictionaryType = "Catalog";
        private const string pagesDictionaryKey = "Pages";
        private const string pageLabelsDictionaryKey = "PageLabels";
        private const string destinationDictionaryKey = "Dests";
        private const string viewerPreferencesDictionaryKey = "ViewerPreferences";
        private const string pageLayoutDictionaryKey = "PageLayout";
        private const string pageModeDictionaryKey = "PageMode";
        private const string outlinesDictionaryKey = "Outlines";
        private const string threadsDictionaryKey = "Threads";
        private const string openActionDictionaryKey = "OpenAction";
        private const string additionalActionsDictionaryKey = "AA";
        private const string acroFormDictionaryKey = "AcroForm";
        private const string structTreeRootDictionaryKey = "StructTreeRoot";
        private const string markInfoDictionaryKey = "MarkInfo";
        private const string outputIntentsDictionaryKey = "OutputIntents";
        private const string ocPropertiesDictionaryKey = "OCProperties";
        private const string needsRenderingDictionaryKey = "NeedsRendering";
        private const string legalContentAttestationDictionaryKey = "Legal";
        private const string associatedFilesKey = "AF";
        internal const string NamesDictionaryKey = "Names";
        internal const string PermissionsDictionaryKey = "Perms";
        private static readonly Dictionary<double, string> fileVersions;
        private readonly string version;
        private readonly Dictionary<string, PdfDeveloperExtension> developerExtensions;
        private readonly PdfPageList pages;
        private readonly PdfDeferredSortedDictionary<int, PdfPageLabel> pageLabels;
        private readonly PdfViewerPreferences viewerPreferences;
        private readonly IList<PdfArticleThread> threads;
        private readonly PdfMarkInfo markInfo;
        private readonly IList<PdfOutputIntent> outputIntents;
        private readonly Dictionary<string, PdfPieceInfoEntry> pieceInfo;
        private readonly PdfOptionalContentProperties optionalContentProperties;
        private readonly bool needsRendering;
        private readonly PdfCreationOptions creationOptions;
        private CultureInfo languageCulture;
        private PdfPageLayout pageLayout;
        private PdfDocumentActions actions;
        private PdfPageMode pageMode;
        private PdfFileAttachmentList fileAttachments;
        private PdfLogicalStructure logicalStructure;
        private PdfOutlines outlines;
        private PdfInteractiveForm acroForm;
        private PdfDestination openDestination;
        private PdfAction openAction;
        private PdfObjectCollection objects;
        private int lastObjectNumber;
        private PdfMetadata metadata;
        private PdfNames names;
        private IDictionary<string, PdfDestination> destinations;
        private PdfBookmarkList bookmarks;
        private bool bookmarksChanged;
        private PdfReaderDictionary dictionary;
        private bool ensured;
        private bool ensuredLogicalStructure;
        private bool ensuredOutlines;
        private IList<PdfFileSpecification> associatedFiles;

        static PdfDocumentCatalog()
        {
            Dictionary<double, string> dictionary1 = new Dictionary<double, string>();
            dictionary1.Add(1.0, "1.0");
            dictionary1.Add(1.1, "1.1");
            dictionary1.Add(1.2, "1.2");
            dictionary1.Add(1.3, "1.3");
            dictionary1.Add(1.4, "1.4");
            dictionary1.Add(1.5, "1.5");
            dictionary1.Add(1.6, "1.6");
            dictionary1.Add(1.7, "1.7");
            dictionary1.Add(2.0, "2.0");
            fileVersions = dictionary1;
        }

        public PdfDocumentCatalog(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            object obj2;
            this.dictionary = dictionary;
            this.objects = dictionary.Objects;
            this.objects.DocumentCatalog = this;
            this.creationOptions = new PdfCreationOptions();
            if (dictionary.TryGetValue("Version", out obj2))
            {
                obj2 = this.objects.TryResolve(obj2, null);
                PdfName name = obj2 as PdfName;
                if (name != null)
                {
                    this.version = name.Name;
                }
                else if (obj2 is double)
                {
                    double key = (double) obj2;
                    fileVersions.TryGetValue(key, out this.version);
                }
            }
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Pages");
            if (dictionary2 == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.developerExtensions = PdfDeveloperExtension.Parse(dictionary.GetDictionary("Extensions"));
            while (true)
            {
                object obj3;
                PdfObjectReference objectReference = dictionary2.GetObjectReference("Parent");
                if (objectReference != null)
                {
                    PdfReaderDictionary dictionary7 = this.objects.TryResolve(objectReference, null) as PdfReaderDictionary;
                    if (dictionary7 != null)
                    {
                        dictionary2 = dictionary7;
                        continue;
                    }
                }
                this.pages = new PdfPageList(new PdfPageTreeNode(null, dictionary2), this);
                PdfCreateTreeElementAction<PdfPageLabel> createElement = <>c.<>9__126_0;
                if (<>c.<>9__126_0 == null)
                {
                    PdfCreateTreeElementAction<PdfPageLabel> local1 = <>c.<>9__126_0;
                    createElement = <>c.<>9__126_0 = (o, v) => new PdfPageLabel(o, v);
                }
                this.pageLabels = PdfNumberTreeNode<PdfPageLabel>.Parse(dictionary.GetDictionary("PageLabels"), createElement, true);
                PdfReaderDictionary dictionary3 = dictionary.GetDictionary("ViewerPreferences", null, true);
                if (dictionary3 != null)
                {
                    this.viewerPreferences = new PdfViewerPreferences(dictionary3);
                }
                this.pageLayout = PdfEnumToStringConverter.Parse<PdfPageLayout>(dictionary.GetName("PageLayout"), true);
                this.pageMode = PdfEnumToStringConverter.Parse<PdfPageMode>(dictionary.GetName("PageMode"), true);
                this.threads = PdfArticleThread.Parse(this.objects, dictionary.GetArray("Threads"));
                if (dictionary.TryGetValue("OpenAction", out obj3))
                {
                    obj3 = this.objects.TryResolve(obj3, null);
                    if (obj3 != null)
                    {
                        if (!(obj3 is IList<object>))
                        {
                            this.openAction = dictionary.GetAction("OpenAction");
                        }
                        else
                        {
                            this.openDestination = this.objects.GetDestination(obj3);
                            this.openDestination.ResolveInternalPage();
                        }
                    }
                }
                PdfReaderDictionary dictionary4 = dictionary.GetDictionary("AA");
                if (dictionary4 != null)
                {
                    this.actions = new PdfDocumentActions(dictionary4);
                }
                PdfReaderDictionary dictionary5 = dictionary.GetDictionary("AcroForm");
                if (dictionary5 != null)
                {
                    this.acroForm = new PdfInteractiveForm(dictionary5);
                }
                this.metadata = dictionary.GetMetadata();
                if (dictionary.TryGetValue("MarkInfo", out obj3))
                {
                    obj3 = this.objects.TryResolve(obj3, null);
                    PdfReaderDictionary dictionary8 = obj3 as PdfReaderDictionary;
                    if (dictionary8 != null)
                    {
                        this.markInfo = new PdfMarkInfo(dictionary8);
                    }
                    else
                    {
                        if (!(obj3 as bool))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.markInfo = new PdfMarkInfo((bool) obj3);
                    }
                }
                this.languageCulture = dictionary.GetLanguageCulture();
                this.outputIntents = dictionary.GetArray<PdfOutputIntent>("OutputIntents", delegate (object d) {
                    PdfReaderDictionary dictionary = this.objects.TryResolve(d, null) as PdfReaderDictionary;
                    if (dictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfOutputIntent(dictionary);
                });
                this.pieceInfo = PdfPieceInfoEntry.Parse(dictionary);
                PdfReaderDictionary dictionary6 = dictionary.GetDictionary("OCProperties");
                if (dictionary6 != null)
                {
                    this.optionalContentProperties = new PdfOptionalContentProperties(dictionary6);
                }
                bool? boolean = dictionary.GetBoolean("NeedsRendering");
                this.needsRendering = (boolean != null) ? boolean.GetValueOrDefault() : false;
                this.associatedFiles = dictionary.GetArray<PdfFileSpecification>("AF", new Func<object, PdfFileSpecification>(this.objects.GetFileSpecification));
                return;
            }
        }

        public PdfDocumentCatalog(PdfObjectCollection objects, PdfCreationOptions creationOptions)
        {
            this.objects = objects;
            PdfCreationOptions options1 = creationOptions;
            if (creationOptions == null)
            {
                PdfCreationOptions local1 = creationOptions;
                options1 = new PdfCreationOptions();
            }
            this.creationOptions = options1;
            objects.DocumentCatalog = this;
            this.pages = new PdfPageList(this);
            this.languageCulture = CultureInfo.InvariantCulture;
            this.names = new PdfNames(null);
            this.destinations = this.names.PageDestinations;
            PdfOutputIntent[] intentArray1 = new PdfOutputIntent[] { new PdfOutputIntent() };
            this.outputIntents = intentArray1;
            if (this.creationOptions.RightToLeftLayout)
            {
                this.viewerPreferences = new PdfViewerPreferences();
            }
        }

        public PdfPage AddPage(PdfRectangle mediaBox, PdfRectangle cropBox, int rotate)
        {
            int num = this.objects.LastObjectNumber + 1;
            this.objects.LastObjectNumber = num;
            PdfPage item = new PdfPage(this, mediaBox, cropBox, rotate);
            item.ObjectNumber = num;
            return this.pages.AddNewPage(item);
        }

        public PdfPage AddPage(int pageNumber, PdfRectangle mediaBox, PdfRectangle cropBox, int rotate)
        {
            int index = pageNumber - 1;
            int count = this.pages.Count;
            if ((index < 0) || (index > count))
            {
                throw new ArgumentOutOfRangeException("position", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectInsertingPageNumber));
            }
            if (index == count)
            {
                return this.AddPage(mediaBox, cropBox, rotate);
            }
            int num3 = this.objects.LastObjectNumber + 1;
            this.objects.LastObjectNumber = num3;
            PdfPage item = new PdfPage(this, mediaBox, cropBox, rotate);
            item.ObjectNumber = num3;
            return this.pages.InsertNewPage(index, item);
        }

        public void Append(PdfDocumentCatalog documentCatalog)
        {
            this.pages.AppendDocument(documentCatalog);
        }

        public void AttachFile(PdfFileAttachment fileAttachment)
        {
            this.FileAttachments.Add(fileAttachment);
            this.associatedFiles ??= new List<PdfFileSpecification>();
            this.associatedFiles.Add(fileAttachment.FileSpecification);
        }

        public void CreateSignatureFormField(PdfSignature signature)
        {
            if (this.pages.Count > 0)
            {
                int pageIndex;
                PdfRectangle signatureBounds;
                PdfSignatureAppearance appearance = signature.Appearance;
                if (appearance == null)
                {
                    pageIndex = 0;
                    signatureBounds = new PdfRectangle(0.0, 0.0, 0.0, 0.0);
                }
                else
                {
                    int count = this.pages.Count;
                    pageIndex = appearance.PageIndex;
                    if ((pageIndex < 0) || (pageIndex > (count - 1)))
                    {
                        throw new ArgumentOutOfRangeException("pageNumber", string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectPageNumber), count));
                    }
                    signatureBounds = appearance.SignatureBounds;
                }
                PdfWidgetAnnotationBuilder widgetBuilder = new PdfWidgetAnnotationBuilder(signatureBounds);
                widgetBuilder.Flags = signature.AnnotationFlags;
                PdfWidgetAnnotation widget = new PdfWidgetAnnotation(this.pages[pageIndex], widgetBuilder);
                PdfForm appearanceForm = widget.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal);
                if (appearance != null)
                {
                    appearance.CreateAppearance(appearanceForm);
                }
                PdfInteractiveForm existingOrCreateNewInteractiveForm = this.GetExistingOrCreateNewInteractiveForm();
                PdfSignatureFormField field1 = new PdfSignatureFormField(existingOrCreateNewInteractiveForm, widget, signature);
                existingOrCreateNewInteractiveForm.NeedAppearances = false;
            }
        }

        public bool DeleteAttachment(PdfFileAttachment fileAttachment)
        {
            if (this.associatedFiles != null)
            {
                this.associatedFiles.Remove(fileAttachment.FileSpecification);
            }
            return this.FileAttachments.Delete(fileAttachment);
        }

        public void DeletePage(int pageNumber)
        {
            this.pages.DeletePage(pageNumber);
        }

        void IPdfBookmarkParent.Invalidate()
        {
            this.bookmarksChanged = true;
        }

        private void Ensure()
        {
            if (!this.ensured && (this.dictionary != null))
            {
                PdfReaderDictionary dictionary = this.dictionary.GetDictionary("Dests");
                this.names = new PdfNames(this.dictionary.GetDictionary("Names"));
                if ((dictionary != null) && (dictionary.Count > 0))
                {
                    foreach (KeyValuePair<string, PdfDestination> pair in PdfDestination.Parse(dictionary))
                    {
                        this.names.PageDestinations[pair.Key] = pair.Value;
                    }
                }
                this.destinations = this.names.PageDestinations;
                this.ensured = true;
                this.FlushDictionary();
            }
        }

        private void EnsureOutlines()
        {
            if (!this.ensuredOutlines && (this.dictionary != null))
            {
                PdfReaderDictionary dictionary = this.dictionary.GetDictionary("Outlines");
                if (dictionary != null)
                {
                    this.outlines = new PdfOutlines(dictionary);
                }
                this.ensuredOutlines = true;
                this.FlushDictionary();
            }
        }

        public PdfPage FindPage(object value)
        {
            if (value == null)
            {
                return null;
            }
            PdfObjectReference reference = value as PdfObjectReference;
            if (reference != null)
            {
                return this.objects.GetPage(reference.Number);
            }
            if (!(value is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = (int) value;
            return (((num < 0) || (num >= this.pages.Count)) ? null : this.pages[num]);
        }

        public PdfWidgetAnnotation FindWidget(int widgetObjectNumber)
        {
            PdfWidgetAnnotation resolvedObject = this.objects.GetResolvedObject<PdfWidgetAnnotation>(widgetObjectNumber);
            if (resolvedObject == null)
            {
                using (IEnumerator<PdfPage> enumerator = ((IEnumerable<PdfPage>) this.pages).GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PdfPage current = enumerator.Current;
                        using (IEnumerator<PdfAnnotation> enumerator2 = current.Annotations.GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator2.MoveNext())
                                {
                                    break;
                                }
                                PdfAnnotation annotation2 = enumerator2.Current;
                                resolvedObject = annotation2 as PdfWidgetAnnotation;
                                if ((resolvedObject != null) && (resolvedObject.ObjectNumber == widgetObjectNumber))
                                {
                                    return resolvedObject;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void FlushDictionary()
        {
            if (this.ensured && (this.ensuredOutlines && this.ensuredLogicalStructure))
            {
                this.dictionary = null;
            }
        }

        public PdfInteractiveForm GetExistingOrCreateNewInteractiveForm()
        {
            this.acroForm ??= new PdfInteractiveForm(new PdfReaderDictionary(this.objects, -1, 0));
            return this.acroForm;
        }

        public ISet<string> GetExistingRootFormFieldNames()
        {
            HashSet<string> set = new HashSet<string>();
            if (this.acroForm != null)
            {
                foreach (PdfInteractiveFormField field in this.acroForm.Fields)
                {
                    set.Add(field.Name);
                }
            }
            return set;
        }

        public bool RemoveForm(PdfDocumentStateBase documentState, bool flatten)
        {
            bool flag = this.acroForm != null;
            foreach (PdfPage page in (IEnumerable<PdfPage>) this.pages)
            {
                flag |= page.RemoveAllWidgetAnnotations(documentState, flatten);
            }
            this.acroForm = null;
            return flag;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            this.Ensure();
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("Catalog"));
            dictionary.Add("Pages", this.pages.GetPageNode(objects, true));
            dictionary.AddIfPresent("PageLabels", PdfNumberTreeNode<PdfPageLabel>.Write(objects, this.pageLabels, null));
            if (this.viewerPreferences != null)
            {
                dictionary.Add("ViewerPreferences", this.viewerPreferences.Write());
            }
            dictionary.AddEnumName<PdfPageLayout>("PageLayout", this.pageLayout);
            dictionary.AddEnumName<PdfPageMode>("PageMode", this.pageMode);
            dictionary.Add("Outlines", this.Outlines);
            dictionary.AddList<PdfArticleThread>("Threads", this.threads);
            dictionary.Add("OpenAction", this.openDestination);
            dictionary.Add("OpenAction", this.openAction);
            dictionary.Add("AA", this.actions);
            dictionary.Add("AcroForm", this.acroForm);
            dictionary.Add("Metadata", this.metadata);
            dictionary.Add("StructTreeRoot", this.LogicalStructure);
            if (this.markInfo != null)
            {
                dictionary.Add("MarkInfo", this.markInfo.Write());
            }
            dictionary.AddLanguage(this.languageCulture);
            if ((this.outputIntents != null) && (this.outputIntents.Count > 0))
            {
                dictionary.AddList<PdfOutputIntent>("OutputIntents", this.outputIntents, i => i.Write(objects));
            }
            PdfPieceInfoEntry.WritePieceInfo(dictionary, this.pieceInfo);
            if (this.optionalContentProperties != null)
            {
                dictionary.Add("OCProperties", this.optionalContentProperties.Write(objects));
            }
            dictionary.Add("NeedsRendering", this.needsRendering, false);
            if (this.names != null)
            {
                dictionary.Add("Names", this.names.Write(objects));
            }
            dictionary.AddList<PdfFileSpecification>("AF", this.associatedFiles);
            return dictionary;
        }

        internal static void ValidateCatalog(PdfDocumentCatalog catalog1, PdfDocumentCatalog catalog2, string argumentName)
        {
            if (!ReferenceEquals(catalog1, catalog2))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectAction), argumentName);
            }
        }

        public string Version =>
            this.version;

        public IDictionary<string, PdfDeveloperExtension> DeveloperExtensions =>
            this.developerExtensions;

        public PdfPageList Pages =>
            this.pages;

        public IDictionary<int, PdfPageLabel> PageLabels =>
            this.pageLabels;

        public PdfFileAttachmentList FileAttachments
        {
            get
            {
                this.fileAttachments ??= new PdfFileAttachmentList(this);
                return this.fileAttachments;
            }
        }

        public PdfNames Names
        {
            get
            {
                this.Ensure();
                return this.names;
            }
        }

        public IDictionary<string, PdfDestination> Destinations
        {
            get
            {
                this.Ensure();
                return this.destinations;
            }
        }

        public PdfViewerPreferences ViewerPreferences =>
            this.viewerPreferences;

        public PdfPageLayout PageLayout
        {
            get => 
                this.pageLayout;
            set => 
                this.pageLayout = value;
        }

        public PdfPageMode PageMode
        {
            get => 
                this.pageMode;
            set => 
                this.pageMode = value;
        }

        public PdfOutlines Outlines
        {
            get
            {
                if (this.bookmarksChanged)
                {
                    this.outlines = PdfBookmarkList.CreateOutlines(this.bookmarks);
                    this.bookmarksChanged = false;
                }
                this.EnsureOutlines();
                return this.outlines;
            }
        }

        public IList<PdfBookmark> Bookmarks
        {
            get
            {
                if (this.bookmarks == null)
                {
                    this.EnsureOutlines();
                    this.bookmarks = new PdfBookmarkList(this, this.outlines);
                }
                return this.bookmarks;
            }
            set
            {
                this.bookmarks = new PdfBookmarkList(this, value);
                this.bookmarksChanged = true;
            }
        }

        public IList<PdfArticleThread> Threads =>
            this.threads;

        public PdfDocumentActions Actions
        {
            get => 
                this.actions;
            set
            {
                if (value != null)
                {
                    ValidateCatalog(this, value.DocumentCatalog, "value");
                }
                this.actions = value;
            }
        }

        public PdfMetadata Metadata
        {
            get => 
                this.metadata;
            internal set => 
                this.metadata = value;
        }

        public PdfLogicalStructure LogicalStructure
        {
            get
            {
                if (!this.ensuredLogicalStructure && (this.dictionary != null))
                {
                    object obj2;
                    if (this.dictionary.TryGetValue("StructTreeRoot", out obj2))
                    {
                        PdfReaderDictionary dictionary = this.dictionary.Objects.TryResolve(obj2, null) as PdfReaderDictionary;
                        if (dictionary != null)
                        {
                            this.logicalStructure = new PdfLogicalStructure(dictionary);
                        }
                    }
                    this.ensuredLogicalStructure = true;
                    this.FlushDictionary();
                }
                return this.logicalStructure;
            }
        }

        public PdfMarkInfo MarkInfo =>
            this.markInfo;

        public CultureInfo LanguageCulture
        {
            get => 
                this.languageCulture;
            set
            {
                Guard.ArgumentNotNull(value, "value");
                this.languageCulture = value;
            }
        }

        public IList<PdfOutputIntent> OutputIntents =>
            this.outputIntents;

        public Dictionary<string, PdfPieceInfoEntry> PieceInfo =>
            this.pieceInfo;

        public PdfOptionalContentProperties OptionalContentProperties =>
            this.optionalContentProperties;

        public bool NeedsRendering =>
            this.needsRendering;

        public PdfInteractiveForm AcroForm =>
            this.acroForm;

        public PdfDestination OpenDestination
        {
            get => 
                this.openDestination;
            set => 
                this.openDestination = value;
        }

        public PdfAction OpenAction
        {
            get => 
                this.openAction;
            set
            {
                if (value != null)
                {
                    ValidateCatalog(this, value.DocumentCatalog, "value");
                }
                this.openAction = value;
            }
        }

        public PdfObjectCollection Objects
        {
            get => 
                this.objects;
            set => 
                this.objects = value;
        }

        public int LastObjectNumber
        {
            get => 
                this.lastObjectNumber;
            set => 
                this.lastObjectNumber = Math.Max(this.lastObjectNumber, value);
        }

        public PdfCreationOptions CreationOptions =>
            this.creationOptions;

        PdfDocumentCatalog IPdfBookmarkParent.DocumentCatalog =>
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfDocumentCatalog.<>c <>9 = new PdfDocumentCatalog.<>c();
            public static PdfCreateTreeElementAction<PdfPageLabel> <>9__126_0;

            internal PdfPageLabel <.ctor>b__126_0(PdfObjectCollection o, object v) => 
                new PdfPageLabel(o, v);
        }
    }
}

