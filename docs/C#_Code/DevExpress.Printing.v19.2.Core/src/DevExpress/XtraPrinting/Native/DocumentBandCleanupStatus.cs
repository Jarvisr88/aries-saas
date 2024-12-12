namespace DevExpress.XtraPrinting.Native
{
    using System;

    [Flags]
    internal enum DocumentBandCleanupStatus
    {
        public const DocumentBandCleanupStatus None = DocumentBandCleanupStatus.None;,
        public const DocumentBandCleanupStatus BricksCleaned = DocumentBandCleanupStatus.BricksCleaned;,
        public const DocumentBandCleanupStatus BandsCleaned = DocumentBandCleanupStatus.BandsCleaned;,
        public const DocumentBandCleanupStatus AllCleaned = DocumentBandCleanupStatus.AllCleaned;
    }
}

