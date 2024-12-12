namespace DevExpress.Export.Xl
{
    using System;

    public class XlPtgUnaryOperator : XlPtgOperator
    {
        public XlPtgUnaryOperator(int typeCode) : base(typeCode)
        {
        }

        private string GetOperatorText()
        {
            switch (this.TypeCode)
            {
                case 0x12:
                    return "+";

                case 0x13:
                    return "-";

                case 20:
                    return "%";
            }
            throw new InvalidOperationException("Invalid unary operator with typeCode + " + this.TypeCode);
        }

        protected override bool IsValidTypeCode(int typeCode) => 
            (typeCode >= 0x12) && (typeCode <= 20);

        public override void Visit(IXlPtgVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string OperatorText =>
            this.GetOperatorText();
    }
}

