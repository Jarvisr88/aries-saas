namespace DevExpress.ReportServer.ServiceModel.Client
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    public class ServiceClientFactoryConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value is ReportServerClientFactory))
            {
                ReportServerClientFactory factory = (ReportServerClientFactory) value;
                Type[] types = new Type[] { typeof(string) };
                ConstructorInfo constructor = typeof(ReportServerClientFactory).GetConstructor(types);
                if (constructor != null)
                {
                    object[] arguments = new object[] { factory.EndpointConfigurationName };
                    return new InstanceDescriptor(constructor, arguments);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

