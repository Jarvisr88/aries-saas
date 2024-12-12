namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsCondFmtValueObject
    {
        private const int fixedPartSize = 3;
        private byte[] formulaBytes = new byte[0];

        public XlsCondFmtValueObject()
        {
            this.ObjectType = XlCondFmtValueObjectType.Number;
        }

        public virtual int GetSize()
        {
            int num = 3 + this.formulaBytes.Length;
            if (!this.OmitValue())
            {
                num += 8;
            }
            return num;
        }

        private bool OmitValue() => 
            (this.formulaBytes.Length != 0) || ((this.ObjectType == XlCondFmtValueObjectType.Min) || (this.ObjectType == XlCondFmtValueObjectType.Max));

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(XlsCondFmtHelper.CfvoTypeToCode(this.ObjectType));
            ushort length = (ushort) this.formulaBytes.Length;
            writer.Write(length);
            if (length > 0)
            {
                writer.Write(this.formulaBytes);
            }
            if (!this.OmitValue())
            {
                writer.Write(this.Value);
            }
        }

        public XlCondFmtValueObjectType ObjectType { get; set; }

        public double Value { get; set; }

        public byte[] FormulaBytes
        {
            get => 
                this.formulaBytes;
            set
            {
                if ((value == null) || (value.Length < 2))
                {
                    this.formulaBytes = new byte[0];
                }
                else
                {
                    int length = BitConverter.ToUInt16(value, 0);
                    this.formulaBytes = new byte[length];
                    Array.Copy(value, 2, this.formulaBytes, 0, length);
                }
            }
        }
    }
}

