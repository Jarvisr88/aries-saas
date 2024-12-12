namespace DevExpress.Utils.Zip.Internal
{
    using System;

    [Flags]
    public enum ExtraFieldType
    {
        LocalHeader = 1,
        CentralDirectoryEntry = 2,
        Both = 3
    }
}

