namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;

    internal class OfficeArtFixedPointProperty : IOfficeArtProperty
    {
        private const double fractionalCoeff = 65536.0;
        private int typeCode;
        private double value;

        public OfficeArtFixedPointProperty(int typeCode, double value)
        {
            this.typeCode = typeCode;
            this.value = value;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.typeCode);
            short num = (short) this.value;
            if (((this.value - num) != 0.0) && (this.value < 0.0))
            {
                num = (short) (num - 1);
            }
            ushort num2 = (ushort) ((this.value - num) * 65536.0);
            writer.Write(num2);
            writer.Write(num);
        }

        public int Size =>
            6;

        public bool Complex =>
            false;

        public byte[] ComplexData =>
            null;
    }
}

