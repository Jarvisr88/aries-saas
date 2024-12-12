namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;

    public abstract class BaseLineController : IDisposable
    {
        protected ILine fLine;

        protected BaseLineController();
        protected abstract ILine CreateLine(LineFactoryBase lineFactory);
        public void Dispose();
        protected virtual void Dispose(bool disposing);
        protected override void Finalize();
        public ILine GetLine(LineFactoryBase lineFactory);
        public static ILine[] GetLines(BaseLineController[] controllers, LineFactoryBase lineFactory);
        protected virtual void InitLine();

        public virtual bool IsRunning { get; }
    }
}

