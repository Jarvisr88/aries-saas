namespace DevExpress.Emf
{
    using System;

    public abstract class EmfRecord
    {
        protected EmfRecord()
        {
        }

        public virtual void Accept(IEmfMetafileVisitor visitor)
        {
        }

        public abstract void Write(EmfContentWriter writer);
    }
}

