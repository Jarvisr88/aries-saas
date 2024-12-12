namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class FixedPoint
    {
        private const double fractionalCoeff = 65536.0;
        private double value;

        public static FixedPoint FromBytes(byte[] data, int offset)
        {
            FixedPoint point = new FixedPoint();
            ushort num = BitConverter.ToUInt16(data, offset);
            point.value = BitConverter.ToInt16(data, offset + 2) + (((double) num) / 65536.0);
            return point;
        }

        public static FixedPoint FromStream(BinaryReader reader)
        {
            FixedPoint point = new FixedPoint();
            point.Read(reader);
            return point;
        }

        public byte[] GetBytes()
        {
            short num = (short) this.Value;
            if (((this.Value - num) != 0.0) && (num < 0))
            {
                num = (short) (num - 1);
            }
            ushort num2 = (ushort) ((this.Value - num) * 65536.0);
            return BitConverter.GetBytes((uint) ((num << 0x10) | num2));
        }

        protected void Read(BinaryReader reader)
        {
            ushort num = reader.ReadUInt16();
            short num2 = reader.ReadInt16();
            this.value = num2 + (((double) num) / 65536.0);
        }

        public void Write(BinaryWriter writer)
        {
            short num = (short) this.Value;
            if (((this.Value - num) != 0.0) && (this.Value < 0.0))
            {
                num = (short) (num - 1);
            }
            ushort num2 = (ushort) ((this.Value - num) * 65536.0);
            writer.Write(num2);
            writer.Write(num);
        }

        public double Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }
    }
}

