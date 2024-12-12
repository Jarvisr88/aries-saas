namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using System;

    internal interface ICleanupBandVisitor
    {
        void Visit(Page page);
    }
}

