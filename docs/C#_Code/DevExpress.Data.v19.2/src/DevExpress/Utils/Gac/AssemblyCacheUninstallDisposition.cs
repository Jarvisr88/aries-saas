namespace DevExpress.Utils.Gac
{
    using System;

    internal enum AssemblyCacheUninstallDisposition
    {
        Unknown,
        Uninstalled,
        StillInUse,
        AlreadyUninstalled,
        DeletePending,
        HasInstallReference,
        ReferenceNotFound
    }
}

