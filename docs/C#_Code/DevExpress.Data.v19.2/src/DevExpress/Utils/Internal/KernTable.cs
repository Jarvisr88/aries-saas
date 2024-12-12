namespace DevExpress.Utils.Internal
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class KernTable
    {
        private uint[] keys;
        private int[] values;
        internal bool isHorizontal;
        internal bool isMinimum;
        internal bool isCrossStream;
        internal bool isOverride;

        public KernTable()
        {
        }

        public KernTable(BigEndianStreamReader reader) : this()
        {
            this.Read(reader);
        }

        private uint GetIndex(int left, int right) => 
            (uint) (((left & 0xffff) << 0x10) | (right & 0xffff));

        private int IndexOfKey(uint value)
        {
            if (this.keys != null)
            {
                int num = 0;
                int num2 = this.keys.Length - 1;
                while (num <= num2)
                {
                    int index = num + ((num2 - num) >> 1);
                    if (this.keys[index] == value)
                    {
                        return index;
                    }
                    if (this.keys[index] < value)
                    {
                        num = index + 1;
                        continue;
                    }
                    num2 = index - 1;
                }
            }
            return -1;
        }

        public void Read(BigEndianStreamReader reader)
        {
            if (reader.ReadUShort() != 0)
            {
                throw new NotSupportedException("kern subtable version");
            }
            reader.ReadUShort();
            ushort coverage = reader.ReadUShort();
            if (this.ReadFlags(coverage) != 0)
            {
                throw new NotSupportedException("only format0 kern tables are supported");
            }
            int num3 = reader.ReadUShort();
            reader.Stream.Seek(6L, SeekOrigin.Current);
            this.keys = new uint[num3];
            this.values = new int[num3];
            for (int i = 0; i < num3; i++)
            {
                this.keys[i] = reader.ReadUInt32();
                this.values[i] = reader.ReadShort();
            }
        }

        private int ReadFlags(ushort coverage)
        {
            this.isHorizontal = (coverage & 1) == 1;
            this.isMinimum = (coverage & 2) == 2;
            this.isCrossStream = (coverage & 4) == 4;
            this.isOverride = (coverage & 8) == 8;
            return (coverage & 240);
        }

        public void ReadInternal(BigEndianStreamReader reader)
        {
            this.ReadFlags(reader.ReadUShort());
            int count = reader.ReadInt32();
            if (count == 0)
            {
                this.keys = new uint[0];
                this.values = new int[0];
            }
            Func<BigEndianStreamReader, uint> readItemAction = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Func<BigEndianStreamReader, uint> local1 = <>c.<>9__28_0;
                readItemAction = <>c.<>9__28_0 = r => ArrayStreamHelper<uint>.ReadUIntCompressed(r);
            }
            uint[] numArray = new ArrayStreamHelper<uint>().ReadArray(reader, count, readItemAction);
            this.values = new ArrayStreamHelper<int>().ReadArray(reader, count, <>c.<>9__28_1 ??= r => r.ReadShort());
            this.keys = new uint[count];
            uint num2 = 0;
            for (int i = 0; i < count; i++)
            {
                this.keys[i] = num2 + (numArray[i] + 1);
            }
        }

        private void WriteFlagsInternal(BigEndianStreamWriter writer)
        {
            ushort num = 0;
            if (this.isHorizontal)
            {
                num = (ushort) (num | 1);
            }
            if (this.isMinimum)
            {
                num = (ushort) (num | 2);
            }
            if (this.isCrossStream)
            {
                num = (ushort) (num | 4);
            }
            if (this.isOverride)
            {
                num = (ushort) (num | 8);
            }
            writer.WriteUShort(num);
        }

        public void WriteInternal(BigEndianStreamWriter writer)
        {
            this.WriteFlagsInternal(writer);
            writer.WriteInt32(this.Count);
            if (this.Count != 0)
            {
                uint[] items = new uint[this.Count];
                uint num = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    items[i] = (this.keys[i] - num) - 1;
                    num = this.keys[i];
                }
                Action<BigEndianStreamWriter, uint> writeItemAction = <>c.<>9__27_0;
                if (<>c.<>9__27_0 == null)
                {
                    Action<BigEndianStreamWriter, uint> local1 = <>c.<>9__27_0;
                    writeItemAction = <>c.<>9__27_0 = (w, value) => ArrayStreamHelper<uint>.WriteUIntCompressed(w, value);
                }
                new ArrayStreamHelper<uint>().WriteArray(writer, items, writeItemAction);
                Action<BigEndianStreamWriter, int> action2 = <>c.<>9__27_1;
                if (<>c.<>9__27_1 == null)
                {
                    Action<BigEndianStreamWriter, int> local2 = <>c.<>9__27_1;
                    action2 = <>c.<>9__27_1 = (w, value) => w.WriteShort((short) value);
                }
                new ArrayStreamHelper<int>().WriteArray(writer, this.values, action2);
            }
        }

        public bool IsHorizontal =>
            this.isHorizontal;

        public bool IsMinimum =>
            this.isMinimum;

        public bool IsCrossStream =>
            this.isCrossStream;

        public bool IsOverride =>
            this.isOverride;

        public int Count =>
            (this.keys != null) ? this.keys.Length : 0;

        public int this[int left, int right] =>
            this[this.GetIndex(left, right)];

        public int this[uint key]
        {
            get
            {
                int index = this.IndexOfKey(key);
                return ((index >= 0) ? this.values[index] : 0);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly KernTable.<>c <>9 = new KernTable.<>c();
            public static Action<BigEndianStreamWriter, uint> <>9__27_0;
            public static Action<BigEndianStreamWriter, int> <>9__27_1;
            public static Func<BigEndianStreamReader, uint> <>9__28_0;
            public static Func<BigEndianStreamReader, int> <>9__28_1;

            internal uint <ReadInternal>b__28_0(BigEndianStreamReader r) => 
                ArrayStreamHelper<uint>.ReadUIntCompressed(r);

            internal int <ReadInternal>b__28_1(BigEndianStreamReader r) => 
                r.ReadShort();

            internal void <WriteInternal>b__27_0(BigEndianStreamWriter w, uint value)
            {
                ArrayStreamHelper<uint>.WriteUIntCompressed(w, value);
            }

            internal void <WriteInternal>b__27_1(BigEndianStreamWriter w, int value)
            {
                w.WriteShort((short) value);
            }
        }
    }
}

