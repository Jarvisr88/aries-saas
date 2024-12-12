namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;

    public class RtfExportOptionsController : ExportOptionsControllerBase
    {
        protected override void CollectLineControllers(ExportOptionsBase options, List<ExportOptionKind> hiddenOptions, List<BaseLineController> list);
        public override string[] GetExportedFileNames(PrintingSystemBase ps, ExportOptionsBase options, string fileName);
        protected override Type GetExportModeType();

        protected override Type ExportOptionsType { get; }

        public override PreviewStringId CaptionStringId { get; }

        protected override string[] LocalizerStrings { get; }

        public override string[] FileExtensions { get; }

        protected override string ExportModePropertyName { get; }
    }
}

