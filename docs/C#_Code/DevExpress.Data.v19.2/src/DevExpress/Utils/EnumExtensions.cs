namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class EnumExtensions
    {
        public static string GetEnumItemDisplayText(object current)
        {
            if (current == null)
            {
                return string.Empty;
            }
            string name = current.ToString();
            MemberInfo[] member = current.GetType().GetMember(name);
            if (member.Length == 0)
            {
                return name;
            }
            object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            int index = 0;
            while (true)
            {
                if (index < customAttributes.Length)
                {
                    Attribute attribute = (Attribute) customAttributes[index];
                    DescriptionAttribute attribute2 = attribute as DescriptionAttribute;
                    if (attribute2 == null)
                    {
                        index++;
                        continue;
                    }
                    name = attribute2.Description;
                }
                return name;
            }
        }

        public static Array GetValues(this Type enumType) => 
            Enum.GetValues(enumType);

        public static bool HasAnyFlag(this Enum value, params Enum[] flags)
        {
            foreach (Enum enum2 in flags)
            {
                if (value.HasFlag(enum2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasAnyValue(this Enum value, params Enum[] values)
        {
            foreach (Enum enum2 in values)
            {
                if (value.Equals(enum2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ToBoolean(this DefaultBoolean value, bool defaultValue) => 
            (value != DefaultBoolean.Default) ? (value == DefaultBoolean.True) : defaultValue;

        public static bool ToBoolean(this DefaultBoolean value, Func<bool> defaultValueEvaluator) => 
            (value != DefaultBoolean.Default) ? (value == DefaultBoolean.True) : defaultValueEvaluator();
    }
}

