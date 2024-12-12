namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    internal static class PropertyHelper
    {
        public static DependencyProperty[] GetAttachedProperties(DependencyObject dObj, bool checkShouldSerialize)
        {
            List<DependencyProperty> list = new List<DependencyProperty>();
            if (dObj != null)
            {
                Attribute[] attributes = new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) };
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(dObj, attributes))
                {
                    DependencyPropertyDescriptor descriptor2 = DependencyPropertyDescriptor.FromProperty(descriptor);
                    if ((descriptor2 != null) && (descriptor2.IsAttached && (!checkShouldSerialize || descriptor2.ShouldSerializeValue(dObj))))
                    {
                        list.Add(descriptor2.DependencyProperty);
                    }
                }
            }
            return list.ToArray();
        }
    }
}

