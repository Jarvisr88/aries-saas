namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using DevExpress.Xpf.Core;
    using System;
    using System.Globalization;

    public class DocumentViewerLocalizer : DXLocalizer<DocumentViewerStringId>
    {
        static DocumentViewerLocalizer()
        {
            if (GetActiveLocalizerProvider() == null)
            {
                SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<DocumentViewerStringId>(new DocumentViewerControlResXLocalizer()));
            }
        }

        public static XtraLocalizer<DocumentViewerStringId> CreateDefaultLocalizer() => 
            new DocumentViewerLocalizer();

        public override XtraLocalizer<DocumentViewerStringId> CreateResXLocalizer() => 
            new DocumentViewerControlResXLocalizer();

        public static string GetString(DocumentViewerStringId id) => 
            Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(DocumentViewerStringId.BarCaption, "Document Viewer");
            this.AddString(DocumentViewerStringId.CommandOpenCaption, "Open");
            this.AddString(DocumentViewerStringId.CommandCloseCaption, "Close");
            this.AddString(DocumentViewerStringId.CommandZoomInCaption, "Zoom In");
            this.AddString(DocumentViewerStringId.CommandZoomOutCaption, "Zoom Out");
            this.AddString(DocumentViewerStringId.CommandZoomCaption, "Zoom");
            this.AddString(DocumentViewerStringId.CommandPreviousPageCaption, "Previous");
            this.AddString(DocumentViewerStringId.CommandNextPageCaption, "Next");
            this.AddString(DocumentViewerStringId.CommandZoom10Caption, "10%");
            this.AddString(DocumentViewerStringId.CommandZoom25Caption, "25%");
            this.AddString(DocumentViewerStringId.CommandZoom50Caption, "50%");
            this.AddString(DocumentViewerStringId.CommandZoom75Caption, "75%");
            this.AddString(DocumentViewerStringId.CommandZoom100Caption, "100%");
            this.AddString(DocumentViewerStringId.CommandZoom125Caption, "125%");
            this.AddString(DocumentViewerStringId.CommandZoom150Caption, "150%");
            this.AddString(DocumentViewerStringId.CommandZoom200Caption, "200%");
            this.AddString(DocumentViewerStringId.CommandZoom400Caption, "400%");
            this.AddString(DocumentViewerStringId.CommandZoom500Caption, "500%");
            this.AddString(DocumentViewerStringId.CommandSetActualSizeZoomModeCaption, "Actual Size");
            this.AddString(DocumentViewerStringId.CommandSetPageLevelZoomModeCaption, "Zoom to Page Level");
            this.AddString(DocumentViewerStringId.CommandSetFitWidthZoomModeCaption, "Fit Width");
            this.AddString(DocumentViewerStringId.CommandSetFitVisibleZoomModeCaption, "Fit Visible");
            this.AddString(DocumentViewerStringId.CommandClockwiseRotateCaption, "Clockwise rotate");
            this.AddString(DocumentViewerStringId.CommandCounterClockwiseRotateCaption, "Counterclockwise rotate");
            this.AddString(DocumentViewerStringId.CommandPreviousViewCaption, "Previous view");
            this.AddString(DocumentViewerStringId.CommandNextViewCaption, "Next view");
            this.AddString(DocumentViewerStringId.CommandOpenDescription, "Open a document.");
            this.AddString(DocumentViewerStringId.CommandCloseDescription, "Close a document.");
            this.AddString(DocumentViewerStringId.CommandZoomInDescription, "Zoom in to get a close-up view of the document.");
            this.AddString(DocumentViewerStringId.CommandZoomOutDescription, "Zoom out to see more of the page at a reduces size.");
            this.AddString(DocumentViewerStringId.CommandZoomDescription, "Change the zoom level of the document.");
            this.AddString(DocumentViewerStringId.CommandPreviousPageDescription, "Show previous page.");
            this.AddString(DocumentViewerStringId.CommandNextPageDescription, "Show next page.");
            this.AddString(DocumentViewerStringId.NavigationRibbonGroupCaption, "Navigation");
            this.AddString(DocumentViewerStringId.ZoomRibbonGroupCaption, "Zoom");
            this.AddString(DocumentViewerStringId.FileRibbonGroupCaption, "File");
            this.AddString(DocumentViewerStringId.RotateRibbonGroupCaption, "Rotate");
            this.AddString(DocumentViewerStringId.PaginationRibbonItemStringFormat, "of {0}");
            this.AddString(DocumentViewerStringId.MessageSettingsCaption, "Settings");
            this.AddString(DocumentViewerStringId.MessageShowFindTextCaption, "Find");
            this.AddString(DocumentViewerStringId.MessageShowFindTextHintCaption, "Find Text");
            this.AddString(DocumentViewerStringId.FindControlCaseSensitive, "Case Sensitive");
            this.AddString(DocumentViewerStringId.FindControlWholeWordsOnly, "Whole Words Only");
            this.AddString(DocumentViewerStringId.FindControlPrevious, "Previous");
            this.AddString(DocumentViewerStringId.FindControlNext, "Next");
            this.AddString(DocumentViewerStringId.FindControlClose, "Close");
            this.AddString(DocumentViewerStringId.FindControlSearchCaption, "Search: ");
            this.AddString(DocumentViewerStringId.OpenFileDialogTitle, "Open");
            this.AddString(DocumentViewerStringId.DocumentMapExpandCurrentCaption, "Expand current bookmark");
            this.AddString(DocumentViewerStringId.DocumentMapExpandTopLevelCaption, "Expand top level bookmark");
            this.AddString(DocumentViewerStringId.DocumentMapCollapseTopLevelCaption, "Collapse top level bookmark");
            this.AddString(DocumentViewerStringId.DocumentMapGoToCaption, "Go to bookmark");
            this.AddString(DocumentViewerStringId.DocumentViewerInfiniteHeightExceptionMessage, "The document viewer should have a finite height.");
        }

        public static XtraLocalizer<DocumentViewerStringId> Active
        {
            get => 
                XtraLocalizer<DocumentViewerStringId>.Active;
            set
            {
                if (GetActiveLocalizerProvider() is DefaultActiveLocalizerProvider<DocumentViewerStringId>)
                {
                    XtraLocalizer<DocumentViewerStringId>.Active = value;
                }
                else
                {
                    SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<DocumentViewerStringId>(value));
                    RaiseActiveChanged();
                }
            }
        }

        public override string Language =>
            CultureInfo.CurrentUICulture.Name;
    }
}

