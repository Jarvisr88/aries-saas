namespace DevExpress.XtraExport.OfficeArt
{
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;

    internal class OfficeArtStringProperty : IOfficeArtProperty
    {
        private int typeCode;
        private string value;

        public OfficeArtStringProperty(int typeCode, string value)
        {
            this.typeCode = typeCode;
            if (value == null)
            {
                this.value = string.Empty;
            }
            else
            {
                this.value = value;
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.typeCode);
            writer.Write((int) ((this.value.Length * 2) + 2));
        }

        public int Size =>
            8 + (this.value.Length * 2);

        public bool Complex =>
            true;

        public byte[] ComplexData =>
            XLStringEncoder.GetEncoding(true).GetBytes(this.value + "\0");
    }
}

