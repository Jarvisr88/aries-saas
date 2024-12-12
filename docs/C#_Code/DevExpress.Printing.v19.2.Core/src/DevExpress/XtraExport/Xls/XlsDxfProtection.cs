namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsDxfProtection
    {
        private const ushort MaskLocked = 1;
        private const ushort MaskHidden = 2;
        public const short Size = 2;
        private ushort packedValues;

        private bool GetBooleanValue(ushort mask) => 
            (this.packedValues & mask) != 0;

        public void Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt16();
        }

        private void SetBooleanValue(ushort mask, bool bitVal)
        {
            if (bitVal)
            {
                this.packedValues = (ushort) (this.packedValues | mask);
            }
            else
            {
                this.packedValues = (ushort) (this.packedValues & ~mask);
            }
        }

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(2);
            }
            writer.Write(this.packedValues);
        }

        public bool Locked
        {
            get => 
                this.GetBooleanValue(1);
            set => 
                this.SetBooleanValue(1, value);
        }

        public bool Hidden
        {
            get => 
                this.GetBooleanValue(2);
            set => 
                this.SetBooleanValue(2, value);
        }
    }
}

