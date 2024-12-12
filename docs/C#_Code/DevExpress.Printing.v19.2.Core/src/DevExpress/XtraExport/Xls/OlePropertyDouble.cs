namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertyDouble : OlePropertyNumericBase
    {
        public OlePropertyDouble(int propertyId) : base(propertyId, 5)
        {
        }

        public override int GetSize(OlePropertySetBase propertySet) => 
            12;

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            base.Value = reader.ReadDouble();
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write(base.Value);
        }
    }
}

