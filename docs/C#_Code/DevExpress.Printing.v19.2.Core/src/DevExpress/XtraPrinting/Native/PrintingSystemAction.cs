namespace DevExpress.XtraPrinting.Native
{
    using System;

    [Flags]
    public enum PrintingSystemAction
    {
        public const PrintingSystemAction None = PrintingSystemAction.None;,
        public const PrintingSystemAction CreateDocument = PrintingSystemAction.CreateDocument;,
        public const PrintingSystemAction HandleNewPageSettings = PrintingSystemAction.HandleNewPageSettings;,
        public const PrintingSystemAction HandleNewScaleFactor = PrintingSystemAction.HandleNewScaleFactor;
    }
}

