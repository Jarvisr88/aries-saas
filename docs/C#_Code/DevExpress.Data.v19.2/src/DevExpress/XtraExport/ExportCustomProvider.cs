namespace DevExpress.XtraExport
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ExportCustomProvider : IDisposable
    {
        private string fileName;
        private System.IO.Stream stream;
        private bool isStreamMode;
        public const string StreamModeName = "Stream";

        public event ProviderProgressEventHandler ProviderProgress;

        public ExportCustomProvider(System.IO.Stream stream)
        {
            this.stream = stream;
            this.isStreamMode = true;
        }

        public ExportCustomProvider(string fileName)
        {
            this.fileName = fileName;
            this.isStreamMode = false;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ExportStyleManagerBase.DisposeInstance(this.fileName, this.stream);
            }
        }

        ~ExportCustomProvider()
        {
            this.Dispose(false);
        }

        public static bool IsValidFileName(string fileName) => 
            !string.IsNullOrEmpty(fileName);

        public static bool IsValidStream(System.IO.Stream stream) => 
            stream != null;

        protected void OnProviderProgress(int position)
        {
            if (this.ProviderProgress != null)
            {
                this.ProviderProgress(this, new ProviderProgressEventArgs(position));
            }
        }

        public string FileName =>
            this.fileName;

        public System.IO.Stream Stream =>
            this.stream;

        public bool IsStreamMode =>
            this.isStreamMode;
    }
}

