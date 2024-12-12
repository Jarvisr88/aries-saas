namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Design;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public static class DisplayTypeNameHelper
    {
        private static Dictionary<Type, DisplayNameAttribute> displayTypeNames = new Dictionary<Type, DisplayNameAttribute>();
        private static Dictionary<Enum, string> enumDisplayNames = new Dictionary<Enum, string>();

        public static string GetDisplayTypeName(Enum value)
        {
            if (!enumDisplayNames.ContainsKey(value))
            {
                EnumTypeConverter converter = new EnumTypeConverter(value.GetType());
                enumDisplayNames.Add(value, (string) converter.ConvertTo(new RuntimeTypeDescriptorContext(null, value), null, value, typeof(string)));
            }
            return enumDisplayNames[value];
        }

        public static string GetDisplayTypeName(Type type)
        {
            if (!displayTypeNames.ContainsKey(type))
            {
                object[] customAttributes = type.GetCustomAttributes(typeof(DXDisplayNameAttribute), true);
                if ((customAttributes == null) || (customAttributes.Length == 0))
                {
                    displayTypeNames.Add(type, new DisplayNameAttribute(type.FullName));
                }
                else
                {
                    DisplayNameAttribute attribute = (DisplayNameAttribute) customAttributes[0];
                    displayTypeNames.Add(type, attribute);
                }
            }
            return displayTypeNames[type].DisplayName;
        }

        public static Enum GetEnumValueFromDisplayName(Type enumType, string displayName) => 
            (Enum) new EnumTypeConverter(enumType).ConvertFromString(displayName);
    }
}

