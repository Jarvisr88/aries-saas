namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Text;

    public class OlePropertyString : OlePropertyBase
    {
        private string value;

        public OlePropertyString(int propertyId, int propertyType) : base(propertyId, propertyType)
        {
            this.value = string.Empty;
            if ((propertyType != 30) && ((propertyType != 0x1f) && (propertyType != 0)))
            {
                throw new ArgumentException($"Such property type is not allowed: {propertyType}");
            }
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            string propertyName = propertySet.GetPropertyName(base.PropertyId);
            if (!string.IsNullOrEmpty(propertyName))
            {
                propertiesContainer.SetText(propertyName, this.Value);
            }
            else
            {
                propertyName = propertySet.GetPropertyName(base.PropertyId ^ 0x1000000);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    propertiesContainer.SetLinkToContent(propertyName, this.Value);
                }
            }
        }

        public override int GetSize(OlePropertySetBase propertySet)
        {
            int length = this.Value.Length;
            if ((length == 0) || ((length > 0) && (this.Value[length - 1] != '\0')))
            {
                length++;
            }
            Encoding encoding = (base.PropertyType == 0x1f) ? DXEncoding.GetEncoding(0x4b0) : propertySet.GetEncoding();
            return ((((8 + (base.IsSingleByteEncoding(encoding) ? length : (length * 2))) + 3) / 4) * 4);
        }

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            if (base.PropertyType == 0)
            {
                this.Value = string.Empty;
            }
            else
            {
                int num = reader.ReadInt32();
                if ((num < 0) || (num > 0xffff))
                {
                    throw new Exception("Invalid property set stream: string char count out of range 0...0xffff");
                }
                Encoding encoding = (base.PropertyType == 0x1f) ? DXEncoding.GetEncoding(0x4b0) : propertySet.GetEncoding();
                bool flag = base.IsSingleByteEncoding(encoding);
                byte[] bytes = reader.ReadBytes(flag ? num : (num * 2));
                this.Value = encoding.GetString(bytes, 0, bytes.Length).TrimEnd(new char[1]);
            }
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            string s = this.Value;
            int length = s.Length;
            if ((length == 0) || ((length > 0) && (this.Value[length - 1] != '\0')))
            {
                s = s + "\0";
                length++;
            }
            writer.Write(length);
            Encoding encoding = (base.PropertyType == 0x1f) ? DXEncoding.GetEncoding(0x4b0) : propertySet.GetEncoding();
            bool flag = base.IsSingleByteEncoding(encoding);
            byte[] bytes = encoding.GetBytes(s);
            writer.Write(bytes);
            int paddingCount = 4 - ((flag ? length : ((length * 2) + 8)) % 4);
            if (paddingCount < 4)
            {
                base.WritePadding(writer, paddingCount);
            }
        }

        public string Value
        {
            get => 
                this.value;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.value = string.Empty;
                }
                else
                {
                    this.value = value;
                }
            }
        }
    }
}

