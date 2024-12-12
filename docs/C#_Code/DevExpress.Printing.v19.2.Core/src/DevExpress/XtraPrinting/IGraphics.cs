namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.Utils;
    using System;

    public interface IGraphics : IGraphicsBase, IPrintingSystemContext, IServiceProvider, IDisposable
    {
        void AddDrawingAction(DeferredAction action);
        int GetPageCount(int basePageNumber, DefaultBoolean continuousPageNumbering);
        void ResetDrawingPage();
        void SetDrawingPage(Page page);

        float Dpi { get; }
    }
}

