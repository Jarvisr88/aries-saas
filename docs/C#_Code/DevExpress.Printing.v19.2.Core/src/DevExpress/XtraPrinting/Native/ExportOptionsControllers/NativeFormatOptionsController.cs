namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;

    public class NativeFormatOptionsController : ExportOptionsControllerBase
    {
        public const string NativeFormatExtension = ".prnx";

        protected override void CollectLineControllers(ExportOptionsBase options, List<ExportOptionKind> hiddenOptions, List<BaseLineController> list);
        public override string[] GetExportedFileNames(PrintingSystemBase ps, ExportOptionsBase options, string fileName);

        protected override Type ExportOptionsType { get; }

        public override PreviewStringId CaptionStringId { get; }

        protected override string[] LocalizerStrings { get; }

        public override string[] FileExtensions { get; }
    }
}

