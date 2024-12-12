namespace DevExpress.Export.Xl
{
    using System;

    public abstract class XlPtgOperator : XlPtgBase
    {
        private int typeCode;

        protected XlPtgOperator(int typeCode)
        {
            if (!this.IsValidTypeCode(typeCode))
            {
                throw new ArgumentException("Invalid type code.");
            }
            this.typeCode = typeCode;
        }

        protected abstract bool IsValidTypeCode(int typeCode);

        public override int TypeCode =>
            this.typeCode;
    }
}

