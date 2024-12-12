namespace DMEWorks.Forms.Native
{
    using System;

    [Flags]
    internal enum SLR_FLAGS
    {
        SLR_NO_UI = 1,
        SLR_ANY_MATCH = 2,
        SLR_UPDATE = 4,
        SLR_NOUPDATE = 8,
        SLR_NOSEARCH = 0x10,
        SLR_NOTRACK = 0x20,
        SLR_NOLINKINFO = 0x40,
        SLR_INVOKE_MSI = 0x80
    }
}

