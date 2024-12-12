namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public interface IHtmlExportProvider : ITableExportProvider
    {
        void RaiseHtmlItemCreated(VisualBrick brick);
        void SetAnchor(string anchorName);
        void SetNavigationUrl(VisualBrick brick);

        DevExpress.XtraPrinting.Native.HtmlExportContext HtmlExportContext { get; }

        DXHtmlContainerControl CurrentCell { get; }

        Rectangle CurrentCellBounds { get; }
    }
}

