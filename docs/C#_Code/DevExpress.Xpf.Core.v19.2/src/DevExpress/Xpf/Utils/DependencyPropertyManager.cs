namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Windows;

    public static class DependencyPropertyManager
    {
        public static DependencyProperty Register(string name, Type propertyType, Type ownerType) => 
            DependencyProperty.Register(name, propertyType, ownerType);

        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata) => 
            DependencyProperty.Register(name, propertyType, ownerType, typeMetadata);

        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback) => 
            DependencyProperty.Register(name, propertyType, ownerType, typeMetadata, validateValueCallback);

        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType) => 
            DependencyProperty.RegisterAttached(name, propertyType, ownerType);

        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata) => 
            DependencyProperty.RegisterAttached(name, propertyType, ownerType, defaultMetadata);

        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata, ValidateValueCallback validateValueCallback) => 
            DependencyProperty.RegisterAttached(name, propertyType, ownerType, defaultMetadata, validateValueCallback);

        public static DependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata) => 
            DependencyProperty.RegisterAttachedReadOnly(name, propertyType, ownerType, defaultMetadata);

        public static DependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata) => 
            DependencyProperty.RegisterReadOnly(name, propertyType, ownerType, typeMetadata);
    }
}

