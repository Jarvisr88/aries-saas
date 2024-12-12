namespace DevExpress.Office.Localization
{
    using DevExpress.Office.Localization.Internal;
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using System;

    public class OfficeLocalizer : OfficeLocalizerBase<OfficeStringId>
    {
        static OfficeLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<OfficeStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<OfficeStringId> CreateDefaultLocalizer() => 
            new OfficeResLocalizer();

        public override XtraLocalizer<OfficeStringId> CreateResXLocalizer() => 
            new OfficeResLocalizer();

        public static string GetString(OfficeStringId id) => 
            XtraLocalizer<OfficeStringId>.Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(OfficeStringId.Msg_IsNotValid, "'{0}' is not a valid value for '{1}'");
            this.AddString(OfficeStringId.Msg_InternalError, "An internal error occurred");
            this.AddString(OfficeStringId.Msg_InvalidBeginUpdate, "Error: call to BeginUpdate inside BeginInit");
            this.AddString(OfficeStringId.Msg_InvalidEndUpdate, "Error: call to EndUpdate or CancelUpate without BeginUpdate or inside BeginInit");
            this.AddString(OfficeStringId.Msg_InvalidBeginInit, "Error: call to BeginInit inside BeginUpdate");
            this.AddString(OfficeStringId.Msg_InvalidEndInit, "Error: call to EndInit or CancelInit without BeginInit or inside BeginUpdate");
            this.AddString(OfficeStringId.Msg_InvalidCopyFromDocumentModel, "Error: source and destination document models are different");
            this.AddString(OfficeStringId.Msg_InvalidRemoveDataSource, "A data source cannot be deleted during the mail-merge process.");
            this.AddString(OfficeStringId.Msg_Loading, "Loading...");
            this.AddString(OfficeStringId.Msg_MagicNumberNotFound, "The file you are trying to open is in different format than specified by the file extension.");
            this.AddString(OfficeStringId.Msg_InvalidFontSize, "The number must be between {0} and {1}.");
            this.AddString(OfficeStringId.Msg_InvalidNumber, "This is not a valid number.");
            this.AddString(OfficeStringId.Msg_InvalidNumberConverterValue, "Value must be between {0} and {1}.");
            this.AddString(OfficeStringId.Msg_UnsupportedFormatOrCorruptedFile, "Unsupported format or corrupted file.");
            this.AddString(OfficeStringId.FileFilterDescription_AllFiles, "All Files");
            this.AddString(OfficeStringId.FileFilterDescription_AllSupportedFiles, "All Supported Files");
            this.AddString(OfficeStringId.FileFilterDescription_BitmapFiles, "Windows Bitmap");
            this.AddString(OfficeStringId.FileFilterDescription_JPEGFiles, "JPEG File Interchange Format");
            this.AddString(OfficeStringId.FileFilterDescription_PNGFiles, "Portable Network Graphics");
            this.AddString(OfficeStringId.FileFilterDescription_GifFiles, "Graphics Interchange Format");
            this.AddString(OfficeStringId.FileFilterDescription_TiffFiles, "Tag Image File Format");
            this.AddString(OfficeStringId.FileFilterDescription_EmfFiles, "Microsoft Enhanced Metafile");
            this.AddString(OfficeStringId.FileFilterDescription_WmfFiles, "Windows Metafile");
            this.AddString(OfficeStringId.UnitAbbreviation_Inch, "\"");
            this.AddString(OfficeStringId.UnitAbbreviation_Centimeter, " cm");
            this.AddString(OfficeStringId.UnitAbbreviation_Millimeter, " mm");
            this.AddString(OfficeStringId.UnitAbbreviation_Pica, " pc");
            this.AddString(OfficeStringId.UnitAbbreviation_Point, " pt");
            this.AddString(OfficeStringId.UnitAbbreviation_Percent, "%");
            this.AddString(OfficeStringId.Caption_UnitPercent, "Percent");
            this.AddString(OfficeStringId.Caption_UnitInches, "Inches");
            this.AddString(OfficeStringId.Caption_UnitCentimeters, "Centimeters");
            this.AddString(OfficeStringId.Caption_UnitMillimeters, "Millimeters");
            this.AddString(OfficeStringId.Caption_UnitPoints, "Points");
            this.AddString(OfficeStringId.MenuCmd_NewEmptyDocument, "New");
            this.AddString(OfficeStringId.MenuCmd_NewEmptyDocumentDescription, "Create a new document.");
            this.AddString(OfficeStringId.MenuCmd_LoadDocument, "Open");
            this.AddString(OfficeStringId.MenuCmd_LoadDocumentDescription, "Open a document.");
            this.AddString(OfficeStringId.MenuCmd_SaveDocument, "Save");
            this.AddString(OfficeStringId.MenuCmd_SaveDocumentDescription, "Save the document.");
            this.AddString(OfficeStringId.MenuCmd_SaveDocumentAs, "Save As");
            this.AddString(OfficeStringId.MenuCmd_SaveDocumentAsDescription, "Open the Save As dialog box to select a file format and save the document to a new location.");
            this.AddString(OfficeStringId.MenuCmd_Undo, "Undo");
            this.AddString(OfficeStringId.MenuCmd_UndoDescription, "Undo");
            this.AddString(OfficeStringId.MenuCmd_Redo, "Redo");
            this.AddString(OfficeStringId.MenuCmd_RedoDescription, "Redo");
            this.AddString(OfficeStringId.MenuCmd_ClearUndo, "ClearUndo");
            this.AddString(OfficeStringId.MenuCmd_ClearUndoDescription, "Clear Undo Buffer");
            this.AddString(OfficeStringId.MenuCmd_Print, "&Print");
            this.AddString(OfficeStringId.MenuCmd_PrintDescription, "Select a printer, number of copies, and other printing options before printing.");
            this.AddString(OfficeStringId.MenuCmd_QuickPrint, "&Quick Print");
            this.AddString(OfficeStringId.MenuCmd_QuickPrintDescription, "Send the document directly to the default printer without making changes.");
            this.AddString(OfficeStringId.MenuCmd_PrintPreview, "Print Pre&view");
            this.AddString(OfficeStringId.MenuCmd_PrintPreviewDescription, "Preview pages before printing.");
            this.AddString(OfficeStringId.MenuCmd_Encrypt, "Encrypt with Password");
            this.AddString(OfficeStringId.MenuCmd_EncryptDescription, "Require a password to open this workbook.");
            this.AddString(OfficeStringId.MenuCmd_CutSelection, "Cut");
            this.AddString(OfficeStringId.MenuCmd_CutSelectionDescription, "Cut the selection from the document and put it on the Clipboard.");
            this.AddString(OfficeStringId.MenuCmd_CopySelection, "Copy");
            this.AddString(OfficeStringId.MenuCmd_CopySelectionDescription, "Copy the selection and put it on the Clipboard.");
            this.AddString(OfficeStringId.MenuCmd_Paste, "Paste");
            this.AddString(OfficeStringId.MenuCmd_PasteDescription, "Paste the contents of the Clipboard.");
            this.AddString(OfficeStringId.MenuCmd_ShowPasteSpecialForm, "Paste Special");
            this.AddString(OfficeStringId.MenuCmd_ShowPasteSpecialFormDescription, "Show the Paste Special dialog box.");
            this.AddString(OfficeStringId.MenuCmd_AlignmentLeft, "Align Text Left");
            this.AddString(OfficeStringId.MenuCmd_AlignmentLeftDescription, "Align text to the left.");
            this.AddString(OfficeStringId.MenuCmd_AlignmentCenter, "Center");
            this.AddString(OfficeStringId.MenuCmd_AlignmentCenterDescription, "Center text.");
            this.AddString(OfficeStringId.MenuCmd_AlignmentRight, "Align Text Right");
            this.AddString(OfficeStringId.MenuCmd_AlignmentRightDescription, "Align text to the right.");
            this.AddString(OfficeStringId.MenuCmd_AlignmentJustify, "Justify");
            this.AddString(OfficeStringId.MenuCmd_AlignmentJustifyDescription, "Align text to both left and right margins, adding extra space between words as necessary.\r\n\r\nThis creates a clean look along the left and right side of the page.");
            this.AddString(OfficeStringId.MenuCmd_ChangeFontName, "Font");
            this.AddString(OfficeStringId.MenuCmd_ChangeFontNameDescription, "Change the font face.");
            this.AddString(OfficeStringId.MenuCmd_ChangeFontSize, "Font Size");
            this.AddString(OfficeStringId.MenuCmd_ChangeFontSizeDescription, "Change the font size.");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontBold, "Bold");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontBoldDescription, "Make the selected text bold.");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontItalic, "Italic");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontItalicDescription, "Italicize the selected text.");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontUnderline, "Underline");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontUnderlineDescription, "Underline the selected text.");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontDoubleUnderline, "Double Underline");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontDoubleUnderlineDescription, "Double underline");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontStrikeout, "Strikethrough");
            this.AddString(OfficeStringId.MenuCmd_ToggleFontStrikeoutDescription, "Draw a line through the middle of the selected text.");
            this.AddString(OfficeStringId.MenuCmd_IncreaseFontSize, "Grow Font");
            this.AddString(OfficeStringId.MenuCmd_IncreaseFontSizeDescription, "Increase the font size.");
            this.AddString(OfficeStringId.MenuCmd_DecreaseFontSize, "Shrink Font");
            this.AddString(OfficeStringId.MenuCmd_DecreaseFontSizeDescription, "Decrease the font size.");
            this.AddString(OfficeStringId.MenuCmd_ChangeFontColor, "Font Color");
            this.AddString(OfficeStringId.MenuCmd_ChangeFontColorDescription, "Change the text color.");
            this.AddString(OfficeStringId.MenuCmd_ZoomIn, "Zoom In");
            this.AddString(OfficeStringId.MenuCmd_ZoomInDescription, "Zoom In");
            this.AddString(OfficeStringId.MenuCmd_ZoomOut, "Zoom Out");
            this.AddString(OfficeStringId.MenuCmd_ZoomOutDescription, "Zoom Out");
            this.AddString(OfficeStringId.MenuCmd_Zoom100Percent, "100%");
            this.AddString(OfficeStringId.MenuCmd_Zoom100PercentDescription, "Zoom the document to 100% of the normal size.");
            this.AddString(OfficeStringId.MenuCmd_StatusBarZoomDescription, "Zoom level. Click to open the Zoom dialog box.");
            this.AddString(OfficeStringId.MenuCmd_StatusBarZoomSliderDescription, "Zoom");
            this.AddString(OfficeStringId.MenuCmd_StatusBarPopupMenuHeader, "Customize Status Bar");
            this.AddString(OfficeStringId.MenuCmd_StatusBarPopupMenuZoom, "Zoom");
            this.AddString(OfficeStringId.MenuCmd_StatusBarPopupMenuZoomSlider, "Zoom Slider");
            this.AddString(OfficeStringId.MenuCmd_Hyperlink, "Hyperlink");
            this.AddString(OfficeStringId.MenuCmd_HyperlinkDescription, "Create a link to a Web page, a picture, an e-mail address, or a program.");
            this.AddString(OfficeStringId.MenuCmd_EditHyperlink, "Edit Hyperlink...");
            this.AddString(OfficeStringId.MenuCmd_EditHyperlinkDescription, "Edit Hyperlink...");
            this.AddString(OfficeStringId.MenuCmd_InsertHyperlink, "Hyperlink...");
            this.AddString(OfficeStringId.MenuCmd_InsertHyperlinkDescription, "Add a new hyperlink.");
            this.AddString(OfficeStringId.MenuCmd_RemoveHyperlink, "Remove Hyperlink");
            this.AddString(OfficeStringId.MenuCmd_RemoveHyperlinkDescription, "Remove Hyperlink");
            this.AddString(OfficeStringId.MenuCmd_RemoveHyperlinks, "Remove Hyperlinks");
            this.AddString(OfficeStringId.MenuCmd_RemoveHyperlinksDescription, "Remove Hyperlinks");
            this.AddString(OfficeStringId.MenuCmd_OpenHyperlink, "Open Hyperlink");
            this.AddString(OfficeStringId.MenuCmd_OpenHyperlinkDescription, "Open Hyperlink");
            this.AddString(OfficeStringId.MenuCmd_InsertComment, "New Comment");
            this.AddString(OfficeStringId.MenuCmd_InsertCommentDescription, "Add a note about this part of the document.");
            this.AddString(OfficeStringId.MenuCmd_EditComment, "Edit Comment");
            this.AddString(OfficeStringId.MenuCmd_EditCommentDescription, "Add a note about this part of the document.");
            this.AddString(OfficeStringId.MenuCmd_DeleteComment, "Delete");
            this.AddString(OfficeStringId.MenuCmd_DeleteCommentDescription, "Delete the selected comment.");
            this.AddString(OfficeStringId.MenuCmd_InsertFloatingObjectPicture, "Picture");
            this.AddString(OfficeStringId.MenuCmd_InsertFloatingObjectPictureDescription, "Insert a picture from a file.");
            this.AddString(OfficeStringId.MenuCmd_InsertSymbol, "Symbol");
            this.AddString(OfficeStringId.MenuCmd_InsertSymbolDescription, "Insert symbols that are not on your keyboard, such as copyright symbols, trademark symbols, paragraph marks and Unicode characters.");
            this.AddString(OfficeStringId.MenuCmd_PageOrientationCommandGroup, "Orientation");
            this.AddString(OfficeStringId.MenuCmd_PageOrientationCommandGroupDescription, "Switch the pages between portrait and landscape layouts.");
            this.AddString(OfficeStringId.MenuCmd_PageOrientationLandscape, "Landscape");
            this.AddString(OfficeStringId.MenuCmd_PageOrientationLandscapeDescription, " ");
            this.AddString(OfficeStringId.MenuCmd_PageOrientationPortrait, "Portrait");
            this.AddString(OfficeStringId.MenuCmd_PageOrientationPortraitDescription, " ");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsNormal, "Normal\r\nTop:\t{1,10}\tBottom:\t{3,10}\r\nLeft:\t{0,10}\tRight:\t\t{2,10}");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsNormalDescription, " ");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsNarrow, "Narrow\r\nTop:\t{1,10}\tBottom:\t{3,10}\r\nLeft:\t{0,10}\tRight:\t\t{2,10}");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsNarrowDescription, " ");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsWide, "Wide\r\nTop:\t{1,10}\tBottom:\t{3,10}\r\nLeft:\t{0,10}\tRight:\t\t{2,10}");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsWideDescription, " ");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsModerate, "Moderate\r\nTop:\t{1,10}\tBottom:\t{3,10}\r\nLeft:\t{0,10}\tRight:\t\t{2,10}");
            this.AddString(OfficeStringId.MenuCmd_PageMarginsModerateDescription, " ");
            this.AddString(OfficeStringId.Caption_EditHyperlinkForm, "Edit Hyperlink");
            this.AddString(OfficeStringId.Caption_EditHyperlinkFormDescription, " ");
            this.AddString(OfficeStringId.Caption_InsertHyperlinkForm, "Insert Hyperlink");
            this.AddString(OfficeStringId.Caption_InsertHyperlinkFormDescription, " ");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectBringForwardCommandGroup, "Bring Forward");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectBringForwardCommandGroupDescription, "Bring the selected object forward one level, or bring it in front of all the other objects.");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectBringForward, "Bring Forward");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectBringForwardDescription, "Bring the selected object forward so that it is hidden by fewer objects that are in front of it.");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectBringToFront, "Bring to Front");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectBringToFrontDescription, "Bring the selected object in front of all other objects so that no part of it is hidden behind other objects.");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectSendBackwardCommandGroup, "Send Backward");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectSendBackwardCommandGroupDescription, "Send the selected object back one level, or send it behind all the other objects.");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectSendBackward, "Send Backward");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectSendBackwardDescription, "Send the selected object backward so that it is hidden by the objects that are in front of it.");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectSendToBack, "Send to Back");
            this.AddString(OfficeStringId.MenuCmd_FloatingObjectSendToBackDescription, "Send the selected object behind all other objects.");
        }
    }
}

