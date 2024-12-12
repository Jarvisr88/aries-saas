namespace DevExpress.Export.Binary
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;
    using System.Text;

    public class BinaryHyperlinkFileMoniker : BinaryHyperlinkMonikerBase
    {
        private const ushort serializationVersionNumber = 0xdead;
        private string path;

        public BinaryHyperlinkFileMoniker() : base(BinaryHyperlinkMonikerFactory.CLSID_FileMoniker)
        {
            this.path = string.Empty;
        }

        public static BinaryHyperlinkFileMoniker FromStream(XlReader reader)
        {
            BinaryHyperlinkFileMoniker moniker = new BinaryHyperlinkFileMoniker();
            moniker.Read(reader);
            return moniker;
        }

        private string GetDirectoryPrefix(ushort count)
        {
            if (count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                builder.Append(@"..\");
            }
            return builder.ToString();
        }

        protected internal int GetParentDirectoryIndicatorsNumber()
        {
            int num = 0;
            for (int i = 0; (i < this.Path.Length) && (this.Path.IndexOf(@"..\", i) == i); i += 3)
            {
                num++;
            }
            return num;
        }

        public override int GetSize()
        {
            int num2 = this.GetParentDirectoryIndicatorsNumber() * 3;
            int num = ((6 + ((this.Path.Length - num2) + 1)) + 0x18) + 4;
            if (XLStringEncoder.StringHasHighBytes(this.Path))
            {
                num += ((this.Path.Length - num2) * 2) + 6;
            }
            return (base.GetSize() + num);
        }

        protected internal int GetUNCServerPartCharCount()
        {
            int num = -1;
            if (this.Path.IndexOf(@"\\") == 0)
            {
                int index = this.Path.IndexOf(@"\", 2);
                num = (index != -1) ? index : this.Path.Length;
            }
            return num;
        }

        protected void Read(XlReader reader)
        {
            ushort count = reader.ReadUInt16();
            int num2 = reader.ReadInt32();
            if (num2 > 0)
            {
                byte[] bytes = reader.ReadBytes(num2);
                this.Path = this.GetDirectoryPrefix(count) + XLStringEncoder.GetEncoding(false).GetString(bytes, 0, num2 - 1);
            }
            reader.ReadUInt16();
            if (reader.ReadUInt16() != 0xdead)
            {
                throw new Exception("Invalid binary file: Wrong file moniker serialization version number");
            }
            reader.ReadBytes(0x10);
            reader.ReadBytes(4);
            if (reader.ReadInt32() > 0)
            {
                int num5 = reader.ReadInt32();
                reader.ReadUInt16();
                byte[] bytes = reader.ReadBytes(num5);
                this.Path = this.GetDirectoryPrefix(count) + XLStringEncoder.GetEncoding(true).GetString(bytes, 0, bytes.Length);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            ushort parentDirectoryIndicatorsNumber = (ushort) this.GetParentDirectoryIndicatorsNumber();
            writer.Write(parentDirectoryIndicatorsNumber);
            byte[] bytes = XLStringEncoder.GetBytes(this.Path.Substring(parentDirectoryIndicatorsNumber * 3) + "\0", false);
            writer.Write(bytes.Length);
            writer.Write(bytes);
            writer.Write((ushort) this.GetUNCServerPartCharCount());
            writer.Write((ushort) 0xdead);
            writer.Write((long) 0L);
            writer.Write((long) 0L);
            writer.Write(0);
            if (!XLStringEncoder.StringHasHighBytes(this.Path))
            {
                writer.Write(0);
            }
            else
            {
                bytes = XLStringEncoder.GetBytes(this.Path.Substring(parentDirectoryIndicatorsNumber * 3), true);
                writer.Write((int) (bytes.Length + 6));
                writer.Write(bytes.Length);
                writer.Write((ushort) 3);
                writer.Write(bytes);
            }
        }

        public string Path
        {
            get => 
                this.path;
            set
            {
                value ??= string.Empty;
                this.path = value;
            }
        }
    }
}

