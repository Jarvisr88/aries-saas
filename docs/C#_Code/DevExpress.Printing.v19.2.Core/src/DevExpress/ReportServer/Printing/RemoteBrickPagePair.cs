namespace DevExpress.ReportServer.Printing
{
    using DevExpress.XtraPrinting;
    using System;

    internal class RemoteBrickPagePair : BrickPagePair
    {
        public RemoteBrickPagePair(int[] brickIndexes, int pageIndex) : base(brickIndexes, pageIndex, (long) pageIndex)
        {
        }

        public override Page GetPage(IPageRepository pages)
        {
            Page page;
            return (pages.TryGetPageByIndex(this.PageIndex, out page) ? page : null);
        }
    }
}

