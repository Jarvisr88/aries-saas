namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class OlePropertyUInt16 : OlePropertyUIntBase
    {
        public OlePropertyUInt16(int propertyId) : base(propertyId, 0x12)
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

