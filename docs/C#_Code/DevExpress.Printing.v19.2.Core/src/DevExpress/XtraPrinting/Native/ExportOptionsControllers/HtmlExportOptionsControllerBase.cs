namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public abstract class HtmlExportOptionsControllerBase : ExportOptionsControllerBase
    {
        protected HtmlExportOptionsControllerBase();
        protected override void CollectLineControllers(ExportOptionsBase options, List<ExportOptionKind> hiddenOptions, List<BaseLineController> list);
        protected override Type GetExportModeType();
        public override bool ValidateInputFileName(ExportOptionsBase options);

        protected override string ExportModePropertyName { get; }
    }
}

