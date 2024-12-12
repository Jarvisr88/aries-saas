namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertyInt16 : OlePropertyIntBase
    {
        public OlePropertyInt16(int propertyId) : base(propertyId, 2)
        {
        }

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            base.Value = reader.ReadInt16();
            reader.ReadInt16();
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write((short) base.Value);
            writer.Write((short) 0);
        }
    }
}

