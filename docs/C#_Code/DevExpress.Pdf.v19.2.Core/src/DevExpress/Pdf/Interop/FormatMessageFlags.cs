namespace DevExpress.Pdf.Interop
{
    using System;

    [Flags]
    public enum FormatMessageFlags
    {
        FROM_SYSTEM = 0x1000,
        ALLOCATE_BUFFER = 0x100,
        IGNORE_INSERTS = 0x200,
        ARGUMENT_ARRAY = 0x2000
    }
}

