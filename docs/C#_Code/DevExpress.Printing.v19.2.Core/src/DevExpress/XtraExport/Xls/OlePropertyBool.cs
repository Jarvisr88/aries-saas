namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class OlePropertyBool : OlePropertyBase
    {
        public OlePropertyBool(int propertyId) : base(propertyId, 11)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            string propertyName = propertySet.GetPropertyName(base.PropertyId);
            if (!string.IsNullOrEmpty(propertyName))
            {
                propertiesContainer.SetBoolean(propertyName, this.Value);
            }
        }

        public override int GetSize(OlePropertySetBase propertySet) => 
            8;

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            this.Value = (reader.ReadInt32() & 0xffff) != 0;
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write(this.Value ? 0xffff : 0);
        }

        public bool Value { get; set; }
    }
}

