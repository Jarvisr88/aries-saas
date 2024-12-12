namespace DevExpress.Utils.Gac
{
    using System;

    [Flags]
    internal enum AssemblyNameDisplayFlags
    {
        VERSION = 1,
        CULTURE = 2,
        PUBLIC_KEY_TOKEN = 4,
        PROCESSORARCHITECTURE = 0x20,
        RETARGETABLE = 0x80,
        ALL = 0xa7
    }
}

