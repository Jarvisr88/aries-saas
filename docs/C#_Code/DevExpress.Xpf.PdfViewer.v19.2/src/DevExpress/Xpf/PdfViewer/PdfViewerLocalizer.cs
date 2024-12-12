namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using DevExpress.Xpf.Core;
    using System;
    using System.Globalization;

    public class PdfViewerLocalizer : DXLocalizer<PdfViewerStringId>
    {
        static PdfViewerLocalizer()
        {
            if (GetActiveLocalizerProvider() == null)
            {
                SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<PdfViewerStringId>(new PdfViewerControlResXLocalizer()));
            }
        }

        public static XtraLocalizer<PdfViewerStringId> CreateDefaultLocalizer() => 
            new PdfViewerLocalizer();

        public override XtraLocalizer<PdfViewerStringId> CreateResXLocalizer() => 
            new PdfViewerControlResXLocalizer();

        public static string GetString(PdfViewerStringId id) => 
            Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(PdfViewerStringId.MessageEnterPasswordLabel, "Please enter a Document Open password");
            this.AddString(PdfViewerStringId.MessageEnterUrlLabel, "Please enter a Document url address");
            this.AddString(PdfViewerStringId.MessageIncorrectPassword, "The password is incorrect. Please make sure that Caps Lock is not enabled.");
            this.AddString(PdfViewerStringId.MessageCantLoadDocument, "There was an error loading a document.");
            this.AddString(PdfViewerStringId.MessageDocumentHasNoPages, "The document does not contain any pages.");
            this.AddString(PdfViewerStringId.CommandOpenFileCaption, "Open");
            this.AddString(PdfViewerStringId.CommandOpenFileDescription, "Open a PDF file.");
            this.AddString(PdfViewerStringId.CommandOpenFileFromWebCaption, "Open from web");
            this.AddString(PdfViewerStringId.CommandOpenFileFromWebDescription, "Open a PDF file from web.");
            this.AddString(PdfViewerStringId.CommandCloseFileCaption, "Close");
            this.AddString(PdfViewerStringId.CommandCloseFileDescription, "Close a PDF file.");
            this.AddString(PdfViewerStringId.CommandPreviousPageCaption, "Previous");
            this.AddString(PdfViewerStringId.CommandPreviousPageDescription, "Show previous page.");
            this.AddString(PdfViewerStringId.CommandNextPageCaption, "Next");
            this.AddString(PdfViewerStringId.CommandNextPageDescription, "Show next page.");
            this.AddString(PdfViewerStringId.CommandZoomInCaption, "Zoom In");
            this.AddString(PdfViewerStringId.CommandZoomInDescription, "Zoom in to get a close-up view of the PDF document.");
            this.AddString(PdfViewerStringId.CommandZoomInShortcutCaption, " (Ctrl + Plus)");
            this.AddString(PdfViewerStringId.CommandZoomOutCaption, "Zoom Out");
            this.AddString(PdfViewerStringId.CommandZoomOutDescription, "Zoom out to see more of the page at a reduced size.");
            this.AddString(PdfViewerStringId.CommandZoomOutShortcutCaption, " (Ctrl + Minus)");
            this.AddString(PdfViewerStringId.CommandViewExactZoomListCaption, "Zoom");
            this.AddString(PdfViewerStringId.CommandViewExactZoomListDescription, "Change the zoom level of the PDF document.");
            this.AddString(PdfViewerStringId.CommandZoom10Caption, "10%");
            this.AddString(PdfViewerStringId.CommandZoom25Caption, "25%");
            this.AddString(PdfViewerStringId.CommandZoom50Caption, "50%");
            this.AddString(PdfViewerStringId.CommandZoom75Caption, "75%");
            this.AddString(PdfViewerStringId.CommandZoom100Caption, "100%");
            this.AddString(PdfViewerStringId.CommandZoom125Caption, "125%");
            this.AddString(PdfViewerStringId.CommandZoom150Caption, "150%");
            this.AddString(PdfViewerStringId.CommandZoom200Caption, "200%");
            this.AddString(PdfViewerStringId.CommandZoom400Caption, "400%");
            this.AddString(PdfViewerStringId.CommandZoom500Caption, "500%");
            this.AddString(PdfViewerStringId.CommandSetActualSizeZoomModeCaption, "Actual Size");
            this.AddString(PdfViewerStringId.CommandSetPageLevelZoomModeCaption, "Zoom to Page Level");
            this.AddString(PdfViewerStringId.CommandSetFitWidthZoomModeCaption, "Fit Width");
            this.AddString(PdfViewerStringId.CommandSetFitVisibleZoomModeCaption, "Fit Visible");
            this.AddString(PdfViewerStringId.CommandPrintFileCaption, "Print");
            this.AddString(PdfViewerStringId.CommandPrintFileDescription, "Print file.");
            this.AddString(PdfViewerStringId.CommandShowDocumentProperties, "Document properties...");
            this.AddString(PdfViewerStringId.CommandViewCursorModeListGroupCaption, "Cursor mode");
            this.AddString(PdfViewerStringId.CommandCursorModeHandToolCaption, "Hand tool");
            this.AddString(PdfViewerStringId.CommandCursorModeSelectToolCaption, "Select tool");
            this.AddString(PdfViewerStringId.CommandCursorModeMarqueeZoomCaption, "Marquee zoom");
            this.AddString(PdfViewerStringId.CommandCursorModeTextHighlightCaption, "Highlight");
            this.AddString(PdfViewerStringId.CommandCursorModeTextStrikethroughCaption, "Strikethrough");
            this.AddString(PdfViewerStringId.CommandCursorModeTextUnderlineCaption, "Underline");
            this.AddString(PdfViewerStringId.CommandCursorModeTextHighlightDescription, "Highlight text");
            this.AddString(PdfViewerStringId.CommandCursorModeTextStrikethroughDescription, "Strikethrough text");
            this.AddString(PdfViewerStringId.CommandCursorModeTextUnderlineDescription, "Underline text");
            this.AddString(PdfViewerStringId.CommandDeleteAnnotationCaption, "Delete");
            this.AddString(PdfViewerStringId.CommandShowAnnotationPropertiesCaption, "Properties...");
            this.AddString(PdfViewerStringId.CommandClockwiseRotateCaption, "Clockwise rotate");
            this.AddString(PdfViewerStringId.CommandCounterclockwiseRotateCaption, "Counterclockwise rotate");
            this.AddString(PdfViewerStringId.CommandPreviousViewCaption, "Previous view");
            this.AddString(PdfViewerStringId.CommandNextViewCaption, "Next view");
            this.AddString(PdfViewerStringId.CommandSelectAllCaption, "Select All");
            this.AddString(PdfViewerStringId.CommandSaveAsCaption, "Save As");
            this.AddString(PdfViewerStringId.CommandCopyCaption, "Copy");
            this.AddString(PdfViewerStringId.CommandImportCaption, "Import");
            this.AddString(PdfViewerStringId.CommandImportDescription, "Import form data from a file.");
            this.AddString(PdfViewerStringId.CommandExportCaption, "Export");
            this.AddString(PdfViewerStringId.CommandExportDescription, "Export form data to a file.");
            this.AddString(PdfViewerStringId.RotateRibbonGroupCaption, "Rotate view");
            this.AddString(PdfViewerStringId.BarCaption, "PDF Viewer");
            this.AddString(PdfViewerStringId.BarFormDataCaption, "Form Data");
            this.AddString(PdfViewerStringId.BarCommentCaption, "Comment");
            this.AddString(PdfViewerStringId.FileRibbonGroupCaption, "File");
            this.AddString(PdfViewerStringId.FindRibbonGroupCaption, "Find");
            this.AddString(PdfViewerStringId.NavigationRibbonGroupCaption, "Navigation");
            this.AddString(PdfViewerStringId.ZoomRibbonGroupCaption, "Zoom");
            this.AddString(PdfViewerStringId.ViewRibbonGroupCaption, "View");
            this.AddString(PdfViewerStringId.FormDataRibbonGroupCaption, "Form Data");
            this.AddString(PdfViewerStringId.CommentTextRibbonGroupCaption, "Text");
            this.AddString(PdfViewerStringId.RecentDocumentsCaption, "Recent documents");
            this.AddString(PdfViewerStringId.OpenDocumentCaption, "Open...");
            this.AddString(PdfViewerStringId.SavingDocumentCaption, "Saving");
            this.AddString(PdfViewerStringId.SavingDocumentMessage, "Saving... Please wait.");
            this.AddString(PdfViewerStringId.LoadingDocumentCaption, "Loading... Please wait.");
            this.AddString(PdfViewerStringId.CancelButtonCaption, "Cancel");
            this.AddString(PdfViewerStringId.PdfFileExtension, ".pdf");
            this.AddString(PdfViewerStringId.PdfFileFilter, "PDF Files (.pdf)|*.pdf");
            this.AddString(PdfViewerStringId.FormDataFileFilter, "FDF Files (.fdf)|*.fdf|XML Files (.xml)|*.xml");
            this.AddString(PdfViewerStringId.MessageImportError, "Unable to import the specified data into the document form.\r\n{0}\r\n Please ensure that the provided data meets the {1} specification.");
            this.AddString(PdfViewerStringId.MessageExportError, "An error occurred while exporting the form data from the document.");
            this.AddString(PdfViewerStringId.MessageErrorCaption, "Error");
            this.AddString(PdfViewerStringId.MessageLoadingError, "Unable to load the PDF document because the following file is not available or it is not a valid PDF document.\r\n{0}\r\nPlease ensure that the application can access this file and that it is valid, or specify a different file.");
            this.AddString(PdfViewerStringId.MessageAddCommandConstructorError, "Cannot find a constructor with a PdfViewer type parameter in the {0} class");
            this.AddString(PdfViewerStringId.MessageErrorZoomFactorOutOfRange, "The zoom factor must be in the range from {0} to {1}.");
            this.AddString(PdfViewerStringId.MessageErrorCurrentPageNumberOutOfRange, "The current page number should be greater than 0.");
            this.AddString(PdfViewerStringId.MessageDocumentIsProtected, "{0} is protected");
            this.AddString(PdfViewerStringId.MessageSearchFinished, "Finished searching throughout the document. No more matches were found.");
            this.AddString(PdfViewerStringId.MessageSearchFinishedNoMatchesFound, "Finished searching throughout the document. No matches were found.");
            this.AddString(PdfViewerStringId.MessageSearchCaption, "Find");
            this.AddString(PdfViewerStringId.OpenDocumentFromWebCaption, "Open from web");
            this.AddString(PdfViewerStringId.MessageIncorrectUrl, "{0} is not valid url address.");
            this.AddString(PdfViewerStringId.PropertiesCaption, "Properties");
            this.AddString(PdfViewerStringId.PageSize, "{0:0.00} x {1:0.00} in");
            this.AddString(PdfViewerStringId.PrintDialogTitle, "Print");
            this.AddString(PdfViewerStringId.PrintDialogPrinterName, "Printer name");
            this.AddString(PdfViewerStringId.PrintDialogPrinterPreferences, "Preferences");
            this.AddString(PdfViewerStringId.PrintDialogStatus, "Status");
            this.AddString(PdfViewerStringId.PrintDialogLocation, "Location");
            this.AddString(PdfViewerStringId.PrintDialogComment, "Comment");
            this.AddString(PdfViewerStringId.PrintDialogDocumentsInQueue, "Document(s) in queue");
            this.AddString(PdfViewerStringId.PrintDialogPrintingDpi, "Printing DPI");
            this.AddString(PdfViewerStringId.PrintDialogNumberOfCopies, "Number of copies");
            this.AddString(PdfViewerStringId.PrintDialogCollate, "Collate");
            this.AddString(PdfViewerStringId.PrintDialogPageRange, "Page range");
            this.AddString(PdfViewerStringId.PrintDialogPageRangeAll, "All");
            this.AddString(PdfViewerStringId.PrintDialogPageRangeCurrent, "Current page");
            this.AddString(PdfViewerStringId.PrintDialogPageRangePages, "Pages:");
            this.AddString(PdfViewerStringId.PrintDialogPageRangePagesExample, "For example, 5-12");
            this.AddString(PdfViewerStringId.PrintDialogPageSizing, "Page sizing");
            this.AddString(PdfViewerStringId.PrintDialogPageSizingFit, "Fit");
            this.AddString(PdfViewerStringId.PrintDialogPageSizingActualSize, "Actual size");
            this.AddString(PdfViewerStringId.PrintDialogPageSizingCustomScale, "Custom scale:");
            this.AddString(PdfViewerStringId.PrintDialogOrientation, "Orientation");
            this.AddString(PdfViewerStringId.PrintDialogPaperSource, "Paper source");
            this.AddString(PdfViewerStringId.PrintDialogFilePath, "File path");
            this.AddString(PdfViewerStringId.PrintDialogPrintToFile, "Print to file");
            this.AddString(PdfViewerStringId.PrintFileExtension, ".prn");
            this.AddString(PdfViewerStringId.PrintFileFilter, "Printable files (.prn)|*.prn");
            this.AddString(PdfViewerStringId.PrintDialogPaginationOf, " ({0})");
            this.AddString(PdfViewerStringId.PrintDialogPrintButtonCaption, "Print");
            this.AddString(PdfViewerStringId.PrintDialogPrintOrientationAuto, "Auto");
            this.AddString(PdfViewerStringId.PrintDialogPrintOrientationLandscape, "Landscape");
            this.AddString(PdfViewerStringId.PrintDialogPrintOrientationPortrait, "Portrait");
            this.AddString(PdfViewerStringId.PropertiesDescriptionCaption, "Description");
            this.AddString(PdfViewerStringId.PropertiesRevisionCaption, "Revision");
            this.AddString(PdfViewerStringId.PropertiesAdvancedCaption, "Advanced");
            this.AddString(PdfViewerStringId.PropertiesFile, "File");
            this.AddString(PdfViewerStringId.PropertiesTitle, "Title");
            this.AddString(PdfViewerStringId.PropertiesAuthor, "Author");
            this.AddString(PdfViewerStringId.PropertiesSubject, "Subject");
            this.AddString(PdfViewerStringId.PropertiesKeywords, "Keywords");
            this.AddString(PdfViewerStringId.PropertiesCreated, "Created");
            this.AddString(PdfViewerStringId.PropertiesModified, "Modified");
            this.AddString(PdfViewerStringId.PropertiesApplication, "Application");
            this.AddString(PdfViewerStringId.PropertiesProducer, "Producer");
            this.AddString(PdfViewerStringId.PropertiesVersion, "Version");
            this.AddString(PdfViewerStringId.PropertiesLocation, "Location");
            this.AddString(PdfViewerStringId.PropertiesFileSize, "File Size");
            this.AddString(PdfViewerStringId.PropertiesNumberOfPages, "Number of Pages");
            this.AddString(PdfViewerStringId.PropertiesPageSize, "Page Size");
            this.AddString(PdfViewerStringId.OutlinesViewerExpandCurrentCaption, "Expand current bookmark");
            this.AddString(PdfViewerStringId.OutlinesViewerExpandTopLevelCaption, "Expand top level bookmark");
            this.AddString(PdfViewerStringId.OutlinesViewerCollapseTopLevelCaption, "Collapse top level bookmark");
            this.AddString(PdfViewerStringId.OutlinesViewerGoToCaption, "Go to bookmark");
            this.AddString(PdfViewerStringId.OutlinesViewerPrintCaption, "Print page(s)");
            this.AddString(PdfViewerStringId.OutlinesViewerPrintSectionCaption, "Print section(s)");
            this.AddString(PdfViewerStringId.OutlinesViewerWrapLongItemsCaption, "Wrap long bookmarks");
            this.AddString(PdfViewerStringId.OutlinesViewerHideAfterUseCaption, "Hide after use");
            this.AddString(PdfViewerStringId.OutlinesViewerPanelCaption, "Bookmarks");
            this.AddString(PdfViewerStringId.AttachmentsViewerOpenCaption, "Open Attachment");
            this.AddString(PdfViewerStringId.AttachmentsViewerOpenDescription, "Open file in its native application");
            this.AddString(PdfViewerStringId.AttachmentsViewerSaveCaption, "Save Attachment...");
            this.AddString(PdfViewerStringId.AttachmentsViewerSaveDescription, "Save Attachment");
            this.AddString(PdfViewerStringId.AttachmentsViewerPanelCaption, "Attachments");
            this.AddString(PdfViewerStringId.MessageFileAttachmentOpening, "The document requests to access an external application to open this file:\r\n'{0}'\r\nDo you want to open it?");
            this.AddString(PdfViewerStringId.AttachmentsViewerFileNameHeader, "Name");
            this.AddString(PdfViewerStringId.AttachmentsViewerDescriptionHeader, "Description");
            this.AddString(PdfViewerStringId.AttachmentsViewerModifiedHeader, "Modified");
            this.AddString(PdfViewerStringId.AttachmentsViewerSizeHeader, "Size");
            this.AddString(PdfViewerStringId.ThumbnailsViewerPanelCaption, "Page Thumbnails");
            this.AddString(PdfViewerStringId.ThumbnailsViewerPrintPagesCaption, "Print Pages...");
            this.AddString(PdfViewerStringId.ThumbnailsViewerPrintPagesDescription, "Print Pages...");
            this.AddString(PdfViewerStringId.ThumbnailsViewerZoomInCaption, "Enlarge Page Thumbnails");
            this.AddString(PdfViewerStringId.ThumbnailsViewerZoomInDescription, "Enlarge Page Thumbnails");
            this.AddString(PdfViewerStringId.ThumbnailsViewerZoomOutCaption, "Reduce Page Thumbnails");
            this.AddString(PdfViewerStringId.ThumbnailsViewerZoomOutDescription, "Reduce Page Thumbnails");
            this.AddString(PdfViewerStringId.SaveChangesCaption, "PDF Viewer");
            this.AddString(PdfViewerStringId.SaveChangesMessage, "Do you want to save the changes to the document before closing it?");
            this.AddString(PdfViewerStringId.MessageSecurityWarningCaption, "Security Warning");
            this.AddString(PdfViewerStringId.MessageSecurityWarningUriOpening, "The document is trying to access an external resource by using the following URL:\r\n'{0}'\r\nDo you want to open it nevertheless?");
            this.AddString(PdfViewerStringId.CommandPageLayoutCaption, "Page Display");
            this.AddString(PdfViewerStringId.CommandPageLayoutDescription, "");
            this.AddString(PdfViewerStringId.CommandSinglePageView, "Single Page View");
            this.AddString(PdfViewerStringId.CommandOneColumnView, "Enable Scrolling");
            this.AddString(PdfViewerStringId.CommandTwoPageView, "Two Page View");
            this.AddString(PdfViewerStringId.CommandTwoColumnView, "Two Page Scrolling");
            this.AddString(PdfViewerStringId.CommandShowCoverPage, "Show Cover Page");
            this.AddString(PdfViewerStringId.MessageSaveAsError, "Cannot save the PDF document with the following name: {0}.");
            this.AddString(PdfViewerStringId.CommandHighlightSelectedTextCaption, "Highlight text");
            this.AddString(PdfViewerStringId.CommandStrikethroughSelectedTextCaption, "Strikethrough text");
            this.AddString(PdfViewerStringId.CommandUnderlineSelectedTextCaption, "Underline text");
            this.AddString(PdfViewerStringId.AnnotationPropertiesCaption, "Annotation Properties");
            this.AddString(PdfViewerStringId.AnnotationPropertyAppearanceGroupCaption, "Appearance");
            this.AddString(PdfViewerStringId.AnnotationPropertyColor, "Color");
            this.AddString(PdfViewerStringId.AnnotationPropertyOpacity, "Opacity");
            this.AddString(PdfViewerStringId.AnnotationPropertyMarkupType, "Markup type");
            this.AddString(PdfViewerStringId.AnnotationPropertyGeneralGroupCaption, "General");
            this.AddString(PdfViewerStringId.AnnotationPropertyAuthor, "Author");
            this.AddString(PdfViewerStringId.AnnotationPropertySubject, "Subject");
            this.AddString(PdfViewerStringId.AnnotationPropertyModified, "Modified");
            this.AddString(PdfViewerStringId.AnnotationPropertyCreated, "Created");
            this.AddString(PdfViewerStringId.AnnotationPropertyComment, "Comment");
            this.AddString(PdfViewerStringId.AnnotationPropertiesSetAsDefaultCaption, "Set As Default");
            this.AddString(PdfViewerStringId.AnnotationPropertiesOkCaption, "OK");
            this.AddString(PdfViewerStringId.AnnotationPropertiesCancelCaption, "Cancel");
            this.AddString(PdfViewerStringId.AnnotationPropertiesHighlightMarkupType, "Highlight");
            this.AddString(PdfViewerStringId.AnnotationPropertiesUnderlineMarkupType, "Underline");
            this.AddString(PdfViewerStringId.AnnotationPropertiesSquigglyMarkupType, "Squiggly Underline");
            this.AddString(PdfViewerStringId.AnnotationPropertiesStrikeOutMarkupType, "Strikethrough");
        }

        public static XtraLocalizer<PdfViewerStringId> Active
        {
            get => 
                XtraLocalizer<PdfViewerStringId>.Active;
            set
            {
                if (GetActiveLocalizerProvider() is DefaultActiveLocalizerProvider<PdfViewerStringId>)
                {
                    XtraLocalizer<PdfViewerStringId>.Active = value;
                }
                else
                {
                    SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<PdfViewerStringId>(value));
                    RaiseActiveChanged();
                }
            }
        }

        public override string Language =>
            CultureInfo.CurrentUICulture.Name;
    }
}

