namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertyInt32 : OlePropertyIntBase
    {
        public OlePropertyInt32(int propertyId) : base(propertyId, 3)
        {
        }

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            base.Value = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write(base.Value);
        }
    }
}

