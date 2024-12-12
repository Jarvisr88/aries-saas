namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing.Native.Lines;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ExportOptionsConfiguratorService : IExportOptionsConfiguratorService
    {
        public event EventHandler<AsyncCompletedEventArgs> Completed;

        public void Configure(ExportOptionsBase options, PrintPreviewOptions previewOptions, AvailableExportModes availableExportModes, List<ExportOptionKind> hiddenOptions)
        {
            if (previewOptions.ShowOptionsBeforeExport)
            {
                this.ShowConfigurationWindow(options, availableExportModes, hiddenOptions);
            }
            else
            {
                this.RaiseCompleted(false);
            }
        }

        private void OnLinesWindowClosed(LinesWindow linesWindow, ExportOptionsBase options, ExportOptionsBase clonedOptions)
        {
            bool cancelled = (linesWindow.DialogResult != null) ? !linesWindow.DialogResult.Value : true;
            if (!cancelled)
            {
                options.Assign(clonedOptions);
            }
            this.RaiseCompleted(cancelled);
        }

        private void RaiseCompleted(bool cancelled)
        {
            if (this.Completed != null)
            {
                this.Completed(this, new AsyncCompletedEventArgs(null, cancelled, null));
            }
        }

        private void ShowConfigurationWindow(ExportOptionsBase options, AvailableExportModes availableExportModes, List<ExportOptionKind> hiddenOptions)
        {
            ExportOptionsControllerBase controllerByOptions = ExportOptionsControllerBase.GetControllerByOptions(options);
            ExportOptionsBase clonedOptions = ExportOptionsHelper.CloneOptions(options);
            LineBase[] lines = controllerByOptions.GetExportLines(clonedOptions, new LineFactory(), availableExportModes, hiddenOptions).Cast<LineBase>().ToArray<LineBase>();
            if (lines.Length != 0)
            {
                LinesWindow window1 = new LinesWindow();
                window1.Title = PreviewLocalizer.GetString(controllerByOptions.CaptionStringId);
                LinesWindow linesWindow = window1;
                linesWindow.Closed += (o, e) => this.OnLinesWindowClosed(linesWindow, options, clonedOptions);
                linesWindow.SetLines(lines);
                linesWindow.ShowDialog();
            }
        }

        public ExportOptionContext Context { get; set; }
    }
}

