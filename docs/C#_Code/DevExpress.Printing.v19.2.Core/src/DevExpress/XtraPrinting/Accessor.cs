namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    public class Accessor
    {
        public static EventDescriptor CreateEvent(Type type, EventDescriptor oldEventDescriptor, Attribute[] attributes)
        {
            if (oldEventDescriptor == null)
            {
                return null;
            }
            ArrayList list = new ArrayList(oldEventDescriptor.Attributes);
            list.AddRange(attributes);
            return TypeDescriptor.CreateEvent(type, oldEventDescriptor, (Attribute[]) list.ToArray(typeof(Attribute)));
        }

        public static PropertyDescriptor CreateProperty(Type type, PropertyDescriptor oldPropertyDescriptor, Attribute[] attributes)
        {
            if (oldPropertyDescriptor == null)
            {
                return null;
            }
            ArrayList list = new ArrayList(oldPropertyDescriptor.Attributes);
            list.AddRange(attributes);
            return TypeDescriptor.CreateProperty(type, oldPropertyDescriptor, (Attribute[]) list.ToArray(typeof(Attribute)));
        }

        public static void GetProperties(object obj, Hashtable ht)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                object obj2 = descriptor.GetValue(obj);
                string name = descriptor.Name;
                ht.Add(name, obj2);
            }
        }

        public static object GetProperty(object obj, string name) => 
            TypeDescriptor.GetProperties(obj)[name].GetValue(obj);

        public static object InvokeMethod(object obj, string name, object[] args) => 
            obj.GetType().InvokeMember(name, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, args);

        public static void SetProperties(object obj, Hashtable ht)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
            foreach (string str in ht.Keys)
            {
                PropertyDescriptor descriptor = properties[str];
                if (descriptor != null)
                {
                    descriptor.SetValue(obj, ht[str]);
                }
            }
        }

        public static void SetProperties(object obj, object[,] array)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
            int length = array.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                string str = (string) array[i, 0];
                object obj2 = array[i, 1];
                PropertyDescriptor descriptor = properties[str];
                if (descriptor != null)
                {
                    descriptor.SetValue(obj, obj2);
                }
            }
        }

        public static void SetProperty(object obj, string name, object value)
        {
            TypeDescriptor.GetProperties(obj)[name].SetValue(obj, value);
        }
    }
}

