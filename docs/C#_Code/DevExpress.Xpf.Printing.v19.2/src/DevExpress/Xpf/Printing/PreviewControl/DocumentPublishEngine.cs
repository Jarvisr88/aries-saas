namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Printing.Native;
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.ReportServer.Printing;
    using DevExpress.ReportServer.Printing.Services;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Exports;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
    using System;
    using System.ComponentModel;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class DocumentPublishEngine
    {
        private readonly PrintingSystemBase printingSystem;

        public DocumentPublishEngine(PrintingSystemBase printingSystem)
        {
            Guard.ArgumentNotNull(printingSystem, "printingSystem");
            this.printingSystem = printingSystem;
        }

        public void Export(ExportOptionsViewModel options)
        {
            if (this.printingSystem is RemotePrintingSystem)
            {
                this.RemoteExport(options, delegate (string fileName) {
                    if (options.OpenFileAfterExport && !string.IsNullOrEmpty(fileName))
                    {
                        ProcessLaunchHelper.StartProcess(fileName, false);
                    }
                });
            }
            else
            {
                this.Export(options.ExportOptions, options.FileName, delegate (string[] fileNames) {
                    this.printingSystem.ExportOptions.PrintPreview.DefaultExportFormat = ExportFormatConverter.ToExportCommand(options.ExportFormat);
                    if (options.OpenFileAfterExport && (fileNames.Any<string>() && (!string.IsNullOrEmpty(fileNames[0]) && File.Exists(fileNames[0]))))
                    {
                        ProcessLaunchHelper.StartProcess(fileNames[0], false);
                    }
                });
            }
        }

        internal void Export(ExportOptionsBase options, string fileName, Action<string[]> afterExport)
        {
            if (!(this.printingSystem is RemotePrintingSystem))
            {
                try
                {
                    string[] strArray = new string[0];
                    strArray = ExportOptionsControllerBase.GetControllerByOptions(options).GetExportedFileNames(this.printingSystem, options, fileName);
                    afterExport(strArray);
                }
                catch (FileNotFoundException exception1)
                {
                    throw ExceptionHelper.CreateFriendlyException(exception1);
                }
                catch (IOException exception2)
                {
                    if ((exception2.HResult != -2147024864) && (exception2.HResult != -2147023672))
                    {
                        throw new Exception(string.Empty, exception2);
                    }
                    throw ExceptionHelper.CreateFriendlyException(exception2, fileName);
                }
                catch (OutOfMemoryException exception5)
                {
                    throw ExceptionHelper.CreateFriendlyException(exception5);
                }
            }
        }

        private static string GetMessage(Exception e)
        {
            if (e == null)
            {
                return string.Empty;
            }
            if ((e is TargetInvocationException) && (e.InnerException != null))
            {
                e = e.InnerException;
            }
            else if (e is AggregateException)
            {
                e = e.GetBaseException();
            }
            return e.Message;
        }

        public void Print(PrintOptionsViewModel options)
        {
            options.SavePrinterSettings();
            if (this.printingSystem is RemotePrintingSystem)
            {
                this.RemotePrint(options);
            }
            else
            {
                PrintDocument document = ((IPrintForm) options).Document;
                this.PrintCore(document);
            }
        }

        public void Print(PrintDocument document)
        {
            this.PrintCore(document);
        }

        private void PrintCore(PrintDocument document)
        {
            this.printingSystem.OnStartPrint(new PrintDocumentEventArgs(document));
            this.printingSystem.PrintDocument(document);
            this.printingSystem.OnEndPrint(EventArgs.Empty);
        }

        public void PrintDirect(string printerName = null)
        {
            if (this.printingSystem is RemotePrintingSystem)
            {
                this.RemotePrintDirect(printerName);
            }
            else
            {
                this.PrintDirectCore(printerName);
            }
        }

        private void PrintDirectCore(string printerName)
        {
            try
            {
                this.printingSystem.Extender.Print(printerName);
            }
            catch (Exception exception1)
            {
                Exception printerException = DevExpress.Printing.Native.PrintHelper.GetPrinterException(exception1);
                if (printerException != null)
                {
                    DXMessageBox.Show(GetMessage(printerException), PrintingLocalizer.GetString(PrintingStringId.Error), MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoteExport(ExportOptionsViewModelBase options, Action<string> afterExport)
        {
            if (!(this.printingSystem is RemotePrintingSystem))
            {
                throw new InvalidOperationException();
            }
            this.RemoteExport(options.ExportOptions, options.FileName, afterExport);
        }

        internal void RemoteExport(ExportOptionsBase options, string fileName, Action<string> afterExport)
        {
            if (!(this.printingSystem is RemotePrintingSystem))
            {
                throw new InvalidOperationException();
            }
            IRemoteExportService exportService = this.printingSystem.GetService<IRemoteExportService>();
            ExceptionEventHandler exceptionHandler = null;
            exceptionHandler = delegate (object s, ExceptionEventArgs args) {
                exportService.Exception -= exceptionHandler;
                if (this.printingSystem.RaiseCreateDocumentException(args.Exception))
                {
                    throw args.Exception;
                }
            };
            exportService.Exception += exceptionHandler;
            exportService.Export(options, fileName, delegate (string name) {
                exportService.Exception -= exceptionHandler;
                afterExport(name);
            });
        }

        private void RemotePrint(PrintOptionsViewModel options)
        {
            this.printingSystem.GetService<IRemotePrintService>().Print(document => this.PrintCore(document), () => ((IPrintForm) options).Document);
        }

        private void RemotePrintDirect(string printerName)
        {
            this.printingSystem.GetService<IRemotePrintService>().PrintDirect(0, this.printingSystem.PageCount - 1, printer => this.PrintDirectCore(printerName));
        }

        public void Send(SendOptionsViewModel options)
        {
            if (this.printingSystem is RemotePrintingSystem)
            {
                this.RemoteExport(options, delegate (string fileName) {
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string[] files = new string[] { fileName };
                        new EmailSender().Send(files, options.EmailOptions);
                    }
                });
            }
            else
            {
                this.Export(options.ExportOptions, options.FileName, delegate (string[] fileNames) {
                    this.printingSystem.ExportOptions.PrintPreview.DefaultSendFormat = ExportFormatConverter.ToExportCommand(options.ExportFormat);
                    if (fileNames.Any<string>())
                    {
                        new EmailSender().Send(fileNames, options.EmailOptions);
                    }
                });
            }
        }
    }
}

