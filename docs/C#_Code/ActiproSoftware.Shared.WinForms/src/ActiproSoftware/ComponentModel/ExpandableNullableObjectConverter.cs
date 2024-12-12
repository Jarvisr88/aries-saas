namespace ActiproSoftware.ComponentModel
{
    using #H;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class ExpandableNullableObjectConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) => 
            !(destType == Type.GetTypeFromHandle(typeof(string).TypeHandle)) ? base.CanConvertTo(context, destType) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType) => 
            !(destType == Type.GetTypeFromHandle(typeof(string).TypeHandle)) ? base.ConvertTo(context, culture, value, destType) : ((value != null) ? (#G.#eg(0x2f1a) + value.GetType().Name + #G.#eg(0xfc2)) : #G.#eg(0x2f11));
    }
}

