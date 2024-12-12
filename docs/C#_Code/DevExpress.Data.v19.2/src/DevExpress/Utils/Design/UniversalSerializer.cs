namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public class UniversalSerializer
    {
        public static void DeserializeObject(object component, SerializationInfo si)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
            foreach (SerializationEntry entry in si)
            {
                PropertyDescriptor descriptor = properties[entry.Name];
                if ((descriptor != null) && !descriptor.IsReadOnly)
                {
                    try
                    {
                        descriptor.SetValue(component, Convert.ChangeType(entry.Value, descriptor.PropertyType));
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void SerializeObject(object component, SerializationInfo si)
        {
            if ((si != null) && (component != null))
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
                for (int i = 0; i < properties.Count; i++)
                {
                    PropertyDescriptor descriptor = properties[i];
                    if (!descriptor.IsReadOnly && descriptor.ShouldSerializeValue(component))
                    {
                        si.AddValue(descriptor.Name, descriptor.GetValue(component));
                    }
                }
            }
        }
    }
}

