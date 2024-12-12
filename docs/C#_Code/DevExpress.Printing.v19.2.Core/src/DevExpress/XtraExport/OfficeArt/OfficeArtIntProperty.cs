namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;

    internal class OfficeArtIntProperty : IOfficeArtProperty
    {
        private int typeCode;
        private int value;

        public OfficeArtIntProperty(int typeCode, int value)
        {
            this.typeCode = typeCode;
            this.value = value;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.typeCode);
            writer.Write(this.value);
        }

        public int Size =>
            6;

        public bool Complex =>
            false;

        public byte[] ComplexData =>
            null;
    }
}

