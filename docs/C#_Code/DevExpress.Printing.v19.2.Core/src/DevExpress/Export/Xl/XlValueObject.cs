namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;

    public class XlValueObject
    {
        private static readonly XlValueObject empty = new XlValueObject(XlVariantValue.Empty);
        private XlVariantValue variantValue;
        private XlCellRange rangeValue;
        private XlExpression expression;

        protected XlValueObject(XlCellRange range)
        {
            Guard.ArgumentNotNull(range, "range");
            this.variantValue = XlVariantValue.Empty;
            this.rangeValue = range;
            this.expression = null;
        }

        protected XlValueObject(XlExpression expression)
        {
            Guard.ArgumentNotNull(expression, "expression");
            this.variantValue = XlVariantValue.Empty;
            this.rangeValue = null;
            this.expression = expression;
        }

        protected XlValueObject(XlVariantValue value)
        {
            this.variantValue = value;
            this.rangeValue = null;
            this.expression = null;
        }

        public static XlValueObject FromObject(object value) => 
            new XlValueObject(XlVariantValue.FromObject(value));

        public static implicit operator XlValueObject(XlCellRange value) => 
            new XlValueObject(value);

        public static implicit operator XlValueObject(XlExpression value) => 
            new XlValueObject(value);

        public static implicit operator XlValueObject(XlVariantValue value) => 
            new XlValueObject(value);

        public static implicit operator XlValueObject(bool value) => 
            new XlValueObject(value);

        public static implicit operator XlValueObject(char value) => 
            new XlValueObject(value);

        public static implicit operator XlValueObject(DateTime value) => 
            new XlValueObject(value);

        public static implicit operator XlValueObject(double value) => 
            new XlValueObject(value);

        public static implicit operator XlValueObject(string value) => 
            new XlValueObject(value);

        public override string ToString() => 
            !this.IsEmpty ? (!this.IsRange ? this.variantValue.ToText().TextValue : this.RangeValue.ToString()) : string.Empty;

        public static XlValueObject Empty =>
            empty;

        protected internal XlVariantValue VariantValue =>
            this.variantValue;

        public bool IsEmpty =>
            !this.IsRange && (!this.IsExpression && this.variantValue.IsEmpty);

        public bool IsNumeric =>
            !this.IsRange && (!this.IsExpression && this.variantValue.IsNumeric);

        public bool IsBoolean =>
            !this.IsRange && (!this.IsExpression && this.variantValue.IsBoolean);

        public bool IsText =>
            !this.IsRange && (!this.IsExpression && (!this.IsFormula && this.variantValue.IsText));

        public bool IsError =>
            !this.IsRange && (!this.IsExpression && this.variantValue.IsError);

        public bool IsRange =>
            this.rangeValue != null;

        public bool IsExpression =>
            !this.IsRange && (this.expression != null);

        public bool IsFormula =>
            !this.IsRange && (!this.IsExpression && (this.variantValue.IsText && (!string.IsNullOrEmpty(this.variantValue.TextValue) && (this.variantValue.TextValue[0] == '='))));

        public double NumericValue =>
            this.variantValue.NumericValue;

        public DateTime DateTimeValue =>
            this.variantValue.DateTimeValue;

        public bool BooleanValue =>
            this.variantValue.BooleanValue;

        public string TextValue =>
            this.variantValue.TextValue;

        public IXlCellError ErrorValue =>
            this.variantValue.ErrorValue;

        public XlCellRange RangeValue =>
            this.rangeValue;

        public XlExpression Expression =>
            this.expression;

        public string Formula =>
            this.IsFormula ? this.variantValue.TextValue : string.Empty;
    }
}

