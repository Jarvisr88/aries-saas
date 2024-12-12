namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;

    public class DateTimeFormatConverter : StringConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            return ((string.IsNullOrEmpty(str) || (string.Compare(str, this.DefaultString, true, CultureInfo.CurrentCulture) == 0)) ? string.Empty : base.ConvertFrom(context, culture, value));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            !string.IsNullOrEmpty(value as string) ? base.ConvertTo(context, culture, value, destinationType) : this.DefaultString;

        protected internal virtual StringCollection GetDateTimeFormats(ITypeDescriptorContext context)
        {
            StringCollection strings = new StringCollection();
            if (context != null)
            {
                strings.AddRange(new DateTimeFormatInfo().GetAllDateTimePatterns());
            }
            return strings;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            StringCollection dateTimeFormats = this.GetDateTimeFormats(context);
            dateTimeFormats.Insert(0, string.Empty);
            return new TypeConverter.StandardValuesCollection(dateTimeFormats);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            true;

        protected virtual string DefaultString =>
            DesignSR.NoneString;
    }
}

