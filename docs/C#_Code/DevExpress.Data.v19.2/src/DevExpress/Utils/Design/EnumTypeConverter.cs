namespace DevExpress.Utils.Design
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class EnumTypeConverter : EnumConverter
    {
        private static readonly object dataGuard = new object();
        private static readonly Dictionary<Type, HashEntry> data = new Dictionary<Type, HashEntry>();

        public EnumTypeConverter(Type type) : base(type)
        {
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string str = this.DisplayNameToName(base.EnumType, (string) value);
                if (!string.IsNullOrEmpty(str))
                {
                    value = str;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((context == null) || !(destinationType == typeof(string)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if (value is string)
            {
                return value;
            }
            string name = (string) base.ConvertTo(context, culture, value, destinationType);
            return this.NameToDisplayName(base.EnumType, name);
        }

        protected string DisplayNameToName(Type enumType, string displayName)
        {
            displayName = displayName.Trim();
            this.InitializeInternal(enumType);
            if (!this.IsFlagsDefined(enumType))
            {
                return GetName(enumType, displayName);
            }
            char[] separator = new char[] { ',' };
            string[] names = GetNames(enumType, displayName.Split(separator));
            return string.Join(",", names);
        }

        private static string GetDisplayName(Type enumType, string name)
        {
            HashEntry entry;
            bool flag;
            object dataGuard = EnumTypeConverter.dataGuard;
            lock (dataGuard)
            {
                flag = data.TryGetValue(enumType, out entry);
            }
            return (flag ? entry.NameToDisplayName(name) : null);
        }

        private static string[] GetDisplayNames(Type enumType, string[] names)
        {
            List<string> list = new List<string>();
            foreach (string str in names)
            {
                string displayName = GetDisplayName(enumType, str);
                if (!string.IsNullOrEmpty(displayName))
                {
                    list.Add(displayName);
                }
            }
            return list.ToArray();
        }

        private static string GetName(Type enumType, string displayName)
        {
            HashEntry entry;
            bool flag;
            object dataGuard = EnumTypeConverter.dataGuard;
            lock (dataGuard)
            {
                flag = data.TryGetValue(enumType, out entry);
            }
            return (flag ? entry.DisplayNameToName(displayName) : null);
        }

        private static string[] GetNames(Type enumType, string[] displayNames)
        {
            List<string> list = new List<string>();
            foreach (string str in displayNames)
            {
                string name = GetName(enumType, str);
                if (!string.IsNullOrEmpty(name))
                {
                    list.Add(name);
                }
            }
            return list.ToArray();
        }

        protected static void Initialize(Type enumType)
        {
            ResourceFinderAttribute attribute = TypeDescriptor.GetAttributes(enumType)[typeof(ResourceFinderAttribute)] as ResourceFinderAttribute;
            if (attribute != null)
            {
                Initialize(enumType, attribute.ResourceFinder, attribute.ResourceFile);
            }
        }

        protected static void Initialize(Type enumType, Type resourceFinder)
        {
            Initialize(enumType, resourceFinder, "PropertyNamesRes");
        }

        protected static void Initialize(Type enumType, Type resourceFinder, string resourceFile)
        {
            object dataGuard = EnumTypeConverter.dataGuard;
            lock (dataGuard)
            {
                if (!data.ContainsKey(enumType))
                {
                    data.Add(enumType, new HashEntry(enumType, resourceFinder, resourceFile));
                }
            }
        }

        protected virtual void InitializeInternal(Type enumType)
        {
            Initialize(enumType);
        }

        private bool IsFlagsDefined(Type type) => 
            type.IsDefined(typeof(FlagsAttribute), false);

        protected string NameToDisplayName(Type enumType, string name)
        {
            this.InitializeInternal(enumType);
            if (!this.IsFlagsDefined(enumType))
            {
                return GetDisplayName(enumType, name);
            }
            char[] separator = new char[] { ',' };
            string[] displayNames = GetDisplayNames(enumType, name.Split(separator));
            return string.Join(",", displayNames);
        }

        internal static void Refresh()
        {
            object dataGuard = EnumTypeConverter.dataGuard;
            lock (dataGuard)
            {
                data.Clear();
            }
        }

        protected class HashEntry
        {
            public string[] names;
            public DXDisplayNameAttribute[] displayNames;

            public HashEntry(Type enumType, Type resourceFinder, string resourceFile)
            {
                this.names = Helpers.GetEnumNames(enumType);
                Array enumValues = Helpers.GetEnumValues(enumType);
                this.displayNames = new DXDisplayNameAttribute[this.names.Length];
                for (int i = 0; i < this.names.Length; i++)
                {
                    this.displayNames[i] = GetDisplayName(resourceFinder, resourceFile, enumValues.GetValue(i), this.names[i]);
                }
            }

            public string DisplayNameToName(string displayName)
            {
                displayName = displayName.Trim();
                for (int i = 0; i < this.displayNames.Length; i++)
                {
                    string str = this.GetDisplayName(i);
                    if ((str != null) && string.Equals(str.Trim(), displayName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return this.names[i];
                    }
                }
                return null;
            }

            private string GetDisplayName(int index) => 
                (index >= 0) ? this.displayNames[index].DisplayName : null;

            private static DXDisplayNameAttribute GetDisplayName(Type resourceFinder, string resourceFile, object enumValue, string enumName) => 
                new DXDisplayNameAttribute(resourceFinder, resourceFile, GetResourceName(enumValue), enumName);

            private static string GetResourceName(object enumValue) => 
                enumValue.GetType().FullName + "." + enumValue;

            public string NameToDisplayName(string name)
            {
                int index = Array.IndexOf<string>(this.names, name.Trim());
                return this.GetDisplayName(index)?.Trim();
            }
        }
    }
}

