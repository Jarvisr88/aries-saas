namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class AttrIfOffsetPositionWriter : IOffsetPositionWriter
    {
        private long startPosition;
        private int offsetPositionThingsCounter;

        public AttrIfOffsetPositionWriter(long startPosition, int offsetPositionThingsCounter)
        {
            this.startPosition = startPosition;
            this.offsetPositionThingsCounter = offsetPositionThingsCounter;
        }

        public void WriteData(BinaryWriter writer)
        {
            writer.BaseStream.Seek(this.startPosition, SeekOrigin.Begin);
            writer.Write((ushort) ((writer.BaseStream.Length - this.startPosition) - 2L));
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

