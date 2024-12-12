namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentStyleExt : XlsContentBase
    {
        private readonly FutureRecordHeader recordHeader = new FutureRecordHeader();
        private LPWideString styleName = new LPWideString();
        private readonly XfProperties properties = new XfProperties();

        public override int GetSize() => 
            (0x10 + this.styleName.Length) + this.properties.GetSize();

        public override void Read(XlReader reader, int size)
        {
            reader.ReadBytes(size);
        }

        public override void Write(BinaryWriter writer)
        {
            this.recordHeader.Write(writer);
            byte num = 0;
            if (this.IsBuiltIn)
            {
                num = (byte) (num | 1);
            }
            if (this.IsHidden)
            {
                num = (byte) (num | 2);
            }
            if (this.CustomBuiltIn)
            {
                num = (byte) (num | 4);
            }
            writer.Write(num);
            writer.Write((byte) this.Category);
            if (!this.IsBuiltIn)
            {
                writer.Write((ushort) 0xffff);
            }
            else
            {
                writer.Write((byte) this.BuiltInId);
                if ((this.BuiltInId != 1) && (this.BuiltInId != 2))
                {
                    writer.Write((byte) 0xff);
                }
                else
                {
                    writer.Write((byte) (this.OutlineLevel - 1));
                }
            }
            this.styleName.Write(writer);
            this.properties.Write(writer);
        }

        public bool IsBuiltIn { get; set; }

        public bool IsHidden { get; set; }

        public bool CustomBuiltIn { get; set; }

        public XlStyleCategory Category { get; set; }

        public int BuiltInId { get; set; }

        public int OutlineLevel { get; set; }

        public string StyleName
        {
            get => 
                this.styleName.Value;
            set => 
                this.styleName.Value = value;
        }

        public XfProperties Properties =>
            this.properties;

        public override FutureRecordHeaderBase RecordHeader =>
            this.recordHeader;
    }
}

