namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public abstract class OfficeDrawingPropertyBase : IOfficeDrawingProperty
    {
        private const int operationCodeSize = 2;
        private const int operandSize = 4;
        private bool complex;
        private byte[] complexData;
        private short id;

        protected OfficeDrawingPropertyBase()
        {
            this.SetIdentifier();
            this.complexData = new byte[0];
        }

        public abstract void Execute(OfficeArtPropertiesBase owner);
        public virtual void Merge(IOfficeDrawingProperty other)
        {
        }

        public abstract void Read(BinaryReader reader);
        protected internal void SetComplex(bool complex)
        {
            this.complex = complex;
        }

        protected internal void SetComplexData(byte[] data)
        {
            this.complexData = data;
        }

        private void SetIdentifier()
        {
            short opcodeByType = OfficePropertiesFactory.GetOpcodeByType(base.GetType());
            this.id = (short) (((ushort) opcodeByType) & 0x3fff);
        }

        public abstract void Write(BinaryWriter writer);

        public short Id =>
            this.id;

        public virtual bool Complex =>
            this.complex;

        public byte[] ComplexData =>
            this.complexData;

        public int Size =>
            this.Complex ? (6 + this.ComplexData.Length) : 6;
    }
}

