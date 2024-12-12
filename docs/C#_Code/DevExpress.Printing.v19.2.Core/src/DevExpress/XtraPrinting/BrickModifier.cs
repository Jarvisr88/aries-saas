namespace DevExpress.XtraPrinting
{
    using System;

    [Flags]
    public enum BrickModifier : short
    {
        None = 0,
        Detail = 1,
        DetailHeader = 2,
        DetailFooter = 4,
        ReportHeader = 8,
        ReportFooter = 0x10,
        MarginalHeader = 0x20,
        MarginalFooter = 0x40,
        InnerPageHeader = 0x80,
        InnerPageFooter = 0x100
    }
}

