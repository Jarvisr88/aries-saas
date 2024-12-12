namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IAfterPrintOnPageProcessor
    {
        void Process(Page page);

        float MaxBrickRight { get; }
    }
}

