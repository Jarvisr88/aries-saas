namespace DevExpress.Office.Design
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    public class EncodingConverter : EnumLikeStructTypeConverter<Encoding>
    {
        protected internal override Encoding ConvertFromStringCore(ITypeDescriptorContext context, CultureInfo culture, string value)
        {
            EncodingInfo[] encodings = Encoding.GetEncodings();
            int length = encodings.Length;
            for (int i = 0; i < length; i++)
            {
                Encoding encoding = encodings[i].GetEncoding();
                if (string.Compare(encoding.EncodingName, value, true) == 0)
                {
                    return encoding;
                }
            }
            return Encoding.Default;
        }

        protected internal override string ConvertToStringCore(ITypeDescriptorContext context, CultureInfo culture, Encoding value) => 
            (value == null) ? base.ConvertToStringCore(context, culture, value) : value.EncodingName;

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            EncodingInfo[] encodings = Encoding.GetEncodings();
            int length = encodings.Length;
            List<Encoding> values = new List<Encoding>(length);
            for (int i = 0; i < length; i++)
            {
                values.Add(encodings[i].GetEncoding());
            }
            values.Sort(new EncodingComparer());
            return new TypeConverter.StandardValuesCollection(values);
        }
    }
}

