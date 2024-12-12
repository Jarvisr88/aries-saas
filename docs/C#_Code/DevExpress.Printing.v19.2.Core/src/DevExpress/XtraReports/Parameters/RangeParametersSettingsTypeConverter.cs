namespace DevExpress.XtraReports.Parameters
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    internal class RangeParametersSettingsTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            RangeParametersSettings settings = value as RangeParametersSettings;
            if ((settings == null) || !(destinationType == typeof(InstanceDescriptor)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            Type[] types = new Type[] { typeof(RangeStartParameter), typeof(RangeEndParameter) };
            object[] arguments = new object[] { settings.StartParameter, settings.EndParameter };
            return new InstanceDescriptor(typeof(RangeParametersSettings).GetConstructor(types), arguments);
        }
    }
}

