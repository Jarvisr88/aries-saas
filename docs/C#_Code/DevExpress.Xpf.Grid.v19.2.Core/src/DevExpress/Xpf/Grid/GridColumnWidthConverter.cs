namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    public class GridColumnWidthConverter : TypeConverter
    {
        private static Dictionary<string, double> unitFactors;

        static GridColumnWidthConverter()
        {
            Dictionary<string, double> dictionary1 = new Dictionary<string, double>();
            dictionary1.Add("px", 1.0);
            dictionary1.Add("in", 96.0);
            dictionary1.Add("cm", 37.795275590551178);
            dictionary1.Add("pt", 1.3333333333333333);
            unitFactors = dictionary1;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            switch (Type.GetTypeCode(sourceType))
            {
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.String:
                    return true;
            }
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(InstanceDescriptor)) || (destinationType == typeof(string));

        public override object ConvertFrom(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object source)
        {
            if (source == null)
            {
                throw base.GetConvertFromException(source);
            }
            return (!(source is string) ? new GridColumnWidth(Convert.ToDouble(source, cultureInfo), GridColumnUnitType.Pixel) : this.FromString((string) source, cultureInfo));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((value != null) || (value is GridColumnWidth))
            {
                GridColumnWidth width = (GridColumnWidth) value;
                if (destinationType == typeof(string))
                {
                    return ToString(width, culture);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    Type[] types = new Type[] { typeof(double), typeof(GridColumnUnitType) };
                    object[] arguments = new object[] { width.Value, width.UnitType };
                    return new InstanceDescriptor(typeof(GridColumnWidth).GetConstructor(types), arguments);
                }
            }
            throw base.GetConvertToException(value, destinationType);
        }

        private GridColumnWidth FromString(string source, CultureInfo cultureInfo)
        {
            string str = source.Trim().ToLowerInvariant();
            if (str == "auto")
            {
                return new GridColumnWidth(120.0);
            }
            GridColumnUnitType pixel = GridColumnUnitType.Pixel;
            double num = 1.0;
            if (str.EndsWith("*", StringComparison.Ordinal))
            {
                pixel = GridColumnUnitType.Star;
                if (str.Length == 1)
                {
                    return new GridColumnWidth(1.0, GridColumnUnitType.Star);
                }
                str = str.Substring(0, str.Length - 1);
            }
            else
            {
                foreach (string str2 in unitFactors.Keys)
                {
                    if (str.EndsWith(str2, StringComparison.Ordinal))
                    {
                        num = unitFactors[str2];
                        str = str.Substring(0, str.Length - str2.Length);
                        break;
                    }
                }
            }
            return new GridColumnWidth(Convert.ToDouble(str, cultureInfo) * num, pixel);
        }

        public static string ToString(GridColumnWidth value, CultureInfo cultureInfo) => 
            (!value.IsStar || (value.Value != 1.0)) ? (Convert.ToString(value.Value, cultureInfo) + (value.IsStar ? "*" : string.Empty)) : "*";
    }
}

