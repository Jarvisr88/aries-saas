namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.ReportServer.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class BrickPagePairFactory : IBrickPagePairFactory
    {
        private readonly IPageListService pageListService;

        public BrickPagePairFactory(IPageListService pageListService)
        {
            this.pageListService = pageListService;
        }

        public BrickPagePair CreateBrickPagePair(int[] brickIndexes, int pageIndex) => 
            new RemoteBrickPagePair(brickIndexes, pageIndex);
    }
}

