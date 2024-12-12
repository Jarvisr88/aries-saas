namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XlsContentIndex : XlsContentBase
    {
        protected const int VariablePartElementSize = 4;
        protected const int FixedPartSize = 0x10;
        private int firstRowIndex;
        private int lastRowIndex;
        private long defaultColumnWidthOffset;
        private readonly List<long> dbCellsPositions = new List<long>();

        public override int GetSize() => 
            0x10 + (this.dbCellsPositions.Count * 4);

        public override void Read(XlReader reader, int size)
        {
            reader.ReadInt32();
            this.firstRowIndex = reader.ReadInt32();
            this.lastRowIndex = reader.ReadInt32();
            this.defaultColumnWidthOffset = reader.ReadUInt32();
            int num = (size - 0x10) / 4;
            for (int i = 0; i < num; i++)
            {
                this.dbCellsPositions.Add((long) reader.ReadUInt32());
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(0);
            writer.Write(this.firstRowIndex);
            writer.Write(this.lastRowIndex);
            writer.Write((uint) this.defaultColumnWidthOffset);
            int count = this.dbCellsPositions.Count;
            for (int i = 0; i < count; i++)
            {
                writer.Write((uint) this.dbCellsPositions[i]);
            }
        }

        public int FirstRowIndex
        {
            get => 
                this.firstRowIndex;
            set => 
                this.firstRowIndex = value;
        }

        public int LastRowIndex
        {
            get => 
                this.lastRowIndex;
            set => 
                this.lastRowIndex = value;
        }

        public long DefaultColumnWidthOffset
        {
            get => 
                this.defaultColumnWidthOffset;
            set => 
                this.defaultColumnWidthOffset = value;
        }

        public List<long> DbCellsPositions =>
            this.dbCellsPositions;
    }
}

