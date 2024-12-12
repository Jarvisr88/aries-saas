namespace DevExpress.Office.Commands.Internal
{
    using System;

    public abstract class PasteSource
    {
        protected PasteSource()
        {
        }

        public virtual bool ContainsData(string format) => 
            this.ContainsData(format, false);

        public abstract bool ContainsData(string format, bool autoConvert);
        public virtual object GetData(string format) => 
            this.GetData(format, false);

        public abstract object GetData(string format, bool autoConvert);

        public virtual bool IsEmpty =>
            false;
    }
}

