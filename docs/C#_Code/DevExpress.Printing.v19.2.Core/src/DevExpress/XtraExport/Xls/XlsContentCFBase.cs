namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class XlsContentCFBase : XlsContentBase
    {
        private const int fixedPartSize = 6;
        private byte[] firstFormulaBytes = new byte[0];
        private byte[] secondFormulaBytes = new byte[0];

        protected XlsContentCFBase()
        {
        }

        private int GetSecondFormulaSize() => 
            (this.RuleType == XlCondFmtType.CellIs) ? (((this.Operator == XlCondFmtOperator.Between) || (this.Operator == XlCondFmtOperator.NotBetween)) ? this.secondFormulaBytes.Length : 0) : 0;

        public override int GetSize() => 
            ((6 + this.DifferentialFormat.GetSize()) + this.firstFormulaBytes.Length) + this.GetSecondFormulaSize();

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(XlsCondFmtHelper.RuleTypeToCode(this.RuleType));
            writer.Write(XlsCondFmtHelper.OperatorToCode(this.Operator));
            ushort length = (ushort) this.firstFormulaBytes.Length;
            writer.Write(length);
            ushort secondFormulaSize = (ushort) this.GetSecondFormulaSize();
            writer.Write(secondFormulaSize);
            this.DifferentialFormat.Write(writer);
            if (length > 0)
            {
                writer.Write(this.firstFormulaBytes);
            }
            if (secondFormulaSize > 0)
            {
                writer.Write(this.secondFormulaBytes);
            }
        }

        public XlCondFmtType RuleType { get; set; }

        public XlCondFmtOperator Operator { get; set; }

        public byte[] FirstFormulaBytes
        {
            get => 
                this.firstFormulaBytes;
            set
            {
                if ((value == null) || (value.Length < 2))
                {
                    this.firstFormulaBytes = new byte[0];
                }
                else
                {
                    int length = BitConverter.ToUInt16(value, 0);
                    this.firstFormulaBytes = new byte[length];
                    Array.Copy(value, 2, this.firstFormulaBytes, 0, length);
                }
            }
        }

        public byte[] SecondFormulaBytes
        {
            get => 
                this.secondFormulaBytes;
            set
            {
                if ((value == null) || (value.Length < 2))
                {
                    this.secondFormulaBytes = new byte[0];
                }
                else
                {
                    int length = BitConverter.ToUInt16(value, 0);
                    this.secondFormulaBytes = new byte[length];
                    Array.Copy(value, 2, this.secondFormulaBytes, 0, length);
                }
            }
        }

        protected abstract XlsDxfN DifferentialFormat { get; }
    }
}

