namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgStr : XlPtgBase
    {
        private string innerString = string.Empty;

        public XlPtgStr(string value)
        {
            this.Value = value;
        }

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int TypeCode =>
            0x17;

        public string Value
        {
            get => 
                this.innerString;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = string.Empty;
                }
                if (value.Length > 0xff)
                {
                    throw new ArgumentException("String value too long");
                }
                this.innerString = value;
            }
        }
    }
}

