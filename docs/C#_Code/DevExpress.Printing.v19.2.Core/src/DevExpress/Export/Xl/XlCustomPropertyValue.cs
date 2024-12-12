namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct XlCustomPropertyValue
    {
        public static readonly XlCustomPropertyValue Empty;
        private XlVariantValueType type;
        private double numericValue;
        private DateTime dateTimeValue;
        private string textValue;
        public XlVariantValueType Type =>
            this.type;
        public double NumericValue
        {
            get => 
                this.numericValue;
            private set
            {
                this.numericValue = value;
                this.textValue = null;
                this.type = XlVariantValueType.Numeric;
            }
        }
        public DateTime DateTimeValue
        {
            get => 
                this.dateTimeValue;
            private set
            {
                this.dateTimeValue = value;
                this.numericValue = 0.0;
                this.textValue = null;
                this.type = XlVariantValueType.DateTime;
            }
        }
        public bool BooleanValue
        {
            get => 
                !(this.numericValue == 0.0);
            private set
            {
                this.numericValue = value ? ((double) 1) : ((double) 0);
                this.textValue = null;
                this.type = XlVariantValueType.Boolean;
            }
        }
        public string TextValue
        {
            get => 
                this.textValue;
            private set
            {
                this.textValue = value;
                this.numericValue = 0.0;
                this.type = XlVariantValueType.Text;
            }
        }
        public static implicit operator XlCustomPropertyValue(double value) => 
            new XlCustomPropertyValue { NumericValue = value };

        public static implicit operator XlCustomPropertyValue(DateTime value) => 
            new XlCustomPropertyValue { DateTimeValue = value };

        public static implicit operator XlCustomPropertyValue(char value) => 
            new XlCustomPropertyValue { TextValue = char.ToString(value) };

        public static implicit operator XlCustomPropertyValue(string value)
        {
            if (value == null)
            {
                return Empty;
            }
            return new XlCustomPropertyValue { TextValue = value };
        }

        public static implicit operator XlCustomPropertyValue(bool value) => 
            new XlCustomPropertyValue { BooleanValue = value };

        static XlCustomPropertyValue()
        {
        }
    }
}

