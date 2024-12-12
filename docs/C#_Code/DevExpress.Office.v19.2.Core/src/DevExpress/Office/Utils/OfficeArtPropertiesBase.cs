namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class OfficeArtPropertiesBase : OfficeDrawingPartBase
    {
        private readonly List<IOfficeDrawingProperty> properties = new List<IOfficeDrawingProperty>();

        protected OfficeArtPropertiesBase()
        {
        }

        public abstract void CreateProperties();
        protected internal override int GetSize()
        {
            int count = this.properties.Count;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                num2 += this.properties[i].Size;
            }
            return num2;
        }

        protected internal virtual void HandleGroupShapeBooleanProperties(DrawingGroupShapeBooleanProperties properties)
        {
        }

        protected internal void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            long position = reader.BaseStream.Position;
            int instanceInfo = header.InstanceInfo;
            for (int i = 0; i < instanceInfo; i++)
            {
                this.Properties.Add(OfficePropertiesFactory.CreateProperty(reader));
            }
            for (int j = 0; j < instanceInfo; j++)
            {
                IOfficeDrawingProperty property = this.Properties[j];
                if (property.Complex)
                {
                    OfficeDrawingIntPropertyBase base2 = property as OfficeDrawingIntPropertyBase;
                    if (base2 != null)
                    {
                        base2.SetComplexData(reader.ReadBytes(base2.Value));
                    }
                    else
                    {
                        OfficeDrawingMsoArrayPropertyBase base3 = property as OfficeDrawingMsoArrayPropertyBase;
                        if (base3 != null)
                        {
                            if (base3.Value <= 6)
                            {
                                base3.SetComplexData(reader.ReadBytes(base3.Value));
                            }
                            else
                            {
                                byte[] buffer = reader.ReadBytes(6);
                                if (BitConverter.ToUInt16(buffer, 4) == 0xfff0)
                                {
                                    base3.Value += 6;
                                }
                                byte[] sourceArray = reader.ReadBytes(base3.Value - 6);
                                byte[] destinationArray = new byte[base3.Value];
                                Array.Copy(buffer, destinationArray, 6);
                                Array.Copy(sourceArray, 0, destinationArray, 6, sourceArray.Length);
                                base3.SetComplexData(destinationArray);
                            }
                        }
                    }
                }
                property.Execute(this);
            }
            reader.BaseStream.Seek(position + header.Length, SeekOrigin.Begin);
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            this.Properties.Sort(new OfficeDrawingPropertiesComparer());
            List<byte[]> list = new List<byte[]>();
            int count = this.Properties.Count;
            for (int i = 0; i < count; i++)
            {
                IOfficeDrawingProperty property = this.Properties[i];
                if (property.Complex)
                {
                    list.Add(property.ComplexData);
                }
                property.Write(writer);
            }
            count = list.Count;
            for (int j = 0; j < count; j++)
            {
                writer.Write(list[j]);
            }
        }

        public List<IOfficeDrawingProperty> Properties =>
            this.properties;

        public override int HeaderInstanceInfo =>
            this.Properties.Count;

        public override int HeaderVersion =>
            3;
    }
}

