namespace DevExpress.Export.Binary
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class BinaryHyperlinkObject
    {
        private const int fixedPartSize = 8;
        private const int predefinedStreamVersion = 2;
        public static readonly Guid CLSID_StdHyperlink = new Guid("{0x79EAC9D0, 0xBAF9, 0x11CE, {0x8C, 0x82, 0x00, 0xAA, 0x00, 0x4B, 0xA9, 0x0B}}");
        private HyperlinkString displayName = new HyperlinkString();
        private HyperlinkString frameName = new HyperlinkString();
        private HyperlinkString moniker = new HyperlinkString();
        private HyperlinkString location = new HyperlinkString();
        private Guid optionalGUID;

        public static BinaryHyperlinkObject FromData(byte[] data)
        {
            BinaryHyperlinkObject obj2;
            using (MemoryStream stream = new MemoryStream(data, false))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    using (XlReader reader2 = new XlReader(reader))
                    {
                        Guid guid = new Guid(reader2.ReadBytes(0x10));
                        if (guid != CLSID_StdHyperlink)
                        {
                            throw new Exception("Invalid binary file: Wrong hyperlink class id");
                        }
                        obj2 = FromStream(reader2);
                    }
                }
            }
            return obj2;
        }

        public static BinaryHyperlinkObject FromStream(XlReader reader)
        {
            BinaryHyperlinkObject obj2 = new BinaryHyperlinkObject();
            obj2.Read(reader);
            return obj2;
        }

        public static BinaryHyperlinkObject FromStream(BinaryReader reader)
        {
            using (XlReader reader2 = new XlReader(reader))
            {
                return FromStream(reader2);
            }
        }

        public byte[] GetHyperlinkData()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(CLSID_StdHyperlink.ToByteArray());
                    this.Write(writer);
                }
                return stream.ToArray();
            }
        }

        public int GetSize()
        {
            int num = 8;
            if (this.HasDisplayName)
            {
                num += this.displayName.Length;
            }
            if (this.HasFrameName)
            {
                num += this.frameName.Length;
            }
            if (this.HasMoniker)
            {
                num = !this.IsMonkerSavedAsString ? (num + this.OleMoniker.GetSize()) : (num + this.moniker.Length);
            }
            if (this.HasLocationString)
            {
                num += this.location.Length;
            }
            if (this.HasGUID)
            {
                num += 0x10;
            }
            if (this.HasCreationTime)
            {
                num += 8;
            }
            return num;
        }

        protected void Read(XlReader reader)
        {
            if (reader.ReadInt32() != 2)
            {
                throw new Exception("Invalid binary file: Wrong hyperlink stream version");
            }
            uint num2 = reader.ReadUInt32();
            this.HasMoniker = Convert.ToBoolean((uint) (num2 & 1));
            this.IsAbsolute = Convert.ToBoolean((uint) (num2 & 2));
            this.SiteGaveDisplayName = Convert.ToBoolean((uint) (num2 & 4));
            this.HasLocationString = Convert.ToBoolean((uint) (num2 & 8));
            this.HasDisplayName = Convert.ToBoolean((uint) (num2 & ((uint) 0x10)));
            this.HasGUID = Convert.ToBoolean((uint) (num2 & ((uint) 0x20)));
            this.HasCreationTime = Convert.ToBoolean((uint) (num2 & ((uint) 0x40)));
            this.HasFrameName = Convert.ToBoolean((uint) (num2 & 0x80));
            this.IsMonkerSavedAsString = Convert.ToBoolean((uint) (num2 & 0x100));
            this.IsAbsoluteFromRelative = Convert.ToBoolean((uint) (num2 & 0x200));
            if (this.HasDisplayName)
            {
                this.displayName = HyperlinkString.FromStream(reader);
            }
            if (this.HasFrameName)
            {
                this.frameName = HyperlinkString.FromStream(reader);
            }
            if (this.HasMoniker)
            {
                if (this.IsMonkerSavedAsString)
                {
                    this.moniker = HyperlinkString.FromStream(reader);
                }
                else
                {
                    this.OleMoniker = BinaryHyperlinkMonikerFactory.Create(reader);
                }
            }
            if (this.HasLocationString)
            {
                this.location = HyperlinkString.FromStream(reader);
            }
            if (this.HasGUID)
            {
                this.optionalGUID = new Guid(reader.ReadBytes(0x10));
            }
            if (this.HasCreationTime)
            {
                this.CreationTime = reader.ReadInt64();
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(2);
            uint num = 0;
            if (this.HasMoniker)
            {
                num |= 1;
            }
            if (this.IsAbsolute)
            {
                num |= 2;
            }
            if (this.SiteGaveDisplayName)
            {
                num |= 4;
            }
            if (this.HasLocationString)
            {
                num |= 8;
            }
            if (this.HasDisplayName)
            {
                num |= (uint) 0x10;
            }
            if (this.HasGUID)
            {
                num |= (uint) 0x20;
            }
            if (this.HasCreationTime)
            {
                num |= (uint) 0x40;
            }
            if (this.HasFrameName)
            {
                num |= 0x80;
            }
            if (this.IsMonkerSavedAsString)
            {
                num |= 0x100;
            }
            if (this.IsAbsoluteFromRelative)
            {
                num |= 0x200;
            }
            writer.Write(num);
            if (this.HasDisplayName)
            {
                this.displayName.Write(writer);
            }
            if (this.HasFrameName)
            {
                this.frameName.Write(writer);
            }
            if (this.HasMoniker)
            {
                if (this.IsMonkerSavedAsString)
                {
                    this.moniker.Write(writer);
                }
                else
                {
                    this.OleMoniker.Write(writer);
                }
            }
            if (this.HasLocationString)
            {
                this.location.Write(writer);
            }
            if (this.HasGUID)
            {
                writer.Write(this.optionalGUID.ToByteArray());
            }
            if (this.HasCreationTime)
            {
                writer.Write(this.CreationTime);
            }
        }

        public bool HasMoniker { get; set; }

        public bool IsAbsolute { get; set; }

        public bool SiteGaveDisplayName { get; set; }

        public bool HasLocationString { get; set; }

        public bool HasDisplayName { get; set; }

        public bool HasGUID { get; set; }

        public bool HasCreationTime { get; set; }

        public bool HasFrameName { get; set; }

        public bool IsMonkerSavedAsString { get; set; }

        public bool IsAbsoluteFromRelative { get; set; }

        public string DisplayName
        {
            get => 
                this.displayName.Value;
            set => 
                this.displayName.Value = value;
        }

        public string FrameName
        {
            get => 
                this.frameName.Value;
            set => 
                this.frameName.Value = value;
        }

        public string Moniker
        {
            get => 
                this.moniker.Value;
            set => 
                this.moniker.Value = value;
        }

        public BinaryHyperlinkMonikerBase OleMoniker { get; set; }

        public string Location
        {
            get => 
                this.location.Value;
            set => 
                this.location.Value = value;
        }

        public Guid OptionalGUID
        {
            get => 
                this.optionalGUID;
            set
            {
                Guard.ArgumentNotNull(value, "OptionalGUID");
                this.optionalGUID = value;
            }
        }

        public long CreationTime { get; set; }
    }
}

