namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using System;

    public class MhtExportOptionsController : HtmlExportOptionsControllerBase
    {
        public override string[] GetExportedFileNames(PrintingSystemBase ps, ExportOptionsBase options, string fileName);

        protected override Type ExportOptionsType { get; }

        public override PreviewStringId CaptionStringId { get; }

        protected override string[] LocalizerStrings { get; }

        public override string[] FileExtensions { get; }
    }
}

