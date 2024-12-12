namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class OlePropertyStreamContent
    {
        private int version;
        private int systemId = 0x20106;
        private Guid classId = Guid.Empty;
        private List<OlePropertySetBase> propertySets = new List<OlePropertySetBase>();

        public void Execute(IDocumentPropertiesContainer propertiesContainer)
        {
            foreach (OlePropertySetBase base2 in this.propertySets)
            {
                base2.Execute(propertiesContainer);
            }
        }

        public static OlePropertyStreamContent FromStream(BinaryReader reader)
        {
            OlePropertyStreamContent content = new OlePropertyStreamContent();
            content.Read(reader);
            return content;
        }

        protected void Read(BinaryReader reader)
        {
            if ((reader.BaseStream.Length >= 0x1c) && (reader.ReadUInt16() == 0xfffe))
            {
                this.Version = reader.ReadUInt16();
                this.SystemId = reader.ReadInt32();
                byte[] b = reader.ReadBytes(0x10);
                this.ClassId = new Guid(b);
                int num2 = reader.ReadInt32();
                if ((num2 >= 1) && (num2 <= 2))
                {
                    Guid empty = Guid.Empty;
                    Guid guid2 = Guid.Empty;
                    int offset = 0;
                    int num4 = 0;
                    empty = new Guid(reader.ReadBytes(0x10));
                    offset = reader.ReadInt32();
                    if (num2 == 2)
                    {
                        guid2 = new Guid(reader.ReadBytes(0x10));
                        num4 = reader.ReadInt32();
                    }
                    if (empty == OlePropDefs.FMTID_SummaryInformation)
                    {
                        this.ReadPropertySet(reader, offset, new OlePropertySetSummary());
                    }
                    else if (empty == OlePropDefs.FMTID_DocSummaryInformation)
                    {
                        this.ReadPropertySet(reader, offset, new OlePropertySetDocSummary());
                        if (guid2 == OlePropDefs.FMTID_UserDefinedProperties)
                        {
                            this.ReadPropertySet(reader, num4, new OlePropertySetUserDefined());
                        }
                    }
                }
            }
        }

        private void ReadPropertySet(BinaryReader reader, int offset, OlePropertySetBase propertySet)
        {
            reader.BaseStream.Position = offset;
            propertySet.Read(reader);
            this.PropertySets.Add(propertySet);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) 0xfffe);
            writer.Write((ushort) this.Version);
            writer.Write(this.SystemId);
            Guid classId = this.ClassId;
            writer.Write(classId.ToByteArray());
            int count = this.PropertySets.Count;
            writer.Write(count);
            int num2 = ((int) writer.BaseStream.Position) + (count * 20);
            foreach (OlePropertySetBase base2 in this.PropertySets)
            {
                writer.Write(base2.FormatId.ToByteArray());
                writer.Write(num2);
                num2 += base2.GetSize();
            }
            foreach (OlePropertySetBase base3 in this.PropertySets)
            {
                base3.Write(writer);
            }
            int num3 = (int) (4L - (writer.BaseStream.Length % 4L));
            if (num3 < 4)
            {
                for (int i = 0; i < num3; i++)
                {
                    writer.Write((byte) 0);
                }
            }
        }

        public int Version
        {
            get => 
                this.version;
            set
            {
                if ((value < 0) || (value > 1))
                {
                    throw new ArgumentOutOfRangeException("Version out of range 0...1");
                }
                this.version = value;
            }
        }

        public int SystemId
        {
            get => 
                this.systemId;
            set => 
                this.systemId = value;
        }

        public Guid ClassId
        {
            get => 
                this.classId;
            set => 
                this.classId = value;
        }

        public List<OlePropertySetBase> PropertySets =>
            this.propertySets;
    }
}

