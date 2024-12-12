namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentList12TableStyleInfo : XlsContentList12Base
    {
        private XLUnicodeString styleName = new XLUnicodeString();

        public override int GetSize() => 
            (base.GetSize() + 2) + this.styleName.Length;

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            ushort num = 0;
            if (this.FirstColumn)
            {
                num = (ushort) (num | 1);
            }
            if (this.LastColumn)
            {
                num = (ushort) (num | 2);
            }
            if (this.RowStripes)
            {
                num = (ushort) (num | 4);
            }
            if (this.ColumnStripes)
            {
                num = (ushort) (num | 8);
            }
            if (this.IsDefaultStyle)
            {
                num = (ushort) (num | 0x40);
            }
            writer.Write(num);
            this.styleName.Write(writer);
        }

        protected override short DataType =>
            1;

        public bool FirstColumn { get; set; }

        public bool LastColumn { get; set; }

        public bool RowStripes { get; set; }

        public bool ColumnStripes { get; set; }

        public bool IsDefaultStyle { get; set; }

        public string StyleName
        {
            get => 
                this.styleName.Value;
            set => 
                this.styleName.Value = value;
        }
    }
}

