namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsDxfNumIFmt : XlsDxfNum
    {
        public override void Read(BinaryReader reader)
        {
            reader.ReadByte();
            this.NumberFormatId = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(this.GetSize());
            }
            writer.Write((byte) 0);
            writer.Write((byte) this.NumberFormatId);
        }

        public override bool IsCustom =>
            false;

        public override int NumberFormatId { get; set; }

        public override string NumberFormatCode
        {
            get => 
                string.Empty;
            set
            {
            }
        }
    }
}

