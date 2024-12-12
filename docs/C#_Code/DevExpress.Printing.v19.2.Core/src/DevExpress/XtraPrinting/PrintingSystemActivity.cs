namespace DevExpress.XtraPrinting
{
    using System;

    [Flags]
    public enum PrintingSystemActivity
    {
        Idle = 0,
        Preparing = 1,
        Exporting = 2,
        Printing = 4
    }
}

