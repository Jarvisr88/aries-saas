namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native.Lines;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class ExportFileHelper : ExportFileHelperBase
    {
        private DevExpress.Xpf.Printing.IDialogService dialogService;
        private readonly Window ownerWindow;

        public ExportFileHelper(PrintingSystemBase ps, EmailSenderBase emailSender, Window ownerWindow, DevExpress.Xpf.Printing.IDialogService dialogService) : base(ps, emailSender)
        {
            this.ownerWindow = ownerWindow;
            this.dialogService = dialogService;
        }

        protected override void CreateExportFiles(ExportOptionsBase options, IDictionary<System.Type, object[]> disabledExportModes, Action<string[]> callback)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            PrintPreviewOptions printPreview = base.ps.ExportOptions.PrintPreview;
            ExportOptionsControllerBase controllerByOptions = ExportOptionsControllerBase.GetControllerByOptions(options);
            if (ExportOptionsHelper.GetShowOptionsBeforeExport(options, printPreview.ShowOptionsBeforeExport))
            {
                ExportOptionsBase base3 = ExportOptionsHelper.CloneOptions(options);
                Converter<ILine, LineBase> converter = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Converter<ILine, LineBase> local1 = <>c.<>9__3_0;
                    converter = <>c.<>9__3_0 = line => (LineBase) line;
                }
                LineBase[] lines = Array.ConvertAll<ILine, LineBase>(controllerByOptions.GetExportLines(base3, new LineFactory(), base.ps.Document.AvailableExportModes, base.ps.ExportOptions.HiddenOptions), converter);
                if (lines.Length != 0)
                {
                    LinesWindow window = new LinesWindow {
                        WindowStyle = WindowStyle.ToolWindow,
                        Title = PreviewLocalizer.GetString(controllerByOptions.CaptionStringId),
                        Owner = this.ownerWindow
                    };
                    if (this.ownerWindow != null)
                    {
                        window.FlowDirection = this.ownerWindow.FlowDirection;
                    }
                    window.SetLines(lines);
                    bool? nullable = window.ShowDialog();
                    bool flag = true;
                    if (!((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false))
                    {
                        return;
                    }
                    options.Assign(base3);
                }
            }
            callback(this.CreateExportFilesCore(options, printPreview, controllerByOptions));
        }

        private string[] CreateExportFilesCore(ExportOptionsBase options, PrintPreviewOptions printPreviewOptions, ExportOptionsControllerBase controller)
        {
            string proposedFileName = ValidateFileName(printPreviewOptions.DefaultFileName, "Document");
            string fileName = string.Empty;
            if (ExportOptionsHelper.GetUseActionAfterExportAndSaveModeValue(options) && (printPreviewOptions.SaveMode == SaveMode.UsingDefaultPath))
            {
                string path = string.IsNullOrEmpty(printPreviewOptions.DefaultDirectory) ? Directory.GetCurrentDirectory() : printPreviewOptions.DefaultDirectory;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileName = Path.Combine(path, proposedFileName + controller.GetFileExtension(options));
            }
            else
            {
                SaveFileDialogService service = CreateSaveFileDialogService(options, printPreviewOptions, controller, proposedFileName);
                if (!service.ShowDialog(null, null) || string.IsNullOrEmpty(service.GetFullFileName()))
                {
                    return ExportFileHelperBase.EmptyStrings;
                }
                fileName = DevExpress.XtraPrinting.Native.FileHelper.SetValidExtension(service.GetFullFileName(), controller.GetFileExtension(options), controller.FileExtensions);
            }
            if (controller.ValidateInputFileName(options) && IsFileReadOnly(fileName))
            {
                object[] args = new object[] { fileName };
                MessageBoxHelper.Show(MessageBoxButton.OK, MessageBoxImage.Exclamation, PreviewStringId.Msg_FileReadOnly, args);
                return ExportFileHelperBase.EmptyStrings;
            }
            try
            {
                return controller.GetExportedFileNames(base.ps, options, fileName);
            }
            catch (IOException)
            {
                this.dialogService.ShowError(string.Format(PreviewLocalizer.GetString(PreviewStringId.Msg_CannotAccessFile), fileName), PrintingLocalizer.GetString(PrintingStringId.Error));
                return ExportFileHelperBase.EmptyStrings;
            }
            catch (OutOfMemoryException)
            {
                this.dialogService.ShowError(PreviewLocalizer.GetString(PreviewStringId.Msg_BigFileToCreate), PrintingLocalizer.GetString(PrintingStringId.Error));
                return ExportFileHelperBase.EmptyStrings;
            }
            catch (Exception exception)
            {
                this.dialogService.ShowError(exception.Message, PrintingLocalizer.GetString(PrintingStringId.Error));
                return ExportFileHelperBase.EmptyStrings;
            }
        }

        private static SaveFileDialogService CreateSaveFileDialogService(ExportOptionsBase options, PrintPreviewOptions printPreviewOptions, ExportOptionsControllerBase controller, string proposedFileName) => 
            new SaveFileDialogService { 
                Title = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title),
                ValidateNames = true,
                DefaultFileName = proposedFileName,
                Filter = controller.Filter,
                InitialDirectory = printPreviewOptions.DefaultDirectory,
                FilterIndex = controller.GetFilterIndex(options),
                OverwritePrompt = controller.ValidateInputFileName(options)
            };

        protected override bool ShouldOpenExportedFile(PreviewStringId messageId, PreviewStringId captionId) => 
            DXMessageBox.Show(PreviewLocalizer.GetString(messageId), PreviewLocalizer.GetString(captionId), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExportFileHelper.<>c <>9 = new ExportFileHelper.<>c();
            public static Converter<ILine, LineBase> <>9__3_0;

            internal LineBase <CreateExportFiles>b__3_0(ILine line) => 
                (LineBase) line;
        }
    }
}

