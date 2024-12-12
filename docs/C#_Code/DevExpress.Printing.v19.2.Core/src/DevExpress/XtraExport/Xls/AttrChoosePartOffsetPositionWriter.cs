namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class AttrChoosePartOffsetPositionWriter : IOffsetPositionWriter
    {
        private long startPosition;
        private int offsetPositionThingsCounter;
        private int partIndex;

        public AttrChoosePartOffsetPositionWriter(long startPosition, int offsetPositionThingsCounter, int partIndex)
        {
            this.startPosition = startPosition;
            this.offsetPositionThingsCounter = offsetPositionThingsCounter;
            this.partIndex = partIndex;
        }

        public void WriteData(BinaryWriter writer)
        {
            writer.BaseStream.Seek(this.startPosition, SeekOrigin.Begin);
            writer.Write((ushort) ((writer.BaseStream.Length - this.startPosition) + ((this.partIndex + 1) * 2)));
            writer.BaseStream.Seek(0L, SeekOrigin.End);
        }

        public long StartPosition
        {
            get => 
                this.startPosition;
            set => 
                this.startPosition = value;
        }

        public int OffsetPositionThingsCounter
        {
            get => 
                this.offsetPositionThingsCounter;
            set => 
                this.offsetPositionThingsCounter = value;
        }
    }
}

