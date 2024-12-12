namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class MemThingOffsetPositionWriter : IOffsetPositionWriter
    {
        private long startPosition;
        private int offsetPositionThingsCounter;

        public MemThingOffsetPositionWriter(long startPosition, int offsetPositionThingsCounter)
        {
            this.startPosition = startPosition;
            this.offsetPositionThingsCounter = offsetPositionThingsCounter;
        }

        public void WriteData(BinaryWriter writer)
        {
            writer.BaseStream.Seek(this.startPosition, SeekOrigin.Begin);
            writer.Write((ushort) ((writer.BaseStream.Length - this.startPosition) - 2L));
            writer.Seek(0, SeekOrigin.End);
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

