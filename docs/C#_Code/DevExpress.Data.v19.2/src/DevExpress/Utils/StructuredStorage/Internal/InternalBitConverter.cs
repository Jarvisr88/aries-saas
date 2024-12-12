namespace DevExpress.Utils.StructuredStorage.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [CLSCompliant(false)]
    public class InternalBitConverter
    {
        public static InternalBitConverter Create(bool isLittleEndian) => 
            !(BitConverter.IsLittleEndian ^ isLittleEndian) ? new InternalBitConverter() : new PrereverseInternalBitConverter();

        [CLSCompliant(false)]
        public List<byte> GetBytes(List<uint> input)
        {
            List<byte> list = new List<byte>(4 * input.Count);
            int count = input.Count;
            for (int i = 0; i < count; i++)
            {
                list.AddRange(this.GetBytes(input[i]));
            }
            return list;
        }

        [CLSCompliant(false)]
        public byte[] GetBytes(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.Preprocess(bytes);
            return bytes;
        }

        [CLSCompliant(false)]
        public byte[] GetBytes(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.Preprocess(bytes);
            return bytes;
        }

        [CLSCompliant(false)]
        public byte[] GetBytes(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.Preprocess(bytes);
            return bytes;
        }

        protected internal virtual void Preprocess(byte[] value)
        {
        }

        public string ToString(byte[] value)
        {
            this.Preprocess(value);
            string str = Encoding.Unicode.GetString(value, 0, value.Length);
            int index = str.IndexOf('\0');
            if (index >= 0)
            {
                str = str.Remove(index);
            }
            return str;
        }

        [CLSCompliant(false)]
        public ushort ToUInt16(byte[] value)
        {
            this.Preprocess(value);
            return BitConverter.ToUInt16(value, 0);
        }

        [CLSCompliant(false)]
        public uint ToUInt32(byte[] value)
        {
            this.Preprocess(value);
            return BitConverter.ToUInt32(value, 0);
        }

        [CLSCompliant(false)]
        public ulong ToUInt64(byte[] value)
        {
            this.Preprocess(value);
            return BitConverter.ToUInt64(value, 0);
        }
    }
}

