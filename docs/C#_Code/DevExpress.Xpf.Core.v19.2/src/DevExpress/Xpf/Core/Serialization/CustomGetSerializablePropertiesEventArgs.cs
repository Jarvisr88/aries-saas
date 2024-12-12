namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    public class CustomGetSerializablePropertiesEventArgs : RoutedEventArgs
    {
        private readonly List<SerializablePropertyDescriptorPair> pairsList;
        private readonly PropertyDescriptorCollection props;

        internal CustomGetSerializablePropertiesEventArgs(object source, List<SerializablePropertyDescriptorPair> pairsList, PropertyDescriptorCollection props) : base(DXSerializer.CustomGetSerializablePropertiesEvent, source)
        {
            this.pairsList = pairsList;
            this.props = props;
        }

        private SerializablePropertyDescriptorPair FindExistingPair(string propertyName) => 
            this.pairsList.Find(pair => pair.Property.Name == propertyName);

        public void SetPropertySerializable(string propertyName, DXSerializable serializable)
        {
            SerializablePropertyDescriptorPair item = this.FindExistingPair(propertyName);
            if (item != null)
            {
                this.pairsList.Remove(item);
            }
            PropertyDescriptor descriptor = this.props[propertyName];
            if ((serializable != null) && (descriptor != null))
            {
                this.pairsList.Add(new SerializablePropertyDescriptorPair(descriptor, serializable.CreateXtraSerializableAttrubute()));
            }
        }

        public void SetPropertySerializable(DependencyProperty property, DXSerializable serializable)
        {
            this.SetPropertySerializable(property.Name, serializable);
        }
    }
}

