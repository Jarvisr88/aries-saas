namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class PdfDocument
    {
        private const PdfDocumentPermissionFlags defaultFlags = (PdfDocumentPermissionFlags.Accessibility | PdfDocumentPermissionFlags.DataExtraction | PdfDocumentPermissionFlags.DocumentAssembling | PdfDocumentPermissionFlags.FormFilling | PdfDocumentPermissionFlags.HighQualityPrinting | PdfDocumentPermissionFlags.Modifying | PdfDocumentPermissionFlags.ModifyingFormFieldsAndAnnotations | PdfDocumentPermissionFlags.Printing);
        private readonly PdfFileVersion version;
        private readonly PdfDocumentInfo documentInfo;
        private readonly PdfDocumentCatalog documentCatalog;
        private PdfDocumentPermissionFlags permissionFlags;
        private byte[][] id;
        private PdfDocumentWriter writer;

        internal PdfDocument() : this(null)
        {
        }

        internal PdfDocument(PdfCreationOptions creationOptions)
        {
            this.permissionFlags = PdfDocumentPermissionFlags.Accessibility | PdfDocumentPermissionFlags.DataExtraction | PdfDocumentPermissionFlags.DocumentAssembling | PdfDocumentPermissionFlags.FormFilling | PdfDocumentPermissionFlags.HighQualityPrinting | PdfDocumentPermissionFlags.Modifying | PdfDocumentPermissionFlags.ModifyingFormFieldsAndAnnotations | PdfDocumentPermissionFlags.Printing;
            this.CheckOptions(creationOptions, null);
            this.documentInfo = new PdfDocumentInfo();
            this.documentCatalog = new PdfDocumentCatalog(new PdfObjectCollection(null), creationOptions);
            this.UpdateMetadata();
        }

        internal PdfDocument(Stream stream, PdfCreationOptions creationOptions, PdfSignature signature, PdfEncryptionParameters encryptionParameters) : this(stream, creationOptions, signature, encryptionParameters, false)
        {
        }

        internal PdfDocument(PdfFileVersion version, PdfDocumentInfo documentInfo, PdfDocumentCatalog documentCatalog, PdfEncryptionInfo encryptionInfo, byte[][] id)
        {
            this.permissionFlags = PdfDocumentPermissionFlags.Accessibility | PdfDocumentPermissionFlags.DataExtraction | PdfDocumentPermissionFlags.DocumentAssembling | PdfDocumentPermissionFlags.FormFilling | PdfDocumentPermissionFlags.HighQualityPrinting | PdfDocumentPermissionFlags.Modifying | PdfDocumentPermissionFlags.ModifyingFormFieldsAndAnnotations | PdfDocumentPermissionFlags.Printing;
            this.version = version;
            this.documentInfo = documentInfo;
            this.documentCatalog = documentCatalog;
            this.id = id;
            if (encryptionInfo != null)
            {
                this.permissionFlags = encryptionInfo.PermissionFlags;
            }
        }

        internal PdfDocument(Stream stream, PdfCreationOptions creationOptions, PdfSignature signature, PdfEncryptionParameters encryptionParameters, bool useOldVersion)
        {
            this.permissionFlags = PdfDocumentPermissionFlags.Accessibility | PdfDocumentPermissionFlags.DataExtraction | PdfDocumentPermissionFlags.DocumentAssembling | PdfDocumentPermissionFlags.FormFilling | PdfDocumentPermissionFlags.HighQualityPrinting | PdfDocumentPermissionFlags.Modifying | PdfDocumentPermissionFlags.ModifyingFormFieldsAndAnnotations | PdfDocumentPermissionFlags.Printing;
            this.CheckOptions(creationOptions, encryptionParameters);
            PdfFileVersion fileVersion = !useOldVersion ? PdfFileVersion.Pdf_1_7 : ((encryptionParameters != null) ? ((encryptionParameters.Algorithm == PdfEncryptionAlgorithm.AES256) ? PdfFileVersion.Pdf_1_7 : PdfFileVersion.Pdf_1_6) : PdfFileVersion.Pdf_1_4);
            this.writer = new PdfDocumentWriter(new BufferedStream(stream), this, signature, encryptionParameters, fileVersion, null);
            this.documentInfo = new PdfDocumentInfo();
            PdfObjectCollection objects = this.writer.Objects;
            this.documentCatalog = new PdfDocumentCatalog(objects, creationOptions);
            this.UpdatePermissionFlags(objects);
            this.UpdateMetadata();
        }

        internal PdfPage AddPage(PdfRectangle mediaBox) => 
            this.AddPage(mediaBox, null, 0);

        internal PdfPage AddPage(PdfRectangle mediaBox, PdfRectangle cropBox, int rotate) => 
            this.documentCatalog.AddPage(mediaBox, cropBox, rotate);

        internal void Append(PdfDocument document)
        {
            this.documentCatalog.Append(document.documentCatalog);
        }

        internal void AttachFile(PdfFileAttachment attachment)
        {
            PdfCompatibility compatibility = this.documentCatalog.CreationOptions.Compatibility;
            if ((compatibility == PdfCompatibility.PdfA1b) || (compatibility == PdfCompatibility.PdfA2b))
            {
                throw new NotSupportedException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnsupportedFileAttachments));
            }
            this.documentCatalog.AttachFile(attachment);
        }

        private void CheckOptions(PdfCreationOptions creationOptions, PdfEncryptionParameters encryptionParameters)
        {
            if ((creationOptions != null) && (creationOptions.Compatibility != PdfCompatibility.Pdf))
            {
                if (creationOptions.DisableEmbeddingAllFonts || ((creationOptions.NotEmbeddedFontFamilies != null) && (creationOptions.NotEmbeddedFontFamilies.Count != 0)))
                {
                    throw new NotSupportedException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgShouldEmbedFonts));
                }
                if (encryptionParameters != null)
                {
                    throw new NotSupportedException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnsupportedEncryption));
                }
            }
        }

        internal bool DeleteAttachment(PdfFileAttachment attachment) => 
            this.documentCatalog.DeleteAttachment(attachment);

        internal void DeletePage(int pageNumber)
        {
            this.documentCatalog.DeletePage(pageNumber);
        }

        internal void FinalizeDocument()
        {
            if (this.writer != null)
            {
                this.writer.Write();
                this.writer = null;
            }
        }

        internal PdfPage InsertPage(int pageNumber, PdfRectangle mediaBox) => 
            this.InsertPage(pageNumber, mediaBox, null, 0);

        internal PdfPage InsertPage(int pageNumber, PdfRectangle mediaBox, PdfRectangle cropBox, int rotate) => 
            this.documentCatalog.AddPage(pageNumber, mediaBox, cropBox, rotate);

        private void UpdateMetadata()
        {
            this.documentCatalog.Metadata = this.documentInfo.GetMetadata(this.documentCatalog.CreationOptions.Compatibility);
        }

        internal void UpdateObjects(PdfObjectCollection objects)
        {
            this.documentCatalog.Objects = objects;
            this.UpdatePermissionFlags(objects);
        }

        internal void UpdatePermissionFlags(PdfObjectCollection objects)
        {
            PdfEncryptionInfo encryptionInfo = objects.EncryptionInfo;
            this.permissionFlags = (encryptionInfo == null) ? (PdfDocumentPermissionFlags.Accessibility | PdfDocumentPermissionFlags.DataExtraction | PdfDocumentPermissionFlags.DocumentAssembling | PdfDocumentPermissionFlags.FormFilling | PdfDocumentPermissionFlags.HighQualityPrinting | PdfDocumentPermissionFlags.Modifying | PdfDocumentPermissionFlags.ModifyingFormFieldsAndAnnotations | PdfDocumentPermissionFlags.Printing) : encryptionInfo.PermissionFlags;
        }

        internal PdfObjectReference[] Write(PdfObjectCollection objects)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            this.documentInfo.CreationDate = new DateTimeOffset?(now);
            this.documentInfo.ModDate = new DateTimeOffset?(now);
            this.UpdateMetadata();
            return new PdfObjectReference[] { objects.AddObject((PdfObject) this.documentInfo), objects.AddObject((PdfObject) this.documentCatalog) };
        }

        public PdfFileVersion Version =>
            this.version;

        public string Title
        {
            get => 
                this.documentInfo.Title;
            set
            {
                this.documentInfo.Title = value;
                this.UpdateMetadata();
            }
        }

        public string Author
        {
            get => 
                this.documentInfo.Author;
            set
            {
                this.documentInfo.Author = value;
                this.UpdateMetadata();
            }
        }

        public string Subject
        {
            get => 
                this.documentInfo.Subject;
            set
            {
                this.documentInfo.Subject = value;
                this.UpdateMetadata();
            }
        }

        public string Keywords
        {
            get => 
                this.documentInfo.Keywords;
            set
            {
                this.documentInfo.Keywords = value;
                this.UpdateMetadata();
            }
        }

        public string Creator
        {
            get => 
                this.documentInfo.Creator;
            set
            {
                this.documentInfo.Creator = value;
                this.UpdateMetadata();
            }
        }

        public string Producer
        {
            get => 
                this.documentInfo.Producer;
            set
            {
                this.documentInfo.Producer = value;
                this.UpdateMetadata();
            }
        }

        internal string AdditionalMetadata
        {
            get => 
                this.documentInfo.AdditionalMetadata;
            set
            {
                this.documentInfo.AdditionalMetadata = value;
                this.UpdateMetadata();
            }
        }

        public DateTimeOffset? CreationDate =>
            this.documentInfo.CreationDate;

        public DateTimeOffset? ModDate =>
            this.documentInfo.ModDate;

        public DefaultBoolean Trapped
        {
            get => 
                this.documentInfo.Trapped;
            set => 
                this.documentInfo.Trapped = value;
        }

        public IList<PdfPage> Pages =>
            this.documentCatalog.Pages;

        public IDictionary<int, PdfPageLabel> PageLabels =>
            this.documentCatalog.PageLabels;

        public PdfNames Names =>
            this.documentCatalog.Names;

        public IDictionary<string, PdfDestination> Destinations =>
            this.documentCatalog.Destinations;

        public PdfViewerPreferences ViewerPreferences =>
            this.documentCatalog.ViewerPreferences;

        public PdfPageLayout PageLayout
        {
            get => 
                this.documentCatalog.PageLayout;
            set => 
                this.documentCatalog.PageLayout = value;
        }

        public PdfPageMode PageMode
        {
            get => 
                this.documentCatalog.PageMode;
            set => 
                this.documentCatalog.PageMode = value;
        }

        public PdfOutlines Outlines =>
            this.documentCatalog.Outlines;

        public IList<PdfBookmark> Bookmarks
        {
            get => 
                this.documentCatalog.Bookmarks;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Bookmarks", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectBookmarkListValue));
                }
                this.documentCatalog.Bookmarks = value;
            }
        }

        public IList<PdfArticleThread> Threads =>
            this.documentCatalog.Threads;

        public PdfDestination OpenDestination =>
            this.documentCatalog.OpenDestination;

        public PdfAction OpenAction
        {
            get => 
                this.documentCatalog.OpenAction;
            set => 
                this.documentCatalog.OpenAction = value;
        }

        public PdfDocumentActions Actions
        {
            get => 
                this.documentCatalog.Actions;
            set => 
                this.documentCatalog.Actions = value;
        }

        public CultureInfo LanguageCulture
        {
            get => 
                this.documentCatalog.LanguageCulture;
            set => 
                this.documentCatalog.LanguageCulture = value;
        }

        public PdfInteractiveForm AcroForm =>
            this.documentCatalog.AcroForm;

        public PdfMetadata Metadata =>
            this.documentCatalog.Metadata;

        public PdfLogicalStructure LogicalStructure =>
            this.documentCatalog.LogicalStructure;

        public PdfMarkInfo MarkInfo =>
            this.documentCatalog.MarkInfo;

        public IList<PdfOutputIntent> OutputIntents =>
            this.documentCatalog.OutputIntents;

        public IDictionary<string, PdfPieceInfoEntry> PieceInfo =>
            this.documentCatalog.PieceInfo;

        public PdfOptionalContentProperties OptionalContentProperties =>
            this.documentCatalog.OptionalContentProperties;

        public bool NeedsRendering =>
            this.documentCatalog.NeedsRendering;

        public bool AllowPrinting =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.Printing);

        public bool AllowModifying =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.Modifying);

        public bool AllowDataExtraction =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.DataExtraction);

        public bool AllowAnnotationsAndFormsModifying =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.ModifyingFormFieldsAndAnnotations);

        public bool AllowFormsFilling =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.FormFilling);

        public bool AllowAccessibility =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.Accessibility);

        public bool AllowDocumentAssembling =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.DocumentAssembling);

        public bool AllowHighQualityPrinting =>
            this.permissionFlags.HasFlag(PdfDocumentPermissionFlags.HighQualityPrinting);

        public IEnumerable<PdfFileAttachment> FileAttachments =>
            this.documentCatalog.FileAttachments;

        public IDictionary<string, string> CustomProperties =>
            this.documentInfo.CustomProperties;

        internal PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;

        internal byte[][] ID
        {
            get
            {
                if (this.id == null)
                {
                    byte[] buffer = Guid.NewGuid().ToByteArray();
                    this.id = new byte[][] { buffer, buffer };
                }
                return this.id;
            }
        }
    }
}

