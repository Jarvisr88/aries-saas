namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentBoolErr : XlsContentCellBase
    {
        public override int GetSize() => 
            base.GetSize() + 2;

        public override void Read(XlReader reader, int size)
        {
            base.Read(reader, size);
            this.Value = reader.ReadByte();
            this.IsError = reader.ReadBoolean();
            int count = size - this.GetSize();
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.Value);
            writer.Write(this.IsError);
        }

        public byte Value { get; set; }

        public bool IsError { get; set; }
    }
}

