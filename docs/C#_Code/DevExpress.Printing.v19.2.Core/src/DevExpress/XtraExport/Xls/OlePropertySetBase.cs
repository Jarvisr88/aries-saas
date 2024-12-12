namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class OlePropertySetBase
    {
        private List<OlePropertyBase> properties = new List<OlePropertyBase>();

        protected OlePropertySetBase()
        {
        }

        protected abstract OlePropertyBase CreateProperty(BinaryReader reader, int propertyId, int offset);
        public void Execute(IDocumentPropertiesContainer propertiesContainer)
        {
            foreach (OlePropertyBase base2 in this.Properties)
            {
                base2.Execute(propertiesContainer, this);
            }
        }

        protected OlePropertyBase FindById(int propertyId)
        {
            OlePropertyBase base3;
            using (List<OlePropertyBase>.Enumerator enumerator = this.Properties.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        OlePropertyBase current = enumerator.Current;
                        if (current.PropertyId != propertyId)
                        {
                            continue;
                        }
                        base3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return base3;
        }

        public Encoding GetEncoding()
        {
            OlePropertyCodePage page = this.FindById(1) as OlePropertyCodePage;
            return DXEncoding.GetEncoding((page != null) ? page.Value : 0x4e4);
        }

        public virtual string GetPropertyName(int propertyId) => 
            string.Empty;

        public int GetSize()
        {
            int num = 8 + (this.Properties.Count * 8);
            foreach (OlePropertyBase base2 in this.Properties)
            {
                num += base2.GetSize(this);
            }
            return num;
        }

        protected bool IsStringPropertyType(int propertyType) => 
            (propertyType == 30) || (propertyType == 0x1f);

        public void Read(BinaryReader reader)
        {
            int position = (int) reader.BaseStream.Position;
            reader.ReadInt32();
            int capacity = reader.ReadInt32();
            List<OlePropIdAndOffset> list = new List<OlePropIdAndOffset>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                OlePropIdAndOffset item = new OlePropIdAndOffset {
                    PropertyId = reader.ReadInt32(),
                    Offset = reader.ReadInt32()
                };
                if (item.PropertyId == 1)
                {
                    list.Insert(0, item);
                }
                else
                {
                    list.Add(item);
                }
            }
            for (int j = 0; j < capacity; j++)
            {
                OlePropertyBase item = this.ReadProperty(reader, list[j].PropertyId, list[j].Offset + position);
                if (item != null)
                {
                    this.Properties.Add(item);
                }
            }
        }

        protected internal OlePropertyBase ReadProperty(BinaryReader reader, int propertyId, int offset)
        {
            OlePropertyBase base2 = this.CreateProperty(reader, propertyId, offset);
            if (base2 != null)
            {
                base2.Read(reader, this);
            }
            return base2;
        }

        public void Write(BinaryWriter writer)
        {
            int size = this.GetSize();
            writer.Write(size);
            int count = this.Properties.Count;
            writer.Write(count);
            int num3 = 8 + (count * 8);
            foreach (OlePropertyBase base2 in this.Properties)
            {
                writer.Write(base2.PropertyId);
                writer.Write(num3);
                num3 += base2.GetSize(this);
            }
            foreach (OlePropertyBase base3 in this.Properties)
            {
                base3.Write(writer, this);
            }
        }

        public abstract Guid FormatId { get; }

        public List<OlePropertyBase> Properties =>
            this.properties;

        private class OlePropIdAndOffset
        {
            public int PropertyId { get; set; }

            public int Offset { get; set; }
        }
    }
}

