namespace DevExpress.XtraPrinting.Native
{
    using System;

    internal interface IContinuousInfoExporter
    {
        void EndExport();
        void ExportCollectedBricks();
        void StartExport();
    }
}

