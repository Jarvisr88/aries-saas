namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertyFloat : OlePropertyNumericBase
    {
        public OlePropertyFloat(int propertyId) : base(propertyId, 4)
        {
        }

        public override int GetSize(OlePropertySetBase propertySet) => 
            8;

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            base.Value = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write((float) base.Value);
        }
    }
}

