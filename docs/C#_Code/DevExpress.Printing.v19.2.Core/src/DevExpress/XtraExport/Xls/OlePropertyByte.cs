namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class OlePropertyByte : OlePropertyBase
    {
        public OlePropertyByte(int propertyId) : base(propertyId, 0x11)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            string propertyName = propertySet.GetPropertyName(base.PropertyId);
            if (!string.IsNullOrEmpty(propertyName))
            {
                propertiesContainer.SetNumeric(propertyName, (double) this.Value);
            }
        }

        public override int GetSize(OlePropertySetBase propertySet) => 
            8;

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            this.Value = reader.ReadByte();
            reader.ReadBytes(3);
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write((uint) this.Value);
        }

        public byte Value { get; set; }
    }
}

