namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;

    public interface IXlExportProvider : ITableExportProvider
    {
        void SetCellData(object data);
        void SetRichTextRuns(IList<XlRichTextRun> richTextRuns);

        DevExpress.XtraPrinting.Native.XlExportContext XlExportContext { get; }
    }
}

