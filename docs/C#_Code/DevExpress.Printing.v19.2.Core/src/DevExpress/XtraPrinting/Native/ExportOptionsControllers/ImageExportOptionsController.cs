namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;

    public class ImageExportOptionsController : ExportOptionsControllerBase
    {
        protected override void CollectLineControllers(ExportOptionsBase options, List<ExportOptionKind> hiddenOptions, List<BaseLineController> list);
        public override string[] GetExportedFileNames(PrintingSystemBase ps, ExportOptionsBase options, string fileName);
        protected override Type GetExportModeType();
        public override string GetFileExtension(ExportOptionsBase options);
        public override int GetFilterIndex(ExportOptionsBase options);
        public override bool ValidateInputFileName(ExportOptionsBase options);

        protected override string[] LocalizerStrings { get; }

        public override string[] FileExtensions { get; }

        protected override Type ExportOptionsType { get; }

        public override PreviewStringId CaptionStringId { get; }

        protected override string ExportModePropertyName { get; }
    }
}

