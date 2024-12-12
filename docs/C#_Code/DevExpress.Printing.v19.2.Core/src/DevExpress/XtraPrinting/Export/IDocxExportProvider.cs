namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public interface IDocxExportProvider : ITableExportProvider
    {
        void CreateDocument();
        void SetCheckBox(bool? checkState);
        void SetPageInfo(PageInfo pageInfo, string format, string text);
        void SetRichText(object richTextDocumentModel);

        DevExpress.XtraPrinting.Native.DocxExportContext DocxExportContext { get; }
    }
}

