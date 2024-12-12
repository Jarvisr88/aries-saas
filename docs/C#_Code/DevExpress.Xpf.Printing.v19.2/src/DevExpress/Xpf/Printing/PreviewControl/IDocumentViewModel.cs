namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public interface IDocumentViewModel
    {
        event EventHandler DocumentCreated;

        event ExceptionEventHandler DocumentException;

        event EventHandler StartDocumentCreation;

        void CreateDocument();
        void Export(ExportOptionsViewModel options);
        IEnumerable<BookmarkNodeItem> GetBookmarks();
        void MarkBrick(BookmarkNodeItem bookmark);
        BrickPagePair PerformSearch(TextSearchParameter parameter);
        void Print(PrintOptionsViewModel model);
        void PrintDirect(string printerName = null);
        void ResetMarkedBricks();
        void Save(string filePath);
        void Scale(ScaleOptionsViewModel model);
        void Send(SendOptionsViewModel options);
        void SetWatermark(DevExpress.XtraPrinting.Drawing.Watermark watermark);
        void StopPageBuilding();

        PrintingSystemBase PrintingSystem { get; }

        bool IsLoaded { get; }

        bool IsCreating { get; }

        bool IsCreated { get; }

        bool HasBookmarks { get; }

        bool CanChangePageSettings { get; }

        string DefaultFileName { get; }

        string InitialDirectory { get; }

        ExportFormat DefaultExportFormat { get; set; }

        ExportFormat DefaultSendFormat { get; set; }

        IEnumerable<EditingField> EditingFields { get; }

        DevExpress.XtraPrinting.Drawing.Watermark Watermark { get; }

        XtraPageSettingsBase PageSettings { get; }

        ObservableCollection<PageViewModel> Pages { get; }

        bool CanStopPageBuilding { get; }
    }
}

