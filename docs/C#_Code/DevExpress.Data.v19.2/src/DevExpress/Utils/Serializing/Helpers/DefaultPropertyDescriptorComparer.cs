namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class DefaultPropertyDescriptorComparer : IComparer<SerializablePropertyDescriptorPair>
    {
        public static readonly IComparer<SerializablePropertyDescriptorPair> Instance = new DefaultPropertyDescriptorComparer();

        private DefaultPropertyDescriptorComparer()
        {
        }

        public int Compare(SerializablePropertyDescriptorPair x, SerializablePropertyDescriptorPair y)
        {
            PropertyDescriptor objA = x.Property;
            PropertyDescriptor objB = y.Property;
            if (ReferenceEquals(objA, objB))
            {
                return 0;
            }
            if (objA == null)
            {
                return -1;
            }
            if (objB == null)
            {
                return 1;
            }
            XtraSerializableProperty attribute = x.Attribute;
            XtraSerializableProperty property2 = y.Attribute;
            return (!ReferenceEquals(attribute, property2) ? ((attribute != null) ? ((property2 != null) ? attribute.Order.CompareTo(property2.Order) : -1) : 1) : 0);
        }
    }
}

