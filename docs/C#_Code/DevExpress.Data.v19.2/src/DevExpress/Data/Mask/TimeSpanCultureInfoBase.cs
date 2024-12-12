namespace DevExpress.Data.Mask
{
    using System;
    using System.Globalization;

    public abstract class TimeSpanCultureInfoBase : CultureInfo
    {
        protected TimeSpanCultureInfoBase(int culture);
        protected TimeSpanCultureInfoBase(string name);
        protected TimeSpanCultureInfoBase(int culture, bool useUserOverride);
        protected TimeSpanCultureInfoBase(string name, bool useUserOverride);
        public abstract string GetStringLiteral(TimeSpanStringLiteralType type, long value);
    }
}

