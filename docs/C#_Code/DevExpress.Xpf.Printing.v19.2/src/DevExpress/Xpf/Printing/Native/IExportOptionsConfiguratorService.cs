namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IExportOptionsConfiguratorService
    {
        event EventHandler<AsyncCompletedEventArgs> Completed;

        void Configure(ExportOptionsBase options, PrintPreviewOptions previewOptions, AvailableExportModes availableExportModes, List<ExportOptionKind> hiddenOptions);

        ExportOptionContext Context { get; set; }
    }
}

