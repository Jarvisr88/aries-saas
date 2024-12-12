namespace DevExpress.XtraPrinting.Native
{
    using System;

    internal class PageBuildInfoService : IPageBuildInfoService
    {
        private Func<DocumentBand, int> callback;

        public PageBuildInfoService(Func<DocumentBand, int> callback);
        int IPageBuildInfoService.GetBuildInfo(DocumentBand band);
    }
}

