namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class OutputHandler : AbstractIOHandler
    {
        public OutputHandler(Stream memoryStream) : base(memoryStream)
        {
            base.InitBitConverter(true);
        }

        internal void Write(byte[] data)
        {
            base.Stream.Write(data, 0, data.Length);
        }

        internal void WriteByte(byte value)
        {
            base.Stream.WriteByte(value);
        }

        internal void WriteSectors(byte[] data, ushort sectorSize, byte padding)
        {
            uint num = (uint) (data.Length % sectorSize);
            base.Stream.Write(data, 0, data.Length);
            if (num != 0)
            {
                for (uint i = 0; i < (sectorSize - num); i++)
                {
                    base.Stream.WriteByte(padding);
                }
            }
        }

        internal void WriteSectors(byte[] data, ushort sectorSize, uint padding)
        {
            uint num = (uint) (data.Length % sectorSize);
            base.Stream.Write(data, 0, data.Length);
            if (num != 0)
            {
                if (((sectorSize - num) % 4) != 0)
                {
                    throw new Exception("Inconsistency found while writing a sector.");
                }
                for (uint i = 0; i < ((sectorSize - num) / 4); i++)
                {
                    this.WriteUInt32(padding);
                }
            }
        }

        internal void WriteSectors(byte[] data, int dataSize, ushort sectorSize, byte padding)
        {
            uint num = (uint) (dataSize % sectorSize);
            base.Stream.Write(data, 0, dataSize);
            if (num != 0)
            {
                for (uint i = 0; i < (sectorSize - num); i++)
                {
                    base.Stream.WriteByte(padding);
                }
            }
        }

        internal void WriteToStream(Stream stream)
        {
            byte[] buffer = new byte[0x200];
            this.BaseStream.Seek(0L, SeekOrigin.Begin);
            while (true)
            {
                int count = this.BaseStream.Read(buffer, 0, 0x200);
                stream.Write(buffer, 0, count);
                if (count != 0x200)
                {
                    stream.Flush();
                    return;
                }
            }
        }

        internal void WriteUInt16(ushort value)
        {
            base.Stream.Write(base.BitConverter.GetBytes(value), 0, 2);
        }

        internal void WriteUInt32(uint value)
        {
            base.Stream.Write(base.BitConverter.GetBytes(value), 0, 4);
        }

        internal void WriteUInt64(ulong value)
        {
            base.Stream.Write(base.BitConverter.GetBytes(value), 0, 8);
        }

        internal Stream BaseStream =>
            base.Stream;

        public override ulong IOStreamSize =>
            ulong.MaxValue;
    }
}

