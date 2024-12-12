namespace DevExpress.Pdf.Localization
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    [ToolboxItem(false)]
    public class PdfCoreLocalizer : XtraLocalizer<PdfCoreStringId>
    {
        static PdfCoreLocalizer()
        {
            if (GetActiveLocalizerProvider() == null)
            {
                SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<PdfCoreStringId>(new PdfCoreResLocalizer()));
            }
        }

        private void AddStrings()
        {
            this.AddString(PdfCoreStringId.DefaultDocumentName, "Document");
            this.AddString(PdfCoreStringId.UnitKiloBytes, "KB");
            this.AddString(PdfCoreStringId.UnitMegaBytes, "MB");
            this.AddString(PdfCoreStringId.UnitGigaBytes, "GB");
            this.AddString(PdfCoreStringId.UnitTeraBytes, "TB");
            this.AddString(PdfCoreStringId.UnitPetaBytes, "PB");
            this.AddString(PdfCoreStringId.UnitExaBytes, "EB");
            this.AddString(PdfCoreStringId.UnitZettaBytes, "ZB");
            this.AddString(PdfCoreStringId.FileSize, "{0:0.00} {1} ({2} Bytes)");
            this.AddString(PdfCoreStringId.FileSizeInBytes, "{0} Bytes");
            this.AddString(PdfCoreStringId.MsgAttachmentHintFileName, "Name: {0}");
            this.AddString(PdfCoreStringId.MsgAttachmentHintSize, "\r\nSize: {0}");
            this.AddString(PdfCoreStringId.MsgAttachmentHintModificationDate, "\r\nModified: {0:G}");
            this.AddString(PdfCoreStringId.MsgAttachmentHintDescription, "\r\nDescription: {0}");
            this.AddString(PdfCoreStringId.MsgIncorrectPdfData, "Input data is not recognized as valid pdf.");
            this.AddString(PdfCoreStringId.MsgIncorrectFormDataFile, "An error occurred while reading the form data from the specified file.");
            this.AddString(PdfCoreStringId.MsgIncorrectPdfPassword, "The Document Open password is empty or incorrect.");
            this.AddString(PdfCoreStringId.MsgIncorrectRectangleWidth, "The right coordinate of the rectangle should be greater than or equal to the left one.");
            this.AddString(PdfCoreStringId.MsgIncorrectRectangleHeight, "The top coordinate of the rectangle should be greater than or equal to the bottom.");
            this.AddString(PdfCoreStringId.MsgIncorrectPageRotate, "The page rotation angle can have one of the following values: 0, 90, 180 or 270 degrees.");
            this.AddString(PdfCoreStringId.MsgIncorrectPageCropBox, "The page cropping box should be less than or equal to the media box.");
            this.AddString(PdfCoreStringId.MsgIncorrectPageBleedBox, "The page bleeding box should be less than or equal to the media box.");
            this.AddString(PdfCoreStringId.MsgIncorrectPageTrimBox, "The page trimming box should be less than or equal to the media box.");
            this.AddString(PdfCoreStringId.MsgIncorrectPageArtBox, "The page art box should be less than or equal to the media box.");
            this.AddString(PdfCoreStringId.MsgIncorrectOpacity, "The opacity value should be greater than or equal to 0 and less than or equal to 1.");
            this.AddString(PdfCoreStringId.MsgIncorrectLineWidth, "The line width should be greater than or equal to 0.");
            this.AddString(PdfCoreStringId.MsgIncorrectMiterLimit, "The miter limit should be greater than 0.");
            this.AddString(PdfCoreStringId.MsgIncorrectDashLength, "The dash length should be greater than or equal to 0.");
            this.AddString(PdfCoreStringId.MsgIncorrectGapLength, "The gap length should be greater than or equal to 0.");
            this.AddString(PdfCoreStringId.MsgIncorrectDashPatternArraySize, "The dash pattern array must not be empty.");
            this.AddString(PdfCoreStringId.MsgIncorrectDashPattern, "The sum of dash and gap lengths should be greater than 0.");
            this.AddString(PdfCoreStringId.MsgIncorrectFlatnessTolerance, "The flatness tolerance should be greater than or equal to 0 and less than or equal to 100.");
            this.AddString(PdfCoreStringId.MsgIncorrectColorComponentValue, "The color component value should be greater than or equal to 0 and less than or equal to 1.");
            this.AddString(PdfCoreStringId.MsgZeroColorComponentsCount, "The color should have at least one component.");
            this.AddString(PdfCoreStringId.MsgIncorrectTextHorizontalScaling, "The text horizontal scaling value should be greater than 0.");
            this.AddString(PdfCoreStringId.MsgIncorrectText, "The text value can't be null.");
            this.AddString(PdfCoreStringId.MsgIncorrectGlyphPosition, "The glyph position should be less or equal than text length.");
            this.AddString(PdfCoreStringId.MsgIncorrectMarkedContentTag, "The marked content tag can't be empty.");
            this.AddString(PdfCoreStringId.MsgIncorrectListSize, "The list should contain at least one value.");
            this.AddString(PdfCoreStringId.MsgIncorrectPageNumber, "The page number should be in the range from 1 to {0}.");
            this.AddString(PdfCoreStringId.MsgIncorrectDestinationPage, "The bookmark destination can't be linked to the page in a different document.");
            this.AddString(PdfCoreStringId.MsgIncorrectInsertingPageNumber, "The page number should be greater than 0, and less than or equal to the next available page number (next to the document page count).");
            this.AddString(PdfCoreStringId.MsgIncorrectLargestEdgeLength, "The largest edge length should be greater than 0.");
            this.AddString(PdfCoreStringId.MsgIncorrectButtonFormFieldValue, "The property value for a button form field should be either the appearance name or a value from the option list.");
            this.AddString(PdfCoreStringId.MsgIncorrectChoiceFormFieldValue, "The property value for a choice form field should be either a string value or a set of values from the options list.");
            this.AddString(PdfCoreStringId.MsgIncorrectTextFormFieldValue, "The property value for a text form field should be a string value.");
            this.AddString(PdfCoreStringId.MsgIncorrectSelectedIndexValue, "The index should be in the range from 0 to {0}.");
            this.AddString(PdfCoreStringId.MsgIncorrectAcroFormExportValue, "The export value cannot be null or an empty string.");
            this.AddString(PdfCoreStringId.MsgAcroFormFieldNameCantBeEmpty, "The form field name cannot be null or an empty string.");
            this.AddString(PdfCoreStringId.MsgIncorrectAcroFormFieldNameContainsPeriod, "The form field name cannot contain a period character.");
            this.AddString(PdfCoreStringId.MsgPageNumberShouldBePositive, "The page number should be greater than or equal to 1.");
            this.AddString(PdfCoreStringId.MsgAcroFormFieldNameDuplication, "The siblings can't have the same names in the form field hierarchy.");
            this.AddString(PdfCoreStringId.MsgCantSetSelectedIndexWithoutValues, "The selected index cannot be set because there are no possible values.");
            this.AddString(PdfCoreStringId.MsgIncorrectZoom, "The zoom value should be greater than or equal to 0.");
            this.AddString(PdfCoreStringId.MsgFormDataNotFound, "The field with the specified name is not found in the document.");
            this.AddString(PdfCoreStringId.MsgUnavailableOperation, "This operation is not available while no document is being loaded.");
            this.AddString(PdfCoreStringId.MsgIncorrectPrintableFilePath, "The specified file name for the printable document is incorrect.");
            this.AddString(PdfCoreStringId.MsgIncompatibleOperationWithCurrentDocumentFormat, "The operation is not supported in PdfCompatibility.{0} compatibility mode. Please create a document in PdfCompatibility.Pdf compatibility mode.");
            this.AddString(PdfCoreStringId.MsgIncorrectBookmarkListValue, "Bookmark list can't be null");
            this.AddString(PdfCoreStringId.MsgIncorrectMarkupAnnotation, "The specified annotation does not belong to the current document.");
            this.AddString(PdfCoreStringId.MsgUnsupportedGraphicsOperation, "This operation is not supported because the PdfGraphics object is not belong to the current document.");
            this.AddString(PdfCoreStringId.MsgUnsupportedGraphicsUnit, "The Display and World units are not supported for the source image coordinate system.");
            this.AddString(PdfCoreStringId.MsgUnsupportedBrushType, "Custom brushes are not supported.");
            this.AddString(PdfCoreStringId.MsgShouldEmbedFonts, "All fonts should be embedded to a PDF/A document.");
            this.AddString(PdfCoreStringId.MsgUnsupportedFileAttachments, "File attachments are not supported in PDF/A-1b and PDF/A-2b documents.");
            this.AddString(PdfCoreStringId.MsgIncorrectDpi, "Resolution (in dots per inch) should be greater than 0.");
            this.AddString(PdfCoreStringId.MsgMissingPageNumbers, "At least one page number should be specified.");
            this.AddString(PdfCoreStringId.MsgPartialTrustEnvironmentLimitation, "This operation cannot be performed in the Partial Trust environment.");
            this.AddString(PdfCoreStringId.MsgStreamIsInUse, "The stream is already used for a document. Please use another stream.");
            this.AddString(PdfCoreStringId.MsgUnsupportedAnnotationType, "Text highlight annotation is not supported in a PDF/A-1 document.");
            this.AddString(PdfCoreStringId.MsgUnsupportedStreamForLoadOperation, "Stream should support read and seek operations for loading.");
            this.AddString(PdfCoreStringId.MsgUnsupportedStreamForSaveOperation, "Stream should support write and seek operations for saving.");
            this.AddString(PdfCoreStringId.MsgUnsupportedStream, "Stream should support read, write, and seek operations.");
            this.AddString(PdfCoreStringId.MsgUnsupportedEncryption, "Encryption is not supported in a PDF/A document.");
            this.AddString(PdfCoreStringId.TextHighlightDefaultSubject, "Highlight");
            this.AddString(PdfCoreStringId.TextStrikethroughDefaultSubject, "Cross-Out");
            this.AddString(PdfCoreStringId.TextUnderlineDefaultSubject, "Underline");
            this.AddString(PdfCoreStringId.MsgWinOnlyLimitation, "This action can be performed only on Windows operating system.");
            this.AddString(PdfCoreStringId.MsgEntryPointNotFound, "Unable to find an entry point named \"{0}\" in shared library \"{1}\"");
            this.AddString(PdfCoreStringId.MsgSharedLibraryNotFound, "Unable to load shared library \"{0}\" or one of its dependencies: \"{1}\"");
            this.AddString(PdfCoreStringId.MsgICULibraryNotFound, "Unable to load ICU library.");
            this.AddString(PdfCoreStringId.MsgEmptyCustomPropertyName, "Custom property name cannot be null or empty string.");
            this.AddString(PdfCoreStringId.MsgIncorrectDestination, "The specified destination does not belong to the current document.");
            this.AddString(PdfCoreStringId.MsgIncorrectAction, "The specified action does not belong to the current document.");
        }

        public override XtraLocalizer<PdfCoreStringId> CreateResXLocalizer() => 
            new PdfCoreResLocalizer();

        public static string GetString(PdfCoreStringId id) => 
            Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddStrings();
        }

        public static XtraLocalizer<PdfCoreStringId> Active
        {
            get => 
                XtraLocalizer<PdfCoreStringId>.Active;
            set
            {
                if (GetActiveLocalizerProvider() is DefaultActiveLocalizerProvider<PdfCoreStringId>)
                {
                    XtraLocalizer<PdfCoreStringId>.Active = value;
                }
                else
                {
                    SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<PdfCoreStringId>(value));
                    RaiseActiveChanged();
                }
            }
        }

        public override string Language =>
            CultureInfo.CurrentUICulture.Name;
    }
}

