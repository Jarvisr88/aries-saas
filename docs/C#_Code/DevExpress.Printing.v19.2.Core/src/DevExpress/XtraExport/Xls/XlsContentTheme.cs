namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentTheme : XlsContentBase
    {
        private byte[] themeContent = new byte[0];
        private readonly FutureRecordHeader recordHeader = new FutureRecordHeader();

        public override int GetSize() => 
            0x10 + this.themeContent.Length;

        public override void Read(XlReader reader, int size)
        {
            reader.ReadBytes(size);
        }

        public override void Write(BinaryWriter writer)
        {
            this.recordHeader.Write(writer);
            writer.Write((uint) this.ThemeVersion);
            writer.Write(this.themeContent);
        }

        public int ThemeVersion { get; set; }

        public override FutureRecordHeaderBase RecordHeader =>
            this.recordHeader;

        public byte[] ThemeContent
        {
            get => 
                this.themeContent;
            set
            {
                if (value == null)
                {
                    this.themeContent = new byte[0];
                }
                else
                {
                    this.themeContent = value;
                }
            }
        }
    }
}

