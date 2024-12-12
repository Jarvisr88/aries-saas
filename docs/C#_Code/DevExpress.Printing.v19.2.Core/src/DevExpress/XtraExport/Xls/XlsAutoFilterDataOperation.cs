namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class XlsAutoFilterDataOperation
    {
        private byte GetComparisonOperator(XlFilterOperator oper)
        {
            switch (oper)
            {
                case XlFilterOperator.GreaterThan:
                    return 4;

                case XlFilterOperator.GreaterThanOrEqual:
                    return 6;

                case XlFilterOperator.LessThan:
                    return 1;

                case XlFilterOperator.LessThanOrEqual:
                    return 3;

                case XlFilterOperator.NotEqual:
                    return 5;
            }
            return 2;
        }

        public int GetSize() => 
            10 + this.GetStringLength();

        public int GetStringLength()
        {
            if (!this.IsString)
            {
                return 0;
            }
            XLUnicodeStringNoCch cch = new XLUnicodeStringNoCch {
                Value = this.Value.TextValue
            };
            return cch.Length;
        }

        public void Write(BinaryWriter writer)
        {
            if (this.IsBlank)
            {
                writer.Write((byte) 0);
                writer.Write((byte) 0);
                writer.Write(0);
                writer.Write(0);
            }
            else if (this.AllBlanksMatched)
            {
                writer.Write((byte) 12);
                writer.Write((byte) 2);
                writer.Write(0);
                writer.Write(0);
            }
            else if (this.Value.IsEmpty || (this.Value.IsText && string.IsNullOrEmpty(this.Value.TextValue)))
            {
                writer.Write((byte) 0);
                writer.Write((byte) 2);
                writer.Write(0);
                writer.Write(0);
            }
            else if (this.Value.IsNumeric)
            {
                writer.Write((byte) 4);
                writer.Write(this.GetComparisonOperator(this.FilterOperator));
                writer.Write(this.Value.NumericValue);
            }
            else if (this.Value.IsText)
            {
                writer.Write((byte) 6);
                writer.Write(this.GetComparisonOperator(this.FilterOperator));
                this.WriteStringOper(writer);
            }
            else
            {
                writer.Write((byte) 8);
                writer.Write(this.GetComparisonOperator(this.FilterOperator));
                if (this.Value.IsBoolean)
                {
                    writer.Write(this.Value.BooleanValue ? ((byte) 1) : ((byte) 0));
                    writer.Write((byte) 0);
                }
                else
                {
                    writer.Write((byte) this.Value.ErrorValue.Type);
                    writer.Write((byte) 1);
                }
                writer.Write((ushort) 0);
                writer.Write(0);
            }
        }

        protected virtual void WriteStringOper(BinaryWriter writer)
        {
            writer.Write(0);
            writer.Write((byte) this.Value.TextValue.Length);
            writer.Write(this.IsSimple ? ((byte) 1) : ((byte) 0));
            writer.Write((ushort) 0);
        }

        public void WriteStringValue(BinaryWriter writer)
        {
            if (this.IsString)
            {
                new XLUnicodeStringNoCch { Value = this.Value.TextValue }.Write(writer);
            }
        }

        public XlFilterOperator FilterOperator { get; set; }

        public XlVariantValue Value { get; set; }

        public bool AllBlanksMatched { get; set; }

        public bool IsBlank { get; set; }

        public bool IsSimple
        {
            get
            {
                if (this.AllBlanksMatched)
                {
                    return true;
                }
                if (!this.Value.IsText)
                {
                    return false;
                }
                char[] anyOf = new char[] { '*', '?' };
                return (this.Value.TextValue.IndexOfAny(anyOf) == -1);
            }
        }

        private bool IsString =>
            (!this.IsBlank && !this.AllBlanksMatched) && this.Value.IsText;
    }
}

