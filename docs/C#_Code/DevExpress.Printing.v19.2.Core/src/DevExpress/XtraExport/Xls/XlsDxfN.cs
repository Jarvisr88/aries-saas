namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsDxfN
    {
        private const int fixedPartSize = 6;
        private readonly XlsDxfNFlags flagsInfo = new XlsDxfNFlags();
        private readonly XlsDxfFont fontInfo = new XlsDxfFont();
        private readonly XlsDxfAlign alignmentInfo = new XlsDxfAlign();
        private readonly XlsDxfBorder borderInfo = new XlsDxfBorder();
        private readonly XlsDxfFill fillInfo = new XlsDxfFill();
        private readonly XlsDxfProtection protectionInfo = new XlsDxfProtection();
        private XlsDxfNum numberFormatInfo = new XlsDxfNumIFmt();

        internal virtual void AddExtProperty(XfPropBase prop)
        {
        }

        protected XlsDxfNum CreateDxfNum(bool isCustom) => 
            !isCustom ? ((XlsDxfNum) new XlsDxfNumIFmt()) : ((XlsDxfNum) new XlsDxfNumUser());

        public virtual short GetSize()
        {
            int num = 6;
            if (this.FlagsInfo.IncludeNumberFormat)
            {
                num += this.NumberFormatInfo.GetSize();
            }
            if (this.FlagsInfo.IncludeFont)
            {
                num += 0x76;
            }
            if (this.FlagsInfo.IncludeAlignment)
            {
                num += 8;
            }
            if (this.FlagsInfo.IncludeBorder)
            {
                num += 8;
            }
            if (this.FlagsInfo.IncludeFill)
            {
                num += 4;
            }
            if (this.FlagsInfo.IncludeProtection)
            {
                num += 2;
            }
            return (short) num;
        }

        public virtual void Read(BinaryReader reader)
        {
            this.FlagsInfo.Read(reader);
            if (this.FlagsInfo.IncludeNumberFormat)
            {
                this.NumberFormatInfo = this.ReadDxfNum(reader, this.FlagsInfo.UserDefinedNumberFormat);
            }
            if (this.FlagsInfo.IncludeFont)
            {
                this.FontInfo.Read(reader);
            }
            if (this.FlagsInfo.IncludeAlignment)
            {
                this.AlignmentInfo.Read(reader);
            }
            if (this.FlagsInfo.IncludeBorder)
            {
                this.BorderInfo.Read(reader);
            }
            if (this.FlagsInfo.IncludeFill)
            {
                this.FillInfo.Read(reader);
            }
            if (this.FlagsInfo.IncludeProtection)
            {
                this.ProtectionInfo.Read(reader);
            }
        }

        private XlsDxfNum ReadDxfNum(BinaryReader reader, bool isCustom)
        {
            XlsDxfNum num = this.CreateDxfNum(isCustom);
            num.Read(reader);
            return num;
        }

        internal virtual void SetIsEmpty(bool value)
        {
        }

        public virtual void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(this.GetSize());
            }
            this.FlagsInfo.Write(writer);
            if (this.FlagsInfo.IncludeNumberFormat)
            {
                this.NumberFormatInfo.Write(writer);
            }
            if (this.FlagsInfo.IncludeFont)
            {
                this.FontInfo.Write(writer);
            }
            if (this.FlagsInfo.IncludeAlignment)
            {
                this.AlignmentInfo.Write(writer);
            }
            if (this.FlagsInfo.IncludeBorder)
            {
                this.BorderInfo.Write(writer);
            }
            if (this.FlagsInfo.IncludeFill)
            {
                this.FillInfo.Write(writer);
            }
            if (this.FlagsInfo.IncludeProtection)
            {
                this.ProtectionInfo.Write(writer);
            }
        }

        public XlsDxfNFlags FlagsInfo =>
            this.flagsInfo;

        public XlsDxfNum NumberFormatInfo
        {
            get => 
                this.numberFormatInfo;
            set => 
                this.numberFormatInfo = value;
        }

        public XlsDxfFont FontInfo =>
            this.fontInfo;

        public XlsDxfAlign AlignmentInfo =>
            this.alignmentInfo;

        public XlsDxfBorder BorderInfo =>
            this.borderInfo;

        public XlsDxfFill FillInfo =>
            this.fillInfo;

        public XlsDxfProtection ProtectionInfo =>
            this.protectionInfo;
    }
}

