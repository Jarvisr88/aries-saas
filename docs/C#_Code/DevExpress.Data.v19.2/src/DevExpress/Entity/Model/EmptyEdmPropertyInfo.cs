namespace DevExpress.Entity.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public sealed class EmptyEdmPropertyInfo : IEdmPropertyInfo
    {
        private readonly Type componentType;
        private readonly DataColumnAttributes attributes;

        public EmptyEdmPropertyInfo(Type componentType)
        {
            Guard.ArgumentNotNull(componentType, "componentType");
            this.componentType = componentType;
            Attribute[] attributes = new Attribute[] { new ReadOnlyAttribute(true) };
            this.attributes = new DataColumnAttributes(new AttributeCollection(attributes), null);
        }

        IEdmPropertyInfo IEdmPropertyInfo.AddAttributes(IEnumerable<Attribute> newAttributes) => 
            this;

        Type IEdmPropertyInfo.PropertyType =>
            this.componentType;

        string IEdmPropertyInfo.Name =>
            null;

        string IEdmPropertyInfo.DisplayName =>
            null;

        bool IEdmPropertyInfo.IsReadOnly =>
            true;

        bool IEdmPropertyInfo.IsForeignKey =>
            false;

        DataColumnAttributes IEdmPropertyInfo.Attributes =>
            this.attributes;

        object IEdmPropertyInfo.ContextObject =>
            null;

        bool IEdmPropertyInfo.IsNavigationProperty =>
            false;
    }
}

