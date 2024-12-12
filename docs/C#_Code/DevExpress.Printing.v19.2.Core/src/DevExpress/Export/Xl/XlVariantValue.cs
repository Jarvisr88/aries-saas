namespace DevExpress.Export.Xl
{
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct XlVariantValue
    {
        private const double MaxDateTimeSerialNumber = 2958465.99999999;
        private const double MaxDateTimeSerialNumber1904 = 2957003.99999999;
        private const string trueConstant = "TRUE";
        private const string falseConstant = "FALSE";
        private static readonly DateTime baseDate;
        private static readonly DateTime baseDate1;
        private static readonly TimeSpan day29Feb1900;
        private static readonly DateTime baseDate1904;
        private static readonly DateTime baseDate1900;
        public static readonly XlVariantValue Empty;
        public static readonly XlVariantValue ErrorInvalidValueInFunction;
        public static readonly XlVariantValue ErrorDivisionByZero;
        public static readonly XlVariantValue ErrorNumber;
        public static readonly XlVariantValue ErrorReference;
        public static readonly XlVariantValue ErrorValueNotAvailable;
        public static readonly XlVariantValue ErrorNullIntersection;
        public static readonly XlVariantValue ErrorName;
        private double numericValue;
        private object referenceValue;
        private XlVariantValueType type;
        public static DateTime BaseDate =>
            baseDate;
        public static TimeSpan Day29Feb1900 =>
            day29Feb1900;
        internal static DateTime BaseDate1900 =>
            baseDate1900;
        public static string TrueConstant =>
            "TRUE";
        public static string FalseConstant =>
            "FALSE";
        public XlVariantValueType Type =>
            this.type;
        public bool IsEmpty =>
            this.type == XlVariantValueType.None;
        public bool IsNumeric =>
            (this.type == XlVariantValueType.Numeric) || (this.type == XlVariantValueType.DateTime);
        public bool IsBoolean =>
            this.type == XlVariantValueType.Boolean;
        public bool IsText =>
            this.type == XlVariantValueType.Text;
        public bool IsError =>
            this.type == XlVariantValueType.Error;
        public double NumericValue
        {
            [DebuggerStepThrough]
            get => 
                this.numericValue;
            [DebuggerStepThrough]
            set
            {
                this.type = XlVariantValueType.Numeric;
                this.numericValue = value;
                this.referenceValue = null;
            }
        }
        public DateTime DateTimeValue
        {
            [DebuggerStepThrough]
            get => 
                (this.type == XlVariantValueType.DateTime) ? FromDateTimeSerialLessThan1900(this.numericValue) : FromDateTimeSerial(this.numericValue);
            [DebuggerStepThrough]
            set => 
                this.SetDateTime(value);
        }
        public bool BooleanValue
        {
            [DebuggerStepThrough]
            get => 
                !(this.numericValue == 0.0);
            [DebuggerStepThrough]
            set
            {
                this.type = XlVariantValueType.Boolean;
                this.numericValue = value ? ((double) 1) : ((double) 0);
                this.referenceValue = null;
            }
        }
        public IXlCellError ErrorValue
        {
            [DebuggerStepThrough]
            get => 
                this.referenceValue as IXlCellError;
            [DebuggerStepThrough]
            set
            {
                this.type = XlVariantValueType.Error;
                this.numericValue = 0.0;
                this.referenceValue = value;
            }
        }
        private void SetDateTime(DateTime value)
        {
            if (((value.Year == 1) && (value.Month == 1)) && (value.Day == 1))
            {
                value = new DateTime(0x76b, 12, 0x1f).AddTicks(value.Ticks);
            }
            else if (value <= baseDate.AddDays(1.0))
            {
                value = baseDate.AddDays(1.0);
            }
            this.type = XlVariantValueType.Numeric;
            this.numericValue = ToDateTimeSerialDouble(value);
            this.referenceValue = null;
        }

        internal void SetDateLessThan1900(DateTime value)
        {
            if (((value.Year == 1) && (value.Month == 1)) && (value.Day == 1))
            {
                value = new DateTime(0x76b, 12, 0x1f).AddTicks(value.Ticks);
                this.numericValue = ToDateTimeSerialDouble(value);
                this.referenceValue = null;
            }
            else
            {
                TimeSpan span = (TimeSpan) (value - BaseDate1900);
                this.numericValue = span.TotalDays;
                this.referenceValue = null;
            }
            this.type = XlVariantValueType.DateTime;
        }

        public string TextValue
        {
            [DebuggerStepThrough]
            get => 
                this.referenceValue as string;
            set
            {
                value ??= string.Empty;
                this.type = XlVariantValueType.Text;
                this.numericValue = 0.0;
                this.referenceValue = value;
            }
        }
        [DebuggerStepThrough]
        public override int GetHashCode() => 
            (this.referenceValue != null) ? (((((int) this.type) << 0x1d) ^ this.numericValue.GetHashCode()) ^ this.referenceValue.GetHashCode()) : ((((int) this.type) << 0x1d) ^ this.numericValue.GetHashCode());

        public override bool Equals(object obj)
        {
            if (!(obj is XlVariantValue))
            {
                return false;
            }
            XlVariantValue value2 = (XlVariantValue) obj;
            return ((value2.type == this.type) && ((value2.numericValue == this.numericValue) && ((this.type != XlVariantValueType.Text) ? (value2.referenceValue == this.referenceValue) : Equals(this.referenceValue as string, value2.referenceValue as string))));
        }

        [DebuggerStepThrough]
        public static bool operator ==(XlVariantValue first, XlVariantValue second) => 
            (first.type == second.type) && ((first.numericValue == second.numericValue) && ((first.Type != XlVariantValueType.Text) ? (first.referenceValue == second.referenceValue) : ((first.referenceValue as string) == (second.referenceValue as string))));

        [DebuggerStepThrough]
        public static bool operator !=(XlVariantValue first, XlVariantValue second) => 
            (first.type != second.type) || ((first.numericValue != second.numericValue) || ((first.Type != XlVariantValueType.Text) ? (first.referenceValue != second.referenceValue) : ((first.referenceValue as string) != (second.referenceValue as string))));

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                return ErrorNumber;
            }
            return new XlVariantValue { NumericValue = !XNumChecker.IsNegativeZero(value) ? value : 0.0 };
        }

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
            {
                return ErrorNumber;
            }
            return new XlVariantValue { NumericValue = !XNumChecker.IsNegativeZero((double) value) ? ConvertFloat(value) : 0.0 };
        }

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(DateTime value) => 
            new XlVariantValue { DateTimeValue = value };

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(TimeSpan value) => 
            new XlVariantValue { DateTimeValue = baseDate1.Add(value) };

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(char value) => 
            new XlVariantValue { TextValue = char.ToString(value) };

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(string value)
        {
            if (value == null)
            {
                return Empty;
            }
            return new XlVariantValue { TextValue = value };
        }

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(bool value) => 
            new XlVariantValue { BooleanValue = value };

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(int value) => 
            new XlVariantValue { NumericValue = value };

        [DebuggerStepThrough]
        public static implicit operator XlVariantValue(long value) => 
            new XlVariantValue { NumericValue = value };

        public XlVariantValue ToText()
        {
            switch (this.type)
            {
                case XlVariantValueType.None:
                case XlVariantValueType.Text:
                    return this.TextValue;

                case XlVariantValueType.Boolean:
                    return (this.BooleanValue ? "TRUE" : "FALSE");

                case XlVariantValueType.Numeric:
                case XlVariantValueType.DateTime:
                    return this.ConvertNumberToText(this.NumericValue);

                case XlVariantValueType.Error:
                    return this.ErrorValue.Name;
            }
            return this;
        }

        public XlVariantValue ToText(CultureInfo culture)
        {
            switch (this.type)
            {
                case XlVariantValueType.None:
                case XlVariantValueType.Text:
                    return this.TextValue;

                case XlVariantValueType.Boolean:
                    return (this.BooleanValue ? "TRUE" : "FALSE");

                case XlVariantValueType.Numeric:
                case XlVariantValueType.DateTime:
                    return this.ConvertNumberToText(this.NumericValue, culture);

                case XlVariantValueType.Error:
                    return this.ErrorValue.Name;
            }
            return this;
        }

        private string ConvertNumberToText(double value)
        {
            string s = value.ToString(CultureInfo.InvariantCulture);
            if ((value > 1E+15) && (value < 1E+16))
            {
                try
                {
                    double num = double.Parse(s, CultureInfo.InvariantCulture);
                    long num2 = (long) num;
                    if (num == num2)
                    {
                        string str2 = num2.ToString(CultureInfo.InvariantCulture);
                        if (str2.Length < s.Length)
                        {
                            return str2;
                        }
                    }
                }
                catch
                {
                }
            }
            return s;
        }

        private string ConvertNumberToText(double value, CultureInfo culture)
        {
            string s = value.ToString(culture);
            if ((value > 1E+15) && (value < 1E+16))
            {
                try
                {
                    double num = double.Parse(s, culture);
                    long num2 = (long) num;
                    if (num == num2)
                    {
                        string str2 = num2.ToString(culture);
                        if (str2.Length < s.Length)
                        {
                            return str2;
                        }
                    }
                }
                catch
                {
                }
            }
            return s;
        }

        internal static bool IsErrorDateTimeSerial(double serialNumber, bool date1904) => 
            !date1904 ? ((serialNumber < 0.0) || (serialNumber > 2958465.99999999)) : ((serialNumber < 0.0) || (serialNumber > 2957003.99999999));

        private static DateTime FromDateTimeSerial(double value) => 
            (value <= 60.0) ? (BaseDate + TimeSpan.FromDays(value + 1.0)) : (BaseDate + TimeSpan.FromDays(value));

        private static DateTime FromDateTimeSerialLessThan1900(double value) => 
            (value <= 60.0) ? ((value >= 0.0) ? (BaseDate + TimeSpan.FromDays(value + 1.0)) : (BaseDate1900 + TimeSpan.FromDays(value))) : (BaseDate + TimeSpan.FromDays(value));

        private static DateTime FromDateTimeSerial(double value, bool date1904) => 
            !date1904 ? FromDateTimeSerial(value) : (baseDate1904 + TimeSpan.FromDays(value));

        private static double ToDateTimeSerialDouble(DateTime value)
        {
            TimeSpan span = (TimeSpan) (value - BaseDate);
            return ((span <= Day29Feb1900) ? (span.TotalDays - 1.0) : span.TotalDays);
        }

        internal DateTime GetDateTime() => 
            (this.numericValue <= 60.0) ? ((this.numericValue < 1.0) ? ((this.numericValue <= 0.0) ? ((this.numericValue >= 0.0) ? DateTime.MinValue : (BaseDate1900 + TimeSpan.FromDays(this.numericValue))) : (DateTime.MinValue + TimeSpan.FromDays(this.numericValue))) : (BaseDate + TimeSpan.FromDays(this.numericValue + 1.0))) : (BaseDate + TimeSpan.FromDays(this.numericValue));

        public void SetDateTimeSerial(double value, bool date1904)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                this.ErrorValue = NumberError.Instance;
            }
            else
            {
                if (XNumChecker.IsNegativeZero(value))
                {
                    value = 0.0;
                }
                if ((value < 0.0) || IsErrorDateTimeSerial(value, date1904))
                {
                    this.NumericValue = value;
                }
                else
                {
                    DateTime time = FromDateTimeSerial(value, date1904);
                    this.SetDateTime(time);
                    this.type = XlVariantValueType.DateTime;
                }
            }
        }

        internal static XlVariantValue FromObject(object value, bool fallbackToString)
        {
            if (value == null)
            {
                return Empty;
            }
            if (Convert.IsDBNull(value))
            {
                return Empty;
            }
            XlVariantValue value2 = FromObject(value);
            return ((!fallbackToString || !value2.IsEmpty) ? value2 : Convert.ToString(value));
        }

        public static XlVariantValue FromObject(object value)
        {
            if (value == null)
            {
                return Empty;
            }
            if (Convert.IsDBNull(value))
            {
                return Empty;
            }
            System.Type type = value.GetType();
            if (type == typeof(string))
            {
                return (string) value;
            }
            if (type == typeof(DateTime))
            {
                return (DateTime) value;
            }
            if (type == typeof(bool))
            {
                return (bool) value;
            }
            if (type == typeof(double))
            {
                return Convert.ToDouble(value);
            }
            if (type == typeof(int))
            {
                return Convert.ToDouble(value);
            }
            if (type == typeof(long))
            {
                return Convert.ToDouble(value);
            }
            if (type == typeof(decimal))
            {
                return Convert.ToDouble(value);
            }
            if (!(type == typeof(float)))
            {
                return (!(type == typeof(short)) ? (!(type == typeof(byte)) ? (!(type == typeof(ushort)) ? (!(type == typeof(uint)) ? (!(type == typeof(ulong)) ? (!(type == typeof(TimeSpan)) ? (!(type == typeof(DateTimeOffset)) ? Empty : ((DateTimeOffset) value).DateTime) : ((TimeSpan) value)) : Convert.ToDouble(value)) : Convert.ToDouble(value)) : Convert.ToDouble(value)) : Convert.ToDouble(value)) : Convert.ToDouble(value));
            }
            float f = Convert.ToSingle(value);
            if (float.IsNaN(f) || float.IsInfinity(f))
            {
                return ErrorNumber;
            }
            if (XNumChecker.IsNegativeZero((double) f))
            {
                f = 0f;
            }
            return ConvertFloat(f);
        }

        private static double ConvertFloat(float value) => 
            (value <= 7.922816E+28f) ? ((value >= -7.922816E+28f) ? ((double) ((decimal) value)) : ((double) value)) : ((double) value);

        private static XlVariantValue FromError(IXlCellError value) => 
            new XlVariantValue { 
                type = XlVariantValueType.Error,
                referenceValue = value
            };

        internal DateTime GetDateTimeForMonthName()
        {
            double numericValue = this.NumericValue;
            return ((numericValue >= 2.0) ? (((numericValue < 60.0) || (numericValue >= 61.0)) ? ((numericValue >= 61.0) ? (BaseDate + TimeSpan.FromDays(numericValue)) : (BaseDate + TimeSpan.FromDays(numericValue + 1.0))) : BaseDate.AddDays(59.0)) : BaseDate.AddDays(2.0));
        }

        internal DateTime GetDateTimeForDayOfWeek() => 
            BaseDate + TimeSpan.FromDays(this.NumericValue);

        static XlVariantValue()
        {
            baseDate = new DateTime(0x76b, 12, 30);
            baseDate1 = new DateTime(0x76b, 12, 0x1f);
            day29Feb1900 = TimeSpan.FromDays(60.0);
            baseDate1904 = new DateTime(0x770, 1, 1);
            baseDate1900 = new DateTime(0x76c, 1, 1);
            Empty = new XlVariantValue();
            ErrorInvalidValueInFunction = FromError(InvalidValueInFunctionError.Instance);
            ErrorDivisionByZero = FromError(DivisionByZeroError.Instance);
            ErrorNumber = FromError(NumberError.Instance);
            ErrorReference = FromError(ReferenceError.Instance);
            ErrorValueNotAvailable = FromError(ValueNotAvailableError.Instance);
            ErrorNullIntersection = FromError(NullIntersectionError.Instance);
            ErrorName = FromError(NameError.Instance);
        }
    }
}

