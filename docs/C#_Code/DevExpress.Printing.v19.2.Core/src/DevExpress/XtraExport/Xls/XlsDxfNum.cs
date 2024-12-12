namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public abstract class XlsDxfNum
    {
        protected XlsDxfNum()
        {
        }

        public virtual int GetSize() => 
            2;

        public abstract void Read(BinaryReader reader);
        public abstract void Write(BinaryWriter writer);

        public abstract bool IsCustom { get; }

        public abstract int NumberFormatId { get; set; }

        public abstract string NumberFormatCode { get; set; }
    }
}

