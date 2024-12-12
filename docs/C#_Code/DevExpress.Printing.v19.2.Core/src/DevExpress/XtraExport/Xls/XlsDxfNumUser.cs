namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsDxfNumUser : XlsDxfNum
    {
        private XLUnicodeString numberFormatCode = new XLUnicodeString();

        public override int GetSize() => 
            base.GetSize() + this.numberFormatCode.Length;

        public override void Read(BinaryReader reader)
        {
            reader.ReadUInt16();
            this.numberFormatCode = XLUnicodeString.FromStream(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            int size = this.GetSize();
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(size);
            }
            writer.Write((ushort) size);
            this.numberFormatCode.Write(writer);
        }

        public override bool IsCustom =>
            true;

        public override int NumberFormatId
        {
            get => 
                0;
            set
            {
            }
        }

        public override string NumberFormatCode
        {
            get => 
                this.numberFormatCode.Value;
            set => 
                this.numberFormatCode.Value = value;
        }
    }
}

