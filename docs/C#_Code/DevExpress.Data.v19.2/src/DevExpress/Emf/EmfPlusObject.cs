namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusObject
    {
        protected EmfPlusObject()
        {
        }

        public abstract void Write(EmfContentWriter writer);

        public abstract EmfPlusObjectType Type { get; }

        public abstract int Size { get; }
    }
}

