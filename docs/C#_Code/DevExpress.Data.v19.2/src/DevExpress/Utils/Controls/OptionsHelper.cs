namespace DevExpress.Utils.Controls
{
    using System;
    using System.ComponentModel;

    public class OptionsHelper
    {
        public static string GetObjectText(object obj) => 
            GetObjectText(obj, false);

        public static string GetObjectText(object obj, bool includeSubObjects)
        {
            string str = string.Empty;
            try
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
                {
                    if (!descriptor.IsBrowsable || (descriptor.SerializationVisibility == DesignerSerializationVisibility.Hidden))
                    {
                        continue;
                    }
                    if (descriptor.SerializationVisibility != DesignerSerializationVisibility.Content)
                    {
                        if (descriptor.IsReadOnly || !descriptor.ShouldSerializeValue(obj))
                        {
                            continue;
                        }
                        if (str.Length > 0)
                        {
                            str = str + ", ";
                        }
                        str = str + descriptor.Name;
                        object obj3 = descriptor.GetValue(obj);
                        str = !descriptor.PropertyType.Equals(typeof(string)) ? (str + $" = {obj3}") : (str + $" = '{descriptor.Converter.ConvertToString(obj3)}'");
                        continue;
                    }
                    if (includeSubObjects)
                    {
                        object obj2 = descriptor.GetValue(obj);
                        string str2 = (obj2 != null) ? obj2.ToString() : string.Empty;
                        if (!string.IsNullOrEmpty(str2))
                        {
                            if (str.Length > 0)
                            {
                                str = str + ", ";
                            }
                            str = str + $"{descriptor.Name} = {{ {str2} }}";
                        }
                    }
                }
            }
            catch
            {
            }
            return str;
        }

        public static object GetOptionValue(object obj, string name)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj)[name];
            return descriptor?.GetValue(obj);
        }

        public static T GetOptionValue<T>(object obj, string name)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj)[name];
            if (descriptor != null)
            {
                return (T) descriptor.GetValue(obj);
            }
            return default(T);
        }

        public static void SetOptionValue(object obj, string name, object value)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj)[name];
            if (descriptor != null)
            {
                descriptor.SetValue(obj, value);
            }
        }
    }
}

