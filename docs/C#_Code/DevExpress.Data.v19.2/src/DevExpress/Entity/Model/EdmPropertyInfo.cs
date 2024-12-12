namespace DevExpress.Entity.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class EdmPropertyInfo : IEdmPropertyInfo
    {
        private readonly PropertyDescriptor property;
        private readonly bool isForeignKey;
        private readonly bool isNavigationProperty;
        private readonly DataColumnAttributes attributes;

        public EdmPropertyInfo(PropertyDescriptor property, DataColumnAttributes attributes, bool isNavigationProperty, bool isForeignKey = false)
        {
            Guard.ArgumentNotNull(property, "property");
            this.property = property;
            this.isForeignKey = isForeignKey;
            this.isNavigationProperty = isNavigationProperty;
            this.attributes = attributes;
        }

        IEdmPropertyInfo IEdmPropertyInfo.AddAttributes(IEnumerable<Attribute> newAttributes) => 
            (newAttributes != null) ? new EdmPropertyInfo(this.property, this.attributes.AddAttributes(newAttributes), this.isNavigationProperty, this.isForeignKey) : this;

        Type IEdmPropertyInfo.PropertyType =>
            this.property.PropertyType;

        string IEdmPropertyInfo.Name =>
            this.property.Name;

        string IEdmPropertyInfo.DisplayName =>
            this.property.DisplayName;

        bool IEdmPropertyInfo.IsReadOnly =>
            this.property.IsReadOnly;

        bool IEdmPropertyInfo.IsForeignKey =>
            this.isForeignKey;

        DataColumnAttributes IEdmPropertyInfo.Attributes =>
            this.attributes;

        object IEdmPropertyInfo.ContextObject =>
            this.property;

        bool IEdmPropertyInfo.IsNavigationProperty =>
            this.isNavigationProperty;
    }
}

