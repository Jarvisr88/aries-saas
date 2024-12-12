namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertyCodePage : OlePropertyInt16
    {
        public OlePropertyCodePage() : base(1)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
        }

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            base.Value = reader.ReadUInt16();
            reader.ReadInt16();
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write((ushort) base.Value);
            writer.Write((short) 0);
        }
    }
}

