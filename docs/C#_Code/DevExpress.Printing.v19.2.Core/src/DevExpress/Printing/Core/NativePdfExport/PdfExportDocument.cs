namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Data.Extensions;
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;

    public class PdfExportDocument : PdfDisposableObject
    {
        private const string bookmarkDestinationPrefix = "bm";
        private const string defaultAcroFormFieldName = "AcroFormField";
        private const string radioGroupAcroFormFieldName = "RadioGroupAcroFormField";
        private const string allFontsSymbol = "*";
        private readonly PdfGraphicsDocument graphicsDocument;
        private readonly List<Destination> destinations;
        private readonly PdfSignature signature;
        private PdfDocument document;
        private PdfPage currentPage;
        private string pageRange;
        private int rasterizationResolution;
        private bool exportEditingFieldsToAcroForms;
        private bool supportsTransparency;
        private int destinationNameIndex;
        private Dictionary<string, int> acroFormFieldNames;

        public PdfExportDocument(Stream stream, PdfCreationOptions creationOptions, PdfSaveOptions saveOptions)
        {
            this.destinations = new List<Destination>();
            this.rasterizationResolution = 0x60;
            this.exportEditingFieldsToAcroForms = true;
            this.acroFormFieldNames = new Dictionary<string, int>();
            PdfEncryptionOptions encryptionOptions = saveOptions.EncryptionOptions;
            this.signature = saveOptions.Signature;
            this.document = new PdfDocument(stream, creationOptions, this.signature, encryptionOptions?.EncryptionParameters);
            this.graphicsDocument = new PdfGraphicsDocument(this.document.DocumentCatalog, 0);
        }

        public PdfExportDocument(Stream stream, PdfExportOptions exportOptions, bool rightToLeftLayout)
        {
            this.destinations = new List<Destination>();
            this.rasterizationResolution = 0x60;
            this.exportEditingFieldsToAcroForms = true;
            this.acroFormFieldNames = new Dictionary<string, int>();
            if (!stream.CanSeek || !stream.CanWrite)
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnsupportedStream), "stream");
            }
            PdfSignatureOptions signatureOptions = exportOptions.SignatureOptions;
            if ((signatureOptions == null) || (signatureOptions.Certificate == null))
            {
                this.signature = null;
            }
            else
            {
                this.signature = new PdfSignature(signatureOptions.Certificate);
                this.signature.ContactInfo = signatureOptions.ContactInfo;
                this.signature.Location = signatureOptions.Location;
                this.signature.Reason = signatureOptions.Reason;
            }
            this.document = new PdfDocument(stream, GetCreationOptions(exportOptions, rightToLeftLayout), this.signature, CreateEncryptionParameters(exportOptions.PasswordSecurityOptions), true);
            this.pageRange = exportOptions.PageRange;
            this.rasterizationResolution = exportOptions.RasterizationResolution;
            this.exportEditingFieldsToAcroForms = exportOptions.ExportEditingFieldsToAcroForms;
            this.graphicsDocument = new PdfGraphicsDocument(this.document.DocumentCatalog, this.rasterizationResolution);
            PdfXObjectResourceCache imageCache = this.graphicsDocument.ImageCache;
            imageCache.ConvertImagesToJpeg = exportOptions.ConvertImagesToJpeg;
            imageCache.JpegQuality = (long) exportOptions.ImageQuality;
            this.document.AdditionalMetadata = exportOptions.AdditionalMetadata;
            this.AddAttachments(exportOptions.Attachments);
            PdfDocumentOptions documentOptions = exportOptions.DocumentOptions;
            if (documentOptions != null)
            {
                this.document.Author = documentOptions.Author;
                this.document.Keywords = documentOptions.Keywords;
                this.document.Subject = documentOptions.Subject;
                this.document.Title = documentOptions.Title;
                this.document.Producer = documentOptions.ActualProducer;
                this.document.Creator = documentOptions.Application;
            }
            if (exportOptions.ShowPrintDialogOnOpen)
            {
                this.AddPrintOnOpenJS();
            }
            this.supportsTransparency = exportOptions.PdfACompatibility != PdfACompatibility.PdfA1b;
        }

        private void AddAttachments(IEnumerable<PdfAttachment> attachments)
        {
            if (attachments != null)
            {
                foreach (PdfAttachment attachment in attachments)
                {
                    DateTimeOffset? nullable2;
                    DateTimeOffset? nullable1;
                    DateTimeOffset? nullable3;
                    PdfFileAttachment attachment1 = new PdfFileAttachment();
                    attachment1.Data = GetAttachmentData(attachment);
                    DateTime? creationDate = attachment.CreationDate;
                    PdfFileAttachment attachment2 = attachment1;
                    if (creationDate != null)
                    {
                        nullable1 = new DateTimeOffset?(creationDate.GetValueOrDefault());
                    }
                    else
                    {
                        nullable2 = null;
                        nullable1 = nullable2;
                    }
                    attachment1.CreationDate = nullable1;
                    PdfFileAttachment local1 = attachment1;
                    local1.Description = attachment.Description;
                    local1.FileName = GetAttachmentFileName(attachment);
                    local1.Relationship = ConvertRelationship(attachment.Relationship);
                    local1.MimeType = attachment.Type;
                    creationDate = attachment.ModificationDate;
                    PdfFileAttachment attachment3 = local1;
                    if (creationDate != null)
                    {
                        nullable3 = new DateTimeOffset?(creationDate.GetValueOrDefault());
                    }
                    else
                    {
                        nullable2 = null;
                        nullable3 = nullable2;
                    }
                    local1.ModificationDate = nullable3;
                    this.document.AttachFile(local1);
                }
            }
        }

        private PdfDestinationObject AddDestination(int pageIndex, double top) => 
            this.AddDestination(this.GetNewDestinationName(), pageIndex, top);

        public PdfDestinationObject AddDestination(string destinationName, int pageIndex, double top)
        {
            this.destinations.Add(new Destination(destinationName, pageIndex, top));
            return new PdfDestinationObject(destinationName);
        }

        public void AddFormField(PdfAcroFormVisualField field)
        {
            PdfAcroFormFieldAppearance appearance = field.Appearance;
            PdfFontStyle fontStyle = appearance.FontStyle;
            string fontFamily = appearance.FontFamily;
            field.CreateFormField(this.graphicsDocument.FontCache, this.document, null);
        }

        public PdfXObjectCachedResource AddImage(EmfMetafile image) => 
            this.graphicsDocument.ImageCache.AddXObject(image);

        public PdfXObjectCachedResource AddImage(Image image) => 
            this.graphicsDocument.ImageCache.AddXObject(image);

        public PdfXObjectCachedResource AddImage(Stream image) => 
            this.graphicsDocument.ImageCache.AddXObject(image);

        public PdfXObjectCachedResource AddImage(Image image, Color backgroundColor) => 
            this.graphicsDocument.ImageCache.AddXObject(image, backgroundColor, this.rasterizationResolution);

        public void AddLinkToPage(PdfRectangle bound, string destinationsName)
        {
            PdfLinkAnnotation annotation1 = new PdfLinkAnnotation(this.currentPage, bound, new PdfDestinationObject(destinationsName));
        }

        public void AddLinkToPage(PdfRectangle bound, int pageIndex, double top)
        {
            this.AddLinkToPage(bound, this.AddDestination(pageIndex, top).DestinationName);
        }

        public void AddLinkToUri(PdfRectangle bound, string uri)
        {
            PdfLinkAnnotation annotation1 = new PdfLinkAnnotation(this.currentPage, bound, uri);
        }

        private void AddPrintOnOpenJS()
        {
            PdfDocumentCatalog documentCatalog = this.document.DocumentCatalog;
            documentCatalog.Names.JavaScriptActions.Add("0", new PdfJavaScriptAction("this.print({bUI: true,bSilent: false,bShrinkToFit: true});this.closeDoc();", documentCatalog));
        }

        public void ApplyConstructor(PdfGraphicsCommandConstructor commandConsructor)
        {
            if (commandConsructor != null)
            {
                this.currentPage.ReplaceCommands(commandConsructor.Commands);
            }
        }

        private static PdfEncryptionAlgorithm ConvertEncryptionLevel(PdfEncryptionLevel encryptionLevel)
        {
            switch (encryptionLevel)
            {
                case PdfEncryptionLevel.AES128:
                    return PdfEncryptionAlgorithm.AES128;

                case PdfEncryptionLevel.AES256:
                    return PdfEncryptionAlgorithm.AES256;

                case PdfEncryptionLevel.ARC4:
                    return PdfEncryptionAlgorithm.ARC4;
            }
            return PdfEncryptionAlgorithm.AES128;
        }

        private static PdfAssociatedFileRelationship ConvertRelationship(PdfAttachmentRelationship relationship)
        {
            switch (relationship)
            {
                case PdfAttachmentRelationship.Alternative:
                    return PdfAssociatedFileRelationship.Alternative;

                case PdfAttachmentRelationship.Data:
                    return PdfAssociatedFileRelationship.Data;

                case PdfAttachmentRelationship.Supplement:
                    return PdfAssociatedFileRelationship.Supplement;

                case PdfAttachmentRelationship.Unspecified:
                    return PdfAssociatedFileRelationship.Unspecified;
            }
            return PdfAssociatedFileRelationship.Source;
        }

        public PdfBookmark CreateBookmark(string title, int destinationPageIndex, float destinationTop)
        {
            this.document.DocumentCatalog.PageMode = PdfPageMode.UseOutlines;
            PdfBookmark bookmark1 = new PdfBookmark(this.AddDestination(destinationPageIndex, (double) destinationTop));
            bookmark1.Title = title;
            return bookmark1;
        }

        public PdfGraphicsCommandConstructor CreateCommandConstructor(SizeF pageSize, int pageIndex, float currentDpi) => 
            this.CreateCommandConstructor(pageSize, pageIndex, currentDpi, true);

        public PdfGraphicsCommandConstructor CreateCommandConstructor(SizeF pageSize, int pageIndex, float currentDpi, bool roundSize)
        {
            double num = (pageSize.Width * 72f) / currentDpi;
            double num2 = (pageSize.Height * 72f) / currentDpi;
            PdfRectangle mediaBox = new PdfRectangle(0.0, 0.0, roundSize ? ((double) ((int) Math.Round(num, MidpointRounding.AwayFromZero))) : num, roundSize ? ((double) ((int) Math.Round(num2, MidpointRounding.AwayFromZero))) : num2);
            if (pageIndex > this.document.Pages.Count)
            {
                pageIndex = this.document.Pages.Count;
            }
            this.currentPage = this.document.InsertPage(pageIndex + 1, mediaBox);
            return new PdfGraphicsCommandConstructor(this.currentPage, this.graphicsDocument, currentDpi, currentDpi);
        }

        private static PdfEncryptionParameters CreateEncryptionParameters(PdfPasswordSecurityOptions passwordOptions)
        {
            PdfPermissionsOptions permissionsOptions = passwordOptions.PermissionsOptions;
            if (permissionsOptions == null)
            {
                return null;
            }
            string str = PreparePassword(passwordOptions.PermissionsPassword);
            string str2 = PreparePassword(passwordOptions.OpenPassword);
            if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(str2))
            {
                return null;
            }
            PrintingPermissions highResolution = PrintingPermissions.HighResolution;
            ChangingPermissions anyExceptExtractingPages = ChangingPermissions.AnyExceptExtractingPages;
            bool enableScreenReaders = true;
            bool enableCoping = true;
            if (!string.IsNullOrEmpty(str))
            {
                highResolution = permissionsOptions.PrintingPermissions;
                anyExceptExtractingPages = permissionsOptions.ChangingPermissions;
                enableScreenReaders = permissionsOptions.EnableScreenReaders;
                enableCoping = permissionsOptions.EnableCopying;
            }
            return new PdfEncryptionParameters(str, str2, ConvertEncryptionLevel(passwordOptions.EncryptionLevel), GetEncryptionFlags(highResolution, anyExceptExtractingPages, enableScreenReaders, enableCoping));
        }

        public PdfGraphicsCommandConstructor CreateFormCommandConstructor(PdfForm form, float currentDpi) => 
            new PdfGraphicsCommandConstructor(form, this.graphicsDocument, currentDpi, currentDpi);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.FinalizeDocument();
                this.graphicsDocument.Dispose();
            }
        }

        public void FinalizeDocument()
        {
            if (this.document != null)
            {
                this.graphicsDocument.FontCache.UpdateFonts();
                PdfDocumentCatalog documentCatalog = this.document.DocumentCatalog;
                IDictionary<string, PdfDestination> destinations = this.document.Destinations;
                float num = 300f;
                foreach (Destination destination in this.destinations)
                {
                    PdfPage page;
                    if (documentCatalog.Pages.TryGetValue<PdfPage>(destination.PageIndex, out page))
                    {
                        double num2 = page.GetSize(0).Height - ((destination.TopCoordinate * 72.0) / ((double) num));
                        double? zoom = null;
                        destinations.Add(destination.DestinationName, new PdfXYZDestination(page, 0.0, new double?(num2), zoom));
                    }
                }
                this.destinations.Clear();
                this.acroFormFieldNames.Clear();
                this.document.FinalizeDocument();
                this.document = null;
            }
        }

        public void FlushPage()
        {
            if (this.currentPage != null)
            {
                this.currentPage.FlushPageData(this.Document.DocumentCatalog.Objects);
            }
        }

        public string GetAcroFormFieldName(string editingFieldName)
        {
            string key = string.IsNullOrEmpty(editingFieldName) ? "AcroFormField" : this.ValidateAcroFormFieldName(editingFieldName);
            if (!this.acroFormFieldNames.ContainsKey(key))
            {
                this.acroFormFieldNames.Add(key, 0);
            }
            string str2 = key;
            int num = this.acroFormFieldNames[str2];
            this.acroFormFieldNames[str2] = num + 1;
            return $"{key}_{num}";
        }

        public string GetAcroFormRadioGroupName(string groupID) => 
            $"{"RadioGroupAcroFormField"}_{this.ValidateAcroFormFieldName(groupID)}";

        private static byte[] GetAttachmentData(PdfAttachment attachment)
        {
            if (attachment.Data != null)
            {
                return attachment.Data;
            }
            byte[] buffer = null;
            try
            {
                buffer = File.ReadAllBytes(attachment.FilePath);
                DateTime? modificationDate = attachment.ModificationDate;
                attachment.ModificationDate = new DateTime?((modificationDate != null) ? modificationDate.GetValueOrDefault() : File.GetLastWriteTime(attachment.FilePath));
                modificationDate = attachment.CreationDate;
                attachment.CreationDate = new DateTime?((modificationDate != null) ? modificationDate.GetValueOrDefault() : File.GetCreationTime(attachment.FilePath));
            }
            catch
            {
            }
            return buffer;
        }

        private static string GetAttachmentFileName(PdfAttachment attachment) => 
            string.IsNullOrEmpty(attachment.FileName) ? (string.IsNullOrEmpty(attachment.FilePath) ? string.Empty : Path.GetFileName(attachment.FilePath)) : attachment.FileName;

        private static PdfCreationOptions GetCreationOptions(PdfExportOptions exportOptions, bool rightToLeftLayout)
        {
            PdfCreationOptions options = new PdfCreationOptions();
            switch (exportOptions.PdfACompatibility)
            {
                case PdfACompatibility.PdfA1b:
                    options.Compatibility = PdfCompatibility.PdfA1b;
                    break;

                case PdfACompatibility.PdfA2b:
                    options.Compatibility = PdfCompatibility.PdfA2b;
                    break;

                case PdfACompatibility.PdfA3b:
                    options.Compatibility = PdfCompatibility.PdfA3b;
                    break;

                default:
                    options.Compatibility = PdfCompatibility.Pdf;
                    break;
            }
            string neverEmbeddedFonts = exportOptions.NeverEmbeddedFonts;
            if (!string.IsNullOrEmpty(neverEmbeddedFonts))
            {
                if (neverEmbeddedFonts == "*")
                {
                    options.DisableEmbeddingAllFonts = true;
                }
                else
                {
                    char[] separator = new char[] { ';' };
                    options.NotEmbeddedFontFamilies = neverEmbeddedFonts.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            options.RightToLeftLayout = rightToLeftLayout;
            return options;
        }

        private static long GetEncryptionFlags(PrintingPermissions printingPermissions, ChangingPermissions changingPermissions, bool enableScreenReaders, bool enableCoping)
        {
            long num = 0xfffff0c0L;
            if (printingPermissions == PrintingPermissions.LowResolution)
            {
                num |= 4L;
            }
            else if (printingPermissions == PrintingPermissions.HighResolution)
            {
                num |= 0x804L;
            }
            switch (changingPermissions)
            {
                case ChangingPermissions.InsertingDeletingRotating:
                    num |= 0x400L;
                    break;

                case ChangingPermissions.FillingSigning:
                    num |= 0x100L;
                    break;

                case ChangingPermissions.CommentingFillingSigning:
                    num |= 0x120L;
                    break;

                case ChangingPermissions.AnyExceptExtractingPages:
                    num |= 0x528L;
                    break;

                default:
                    break;
            }
            if (enableScreenReaders)
            {
                num |= 0x200L;
            }
            if (enableCoping)
            {
                num |= 0x210L;
            }
            return num;
        }

        public PdfExportFontInfo GetFontInfo(Font font) => 
            this.graphicsDocument.FontCache.GetFontInfo(font);

        public PdfExportFontInfo GetFontInfo(IPdfExportPlatformFontProvider font, float fontSize, DXFontDecorations decorations) => 
            new PdfExportFontInfo(this.graphicsDocument.FontCache.GetExportFont(font), fontSize, decorations);

        public string GetNewDestinationName()
        {
            int destinationNameIndex = this.destinationNameIndex;
            this.destinationNameIndex = destinationNameIndex + 1;
            return ("bm" + destinationNameIndex);
        }

        private static string PreparePassword(string password) => 
            !string.IsNullOrEmpty(password) ? password : null;

        public void SetSignatureAppearanceForm(PdfFormSignatureAppearance appearance)
        {
            if (this.signature != null)
            {
                this.signature.Appearance = appearance;
            }
        }

        private string ValidateAcroFormFieldName(string name) => 
            name.Replace(".", "_");

        public string PageRange =>
            this.pageRange;

        public bool ExportEditingFieldsToAcroForms =>
            this.exportEditingFieldsToAcroForms;

        public bool ConvertImagesToJpeg =>
            this.graphicsDocument.ImageCache.ConvertImagesToJpeg;

        public bool SupportsTransparency =>
            this.supportsTransparency;

        public IList<PdfBookmark> Bookmarks =>
            this.document.Bookmarks;

        public PdfDocument Document =>
            this.document;

        public PdfPage CurrentPage =>
            this.currentPage;

        [StructLayout(LayoutKind.Sequential)]
        private struct Destination
        {
            private readonly string destinationName;
            private readonly int pageIndex;
            private readonly double topCoordinate;
            public string DestinationName =>
                this.destinationName;
            public int PageIndex =>
                this.pageIndex;
            public double TopCoordinate =>
                this.topCoordinate;
            public Destination(string destinationName, int pageIndex, double topCoordinate)
            {
                this.destinationName = destinationName;
                this.pageIndex = pageIndex;
                this.topCoordinate = topCoordinate;
            }
        }
    }
}

