namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class DXPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor sourceDescriptor;

        public DXPropertyDescriptor(PropertyDescriptor sourceDescriptor) : base(sourceDescriptor)
        {
            this.sourceDescriptor = sourceDescriptor;
        }

        public override bool CanResetValue(object component) => 
            this.SourceDescriptor.CanResetValue(component);

        public static void ConvertDescriptors(IDictionary properties)
        {
            List<object> list = new List<object>(3);
            List<KeyValuePair<object, DXPropertyDescriptor>> list2 = new List<KeyValuePair<object, DXPropertyDescriptor>>(3);
            foreach (DictionaryEntry entry in properties)
            {
                PropertyDescriptor descriptor = entry.Value as PropertyDescriptor;
                if (IsConvertible(descriptor))
                {
                    if (descriptor.PropertyType.Equals(typeof(Type)))
                    {
                        list.Add(entry.Key);
                        continue;
                    }
                    if (IsRequireConvert(descriptor))
                    {
                        list2.Add(new KeyValuePair<object, DXPropertyDescriptor>(entry.Key, new DXPropertyDescriptor(descriptor)));
                    }
                }
            }
            foreach (object obj2 in list)
            {
                properties.Remove(obj2);
            }
            foreach (KeyValuePair<object, DXPropertyDescriptor> pair in list2)
            {
                properties[pair.Key] = pair.Value;
            }
        }

        public static void ConvertDescriptors(IDictionary properties, string[] excludeList)
        {
            object[] array = new object[properties.Count];
            properties.Keys.CopyTo(array, 0);
            foreach (object obj2 in array)
            {
                if ((excludeList == null) || (Array.IndexOf<string>(excludeList, (string) obj2, 0) == -1))
                {
                    PropertyDescriptor descriptor = properties[obj2] as PropertyDescriptor;
                    if (IsConvertible(descriptor))
                    {
                        if (descriptor.PropertyType.Equals(typeof(Type)))
                        {
                            properties.Remove(obj2);
                        }
                        else if (IsRequireConvert(descriptor))
                        {
                            properties[obj2] = new DXPropertyDescriptor(descriptor);
                        }
                    }
                }
            }
        }

        public override object GetValue(object component) => 
            this.SourceDescriptor.GetValue(component);

        protected static bool IsConvertible(PropertyDescriptor descriptor) => 
            (descriptor != null) && (!(descriptor is DXPropertyDescriptor) && (descriptor.GetType().Name != "ExtendedPropertyDescriptor"));

        protected static bool IsRequireConvert(PropertyDescriptor descriptor)
        {
            DesignerSerializationVisibility serializationVisibility = descriptor.SerializationVisibility;
            return ((serializationVisibility != DesignerSerializationVisibility.Hidden) ? ((serializationVisibility != DesignerSerializationVisibility.Content) && descriptor.IsReadOnly) : true);
        }

        public override void ResetValue(object component)
        {
            this.SourceDescriptor.ResetValue(component);
        }

        public override void SetValue(object component, object val)
        {
            this.SourceDescriptor.SetValue(component, val);
        }

        public override bool ShouldSerializeValue(object component) => 
            (!this.SourceDescriptor.IsReadOnly || (base.SerializationVisibility == DesignerSerializationVisibility.Content)) ? this.SourceDescriptor.ShouldSerializeValue(component) : false;

        protected PropertyDescriptor SourceDescriptor =>
            this.sourceDescriptor;

        public override bool IsReadOnly =>
            this.SourceDescriptor.IsReadOnly;

        public override string Name =>
            this.SourceDescriptor.Name;

        public override Type ComponentType =>
            this.SourceDescriptor.ComponentType;

        public override Type PropertyType =>
            this.SourceDescriptor.PropertyType;
    }
}

