namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentAddInExternName : XlsContentBase
    {
        private ShortXLUnicodeString name = new ShortXLUnicodeString();

        public override int GetSize() => 
            this.name.Length + 8;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) 0);
            writer.Write((uint) 0);
            this.name.Write(writer);
            writer.Write((ushort) 0);
        }

        public string Name
        {
            get => 
                this.name.Value;
            set => 
                this.name.Value = value;
        }
    }
}

