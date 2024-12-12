namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class ColumnPanelOffsetsPropertyConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            sourceType == typeof(string);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            false;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            char[] separator = new char[] { ',' };
            string[] strArray = ((string) value).Split(separator);
            int[] numArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                numArray[i] = int.Parse(strArray[i]);
            }
            return numArray;
        }
    }
}

