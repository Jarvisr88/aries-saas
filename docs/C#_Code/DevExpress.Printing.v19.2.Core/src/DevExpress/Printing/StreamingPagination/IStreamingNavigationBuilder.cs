namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.Printing.Core.Native.Navigation;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public interface IStreamingNavigationBuilder : INavigationBuilder
    {
        void SetNavigationPairs(BrickPagePairCollection bpPairs, Page page);
    }
}

