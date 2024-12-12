namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    public class UniversalTypeConverterEx : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            (!(destinationType == typeof(InstanceDescriptor)) || (value == null)) ? base.ConvertTo(context, culture, value, destinationType) : new InstanceDescriptor(this.GetObjectType(value).GetConstructor(Type.EmptyTypes), null, false);

        protected virtual Type GetObjectType(object value) => 
            value.GetType();
    }
}

