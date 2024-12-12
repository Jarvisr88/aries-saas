namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public abstract class XlsExportOptionsControllerBase : ExportOptionsControllerBase
    {
        protected XlsExportOptionsControllerBase();
        protected override void CollectLineControllers(ExportOptionsBase options, List<ExportOptionKind> hiddenOptions, List<BaseLineController> list);
    }
}

