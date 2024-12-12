namespace DevExpress.XtraPrinting.Design
{
    using DevExpress.Utils.Design;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class BordersConverter : EnumTypeConverter
    {
        public BordersConverter() : base(typeof(BorderSide))
        {
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            try
            {
                return base.ConvertFrom(context, culture, value);
            }
            catch (FormatException)
            {
                return BorderSide.None;
            }
        }
    }
}

