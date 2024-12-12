namespace DevExpress.Utils.Internal
{
    using System;
    using System.IO;

    public class ArrayStreamHelper<T> where T: struct
    {
        public T[] ReadArray(BigEndianStreamReader reader, int count, Func<BigEndianStreamReader, T> readItemAction) => 
            (reader.ReadByte() == 0) ? this.ReadArrayDefaultFormat(reader, count, readItemAction) : this.ReadArrayCompressedFormat(reader, count, readItemAction);

        private T[] ReadArrayCompressedFormat(BigEndianStreamReader reader, int count, Func<BigEndianStreamReader, T> readItemAction)
        {
            T[] localArray = new T[count];
            int index = 0;
            while (index < count)
            {
                uint num2 = ArrayStreamHelper<T>.ReadUIntCompressed(reader);
                T local = readItemAction(reader);
                int num3 = 0;
                while (num3 < num2)
                {
                    localArray[index] = local;
                    num3++;
                    index++;
                }
            }
            return localArray;
        }

        private T[] ReadArrayDefaultFormat(BigEndianStreamReader reader, int count, Func<BigEndianStreamReader, T> readItemAction)
        {
            T[] localArray = new T[count];
            for (int i = 0; i < count; i++)
            {
                localArray[i] = readItemAction(reader);
            }
            return localArray;
        }

        internal static uint ReadUIntCompressed(BigEndianStreamReader reader)
        {
            byte num = reader.ReadByte();
            return ((num >= 0xfe) ? ((num != 0xfe) ? reader.ReadUInt32() : reader.ReadUShort()) : num);
        }

        public void WriteArray(BigEndianStreamWriter writer, T[] items, Action<BigEndianStreamWriter, T> writeItemAction)
        {
            byte[] buffer;
            byte[] buffer2;
            using (MemoryStream stream = new MemoryStream())
            {
                BigEndianStreamWriter writer2 = new BigEndianStreamWriter(stream);
                this.WriteArrayCompressedFormat(writer2, items, writeItemAction);
                buffer = stream.ToArray();
            }
            using (MemoryStream stream2 = new MemoryStream())
            {
                BigEndianStreamWriter writer3 = new BigEndianStreamWriter(stream2);
                this.WriteArrayDefaultFormat(writer3, items, writeItemAction);
                buffer2 = stream2.ToArray();
            }
            if (buffer.Length < buffer2.Length)
            {
                writer.WriteByte(1);
                writer.Stream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                writer.WriteByte(0);
                writer.Stream.Write(buffer2, 0, buffer2.Length);
            }
        }

        private void WriteArrayCompressedFormat(BigEndianStreamWriter writer, T[] items, Action<BigEndianStreamWriter, T> writeItemAction)
        {
            T local = default(T);
            int length = items.Length;
            uint equalItemsCount = 0;
            for (int i = 0; i < length; i++)
            {
                if (items[i].Equals(local))
                {
                    equalItemsCount++;
                }
                else
                {
                    if (equalItemsCount > 0)
                    {
                        ArrayStreamHelper<T>.WriteUIntCompressed(writer, equalItemsCount);
                        writeItemAction(writer, local);
                    }
                    local = items[i];
                    equalItemsCount = 1;
                }
            }
            if (equalItemsCount > 0)
            {
                ArrayStreamHelper<T>.WriteUIntCompressed(writer, equalItemsCount);
                writeItemAction(writer, local);
            }
        }

        private void WriteArrayDefaultFormat(BigEndianStreamWriter writer, T[] items, Action<BigEndianStreamWriter, T> writeItemAction)
        {
            int length = items.Length;
            for (int i = 0; i < length; i++)
            {
                writeItemAction(writer, items[i]);
            }
        }

        internal static void WriteUIntCompressed(BigEndianStreamWriter writer, uint equalItemsCount)
        {
            if (equalItemsCount > 0xffff)
            {
                writer.WriteByte(0xff);
                writer.WriteUInt32(equalItemsCount);
            }
            else if (equalItemsCount < 0xfe)
            {
                writer.WriteByte((byte) equalItemsCount);
            }
            else
            {
                writer.WriteByte(0xfe);
                writer.WriteUShort((ushort) equalItemsCount);
            }
        }
    }
}

