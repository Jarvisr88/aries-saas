namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;

    [Flags]
    internal enum GCPFlags : uint
    {
        GCP_REORDER = 2,
        GCP_USEKERNING = 8,
        GCP_LIGATE = 0x20
    }
}

