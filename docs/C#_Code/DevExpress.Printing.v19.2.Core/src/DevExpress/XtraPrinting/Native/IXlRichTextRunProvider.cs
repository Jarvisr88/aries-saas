namespace DevExpress.XtraPrinting.Native
{
    using System.Collections.Generic;

    public interface IXlRichTextRunProvider
    {
        IList<XlRichTextRun> GetTextRuns();
    }
}

