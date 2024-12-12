namespace ActiproSoftware.WinUICore
{
    using #H;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class UIElementCollectionConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) => 
            !(destType == Type.GetTypeFromHandle(typeof(string).TypeHandle)) ? base.CanConvertTo(context, destType) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (destType == Type.GetTypeFromHandle(typeof(string).TypeHandle))
            {
                UIElementCollection elements = (UIElementCollection) value;
                if (elements != null)
                {
                    return ((elements.Count != 0) ? ((elements.Count != 1) ? string.Format(#G.#eg(0x5fe), elements.Count, elements.PluralElementName) : string.Format(#G.#eg(0x5f1), elements.SingularElementName)) : string.Format(#G.#eg(0x5e4), elements.PluralElementName));
                }
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}

