namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class b : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !ReferenceEquals(destinationType, typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentException("Invalid destination type");
            }
            if (ReferenceEquals(destinationType, typeof(InstanceDescriptor)) && (value is ProxyOptions))
            {
                ProxyOptions options = (ProxyOptions) value;
                Type[] types = new Type[] { typeof(string), typeof(int), typeof(string), typeof(string) };
                ConstructorInfo constructor = typeof(ProxyOptions).GetConstructor(types);
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, new object[] { options.Host, options.Port, options.User, options.Password });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

