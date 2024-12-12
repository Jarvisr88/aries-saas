namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentDimensions : XlsContentBase
    {
        public override int GetSize() => 
            14;

        public override void Read(XlReader reader, int size)
        {
            this.FirstRowIndex = reader.ReadInt32() + 1;
            this.LastRowIndex = reader.ReadInt32();
            this.FirstColumnIndex = reader.ReadInt16() + 1;
            this.LastColumnIndex = reader.ReadInt16();
            int count = size - 12;
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((int) (this.FirstRowIndex - 1));
            writer.Write(this.LastRowIndex);
            writer.Write((short) (this.FirstColumnIndex - 1));
            writer.Write((short) this.LastColumnIndex);
            writer.Write((short) 0);
        }

        public int FirstRowIndex { get; set; }

        public int LastRowIndex { get; set; }

        public int FirstColumnIndex { get; set; }

        public int LastColumnIndex { get; set; }
    }
}

