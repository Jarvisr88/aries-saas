namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentSharedFormula : XlsContentBase
    {
        private const int fixedPartSize = 8;
        private XlsRefU range = new XlsRefU();
        private byte[] formulaBytes = new byte[2];

        public override int GetSize() => 
            8 + this.formulaBytes.Length;

        public override void Read(XlReader reader, int size)
        {
            this.range = XlsRefU.FromStream(reader);
            reader.ReadByte();
            this.UseCount = reader.ReadByte();
            int count = size - 8;
            this.formulaBytes = reader.ReadBytes(count);
        }

        public override void Write(BinaryWriter writer)
        {
            this.range.Write(writer);
            writer.Write((byte) 0);
            writer.Write(this.UseCount);
            writer.Write(this.formulaBytes);
        }

        public XlsRefU Range
        {
            get => 
                this.range;
            set
            {
                value ??= new XlsRefU();
                this.range = value;
            }
        }

        public byte UseCount { get; set; }

        public byte[] FormulaBytes
        {
            get => 
                this.formulaBytes;
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    this.formulaBytes = new byte[2];
                }
                else
                {
                    if (value.Length < 2)
                    {
                        throw new ArgumentException("value must be at least 2 bytes long");
                    }
                    this.formulaBytes = value;
                }
            }
        }
    }
}

