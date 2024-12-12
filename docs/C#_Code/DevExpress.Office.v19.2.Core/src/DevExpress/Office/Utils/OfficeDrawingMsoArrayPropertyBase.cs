namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class OfficeDrawingMsoArrayPropertyBase : OfficeDrawingPropertyBase
    {
        protected OfficeDrawingMsoArrayPropertyBase()
        {
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }

        public abstract byte[] GetElementsData();
        public override void Read(BinaryReader reader)
        {
            this.Value = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) OfficePropertiesFactory.GetOpcodeByType(base.GetType()));
            writer.Write(this.Value);
        }

        public override bool Complex =>
            true;

        public int Value { get; set; }
    }
}

