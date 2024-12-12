namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting.Native;
    using System;

    internal interface IStreamingExportPageBuildEngine : IPageBuildEngine
    {
        void AfterFillPage(DocumentBand root);
        ContinuousExportInfo CreateContinuousExportInfo();
        void FillPage(DocumentBand root, IPageBuildEngine externalEngine);

        IStreamingContinuousInfo ContinuousInfo { get; }
    }
}

