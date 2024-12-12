namespace DevExpress.XtraReports.Design
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class DataBarExpandedFNC1Converter : StringConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            DataBarGenerator instance = context.Instance as DataBarGenerator;
            return (((sourceType != typeof(string)) || ((instance != null) && ((instance.Type == DataBarType.ExpandedStacked) || (instance.Type == DataBarType.Expanded)))) ? base.CanConvertFrom(context, sourceType) : false);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            DataBarGenerator instance = context.Instance as DataBarGenerator;
            return (((destinationType != typeof(string)) || ((instance != null) && ((instance.Type == DataBarType.ExpandedStacked) || (instance.Type == DataBarType.Expanded)))) ? base.ConvertTo(context, culture, value, destinationType) : "(None)");
        }
    }
}

