namespace DevExpress.Emf
{
    using System;

    public abstract class DXBrush : IDisposable
    {
        protected DXBrush()
        {
        }

        public abstract void Accept(IDXBrushVisitor visitor);
        public virtual void Dispose()
        {
        }

        public abstract void Write(EmfContentWriter writer);

        public abstract int DataSize { get; }
    }
}

