namespace DevExpress.Pdf.Native
{
    using System;

    [Flags]
    public enum PdfFontHeadTableDirectoryEntryFlags
    {
        Empty = 0,
        BaselineForFontAt0 = 1,
        LeftSidebearingPointAt0 = 2,
        InstructionsMayDependOnPointSize = 4,
        ForcePPEMToIntegerValues = 8,
        InstructionsMayAlterAdvanceWidth = 8,
        FontDataIsLossless = 0x800,
        ProduceCompatibleMetrics = 0x1000,
        OptimizedForClearType = 0x2000,
        LastResort = 0x4000
    }
}

