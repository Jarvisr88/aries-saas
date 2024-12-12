namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.InteropServices;

    public interface IBandManager
    {
        void EnsureGroupFooter(DocumentBand parentBand);
        void EnsureReportFooterBand(DocumentBand parentBand);
        DocumentBand GetBand(DocumentBand parentBand, PageBuildInfo pageBuildInfo);
        DocumentBand GetPageFooterBand(DocumentBand parentBand);
        bool TryCollectFriends(DocumentBand docBand, out DocumentBand result);

        bool IsCompleted { get; }

        PrintingSystemBase PrintingSystem { get; }
    }
}

