namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsFormatRun
    {
        public const int RecordSize = 4;

        public override bool Equals(object obj)
        {
            XlsFormatRun run = obj as XlsFormatRun;
            return ((run != null) ? ((this.CharIndex == run.CharIndex) && (this.FontIndex == run.FontIndex)) : false);
        }

        public static XlsFormatRun FromStream(BinaryDataReaderBase reader)
        {
            XlsFormatRun run = new XlsFormatRun();
            run.Read(reader);
            return run;
        }

        public static XlsFormatRun FromStream(BinaryReader reader)
        {
            XlsFormatRun run = new XlsFormatRun();
            run.Read(reader);
            return run;
        }

        public override int GetHashCode() => 
            this.CharIndex & (0xffff + ((this.FontIndex & 0xffff) << 0x10));

        public bool IsDefault() => 
            (this.CharIndex == 0) && (this.FontIndex == 0);

        protected void Read(BinaryDataReaderBase reader)
        {
            this.CharIndex = reader.ReadUInt16();
            this.FontIndex = reader.ReadUInt16();
        }

        protected void Read(BinaryReader reader)
        {
            this.CharIndex = reader.ReadUInt16();
            this.FontIndex = reader.ReadUInt16();
        }

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginBlock();
            }
            writer.Write((ushort) this.CharIndex);
            writer.Write((ushort) this.FontIndex);
            if (writer2 != null)
            {
                writer2.EndBlock();
            }
        }

        public int CharIndex { get; set; }

        public int FontIndex { get; set; }
    }
}

