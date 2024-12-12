namespace DevExpress.Printing.Core.Native.Navigation
{
    using DevExpress.XtraPrinting.Native;
    using System;

    public interface INavigationBuilder
    {
        void FinalizeBuild();
        void SetNavigationPairs(BrickPagePairCollection bpPairs);
    }
}

