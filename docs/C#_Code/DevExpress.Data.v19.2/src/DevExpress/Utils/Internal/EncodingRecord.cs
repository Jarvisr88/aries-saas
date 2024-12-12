namespace DevExpress.Utils.Internal
{
    using System;

    internal class EncodingRecord
    {
        public DevExpress.Utils.Internal.Platform Platform;
        public ushort EncodingID;
        public uint Offset;

        public EncodingRecord()
        {
        }

        public EncodingRecord(BigEndianStreamReader reader) : this()
        {
            this.Read(reader);
        }

        public void Read(BigEndianStreamReader reader)
        {
            this.Platform = (DevExpress.Utils.Internal.Platform) reader.ReadUShort();
            this.EncodingID = reader.ReadUShort();
            this.Offset = reader.ReadUInt32();
        }
    }
}

