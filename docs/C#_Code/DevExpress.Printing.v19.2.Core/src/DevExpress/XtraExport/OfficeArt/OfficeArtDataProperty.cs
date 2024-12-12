namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;

    internal class OfficeArtDataProperty : IOfficeArtProperty
    {
        private int typeCode;
        private int value;
        private byte[] complexData;

        public OfficeArtDataProperty(int typeCode, byte[] data)
        {
            this.typeCode = typeCode;
            this.complexData = data;
            this.value = data.Length;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.typeCode);
            writer.Write(this.value);
        }

        public int Size =>
            6 + this.complexData.Length;

        public bool Complex =>
            true;

        public byte[] ComplexData =>
            this.complexData;
    }
}

