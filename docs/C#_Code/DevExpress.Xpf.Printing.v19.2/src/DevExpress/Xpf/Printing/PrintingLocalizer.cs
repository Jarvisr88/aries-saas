﻿namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using DevExpress.Xpf.Core;
    using System;

    public class PrintingLocalizer : DXLocalizer<PrintingStringId>
    {
        static PrintingLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<PrintingStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<PrintingStringId> CreateDefaultLocalizer() => 
            new PrintingResXLocalizer();

        public override XtraLocalizer<PrintingStringId> CreateResXLocalizer() => 
            new PrintingResXLocalizer();

        public static string GetString(PrintingStringId id) => 
            XtraLocalizer<PrintingStringId>.Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(PrintingStringId.OK, "OK");
            this.AddString(PrintingStringId.Cancel, "Cancel");
            this.AddString(PrintingStringId.ToolBarCaption, "Print Preview");
            this.AddString(PrintingStringId.StatusBarCaption, "Status Bar");
            this.AddString(PrintingStringId.Print, "Print...");
            this.AddString(PrintingStringId.Print_Hint, "Specify page settings and print the document.");
            this.AddString(PrintingStringId.PrintPdf, "Print via Pdf...");
            this.AddString(PrintingStringId.PrintPdf_Hint, "Prints the current document via PDF.");
            this.AddString(PrintingStringId.PrintDirect, "Quick Print");
            this.AddString(PrintingStringId.PrintDirect_Hint, "Print the document with default page settings using the system's default printer.");
            this.AddString(PrintingStringId.PageSetup, "Page Setup...");
            this.AddString(PrintingStringId.PageSetup_Hint, "Adjust the document's page settings.");
            this.AddString(PrintingStringId.Zoom, "Zoom");
            this.AddString(PrintingStringId.Zoom_Hint, "Change the current zoom factor of the document preview.");
            this.AddString(PrintingStringId.DecreaseZoom, "Zoom Out");
            this.AddString(PrintingStringId.DecreaseZoom_Hint, "Zoom out to see more of the page at a reduced size.");
            this.AddString(PrintingStringId.IncreaseZoom, "Zoom In");
            this.AddString(PrintingStringId.IncreaseZoom_Hint, "Zoom in to get a close-up view of the document.");
            this.AddString(PrintingStringId.ZoomToPageWidth, "Page Width");
            this.AddString(PrintingStringId.ZoomToPageHeight, "Page Height");
            this.AddString(PrintingStringId.ZoomToWholePage, "Whole Page");
            this.AddString(PrintingStringId.ZoomToTwoPages, "Two Pages");
            this.AddString(PrintingStringId.FirstPage, "First Page");
            this.AddString(PrintingStringId.FirstPage_Hint, "Navigate to the document first page.");
            this.AddString(PrintingStringId.PreviousPage, "Previous Page");
            this.AddString(PrintingStringId.PreviousPage_Hint, "Navigate to the document previous page.");
            this.AddString(PrintingStringId.NextPage, "Next Page");
            this.AddString(PrintingStringId.NextPage_Hint, "Navigate to the document next page.");
            this.AddString(PrintingStringId.LastPage, "Last Page");
            this.AddString(PrintingStringId.LastPage_Hint, "Navigate to the document last page.");
            this.AddString(PrintingStringId.ExportPdf, "PDF File");
            this.AddString(PrintingStringId.ExportHtm, "HTML File");
            this.AddString(PrintingStringId.ExportMht, "MHT File");
            this.AddString(PrintingStringId.ExportRtf, "RTF File");
            this.AddString(PrintingStringId.ExportXls, "XLS File");
            this.AddString(PrintingStringId.ExportXlsx, "XLSX File");
            this.AddString(PrintingStringId.ExportCsv, "CSV File");
            this.AddString(PrintingStringId.ExportTxt, "Text File");
            this.AddString(PrintingStringId.ExportImage, "Image File");
            this.AddString(PrintingStringId.ExportXps, "XPS File");
            this.AddString(PrintingStringId.ExportDocx, "DOCX File");
            this.AddString(PrintingStringId.ExportFile, "Export...");
            this.AddString(PrintingStringId.ExportFile_Hint, "Export the document in one of the available formats and save it to a file on the disk.");
            this.AddString(PrintingStringId.Scaling, "Scale");
            this.AddString(PrintingStringId.Scaling_Hint, "Stretch or shrink the document's content to a percentage of its actual size.");
            this.AddString(PrintingStringId.Scaling_Adjust_Start_Label, "Adjust to");
            this.AddString(PrintingStringId.Scaling_Adjust_End_Label, "normal size");
            this.AddString(PrintingStringId.Scaling_Fit_Start_Label, "Fit to");
            this.AddString(PrintingStringId.Scaling_Fit_End_Label, "page(s) wide");
            this.AddString(PrintingStringId.Scaling_ComboBoxEdit_Validation_Error, "The value is not valid");
            this.AddString(PrintingStringId.Scaling_ComboBoxEdit_Validation_OutOfRange_Error, "The value is out of range");
            this.AddString(PrintingStringId.Search, "Search");
            this.AddString(PrintingStringId.Search_Hint, "Shows the Find dialog to search for an occurrence of a specified text throughout the document.");
            this.AddString(PrintingStringId.SendPdf, "PDF File");
            this.AddString(PrintingStringId.SendMht, "MHT File");
            this.AddString(PrintingStringId.SendRtf, "RTF File");
            this.AddString(PrintingStringId.SendXls, "XLS File");
            this.AddString(PrintingStringId.SendXlsx, "XLSX File");
            this.AddString(PrintingStringId.SendCsv, "CSV File");
            this.AddString(PrintingStringId.SendTxt, "Text File");
            this.AddString(PrintingStringId.SendImage, "Image File");
            this.AddString(PrintingStringId.SendXps, "XPS File");
            this.AddString(PrintingStringId.SendFile, "Send...");
            this.AddString(PrintingStringId.SendFile_Hint, "Export the document in one of the available formats and attach it to an e-mail.");
            this.AddString(PrintingStringId.StopPageBuilding, "Stop");
            this.AddString(PrintingStringId.CurrentPageDisplayFormat, "Page {0} of {1}");
            this.AddString(PrintingStringId.ZoomDisplayFormat, "Zoom: {0:0}%");
            this.AddString(PrintingStringId.MsgCaption, "DXPrinting");
            this.AddString(PrintingStringId.GoToPage, "Page:");
            this.AddString(PrintingStringId.PrintPreviewWindowCaption, "Print Preview");
            this.AddString(PrintingStringId.DefaultPrintJobDescription, "Document");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_Title, "PDF Password Security");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_RequireOpenPassword, "Require a password to open the document");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_OpenPassword, "Document open password:");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_OpenPasswordHeader, "Document Open Password");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_RestrictPermissions, "Restrict editing and printing of the document");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_PermissionsPassword, "Change permissions password:");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_PrintingPermissions, "Printing allowed:");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_ChangingPermissions, "Changes allowed:");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_EnableCopying, "Enable copying of text, images and other content");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_EnableScreenReaders, "Enable text access for screen reader devices for the visually impaired");
            this.AddString(PrintingStringId.PdfPasswordSecurityOptions_Permissions, "Permissions");
            this.AddString(PrintingStringId.RepeatPassword_OpenPassword_Title, "Confirm Document Open Password");
            this.AddString(PrintingStringId.RepeatPassword_OpenPassword_Note, "Please confirm the Document Open Password. Be sure to make a note of the password. It will be required to open the document.");
            this.AddString(PrintingStringId.RepeatPassword_PermissionsPassword_Title, "Confirm Permissions Password");
            this.AddString(PrintingStringId.RepeatPassword_PermissionsPassword_Note, "Please confirm the Permissions Password. Be sure to make a note of the password. You will need it to change these settings in the future.");
            this.AddString(PrintingStringId.Watermark, "Watermark");
            this.AddString(PrintingStringId.Watermark_Hint, "Insert ghosted text and/or image behind the page content.");
            this.AddString(PrintingStringId.WatermarkTitle, "Watermark");
            this.AddString(PrintingStringId.PictureWatermarkTitle, "Watermark");
            this.AddString(PrintingStringId.TextWatermarkTitle, "Text Watermark");
            this.AddString(PrintingStringId.PictureWatermarkTitle, "Picture Watermark");
            this.AddString(PrintingStringId.WatermarkText, "Text:");
            this.AddString(PrintingStringId.WatermarkTextDirection, "Direction:");
            this.AddString(PrintingStringId.WatermarkTextColor, "Color:");
            this.AddString(PrintingStringId.WatermarkFontName, "Font:");
            this.AddString(PrintingStringId.WatermarkFontSize, "Size:");
            this.AddString(PrintingStringId.WatermarkFontBold, "Bold");
            this.AddString(PrintingStringId.WatermarkFontItalic, "Italic");
            this.AddString(PrintingStringId.WatermarkTransparency, "Transparency:");
            this.AddString(PrintingStringId.WatermarkPosition, "Position");
            this.AddString(PrintingStringId.WatermarkPositionInFront, "In front");
            this.AddString(PrintingStringId.WatermarkPositionBehind, "Behind");
            this.AddString(PrintingStringId.WatermarkPageRange, "Page Range");
            this.AddString(PrintingStringId.WatermarkPageRangeAllPages, "All");
            this.AddString(PrintingStringId.WatermarkPageRangePages, "Pages");
            this.AddString(PrintingStringId.WatermarkPageRangeHint, "For example: 1,3,5-12");
            this.AddString(PrintingStringId.WatermarkLoadImage, "Image:");
            this.AddString(PrintingStringId.WatermarkImageSizeMode, "Size mode:");
            this.AddString(PrintingStringId.WatermarkImageHorizontalAlignment, "Horizontal alignment:");
            this.AddString(PrintingStringId.WatermarkImageVerticalAlignment, "Vertical alignment:");
            this.AddString(PrintingStringId.WatermarkImageTiling, "Tiling");
            this.AddString(PrintingStringId.WatermarkClearAll, "Clear All");
            this.AddString(PrintingStringId.WatermarkImageLoadError, "File is corrupted");
            this.AddString(PrintingStringId.RepeatPassword_PermissionsPassword, "Permissions password:");
            this.AddString(PrintingStringId.RepeatPassword_OpenPassword, "Document open password:");
            this.AddString(PrintingStringId.RepeatPassword_ConfirmationPasswordDoesNotMatch, "Confirmation password does not match. Please start over and enter the password again.");
            this.AddString(PrintingStringId.PageSetupMarginsCaptionFormat, "Margins in {0}");
            this.AddString(PrintingStringId.PageSetupPageMargins, "Page Margins");
            this.AddString(PrintingStringId.PageSetupPrinterCaption, "Printer");
            this.AddString(PrintingStringId.PageSetupPrinter, "Printer:");
            this.AddString(PrintingStringId.PageSetupPrinterType, "Type:");
            this.AddString(PrintingStringId.PageSetupPrinterPort, "Port:");
            this.AddString(PrintingStringId.PageSetupPrinterComment, "Comment:");
            this.AddString(PrintingStringId.PageSetupPaperCaption, "Paper");
            this.AddString(PrintingStringId.PageSetupPaperSize, "Paper size:");
            this.AddString(PrintingStringId.PageSetupOrientationCaption, "Orientation:");
            this.AddString(PrintingStringId.PageSetupOrientationPortrait, "Portrait");
            this.AddString(PrintingStringId.PageSetupOrientationLandscape, "Landscape");
            this.AddString(PrintingStringId.PageSetupMarginsLeft, "Left:");
            this.AddString(PrintingStringId.PageSetupMarginsTop, "Top:");
            this.AddString(PrintingStringId.PageSetupMarginsRight, "Right:");
            this.AddString(PrintingStringId.PageSetupMarginsBottom, "Bottom:");
            this.AddString(PrintingStringId.PageSetupMillimeters, "Millimeters");
            this.AddString(PrintingStringId.PageSetupInches, "Inches");
            this.AddString(PrintingStringId.PageSetupSize, "Size");
            this.AddString(PrintingStringId.PageSetupWidth, "Width");
            this.AddString(PrintingStringId.PageSetupHeight, "Height");
            this.AddString(PrintingStringId.PageSetupUnits, "Units");
            this.AddString(PrintingStringId.Parameters, "Parameters");
            this.AddString(PrintingStringId.Parameters_Hint, "Shows or hides the Parameters panel, where you can customize the values of report parameters.");
            this.AddString(PrintingStringId.ParametersReset, "Reset");
            this.AddString(PrintingStringId.ParametersSubmit, "Submit");
            this.AddString(PrintingStringId.ZoomValueItemFormat, "{0}%");
            this.AddString(PrintingStringId.Open, "Open");
            this.AddString(PrintingStringId.Open_Hint, "Open a report document.");
            this.AddString(PrintingStringId.Save, "Save");
            this.AddString(PrintingStringId.Save_Hint, "Save the report document.");
            this.AddString(PrintingStringId.Error, "Error");
            this.AddString(PrintingStringId.PageSetupWindowTitle, "Page Setup");
            this.AddString(PrintingStringId.DocumentMap, "Document Map");
            this.AddString(PrintingStringId.DocumentMap_Hint, "Shows the Document Map panel, which reflects the document's structure, and where you can navigate through the report's bookmarks.");
            this.AddString(PrintingStringId.Refresh, "Refresh");
            this.AddString(PrintingStringId.Information, "Information");
            this.AddString(PrintingStringId.Search_EmptyResult, "Your search did not match any text.");
            this.AddString(PrintingStringId.PreparingPages, "Preparing pages...");
            this.AddString(PrintingStringId.PagesArePrepared, "Pages are ready. Continue printing?");
            this.AddString(PrintingStringId.ExportPdfToWindow, "PDF File");
            this.AddString(PrintingStringId.ExportHtmToWindow, "HTML File");
            this.AddString(PrintingStringId.ExportMhtToWindow, "MHT File");
            this.AddString(PrintingStringId.ExportRtfToWindow, "RTF File");
            this.AddString(PrintingStringId.ExportXlsToWindow, "XLS File");
            this.AddString(PrintingStringId.ExportXlsxToWindow, "XLSX File");
            this.AddString(PrintingStringId.ExportCsvToWindow, "CSV File");
            this.AddString(PrintingStringId.ExportTxtToWindow, "Text File");
            this.AddString(PrintingStringId.ExportImageToWindow, "Image File");
            this.AddString(PrintingStringId.ExportXpsToWindow, "XPS File");
            this.AddString(PrintingStringId.ExportFileToWindow, "Export Document to Window...");
            this.AddString(PrintingStringId.ExportFileToWindow_Hint, "Exports the current document, and shows the result in a new browser window.");
            this.AddString(PrintingStringId.ClosePreview, "Close");
            this.AddString(PrintingStringId.Exception_NoPrinterFound, "No printer has been found on the machine");
            this.AddString(PrintingStringId.Msg_EmptyDocument, "The document does not contain any pages.");
            this.AddString(PrintingStringId.Msg_Waiting_ForParameterValues, "Waiting for parameter values.");
            this.AddString(PrintingStringId.Msg_DocumentIsPrinting, "Document is printing.");
            this.AddString(PrintingStringId.PdfSignatureEditorWindow_Certificate, "Certificate:");
            this.AddString(PrintingStringId.PdfSignatureEditorWindow_Reason, "Reason:");
            this.AddString(PrintingStringId.PdfSignatureEditorWindow_Location, "Location:");
            this.AddString(PrintingStringId.PdfSignatureEditorWindow_ContactInformation, "Contact Information:");
            this.AddString(PrintingStringId.PdfSignatureEditorWindow_Title, "Signature Options");
            this.AddString(PrintingStringId.PaperKind_Custom, "Custom");
            this.AddString(PrintingStringId.PaperKind_Letter, "Letter");
            this.AddString(PrintingStringId.PaperKind_LetterSmall, "Letter Small");
            this.AddString(PrintingStringId.PaperKind_Tabloid, "Tabloid");
            this.AddString(PrintingStringId.PaperKind_Ledger, "Ledger");
            this.AddString(PrintingStringId.PaperKind_Legal, "Legal");
            this.AddString(PrintingStringId.PaperKind_Statement, "Statement");
            this.AddString(PrintingStringId.PaperKind_Executive, "Executive");
            this.AddString(PrintingStringId.PaperKind_A3, "A3");
            this.AddString(PrintingStringId.PaperKind_A4, "A4");
            this.AddString(PrintingStringId.PaperKind_A4Small, "A4 Small");
            this.AddString(PrintingStringId.PaperKind_A5, "A5");
            this.AddString(PrintingStringId.PaperKind_B4, "B4");
            this.AddString(PrintingStringId.PaperKind_B5, "B5");
            this.AddString(PrintingStringId.PaperKind_Folio, "Folio");
            this.AddString(PrintingStringId.PaperKind_Quarto, "Quarto");
            this.AddString(PrintingStringId.PaperKind_Standard10x14, "Standard 10x14");
            this.AddString(PrintingStringId.PaperKind_Standard11x17, "Standard 11x17");
            this.AddString(PrintingStringId.PaperKind_Note, "Note");
            this.AddString(PrintingStringId.PaperKind_Number9Envelope, "Number 9 Envelope");
            this.AddString(PrintingStringId.PaperKind_Number10Envelope, "Number 10 Envelope");
            this.AddString(PrintingStringId.PaperKind_Number11Envelope, "Number 11 Envelope");
            this.AddString(PrintingStringId.PaperKind_Number12Envelope, "Number 12 Envelope");
            this.AddString(PrintingStringId.PaperKind_Number14Envelope, "Number 14 Envelope");
            this.AddString(PrintingStringId.PaperKind_CSheet, "C Sheet");
            this.AddString(PrintingStringId.PaperKind_DSheet, "D Sheet");
            this.AddString(PrintingStringId.PaperKind_ESheet, "E Sheet");
            this.AddString(PrintingStringId.PaperKind_DLEnvelope, "DL Envelope");
            this.AddString(PrintingStringId.PaperKind_C5Envelope, "C5 Envelope");
            this.AddString(PrintingStringId.PaperKind_C3Envelope, "C3 Envelope");
            this.AddString(PrintingStringId.PaperKind_C4Envelope, "C4 Envelope");
            this.AddString(PrintingStringId.PaperKind_C6Envelope, "C6 Envelope");
            this.AddString(PrintingStringId.PaperKind_C65Envelope, "C65 Envelope");
            this.AddString(PrintingStringId.PaperKind_B4Envelope, "B4 Envelope");
            this.AddString(PrintingStringId.PaperKind_B5Envelope, "B5 Envelope");
            this.AddString(PrintingStringId.PaperKind_B6Envelope, "B6 Envelope");
            this.AddString(PrintingStringId.PaperKind_ItalyEnvelope, "Italy Envelope");
            this.AddString(PrintingStringId.PaperKind_MonarchEnvelope, "Monarch Envelope");
            this.AddString(PrintingStringId.PaperKind_PersonalEnvelope, "Personal Envelope (6 3/4)");
            this.AddString(PrintingStringId.PaperKind_USStandardFanfold, "US Standard Fanfold");
            this.AddString(PrintingStringId.PaperKind_GermanStandardFanfold, "German Standard Fanfold");
            this.AddString(PrintingStringId.PaperKind_GermanLegalFanfold, "German Legal Fanfold");
            this.AddString(PrintingStringId.PaperKind_IsoB4, "Iso B4");
            this.AddString(PrintingStringId.PaperKind_JapanesePostcard, "Japanese Postcard");
            this.AddString(PrintingStringId.PaperKind_Standard9x11, "Standard 9x11");
            this.AddString(PrintingStringId.PaperKind_Standard10x11, "Standard 10x11");
            this.AddString(PrintingStringId.PaperKind_Standard15x11, "Standard 15x11");
            this.AddString(PrintingStringId.PaperKind_InviteEnvelope, "Invite Envelope");
            this.AddString(PrintingStringId.PaperKind_LetterExtra, "Letter Extra");
            this.AddString(PrintingStringId.PaperKind_LegalExtra, "Legal Extra");
            this.AddString(PrintingStringId.PaperKind_TabloidExtra, "Tabloid Extra");
            this.AddString(PrintingStringId.PaperKind_A4Extra, "A4 Extra");
            this.AddString(PrintingStringId.PaperKind_LetterTransverse, "Letter Transverse");
            this.AddString(PrintingStringId.PaperKind_A4Transverse, "A4 Transverse");
            this.AddString(PrintingStringId.PaperKind_LetterExtraTransverse, "Letter Extra Transverse");
            this.AddString(PrintingStringId.PaperKind_APlus, "SuperA/SuperA/A4");
            this.AddString(PrintingStringId.PaperKind_BPlus, "SuperB/SuperB/A3");
            this.AddString(PrintingStringId.PaperKind_LetterPlus, "Letter Plus");
            this.AddString(PrintingStringId.PaperKind_A4Plus, "A4 Plus");
            this.AddString(PrintingStringId.PaperKind_A5Transverse, "A5 Transverse");
            this.AddString(PrintingStringId.PaperKind_B5Transverse, "JIS B5 Transverse");
            this.AddString(PrintingStringId.PaperKind_A3Extra, "A3 Extra");
            this.AddString(PrintingStringId.PaperKind_A5Extra, "A5 Extra");
            this.AddString(PrintingStringId.PaperKind_B5Extra, "ISO B5 Extra");
            this.AddString(PrintingStringId.PaperKind_A2, "A2");
            this.AddString(PrintingStringId.PaperKind_A3Transverse, "A3 Transverse");
            this.AddString(PrintingStringId.PaperKind_A3ExtraTransverse, "A3 Extra Transverse");
            this.AddString(PrintingStringId.PaperKind_JapaneseDoublePostcard, "Japanese Double Postcard");
            this.AddString(PrintingStringId.PaperKind_A6, "A6");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeKakuNumber2, "Japanese Envelope Kaku Number 2");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeKakuNumber3, "Japanese Envelope Kaku Number 3");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeChouNumber3, "Japanese Envelope Chou Number 3");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeChouNumber4, "Japanese Envelope Chou Number 4");
            this.AddString(PrintingStringId.PaperKind_LetterRotated, "Letter Rotated");
            this.AddString(PrintingStringId.PaperKind_A3Rotated, "A3 Rotated");
            this.AddString(PrintingStringId.PaperKind_A4Rotated, "A4 Rotated");
            this.AddString(PrintingStringId.PaperKind_A5Rotated, "A5 Rotated");
            this.AddString(PrintingStringId.PaperKind_B4JisRotated, "JIS B4 Rotated ");
            this.AddString(PrintingStringId.PaperKind_B5JisRotated, "JIS B5 Rotated");
            this.AddString(PrintingStringId.PaperKind_JapanesePostcardRotated, "Japanese Postcard Rotated");
            this.AddString(PrintingStringId.PaperKind_JapaneseDoublePostcardRotated, "Japanese Double Postcard Rotated");
            this.AddString(PrintingStringId.PaperKind_A6Rotated, "A6 Rotated");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeKakuNumber2Rotated, "Japanese Envelope Kaku Number 2 Rotated");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeKakuNumber3Rotated, "Japanese Envelope Kaku Number 3 Rotated");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeChouNumber3Rotated, "Japanese Envelope Chou Number 3 Rotated");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeChouNumber4Rotated, "Japanese Envelope Chou Number 4 Rotated");
            this.AddString(PrintingStringId.PaperKind_B6Jis, "JIS B6");
            this.AddString(PrintingStringId.PaperKind_B6JisRotated, "JIS B6 Rotated");
            this.AddString(PrintingStringId.PaperKind_Standard12x11, "Standard 12x11");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeYouNumber4, "Japanese Envelope You Number 4");
            this.AddString(PrintingStringId.PaperKind_JapaneseEnvelopeYouNumber4Rotated, "Japanese Envelope You Number 4 Rotated");
            this.AddString(PrintingStringId.PaperKind_Prc16K, "Prc 16K");
            this.AddString(PrintingStringId.PaperKind_Prc32K, "Prc 32K");
            this.AddString(PrintingStringId.PaperKind_Prc32KBig, "Prc 32K Big");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber1, "Prc Envelope Number 1");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber2, "Prc Envelope Number 2");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber3, "Prc Envelope Number 3");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber4, "Prc Envelope Number 4");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber5, "Prc Envelope Number 5");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber6, "Prc Envelope Number 6");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber7, "Prc Envelope Number 7");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber8, "Prc Envelope Number 8");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber9, "Prc Envelope Number 9");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber10, "Prc Envelope Number 10");
            this.AddString(PrintingStringId.PaperKind_Prc16KRotated, "Prc 16K Rotated");
            this.AddString(PrintingStringId.PaperKind_Prc32KRotated, "Prc 32K Rotated");
            this.AddString(PrintingStringId.PaperKind_Prc32KBigRotated, "Prc 32K Big Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber1Rotated, "Prc Envelope Number 1 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber2Rotated, "Prc Envelope Number 2 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber3Rotated, "Prc Envelope Number 3 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber4Rotated, "Prc Envelope Number 4 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber5Rotated, "Prc Envelope Number 5 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber6Rotated, "Prc Envelope Number 6 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber7Rotated, "Prc Envelope Number 7 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber8Rotated, "Prc Envelope Number 8 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber9Rotated, "Prc Envelope Number 9 Rotated");
            this.AddString(PrintingStringId.PaperKind_PrcEnvelopeNumber10Rotated, "Prc Envelope Number 10 Rotated");
            this.AddString(PrintingStringId.RibbonPageGroup_File, "File");
            this.AddString(PrintingStringId.RibbonPageGroup_Print, "Print");
            this.AddString(PrintingStringId.RibbonPageGroup_Navigation, "Navigation");
            this.AddString(PrintingStringId.RibbonPageGroup_Zoom, "Zoom");
            this.AddString(PrintingStringId.RibbonPageGroup_Export, "Export");
            this.AddString(PrintingStringId.RibbonPageGroup_Document, "Document");
            this.AddString(PrintingStringId.RibbonPageGroup_Tools, "Tools");
            this.AddString(PrintingStringId.RibbonPageGroup_View, "View");
            this.AddString(PrintingStringId.RibbonPageCaption, "Preview");
            this.AddString(PrintingStringId.True, "True");
            this.AddString(PrintingStringId.False, "False");
            this.AddString(PrintingStringId.HandTool, "Hand Tool");
            this.AddString(PrintingStringId.SelectTool, "Select Tool");
            this.AddString(PrintingStringId.Copy, "Copy");
            this.AddString(PrintingStringId.FilePath, "File path");
            this.AddString(PrintingStringId.OpenFileAfterExport, "Open file after exporting");
            this.AddString(PrintingStringId.ExportFormat, "Export format");
            this.AddString(PrintingStringId.MoreOptions, "More Options");
            this.AddString(PrintingStringId.Printer, "Printer");
            this.AddString(PrintingStringId.Preferences, "Preferences");
            this.AddString(PrintingStringId.PrinterStatus, "Status");
            this.AddString(PrintingStringId.PrinterLocation, "Location");
            this.AddString(PrintingStringId.PrinterComment, "Comment");
            this.AddString(PrintingStringId.PrinterQueue, "Document(s) in Queue");
            this.AddString(PrintingStringId.Copies, "Number of copies");
            this.AddString(PrintingStringId.Collate, "Collate");
            this.AddString(PrintingStringId.PageRange, "Page range");
            this.AddString(PrintingStringId.AllPages, "All pages");
            this.AddString(PrintingStringId.CurrentPage, "Current");
            this.AddString(PrintingStringId.SomePages, "Some pages");
            this.AddString(PrintingStringId.PageRangeHint, "For example: 1,3,5-12");
            this.AddString(PrintingStringId.PaperSource, "Paper source");
            this.AddString(PrintingStringId.PrintFilePath, "File path");
            this.AddString(PrintingStringId.PrintToFile, "Print to file");
            this.AddString(PrintingStringId.DuplexMode, "Print on both sides");
            this.AddString(PrintingStringId.DuplexMode_Simplex, "None");
            this.AddString(PrintingStringId.DuplexMode_Vertical, "Flip on Long Edge");
            this.AddString(PrintingStringId.DuplexMode_Horizontal, "Flip on Short Edge");
            this.AddString(PrintingStringId.PrintAllPages, "Print All Pages");
            this.AddString(PrintingStringId.PrintAllPagesDescription, "The whole document");
            this.AddString(PrintingStringId.PrintCurrentPage, "Print Current Page");
            this.AddString(PrintingStringId.PrintCurrentPageDescription, "Just this page");
            this.AddString(PrintingStringId.PrintCustomPages, "Custom Print");
            this.AddString(PrintingStringId.PrintCustomPagesDescription, "Enter page numbers and/or page ranges");
            this.AddString(PrintingStringId.PrintOneSided, "Print One Sided");
            this.AddString(PrintingStringId.PrintOneSidedDescription, "Only print one side of the page");
            this.AddString(PrintingStringId.PrintOnBothSides, "Print on Both Sides");
            this.AddString(PrintingStringId.PrintCopies, "Copies");
            this.AddString(PrintingStringId.Collated, "Collated");
            this.AddString(PrintingStringId.Uncollated, "Uncollated");
            this.AddString(PrintingStringId.PrintPreviewTitle, "Print");
            this.AddString(PrintingStringId.PrintPreviewSettings, "Settings");
            this.AddString(PrintingStringId.NavigationPane_ButtonCaption, "Navigation Pane");
            this.AddString(PrintingStringId.NavigationPane_ButtonHint, "Show the Navigation Pane, which allows you to search for a specified text and navigate through the document.");
            this.AddString(PrintingStringId.NavigationPane_Title, "Navigation");
            this.AddString(PrintingStringId.NavigationPane_Searching, "Searching...");
            this.AddString(PrintingStringId.NavigationPane_SearchBoxPlaceholder, "Enter text to search...");
            this.AddString(PrintingStringId.NavigationPane_NoMatches, "No matches");
            this.AddString(PrintingStringId.NavigationPane_NoMatchesFound, "No matches were found for the specified search. Try to enter another text to search.");
            this.AddString(PrintingStringId.NavigationPane_ResultIndex, "Result {0} of {1}");
            this.AddString(PrintingStringId.NavigationPane_EnterTextToSearch, "Enter text in the search box above to start your search.");
            this.AddString(PrintingStringId.NavigationPane_DocumentMapTabCaption, "Document Map");
            this.AddString(PrintingStringId.NavigationPane_PagesTabCaption, "Pages");
            this.AddString(PrintingStringId.NavigationPane_SearchResultsTabCaption, "Search Results");
            this.AddString(PrintingStringId.NavigationPane_SearchResultHint, "[Page {0}] {1}");
            this.AddString(PrintingStringId.NavigationPane_NoBookmarks, "No bookmarks to display");
            this.AddString(PrintingStringId.NavigationPane_ResultCount, "{0} results");
            this.AddString(PrintingStringId.DocumentSourceNotSupported, "This type of document source is not supported.");
            this.AddString(PrintingStringId.SearchFinished_NoMatchesFound, "Finished searching throughout the document. No matches were found.");
            this.AddString(PrintingStringId.Thumbnails, "Thumbnails");
            this.AddString(PrintingStringId.Thumbnails_Hint, "Open the Thumbnails, which allows you to navigate through the document.");
            this.AddString(PrintingStringId.EditingFields, "Editing Fields");
            this.AddString(PrintingStringId.EditingFields_Hint, "Highlight all editing fields to quickly discover which of the document elements are editable.");
            this.AddString(PrintingStringId.PageLayout, "Page Layout");
            this.AddString(PrintingStringId.PageLayout_Hint, "Select how many pages to show side-by-side.");
            this.AddString(PrintingStringId.PageLayout_SinglePage, "Single Page");
            this.AddString(PrintingStringId.PageLayout_SinglePage_Hint, "Show only one page at a time.");
            this.AddString(PrintingStringId.PageLayout_TwoPages, "Two Pages");
            this.AddString(PrintingStringId.PageLayout_TwoPages_Hint, "Show two pages side by side.");
            this.AddString(PrintingStringId.PageLayout_WrapPages, "Wrap Pages");
            this.AddString(PrintingStringId.PageLayout_WrapPages_Hint, "Show as many pages side by side, as the current zoom factor allows.");
            this.AddString(PrintingStringId.EnableContinuousScrolling, "Enable Continuous Scrolling");
            this.AddString(PrintingStringId.EnableContinuousScrolling_Hint, "Enable continuous scrolling between pages in a single or two pages view.");
            this.AddString(PrintingStringId.ShowCoverPage, "Show Cover Page");
            this.AddString(PrintingStringId.ShowCoverPage_Hint, "Show the document's first page separately or alongside the next page in the two pages view.");
            this.AddString(PrintingStringId.ReportBehavior_RibbonReportsCaption, "Reports");
            this.AddString(PrintingStringId.ReportBehavior_MenuPrintPreview, "Print Preview...");
            this.AddString(PrintingStringId.ReportBehavior_MenuDesignReport, "Design Report...");
            this.AddString(PrintingStringId.ReportBehavior_MenuPrint, "Print...");
            this.AddString(PrintingStringId.ReportBehavior_MenuEdit, "Edit...");
            this.AddString(PrintingStringId.ReportBehavior_MenuRename, "Rename...");
            this.AddString(PrintingStringId.ReportBehavior_MenuDelete, "Delete");
            this.AddString(PrintingStringId.ReportBehavior_DeleteDialogCaption, "Delete Report");
            this.AddString(PrintingStringId.ReportBehavior_DeleteDialogMessage, "Are you sure you want to delete report {0}?");
            this.AddString(PrintingStringId.ReportBehavior_RenameDialogCaption, "Rename Report");
            this.AddString(PrintingStringId.ReportBehavior_RenameDialogNameLabel, "Name");
            this.AddString(PrintingStringId.ReportBehavior_SaveDialogCaption, "Save Report");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_SelectTheme, "Select Theme");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_WindowTitle, "Report Wizard");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_SelectPageOptions, "Select Page Options");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_Paper, "Paper");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_PaperSize, "Size");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_ReportUnits, "Units");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_PageOrientation, "Orientation");
            this.AddString(PrintingStringId.ReportManagerServiceWizard_PageSetup, "Page Setup");
            this.AddString(PrintingStringId.PdfPasswordSecurityEncryptionLevel, "Encryption Level");
        }
    }
}
