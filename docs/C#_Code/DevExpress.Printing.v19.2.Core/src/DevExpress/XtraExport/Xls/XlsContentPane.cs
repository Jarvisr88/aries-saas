namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentPane : XlsContentBase
    {
        public override int GetSize() => 
            10;

        public override void Read(XlReader reader, int size)
        {
            this.XPos = reader.ReadUInt16();
            this.YPos = reader.ReadUInt16();
            this.TopRow = reader.ReadUInt16();
            this.LeftColumn = reader.ReadUInt16() & 0xff;
            this.ActivePane = reader.ReadByte();
            int count = size - 9;
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.XPos);
            writer.Write((ushort) this.YPos);
            writer.Write((ushort) this.TopRow);
            writer.Write((ushort) this.LeftColumn);
            writer.Write(this.ActivePane);
            writer.Write((byte) 0);
        }

        public int XPos { get; set; }

        public int YPos { get; set; }

        public int TopRow { get; set; }

        public int LeftColumn { get; set; }

        public byte ActivePane { get; set; }
    }
}

