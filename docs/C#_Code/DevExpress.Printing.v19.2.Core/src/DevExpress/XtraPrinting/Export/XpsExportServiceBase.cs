namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using System;
    using System.IO;

    public abstract class XpsExportServiceBase
    {
        protected XpsExportServiceBase()
        {
        }

        public abstract void Export(Stream stream, XpsExportOptions options, ProgressReflector progressReflector);
    }
}

