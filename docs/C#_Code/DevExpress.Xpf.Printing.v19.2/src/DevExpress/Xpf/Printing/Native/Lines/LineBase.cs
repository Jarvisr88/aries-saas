namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.Windows.Controls;

    public abstract class LineBase : ILine, IDisposable
    {
        protected LineBase()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~LineBase()
        {
            this.Dispose(false);
        }

        public abstract void RefreshContent();
        public abstract void SetText(string text);

        public abstract Label Header { get; }

        public abstract Control Content { get; }
    }
}

