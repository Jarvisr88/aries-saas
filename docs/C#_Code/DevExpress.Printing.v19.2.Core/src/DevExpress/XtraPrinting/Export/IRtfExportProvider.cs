namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public interface IRtfExportProvider : ITableExportProvider
    {
        void SetAnchor(string anchorName);
        void SetAngle(float angle);
        void SetCellTextWithUrl(string text, string hyperLink, bool hasCrossReference);
        void SetContent(string content);
        void SetPageInfo(PageInfo pageInfo, int startPageNumber, string text, string hyperLink, bool hasCrossReference);

        DevExpress.XtraPrinting.Native.RtfExportContext RtfExportContext { get; }
    }
}

