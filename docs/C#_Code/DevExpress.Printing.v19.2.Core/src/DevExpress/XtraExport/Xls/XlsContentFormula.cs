namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentFormula : XlsContentCellBase
    {
        private const short formulaValueSize = 8;
        private const short flagsSize = 2;
        private const short chnSize = 4;
        private XlsFormulaValue formulaValue = new XlsFormulaValue();
        private byte[] formulaBytes = new byte[2];

        private int GetFixedPartSize() => 
            ((base.GetSize() + 8) + 2) + 4;

        public override int GetSize() => 
            this.GetFixedPartSize() + this.formulaBytes.Length;

        public override void Read(XlReader reader, int size)
        {
            base.Read(reader, size);
            this.formulaValue.Read(reader);
            ushort num = reader.ReadUInt16();
            this.AlwaysCalc = (num & 1) != 0;
            this.CalcOnLoad = (num & 2) != 0;
            this.HasFillAlignment = (num & 4) != 0;
            this.PartOfSharedFormula = (num & 8) != 0;
            this.ClearErrors = (num & 0x20) != 0;
            long num2 = reader.ReadUInt32();
            if ((num2 >> 0x18) != 0)
            {
                this.NextCellOnChainRow = (int) (num2 & 0xffffL);
                this.NextCellOnChainColumn = (int) ((num2 >> 0x10) & 0xffL);
                this.IsStartOfChain = ((num2 >> 0x18) & 1L) == 0L;
                this.IsEndOfChain = ((num2 >> 0x19) & 1L) == 0L;
                this.IsArrayFormulaNext = ((num2 >> 0x1a) & 1L) == 0L;
            }
            int count = size - this.GetFixedPartSize();
            this.formulaBytes = reader.ReadBytes(count);
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            this.formulaValue.Write(writer);
            ushort num = 0;
            if (this.AlwaysCalc)
            {
                num = (ushort) (num | 1);
            }
            if (this.HasFillAlignment)
            {
                num = (ushort) (num | 4);
            }
            if (this.PartOfSharedFormula)
            {
                num = (ushort) (num | 8);
            }
            if (this.ClearErrors)
            {
                num = (ushort) (num | 0x20);
            }
            writer.Write(num);
            uint num2 = 0;
            if ((this.NextCellOnChainColumn != -1) && (this.NextCellOnChainRow != -1))
            {
                num2 = 0xff;
                if (this.IsStartOfChain)
                {
                    num2 &= 0xfe;
                }
                if (this.IsEndOfChain)
                {
                    num2 &= 0xfd;
                }
                if (this.IsArrayFormulaNext)
                {
                    num2 &= 0xfb;
                }
                num2 = (uint) ((((num2 << 8) | this.NextCellOnChainColumn) << 0x10) | this.NextCellOnChainRow);
            }
            writer.Write(num2);
            writer.Write(this.formulaBytes);
        }

        public XlsFormulaValue Value =>
            this.formulaValue;

        public bool AlwaysCalc { get; set; }

        public bool CalcOnLoad { get; private set; }

        public bool HasFillAlignment { get; set; }

        public bool PartOfSharedFormula { get; set; }

        public bool ClearErrors { get; set; }

        public int NextCellOnChainRow { get; set; }

        public int NextCellOnChainColumn { get; set; }

        public bool IsStartOfChain { get; set; }

        public bool IsEndOfChain { get; set; }

        public bool IsArrayFormulaNext { get; set; }

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

