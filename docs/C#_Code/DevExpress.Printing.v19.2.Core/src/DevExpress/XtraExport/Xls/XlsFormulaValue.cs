namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class XlsFormulaValue
    {
        private const ushort nanExpr = 0xffff;
        private FormulaValueUnion value;

        public void Read(XlReader reader)
        {
            this.value.Int64Value = reader.ReadInt64();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.value.Int64Value);
        }

        public bool IsString
        {
            get => 
                (this.value.ExprO == 0xffff) && (this.value.Byte1 == 0);
            set
            {
                if (value)
                {
                    this.value.Int64Value = 0L;
                    this.value.ExprO = 0xffff;
                }
            }
        }

        public bool IsBoolean =>
            (this.value.ExprO == 0xffff) && (this.value.Byte1 == 1);

        public bool IsError =>
            (this.value.ExprO == 0xffff) && (this.value.Byte1 == 2);

        public bool IsBlankString
        {
            get => 
                (this.value.ExprO == 0xffff) && (this.value.Byte1 == 3);
            set
            {
                if (value)
                {
                    this.value.Int64Value = 0L;
                    this.value.Byte1 = 3;
                    this.value.ExprO = 0xffff;
                }
            }
        }

        public bool IsNumeric =>
            this.value.ExprO != 0xffff;

        public bool BooleanValue
        {
            get => 
                this.IsBoolean && (this.value.Byte3 != 0);
            set
            {
                this.value.Int64Value = 0L;
                this.value.Byte1 = 1;
                this.value.Byte3 = value ? ((byte) 1) : ((byte) 0);
                this.value.ExprO = 0xffff;
            }
        }

        public byte ErrorCode
        {
            get => 
                !this.IsError ? 0 : this.value.Byte3;
            set
            {
                this.value.Int64Value = 0L;
                this.value.Byte1 = 2;
                this.value.Byte3 = value;
                this.value.ExprO = 0xffff;
            }
        }

        public double NumericValue
        {
            get => 
                !this.IsNumeric ? double.NaN : this.value.DoubleValue;
            set
            {
                if (XNumChecker.IsNegativeZero(value))
                {
                    this.value.DoubleValue = 0.0;
                }
                else
                {
                    XNumChecker.CheckValue(value);
                    this.value.DoubleValue = value;
                }
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct FormulaValueUnion
        {
            [FieldOffset(0)]
            public long Int64Value;
            [FieldOffset(0)]
            public double DoubleValue;
            [FieldOffset(0)]
            public byte Byte1;
            [FieldOffset(2)]
            public byte Byte3;
            [FieldOffset(6)]
            public ushort ExprO;
        }
    }
}

