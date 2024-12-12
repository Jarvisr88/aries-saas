namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Text;

    public class XlsContentEncoding : XlsContentBase
    {
        private const short defaultCodePage = 0x4b0;
        private Encoding value = DXEncoding.GetEncodingFromCodePage(0x4b0);

        public override int GetSize() => 
            2;

        public override void Read(XlReader reader, int size)
        {
            try
            {
                int codePage = reader.ReadUInt16();
                if (codePage == 0x8001)
                {
                    codePage = 0x4e4;
                }
                if (codePage == 0x8000)
                {
                    codePage = 0x2710;
                }
                this.value = DXEncoding.GetEncodingFromCodePage(codePage);
            }
            catch
            {
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((short) DXEncoding.GetEncodingCodePage(this.value));
        }

        public Encoding Value
        {
            get => 
                this.value;
            set
            {
                Guard.ArgumentNotNull(value, "Value");
                this.value = value;
            }
        }
    }
}

