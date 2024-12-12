namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
    using DevExpress.ReportServer.Printing;
    using DevExpress.ReportServer.ServiceModel.Native.RemoteOperations;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class RemoteExportService : IRemoteExportService
    {
        private readonly RemotePrintingSystem printingSystem;
        private string fileName;
        private Action<string> afterExport;

        public event ExceptionEventHandler Exception;

        public RemoteExportService(RemotePrintingSystem printingSystem)
        {
            Guard.ArgumentNotNull(printingSystem, "printingSystem");
            this.printingSystem = printingSystem;
        }

        public void Export(ExportOptionsBase options, string fileName, Action<string> afterExport)
        {
            this.fileName = fileName;
            this.afterExport = afterExport;
            RemoteOperationFactory service = this.printingSystem.GetService<RemoteOperationFactory>();
            ExportOptions options2 = new ExportOptions();
            options2.Options.Clear();
            options2.Options.Add(options.GetType(), options);
            ExportDocumentOperation operation = service.CreateExportDocumentOperation(options.GetFormat(), options2);
            operation.Progress += new EventHandler<ExportDocumentProgressEventArgs>(this.OnProgress);
            operation.Completed += new EventHandler<ExportDocumentCompletedEventArgs>(this.OnCompleted);
            this.printingSystem.ProgressReflector.InitializeRange(100);
            operation.Start();
        }

        private void OnCompleted(object sender, ExportDocumentCompletedEventArgs args)
        {
            ExportDocumentOperation operation = (ExportDocumentOperation) sender;
            operation.Progress -= new EventHandler<ExportDocumentProgressEventArgs>(this.OnProgress);
            operation.Completed -= new EventHandler<ExportDocumentCompletedEventArgs>(this.OnCompleted);
            this.printingSystem.ProgressReflector.MaximizeRange();
            if (args.Error != null)
            {
                this.RaiseException(args.Error);
            }
            else
            {
                try
                {
                    WriteRemoteDocument(args.Data, this.fileName);
                    this.afterExport(this.fileName);
                }
                catch (System.Exception exception)
                {
                    this.RaiseException(exception);
                }
            }
        }

        private void OnProgress(object sender, ExportDocumentProgressEventArgs args)
        {
            this.printingSystem.ProgressReflector.SetPosition(args.ProgressPosition);
        }

        private void RaiseException(System.Exception e)
        {
            if (this.Exception != null)
            {
                this.Exception(this, new ExceptionEventArgs(e));
            }
        }

        private static string WriteRemoteDocument(byte[] data, string fileName)
        {
            string str;
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            try
            {
                stream.Write(data, 0, data.Length);
                str = fileName;
            }
            catch (System.Exception exception)
            {
                if (stream != null)
                {
                    File.Delete(fileName);
                }
                throw new System.Exception(string.Empty, exception);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
            return str;
        }
    }
}

