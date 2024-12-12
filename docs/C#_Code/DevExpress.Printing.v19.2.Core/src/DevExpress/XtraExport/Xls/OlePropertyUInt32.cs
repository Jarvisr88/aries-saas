namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class OlePropertyUInt32 : OlePropertyUIntBase
    {
        public OlePropertyUInt32(int propertyId) : base(propertyId, 0x13)
        {
        }

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            base.Value = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write(base.Value);
        }
    }
}

